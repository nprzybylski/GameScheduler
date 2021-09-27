using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GameScheduler.Models;
using GameScheduler.Services;
using GameScheduler.Repository;

namespace GameScheduler.Controllers {


    [ApiController]

    [Route("[GameEvents]")]

    public class GameEventController: ControllerBase{

        private IGameEventServices _gameEventServices;


        public GameEventController(IGameEventServices gameEventServices){

            _gameEventServices = gameEventServices;
        }


        
        [HttpGet]

        public IActionResult GetAllGameEvents(){

            try{

                IEnumerable<GameEvent> list = _gameEventServices.GetAllGameEvents();

                if(list!=null) return Ok(list);
                else return BadRequest();

            }
            catch(Exception ex){

                return StatusCode(500, "Internal Server Error");

            }
        }


        [HttpGet("{id}", Name="GetGameEventById")]


        public IActionResult GetGameEventById(int id){

            try{
                
                // Testing this method, if not we may need to use foreach and search through a list of GameEvents that have a matching ID. 
                
                GameEvent g = _gameEventServices.GetGameEventById(id);


                if(g!=null) return Ok(g);
                else return BadRequest();

            }
            catch(Exception ex){

                return StatusCode(500, "Internal Server Error");

            }

        }


        [HttpPost]

        public IActionResult InsertGameEvent(GameEvent g){

            try{
  
                GameEvent returnedGameEvent = _gameEventServices.InsertGameEvent(g);
                if(g !=null) return CreatedAtRoute("GetAllGameEvents", new {name=returnedGameEvent.Title}, returnedGameEvent);
                else return BadRequest();
            }
            catch(Exception ex){

                return StatusCode(500, "Internal Server Error");

            }


        }




        [HttpPut("{id}")]


        public IActionResult UpdateGameEvent(int id, GameEvent g){

            _gameEventServices.UpdateGameEvent(id, g);

            return NoContent();

        }



        [HttpDelete("{id}")]


        public IActionResult DeleteGameEvent(int id){

            _gameEventServices.DeleteGameEvent(id);

            return NoContent();

        }



        [HttpGet("{gameId}/gameid/")]


        public IActionResult GetAllGameEventsWithGameId(string gameId){

            IEnumerable<GameEvent> list = _gameEventServices.GetAllGameEventsWithGameId(gameId);

            List<GameEvent> eventList = new List<GameEvent>();

            if(eventList.Count >= 1){return Ok(eventList);}

            return BadRequest();

        }




        













    }
}
