using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.Data.interfaces;
using Server.Data.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _orders;
        private readonly IOrderValidator _validator;
        public OrderController(IOrder orders, IOrderValidator validator)
        {
            _orders = orders;
            _validator = validator;
        }
   
        [HttpGet]
        public ActionResult<IEnumerable<OrderWithProductInfo>> Get()
        {
            var response = _orders.GetAllOrders();
            if (response.Any())
                return Ok(response);

            return NotFound();
        }

   
        [HttpGet("{Name}")]
        public ActionResult<IEnumerable<OrderWithProductInfo>> Get(string Name)
        { 
            if (!_validator.ValidateName(name:Name))
                ModelState.AddModelError("Name", "Такого пользователя нет");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _orders.GetOrders(Name);
            if (response.Any())
                return Ok(response);

            return NotFound();
        }

        [HttpGet("element/{id}")]
        public ActionResult<OrderWithProductInfo> GetSingle(string id)
        {
            if (!_validator.ValidateId(id:id))
                ModelState.AddModelError("Id", "Такого заказа нет");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _orders.GetOrder(int.Parse(id));
            if (response != null)
                return Ok(response);

            return NotFound();
        }

        [HttpPost]
        public ActionResult Post(Order webOrder)
        {
            if (!_validator.ValidateProductId(webOrder))
                ModelState.AddModelError("ProductId", "Такого товара нет");

            if (!_validator.ValidateName(webOrder))
                ModelState.AddModelError("Name", "Поле имени не должно быть пустым");

            if (!_validator.ValidateCount(webOrder))
                ModelState.AddModelError("Count", "Количество товара должно быть больше или равно 1");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                _orders.AddNewOrder(webOrder);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, Order webOrder)
        {
            if (!_validator.ValidateId(id))
                ModelState.AddModelError("Id", "Такого заказа нет");

            if (!_validator.ValidateProductId(webOrder))
                ModelState.AddModelError("ProductId", "Такого товара нет");

            if (!_validator.ValidateName(webOrder))
                ModelState.AddModelError("Name", "Поле имени не должно быть пустым");

            if (!_validator.ValidateCount(webOrder))
                ModelState.AddModelError("Count", "Количество товара должно быть больше или равно 1");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _orders.EditOrder(int.Parse(id), webOrder);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (!_validator.ValidateId(id:id))
                ModelState.AddModelError("Id", "Такого заказа нет");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _orders.DeleteOrder(int.Parse(id));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
