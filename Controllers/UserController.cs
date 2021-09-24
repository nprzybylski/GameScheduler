using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GameScheduler.Models;
using GameScheduler.Services;

namespace GameScheduler.Controllers {
    [ApiController]
    [Route("registration/api/user")]
    public class UserController : ControllerBase {
        private IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet(Name="GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try {
                IEnumerable<User> list = _userServices.getUsers();
                if(list!=null) return Ok(list);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{title}", Name="loginUser")]
        public IActionResult loginUser(string name, string password) {
            try {
                User u = _userServices.loginUser(name, password);
                if(u != null) return Ok(u);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult InsertUser(User u) {
            try {
                User returnedUser = _userServices.insertUser(u);
                if(u!=null) return CreatedAtRoute("GetAllUsers", new {name=returnedUser.name}, returnedUser);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{title}")]
        public IActionResult DeleteUser(string name) {
            try {
                _userServices.deleteUser(name);
                if(name!=null) return NoContent();
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }
    }

    
}