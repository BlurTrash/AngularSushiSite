using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShushiSiteApi.DbModels;
using MyShushiSiteApi.ViewModels;
using MyShushiSiteApi.ViewModels.Orders;
using System;

namespace MyShushiSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private MyDbContext _db;
        private IWebHostEnvironment _hostingEnvironment;

        public OrdersController(MyDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _hostingEnvironment = environment;
        }


        /// <summary>
        /// Создает: Новый элемент заказа
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OrderVM>> Post([FromForm] OrderViewModel orderViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                };

                Order newOrder = new Order
                {
                    Id = 0,
                    Address = orderViewModel.Address,
                    Comment = orderViewModel.Comment,
                    DeliveryMethodId = orderViewModel.DeliveryMethodId,
                    Email = orderViewModel.Email,
                    FullPrice = orderViewModel.FullPrice,
                    PaymentId = orderViewModel.PaymentId,
                    StatusId = (int)OrderStatusesEnum.New,
                    UserId = orderViewModel.UserId,
                    UserName = orderViewModel.UserName,
                    UserPhone = orderViewModel.UserPhone
                };

                _db.Orders.Add(newOrder);
                _db.SaveChanges();

                foreach (var orderLine in orderViewModel.OrderLines) 
                {
                    OrderLine orderPosition = new OrderLine
                    {
                        Id = 0,
                        ItemId = orderLine.ItemId,
                        OrderId = newOrder.Id,
                        Quantity = orderLine.Quantity,
                        Price = orderLine.Price,
                        UnitPrice = orderLine.UnitPrice
                    };

                    _db.OrderLines.Add(orderPosition);
                    _db.SaveChanges();
                }

                OrderVM orderVM = new OrderVM
                {
                    Id = newOrder.Id,
                    Address = newOrder.Address,
                    Comment = newOrder.Comment,
                    DeliveryMethodId = newOrder.DeliveryMethodId,
                    Email = newOrder.Email,
                    FullPrice = newOrder.FullPrice,
                    PaymentId = newOrder.PaymentId,
                    StatusId = newOrder.StatusId,
                    UserId = newOrder.UserId,
                    UserName = newOrder.UserName,
                    UserPhone = newOrder.UserPhone,
                    OrderLines = newOrder.OrderLines.Select(ol => new OrderLineVM 
                    { 
                        Id = ol.Id, ItemId = ol.ItemId, OrderId = ol.OrderId, Price = ol.Price, Quantity = ol.Quantity, UnitPrice = ol.UnitPrice 
                    }).ToList()
                };

                return Ok(orderVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }
    }
}
