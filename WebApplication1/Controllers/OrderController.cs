using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using WebApplication1.Models;
using WebApplication1.Repository;
using AutoMapper;
using WebApplication1.DTO;
using System.Linq;
using WebApplication1.Data.DataBaseModels;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebApplication1.DTO_s.Order;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderRepository orderRepo;
        private readonly IProductRepository productRepo;
        private readonly IOrderHistoryRepository orderHistoryRepo;
        private readonly ICartRepository cartRepo;
        private readonly IMapper _mapper;



        public OrderController(UserManager<ApplicationUser> userManager, IOrderRepository orderRepo, IProductRepository productRepo, IOrderHistoryRepository orderHistoryRepo, ICartRepository cartRepo, IMapper mapper)
        {
            this.userManager = userManager;
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.orderHistoryRepo = orderHistoryRepo;
            this.cartRepo = cartRepo;

            _mapper = mapper;

        }



        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<OrderReadDto>> GetAll()
        {
            var ordersFromDB = orderRepo.GetAll();
            return _mapper.Map<List<OrderReadDto>>(ordersFromDB);

        }

        [HttpGet]
        [Route("GetOrderHistory")]
        [Authorize]
        public async Task<ActionResult<List<OrderHistoryReadDto>>> GetOrderHistory()
        {
            var username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByNameAsync(username);
            var ordersFromDB = orderHistoryRepo.GetOrders(user.Id);
            var orders=_mapper.Map<List<OrderHistoryReadDto>>(ordersFromDB);
            orders.ForEach(p => p.Products = _mapper.Map<List<ProductReadDto>>(productRepo.GetProductsByIds(p.ProductsIds)));
            return orders;

        }

        
        [HttpGet("GetUserId")]
        [Authorize]
        public async Task<string> GetUserId()
        {
            var username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByNameAsync(username);
            return user.Id;

        }








        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<OrderReadDto> GetById(Guid id)
        {
            Order order = orderRepo.GetById(id);
            return _mapper.Map<OrderReadDto>(order);
        }


        [HttpGet("GetOrderByCartId/{cartId}")]
        [Authorize]
        public ActionResult<OrderReadDto> GetOrderByCartId(Guid cartId)
        {
            Order order = orderRepo.GetOrderByCartId(cartId);
            return _mapper.Map<OrderReadDto>(order);
        }















        //[HttpGet("userId/{userId}")]
        ////[Authorize]
        //public ActionResult<OrderReadDto> GetOrderHistoryByUserId(string userId)
        //{
        //    Order order = orderRepo.GetOrderHistoryByUserId(userId);
        //    return _mapper.Map<OrderReadDto>(order);
        //}



        



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OrderWriteDto order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var o = _mapper.Map<Order>(order);
                    o.Id = Guid.NewGuid();
                    var cart = cartRepo.GetById(order.CartId);
                    o.Cart = cart;
                    orderRepo.Create(o);
                    orderRepo.SaveChanges();
                    var ps = string.Join(',', cart.Products.Select(p => p.Product.Id.ToString()).ToArray());
                    var totalPrice = cart.Products.Sum(p => p.Quantity * p.Product.UnitPrice);
                    var username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    var user = await userManager.FindByNameAsync(username);
                    orderHistoryRepo.AddOrderHistory(new OrderHistory
                    {
                        UserId = user.Id,
                        Id = Guid.NewGuid(),
                        PaymentMethod = o.PaymentMethod,
                        ShippmentAddress = o.ShipmentAddress,
                        ProductsIds = ps,
                        TotalPrice = totalPrice
                    });
                    orderHistoryRepo.SaveChanges();

                    return Ok(_mapper.Map<OrderReadDto>(o));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, OrderUpdateDto order)
        {
            if (ModelState.IsValid)
            {
                if (order.Id != id)
                {
                    return BadRequest();
                }
                try
                {
                    var orderToEdit = orderRepo.GetById(id);
                    if (orderToEdit is null)
                    {
                        return NotFound();
                    }
                    _mapper.Map(order, orderToEdit);
                    orderRepo.Update(orderToEdit);
                    orderRepo.SaveChanges();
                    return Ok(_mapper.Map<OrderReadDto>(orderToEdit));

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpDelete("{id}")]
        //[Authorize(Policy = "Admin")]
        public IActionResult Delete(Guid id)
        {
            if (orderRepo.GetById(id) != null)
            {
                try
                {
                    var deletedOrder = orderRepo.GetById(id);
                    orderRepo.Delete(id);
                    orderRepo.SaveChanges();
                    return Ok(_mapper.Map<OrderReadDto>(deletedOrder));


                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }


        [HttpDelete("DeleteOrderByCartId/{cartId}")]
        //[Authorize(Policy = "Admin")]
        public IActionResult DeleteOrderByCartId(Guid cartId)
        {
           
                try
                {
                    var deletedOrder = orderRepo.GetOrderByCartId(cartId);
                    orderRepo.DeleteOrderByCartId(cartId);
                    orderRepo.SaveChanges();
                    return Ok(_mapper.Map<OrderReadDto>(deletedOrder));


                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
        }
        
    }
}