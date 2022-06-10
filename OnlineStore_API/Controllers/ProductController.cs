using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OnlineStore_API.Models;
using OnlineStore_API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowPolicy")]

    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            ProductRepository productRepository = new ProductRepository();
            List<Product> products = productRepository.GetAllProducts();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            ProductRepository productRepository = new ProductRepository();
            Product product = productRepository.GetProductDetailsById(id);
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            ProductRepository productRepository = new ProductRepository();
            productRepository.AddProduct(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Product product)
        {
            ProductRepository productRepository = new ProductRepository();
            string res = productRepository.UpdateProduct(id,product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ProductRepository productRepository = new ProductRepository();
            string res = productRepository.DeleteProduct(id);
        }
    }
}
