using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GameScheduler.Models;
using GameScheduler.Services;
using GameScheduler.Repository;

namespace GameScheduler.Controllers {


    [ApiController]

    [Route("[[GameEvents]]")]

    public class GameEventController: ControllerBase{

        private IGameEventServices _gameEventServices;


        public GameEventController(IGameEventServices gameEventServices){

            _gameEventServices = gameEventServices;
        }


        
        [HttpGet(Name="GetAllGameEvents")]

        public IActionResult GetAllGameEvents(){

            try{

                List<GameEvent> list = _gameEventServices.GetAllGameEvents();
                

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

            try {
                _gameEventServices.UpdateGameEvent(id, g);
                if(id!=null || g!=null) return NoContent();
                else return BadRequest();
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal server error");
            }

            // return NoContent();

        }



        [HttpDelete("{id}")]


        public IActionResult DeleteGameEvent(int id){

            _gameEventServices.DeleteGameEvent(id);

            return NoContent();

        }



        [HttpGet("gametitle/{gameTitle}")]


        public IActionResult GetAllGameEventsWithGameTitle(string gameTitle){

            IEnumerable<GameEvent> list = _gameEventServices.GetAllGameEventsWithGameTitle(gameTitle);

            List<GameEvent> eventList = new List<GameEvent>();

            if(eventList.Count >= 1){return Ok(eventList);}

            return BadRequest();

        }

        // Get all game events with date
        [HttpGet("date/{date}")]


        public IActionResult GetAllGameEventsWithDate(string date){

            //date = "2021-09-27";
            GameEvent e = new GameEvent{

                Id = 234,
                Title = "Get all game events is being called",
                Users = "Bad Request", 
                GameTitle = "test title",
                Capacity = 1, 
                Time = DateTime.Now, 
                Description = "test descr"
                
            };
                
            List<GameEvent> list = _gameEventServices.GetAllGameEventsWithDate(date);

            List<GameEvent> testList = new List<GameEvent>();

            if(list.Count >= 1){return Ok(list);}

            testList.Add(e);
            return Ok(testList);


        }




        [HttpGet("date/{date}/{gtitle}")]


        public IActionResult GetGameEventsByGameTitleAndDate(string gtitle, string date){


                
            List<GameEvent> list = _gameEventServices.GetGameEventsByGameTitleAndDate(gtitle, date);

            if(list.Count >= 1){return Ok(list);}
            else{return BadRequest();}


        }











    }
}
