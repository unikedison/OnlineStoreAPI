using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OnlineStore_API.Repository;
using OnlineStore_API.Models;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowPolicy")]

    public class LoginController : ControllerBase
    {
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            UserRepository userRepository = new UserRepository();
            return Ok(userRepository.AuthenticateUser(user));
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
