using Server.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.Data.interfaces;
using System;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _products;
        private readonly IProductValidator _validator;
        public ProductController(IProduct products, IProductValidator validator)
        {
            _products = products;
            _validator = validator;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var response = _products.GetAllProducts();
            if (response.Any())
                return Ok(response);

            return NotFound();
        }


        [HttpGet("{id}")]
        public ActionResult<Product> Get(string id)
        {
            if (!_validator.ValidateId(id: id))
                ModelState.AddModelError("Id", "Такого товара нет");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _products.GetProduct(int.Parse(id));
            if (response != null)
                return Ok(response);

            return NotFound();
        }


        [HttpPost]
        public ActionResult Post(Product webProduct)
        {
            if (!_validator.ValidateTitle(webProduct))
                ModelState.AddModelError("Title", "Название товара не должно быть пустым");

            if (!_validator.ValidatePrice(webProduct))
                ModelState.AddModelError("Price", "Цена товара должно быть больше или равна 1");

            if (!ModelState.IsValid)
                 return BadRequest(ModelState);

            try
            {
                _products.AddNewProduct(webProduct);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("{id}")]
        public ActionResult Put(string id, Product webProduct)
        {

            if (!_validator.ValidateId(id: id))
                ModelState.AddModelError("Id", "Такого товара нет");

            if (!_validator.ValidateTitle(webProduct))
                ModelState.AddModelError("Title", "Название товара не должно быть пустым");

            if (!_validator.ValidatePrice(webProduct))
                ModelState.AddModelError("Price", "Цена товара должно быть больше или равна 1");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _products.EditProduct(int.Parse(id), webProduct);
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
            if (!_validator.ValidateId(id: id))
                ModelState.AddModelError("Id", "Такого товара нет");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _products.DeleteProduct(int.Parse(id));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
