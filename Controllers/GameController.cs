using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GameScheduler.Models;
using GameScheduler.Services;

namespace GameScheduler.Controllers {

    [ApiController]

    [Route("[controller]")]
    public class GameSchedulerController : ControllerBase {
        private IGameServices _gameServices;
        public GameSchedulerController(IGameServices gameServices)
        {
            _gameServices = gameServices;
        }

        [HttpGet(Name="GetAllGames")]
        public IActionResult GetAllGames()
        {
            try {
                IEnumerable<Game> list = _gameServices.getGames();
                if(list!=null) return Ok(list);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult AddGame(Game g) {
            try {
                Game returnedGame = _gameServices.insertGame(g);
                if(g !=null) return CreatedAtRoute("GetAllGames", new {name=returnedGame.Title}, returnedGame);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{title}")]
        public IActionResult UpdateGame(string title, Game gameIn) {
            try {
                _gameServices.updateGame(title, gameIn);
                if(title!=null || gameIn!=null) return NoContent();
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{title}")]
        public IActionResult DeleteGame(string title) {
            try {
                _gameServices.deleteGame(title);
                if(title!=null) return NoContent();
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{title}", Name="GetGameByTitle")]
        public IActionResult GetGameeByTitle(string title) {
            try {
                Game g = _gameServices.getGameByTitle(title);
                if(g != null) return Ok(g);
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }
        }
    }

    
}