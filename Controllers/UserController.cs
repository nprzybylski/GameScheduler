using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GameScheduler.Models;
using GameScheduler.Services;

namespace GameScheduler.Controllers {

    [ApiController]

    [Route("[controller]")]
    public class GameSchedulerController : ControllerBase {
        private IUserServices _userServices;
        public GameSchedulerController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost]
        public IActionResult AddUser(User u) {
            try {
                User returnedUser = _userServices.AddUser(u);
                if(g !=null) return CreatedAtRoute("GetAllUsers", new {name=returnedUser.Name}, returnedUser);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{title}")]
        public IActionResult DeleteUser(string Name) {
            try {
                _userServices.DeleteUser(Name);
                if(title!=null) return NoContent();
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }
    }

    
}