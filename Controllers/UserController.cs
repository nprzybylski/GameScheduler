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
     
        [HttpPost("{name}/{password}")]
        public IActionResult loginUser(string name, string password) {
            try {
                bool log = _userServices.loginUser(name, password);
                if(log == true){
                return Ok(log);
                }else{ 
                return BadRequest();
                }
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpPost]
        public IActionResult InsertUser(User u) {
            try {
                User returnedUser = _userServices.insertUser(u);
                if(u!=null){
                    return CreatedAtRoute("GetAllUsers", new {name=returnedUser.name}, returnedUser); }
                    // return Redirect("https://localhost:5001/login.html"); }
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{name}")]
        public IActionResult UpdateUser(string name, User userIn) {
            try {
                _userServices.updateUser(name, userIn);
                if(name!=null || userIn!=null) return NoContent();
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpDelete("{name}")]
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