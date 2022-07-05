using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Ubiety.Dns.Core.Common;
using WebApplication1.Data.DataBaseModels;
using WebApplication1.DTO;
using WebApplication1.DTO_s.Products;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepo;
        private readonly ICategoryRepository categoryRepo;
        private readonly IVendorRepository vendorRepo;
        private readonly IProductInCartRepository productInCartRepo;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo, IVendorRepository vendorRepo, IProductInCartRepository productInCartRepo, IMapper mapper)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
            this.vendorRepo = vendorRepo;
            this.productInCartRepo = productInCartRepo;
            _mapper = mapper;

        }



        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAll()
        {
            var productsFromDB = productRepo.GetAll();
            return _mapper.Map<List<ProductReadDto>>(productsFromDB);
        }


        [HttpPost]
        [Route("sortedProduct")]
        public ActionResult<ProductPaginationReadDto> GetProductSorted(ProductParameters productParameters)
        {
            var productsFromDB = productRepo.GetAllProductsSorted(productParameters);
            var products= _mapper.Map<List<ProductGroupingOutputReadDto>>(productsFromDB);
            var totalCount= productRepo.GetNumOfProducts();
            return new ProductPaginationReadDto
            {
                TotalCount = totalCount,
                Products = products

            };
        }


        [HttpPost]
        [Route("sortedProduct/getFilteredProducts/{productName}")]
        public ActionResult<ProductPaginationReadDto> getFilteredProducts(ProductParameters productParameters,string productName)
        {
            var productsFromDB = productRepo.getFilteredProducts(productParameters, productName);
            var products = _mapper.Map<List<ProductGroupingOutputReadDto>>(productsFromDB);
            var totalCount = productRepo.GetNumOfProducts();
            return new ProductPaginationReadDto
            {
                TotalCount = totalCount,
                Products = products

            };
        }




        

        [HttpGet("{id}")]
        public ActionResult<ProductReadDto> GetById(Guid id)
        {
            Products product = productRepo.GetById(id);
            return _mapper.Map<ProductReadDto>(product);
        }



        [HttpPost]
        //[Authorize(Policy = "Admin")]
        public IActionResult Create([FromForm]ProductWriteDto product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var p = _mapper.Map<Products>(product);
                    p.Id = Guid.NewGuid();

                    try
                    {
                        if (product.Image == null)
                        {
                            return BadRequest(new {error="image is require"});
                        }
                        var file = Request.Form.Files[0];

                        if (file.Length > 1_000_000)
                        {
                            return BadRequest();
                        }
                        var allwedExtensions = new string[] { ".jpg", ".gif", ".BMP", ".png" };
                        if (!allwedExtensions.Any(ext => file.FileName.EndsWith(ext)))
                        {
                            return BadRequest();
                        }
                        var folderName = Path.Combine("Resources", "img");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition).FileName.Replace("\"", String.Empty);

                            fileName = Guid.NewGuid().ToString() + fileName;
                            var fullPath = System.IO.Path.Combine(pathToSave, path2: fileName.ToString());
                            var dbPath = Path.Combine(folderName, fileName.ToString());
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            p.Image = dbPath;
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Internal server error: {ex}");
                    }
                    var category=categoryRepo.GetById(product.CategoryId);
                    var vendor=vendorRepo.GetById(product.VendorId);
                    p.Vendor = vendor;
                    p.Category=category;
                    productRepo.Create(p);
                    productRepo.SaveChanges();
                    return Ok(_mapper.Map<ProductReadDto>(p));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        
       [HttpPut("{id}/{quantity}")]
        // [Authorize(Policy = "Admin")]
        public ActionResult UpdateProductQuantity(Guid id,int quantity)
        {
            var productToEdit = productRepo.GetById(id);
            if(productToEdit == null)
            {
                return NotFound();
            }
            else
            {

                productRepo.UpdateProductQuantity(id, productToEdit.Quantity-quantity);
                productRepo.SaveChanges();
                return Ok(_mapper.Map<ProductReadDto>(productToEdit));

            }

        }













        [HttpPut("{id}")]
       // [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id, [FromForm]ProductUpdateDto product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id != id)
                {
                    return BadRequest();
                }
                try
                {

                    var productToEdit = productRepo.GetById(id);
                    if (productToEdit is null)
                    {
                        return NotFound();
                    }
                    try
                    {
                        if (Request.Form.Files.Count != 0)
                        {
                            var file = Request.Form.Files[0];

                            if (file.Length > 1_000_00)
                            {
                                return BadRequest();
                            }
                            var allwedExtensions = new string[] { ".jpg", ".gif", ".BMP", ".png" };
                            if (!allwedExtensions.Any(ext => file.FileName.EndsWith(ext)))
                            {
                                return BadRequest();
                            }
                            var folderName = Path.Combine("Resources", "img");
                            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                            var fileName = ContentDispositionHeaderValue
                                   .Parse(file.ContentDisposition).FileName.Replace("\"", String.Empty);

                            fileName = Guid.NewGuid().ToString() + fileName;
                            var fullPath = System.IO.Path.Combine(pathToSave, path2: fileName.ToString());
                            var dbPath = Path.Combine(folderName, fileName.ToString());
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            product.Image = dbPath;


                        }
                        if(product.Image == null)
                        {
                            product.Image = productToEdit.Image;
                        }

                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Internal server error: {ex}");
                    }
                    _mapper.Map(product, productToEdit);
                    productRepo.Update(productToEdit);
                    productRepo.SaveChanges();
                    return Ok(_mapper.Map<ProductReadDto>(productToEdit));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState.FirstOrDefault());
        }



        [HttpDelete("{id}")]
        //[Authorize(Policy = "Admin")]
        public IActionResult Delete(Guid id)
        {
            if(productRepo.GetById(id) != null)
            {
                try
                {
                    //var category = categoryRepo.GetById(product.CategoryId);
                    //var vendor = vendorRepo.GetById(product.VendorId);
                    //p.Vendor = vendor;
                    //p.Category = category;

                    var DeletedProduct=productRepo.GetById(id); 
                    productRepo.Delete(id);
                    productRepo.SaveChanges();
                    productInCartRepo.DeleteProductsInCartByProductId(id);
                    productInCartRepo.SaveChanges();
                    return Ok(_mapper.Map<ProductReadDto>(DeletedProduct));
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
