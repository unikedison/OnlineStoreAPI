using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore_API.Repository;
using OnlineStore_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowPolicy")]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            UserRepository userRepository = new UserRepository();
            List<User> users = userRepository.GetUserDetails();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            UserRepository userRepository = new UserRepository();
            User user = userRepository.GetUserDetailsById(id);
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] User user)
        {
            UserRepository userRepository = new UserRepository();
            string result = userRepository.AddUser(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] User user)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.UpdateUser(id, user);
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.DeleteUser(id);
        }
    }
}
