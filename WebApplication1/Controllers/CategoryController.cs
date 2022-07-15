using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.Data.DataBaseModels;
using WebApplication1.DTO;
using WebApplication1.DTO_s.Categories;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly IParentCategoryRepository parentCategoryRepo;
        private readonly IProductRepository productRepo;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepo, IProductRepository productRepo, IParentCategoryRepository parentCategoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
            this.parentCategoryRepo = parentCategoryRepo;
            _mapper = mapper;

        }



        [HttpGet]
        public ActionResult<IEnumerable<CategoryReadDto>> GetAll()
        {
            var categoriesFromDB = categoryRepo.GetAll();
            return _mapper.Map<List<CategoryReadDto>>(categoriesFromDB);

        }



        [HttpGet("{id}")]
        public ActionResult<CategoryReadDto> GetById(Guid id)
        {
            Categories category = categoryRepo.GetById(id);
            return _mapper.Map<CategoryReadDto>(category);
        }


        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult Create(CategoryWriteDto category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var c = _mapper.Map<Categories>(category);
                    c.Id = Guid.NewGuid();
                    var parentCategory=parentCategoryRepo.GetById(category.ParentCategoryId);
                    c.ParentCategory = parentCategory;
                    categoryRepo.Create(c);
                    categoryRepo.SaveChanges();
                    return Ok(_mapper.Map<CategoryReadDto>(c));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("sortedCategory")]
        public ActionResult<CategoryPaginationReadDto> GetCategoriesSorted(CategoryParameters categoryParameters)
        {
            var categoriesFromDB = categoryRepo.GetAllCategoriesSorted(categoryParameters);
            var categories = _mapper.Map<List<CategoryReadDto>>(categoriesFromDB);
            var totalCount = categoryRepo.GetNumOfCategories();
            return new CategoryPaginationReadDto
            {
                TotalCount = totalCount,
                Categories = categories

            };
        }


        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id, CategoryUpdateDto category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id != id)
                {
                    return BadRequest();
                }
                try
                {
                    var categoryToEdit = categoryRepo.GetById(id);
                    if (categoryToEdit is null)
                    {
                        return NotFound();
                    }
                    _mapper.Map(category, categoryToEdit);
                    categoryRepo.Update(categoryToEdit);
                    categoryRepo.SaveChanges();
                    return Ok(_mapper.Map<CategoryReadDto>(categoryToEdit));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
          return BadRequest(ModelState);
            
        }



        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(Guid id)
        {
            if (categoryRepo.GetById(id) != null)
            {
                try
                {
                    var deletedCategory=categoryRepo.GetById(id);
                    categoryRepo.Delete(id);
                    categoryRepo.SaveChanges();
                    productRepo.DeleteProductsByCategoryId(id);
                    return Ok(_mapper.Map<CategoryReadDto>(deletedCategory));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }
    }
}
