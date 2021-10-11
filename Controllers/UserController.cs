using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GameScheduler.Models;
using GameScheduler.Services;

namespace GameScheduler.Controllers {

    [ApiController]

    [Route("[controller]")]
    public class UserController : ControllerBase {
        private IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost]
        public IActionResult AddUser(User u) {
            try {
                Console.WriteLine("Trying Controller...");
                User returnedUser = _userServices.AddUser(u);
                Console.WriteLine("Done...");
                if(u !=null) return CreatedAtRoute("GetAllUsers", new {name=returnedUser.Name}, returnedUser);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error -- Add");
            }
        }
        [HttpDelete("{Name}")]
        public IActionResult DeleteUser(string Name) {
            try {
                _userServices.DeleteUser(Name);
                if(Name!=null) return NoContent();
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet(Name="GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try {
                IEnumerable<User> list = _userServices.GetAllUsers();
                if(list!=null) return Ok(list);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error -- Get");
            }
        }

        [HttpGet("{name}", Name="GetUserByName")]
        public IActionResult GetUserByName(string name) {
            try {
                User u = _userServices.getUserByName(name);
                if(u != null) return Ok(u);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{name}")]
        public IActionResult UpdateGame(string name, User userIn) {
            try {
                _userServices.updateUser(name, userIn);
                if(name!=null || userIn!=null) return NoContent();
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }
    }

    
}