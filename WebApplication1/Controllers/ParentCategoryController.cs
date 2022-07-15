using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentCategoryController : ControllerBase
    {
        private readonly IParentCategoryRepository parentCategoryRepo;
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper _mapper;

        public ParentCategoryController(IParentCategoryRepository parentCategoryRepo, ICategoryRepository categoryRepo, IMapper mapper)
        {
            this.parentCategoryRepo = parentCategoryRepo;
            this.categoryRepo = categoryRepo;
            _mapper = mapper;

        }



        [HttpGet]
        public ActionResult<IEnumerable<ParentCategoryReadDto>> GetAll()
        {
            var parentCategoriesFromDB = parentCategoryRepo.GetAll();
            return _mapper.Map<List<ParentCategoryReadDto>>(parentCategoriesFromDB);

        }



        [HttpGet("{id}")]
        public ActionResult<ParentCategoryReadDto> GetById(Guid id)
        {
            ParentCategory parentCategory = parentCategoryRepo.GetById(id);
            return _mapper.Map<ParentCategoryReadDto>(parentCategory);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult Create(ParentCategoryWriteDto parentCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var p = _mapper.Map<ParentCategory>(parentCategory);
                    p.Id = Guid.NewGuid();
                    parentCategoryRepo.Create(p);
                    parentCategoryRepo.SaveChanges();
                    return Ok(p);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id, ParentCategoryUpdateDto parentCategory)
        {
            if (ModelState.IsValid)
            {
                if (parentCategory.Id != id)
                {
                    return BadRequest();
                }
                try
                {
                    var parentCategoryToEdit = parentCategoryRepo.GetById(id);
                    if (parentCategoryToEdit is null)
                    {
                        return NotFound();
                    }
                    _mapper.Map(parentCategory, parentCategoryToEdit);
                    parentCategoryRepo.Update(parentCategoryToEdit);
                    parentCategoryRepo.SaveChanges();
                    return Ok(_mapper.Map<ParentCategoryReadDto> (parentCategoryToEdit));
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
            if (parentCategoryRepo.GetById(id) != null)
            {
                try
                {
                    var deletedparentCategory=parentCategoryRepo.GetById(id);
                    parentCategoryRepo.Delete(id);
                    categoryRepo.DeleteCategoriesByParentCategoryId(id);
                    categoryRepo.SaveChanges();
                    return Ok(_mapper.Map<ParentCategoryReadDto>(deletedparentCategory));
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
