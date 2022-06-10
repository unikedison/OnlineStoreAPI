using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OnlineStore_API.Repository;
using OnlineStore_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowPolicy")]

    public class ResetController : ControllerBase
    {
        // GET: api/<ResetController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ResetController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            UserRepository userRepository = new UserRepository();
            return Ok(userRepository.GetUserDetailsByEmail(id));
        }

        // POST api/<ResetController>
        [HttpPost]
        public void Post([FromBody] User user)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.ForgotPassword(user);
        }

        // PUT api/<ResetController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ResetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
