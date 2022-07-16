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
    [Authorize]
    public class ProductInCartController : ControllerBase
    {
        private readonly IProductInCartRepository productInCartRepo;
        private readonly ICartRepository cartRepo;
        private readonly IProductRepository productRepo;
        private readonly IMapper _mapper;

        public ProductInCartController(IProductInCartRepository productInCartRepo, IProductRepository productRepo, ICartRepository cartRepo, IMapper mapper)
        {
            this.productInCartRepo = productInCartRepo;
            this.productRepo = productRepo;
            this.cartRepo = cartRepo;
            _mapper = mapper;

        }



        [HttpGet]
        public ActionResult<IEnumerable<ProductInCartReadDto>> GetAll()
        {
            var productInCartFromDB = productInCartRepo.GetAll();
            return _mapper.Map<List<ProductInCartReadDto>>(productInCartFromDB);

        }



        [HttpGet("{id}")]
        public ActionResult<ProductInCartReadDto> GetById(Guid id)
        {
            ProductInCart productInCart = productInCartRepo.GetById(id);
            return _mapper.Map<ProductInCartReadDto>(productInCart);
        }



        [HttpGet]
        [Route("{cartId:Guid}")]
        public ActionResult<IEnumerable<ProductInCartReadDto>> GetProductsInCartByCartId(Guid cartId)
        {
            var productInCartFromDB = productInCartRepo.GetProductsInCartByCartId(cartId);
            return _mapper.Map<List<ProductInCartReadDto>>(productInCartFromDB);

        }






        [HttpGet]
        [Route("getProd/{cartId:Guid}/{productId:Guid}")]
        public Guid GetProductInCartIdByCartIdAndProductId(Guid cartId, Guid productId)
        {
            return productInCartRepo.GetProductInCartIdByCartIdAndProductId(cartId, productId);

        }


        [HttpPost]
        public IActionResult Create(ProductInCartWriteDto productInCart)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var p = _mapper.Map<ProductInCart>(productInCart);
                    p.Id = Guid.NewGuid();
                    var product = productRepo.GetById(productInCart.ProductId);
                   
                    var cart = cartRepo.GetById(productInCart.CartId);
                    p.Product = product;
                    p.Cart = cart;
                    p.Quantity=1;

                    productInCartRepo.Create(p);
                    productInCartRepo.SaveChanges();
                    return Ok(_mapper.Map<ProductInCartReadDto>(p));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpPut("{id}")]
        public IActionResult Update( ProductInCartUpdateDto productInCart, Guid id)
        {
            if (ModelState.IsValid)
            {
                if (productInCart.Id != id)
                {
                    return BadRequest();
                }
                try
                {
                    var productInCartToEdit = productInCartRepo.GetById(id);
                    if (productInCartToEdit is null)
                    {
                        return NotFound();
                    }
                    _mapper.Map(productInCart, productInCartToEdit);
                    if (productInCartToEdit.Product.Quantity < productInCartToEdit.Quantity)
                    {
                        return BadRequest(new {message= "Sorry it's the last piece " });
                    }
                    productInCartRepo.Update(productInCartToEdit);
                    productInCartRepo.SaveChanges();
                    return Ok(_mapper.Map<ProductInCartReadDto>(productInCartToEdit));

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
            if (productInCartRepo.GetById(id) != null)
            {
                try
                {
                    var deletedproductInCart=productInCartRepo.GetById(id);
                    productInCartRepo.Delete(id);
                    productInCartRepo.SaveChanges();
                    return Ok(_mapper.Map<ProductInCartReadDto>(deletedproductInCart));

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }





        [HttpDelete("DeleteProductsInCartByCartId/{cartId}")]
        public ActionResult DeleteProductsInCartByCartId(Guid cartId)
        {

                try
                {
                var deletedproductInCart = productInCartRepo.GetProductsInCartByCartId(cartId);
                var output = _mapper.Map<List<ProductInCartReadDto>>(deletedproductInCart);
                    productInCartRepo.DeleteProductsInCartByCartId(cartId);
                    productInCartRepo.SaveChanges();
                    return Ok(output);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
         
        }
        



    }

}