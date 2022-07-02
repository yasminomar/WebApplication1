using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using WebApplication1.Models;
using WebApplication1.Repository;
using AutoMapper;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepo;
        private readonly ICartRepository cartRepo;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepo, ICartRepository cartRepo, IMapper mapper)
        {
            this.orderRepo = orderRepo;
            this.cartRepo = cartRepo;
            _mapper = mapper;

        }



        [HttpGet]
        //[Authorize]
        public ActionResult<IEnumerable<OrderReadDto>> GetAll()
        {
            var ordersFromDB = orderRepo.GetAll();
            return _mapper.Map<List<OrderReadDto>>(ordersFromDB);

        }



        [HttpGet("{id}")]
        //[Authorize]
        public ActionResult<OrderReadDto> GetById(Guid id)
        {
            Order order = orderRepo.GetById(id);
            return _mapper.Map<OrderReadDto>(order);
        }



        [HttpPost]
        //[Authorize]
        public IActionResult Create(OrderWriteDto order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var o = _mapper.Map<Order>(order);
                    o.Id = Guid.NewGuid();
                    var cart=cartRepo.GetById(order.CartId);
                    o.Cart = cart;
                    orderRepo.Create(o);
                    orderRepo.SaveChanges();
                    return Ok(_mapper.Map<OrderReadDto>(o));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }


        //[Authorize]
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
                    var deletedOrder=orderRepo.GetById(id);
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
    }
}