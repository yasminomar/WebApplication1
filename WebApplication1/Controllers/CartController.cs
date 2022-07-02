using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using WebApplication1.Models;
using WebApplication1.Repository;
using AutoMapper;
using WebApplication1.DTO;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository cartRepo;
        private readonly IProductInCartRepository productInCartRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public CartController(ICartRepository cartRepo, UserManager<ApplicationUser> userManager, IProductInCartRepository productInCartRepo, IMapper mapper)
        {
            this.cartRepo = cartRepo;
            this.productInCartRepo = productInCartRepo;
            _mapper = mapper;
            _userManager= userManager;

        }



        [HttpGet]
        public ActionResult<IEnumerable<CartReadDto>> GetAll()
        {
            var productsFromDB = cartRepo.GetAll();
            return _mapper.Map<List<CartReadDto>>(productsFromDB);

        }


        [HttpGet]
        [Route("CartByUserId")]
        public async Task<ActionResult<CartReadDto>> CartByUserId()
        {
            var email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(email);

            var productsFromDB = cartRepo.CartByUserId(user.Id);
            return _mapper.Map<CartReadDto>(productsFromDB);

        }
        


        [HttpGet("{id}")]
        public ActionResult<CartReadDto> GetById(Guid id)
        {
            Cart cart = cartRepo.GetById(id);
            return _mapper.Map<CartReadDto>(cart);
        }


  
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, CartUpdateDto cart)
        {
            if (ModelState.IsValid)
            {
                if (cart.Id != id)
                {
                    return BadRequest();
                }
                try
                {
                    var cartToEdit = cartRepo.GetById(id);
                    if (cartToEdit is null)
                    {
                        return NotFound();
                    }
                    _mapper.Map(cart, cartToEdit);
                    cartRepo.Update(cartToEdit);
                    cartRepo.SaveChanges();
                    return Ok(_mapper.Map<CartReadDto>(cartToEdit));
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
            if (cartRepo.GetById(id) != null)
            {
                try
                {
                    var deletedCart = cartRepo.GetById(id);
                    cartRepo.Delete(id);
                    productInCartRepo.DeleteProductsInCartByCartId(id);
                    return Ok(_mapper.Map<CartReadDto>(deletedCart));

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