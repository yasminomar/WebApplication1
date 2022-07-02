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
    //[Authorize(Policy = "Admin")]

    public class VendorController : ControllerBase
    {
        private readonly IVendorRepository vendorRepo;
        private readonly IProductRepository productRepo;
        private readonly IMapper _mapper;

        public VendorController(IVendorRepository vendorRepo, IProductRepository productRepo, IMapper mapper)
        {
            this.vendorRepo = vendorRepo;
            this.productRepo = productRepo;
            _mapper = mapper;

        }



        [HttpGet]

        public ActionResult<IEnumerable<VendorReadDto>> GetAll()
        {
            var vendorsFromDB = vendorRepo.GetAll();
            return _mapper.Map<List<VendorReadDto>>(vendorsFromDB);

        }



        [HttpGet("{id}")]
        public ActionResult<VendorReadDto> GetById(Guid id)
        {
            Vendor vendor = vendorRepo.GetById(id);
            return _mapper.Map<VendorReadDto>(vendor);
        }



        [HttpPost]
        public IActionResult Create(VendorWriteDto vendor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var v = _mapper.Map<Vendor>(vendor);
                    v.Id = Guid.NewGuid();
                    //var newVendorProducts = productRepo.GetGroup(vendor.ProductsId);
                    //v.Products = newVendorProducts;
                    vendorRepo.Create(v);
                    vendorRepo.SaveChanges();
                    return Ok(_mapper.Map<VendorReadDto>(v));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpPut("{id}")]
        public IActionResult Update(Guid id, VendorUpdateDto vendor)
        {
            if (ModelState.IsValid)
            {
                if (vendor.Id != id)
                {
                    return BadRequest();
                }
                try
                {
                    var vendorToEdit = vendorRepo.GetById(id);
                    if (vendorToEdit is null)
                    {
                        return NotFound();
                    }
                    _mapper.Map(vendor, vendorToEdit);
                    vendorRepo.Update(vendorToEdit);
                    vendorRepo.SaveChanges();
                    return Ok(_mapper.Map<VendorReadDto>(vendorToEdit));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (vendorRepo.GetById(id) != null)
            {
                try
                {
                    var deletedVendor=vendorRepo.GetById(id);
                    vendorRepo.Delete(id);
                    vendorRepo.SaveChanges();
                    return Ok(_mapper.Map<VendorReadDto>(deletedVendor));
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