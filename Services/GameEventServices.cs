using System;
using System.Collections.Generic;
using GameScheduler.Models;
using GameScheduler.Repository;
using System.Linq;


namespace GameScheduler.Services{

    public interface IGameEventServices
    {

        public List<GameEvent> GetAllGameEvents();

        public GameEvent GetGameEventById(int id);

        public GameEvent InsertGameEvent(GameEvent newGameEvent);

        public void UpdateGameEvent(int id, GameEvent newGameEvent);

        public void DeleteGameEvent(int id);

        public List<GameEvent> GetAllGameEventsWithGameTitle(string Title);

        //Get all Game Events with date
        public List<GameEvent> GetAllGameEventsWithDate(string date);


    }



    public class GameEventServices : IGameEventServices
    {

        private readonly IGameEventRepository _repo;


        public GameEventServices(IGameEventRepository gameEventRepo){

            _repo = gameEventRepo;
        }



        public List<GameEvent> GetAllGameEvents(){

            List<GameEvent> allGameEvents = _repo.GetAllGameEvents().ToList<GameEvent>();
            return allGameEvents;

        }
        
        public GameEvent GetGameEventById(int id){

            GameEvent g = _repo.GetGameEventById(id);
            return g;
        }

        public GameEvent InsertGameEvent(GameEvent newGameEvent){

            return _repo.InsertGameEvent(newGameEvent);
        }


        public void UpdateGameEvent(int id, GameEvent newGameEvent){

            _repo.UpdateGameEvent(id, newGameEvent);
        }

        public void DeleteGameEvent(int id){

            _repo.DeleteGameEvent(id);
        }

        public List<GameEvent> GetAllGameEventsWithGameTitle(string Title){
            List<GameEvent> allGameEvents = _repo.GetAllGameEventsWithGameTitle(Title).ToList<GameEvent>();
            return allGameEvents;
        }

        //Get all Game Events with date
        public List<GameEvent> GetAllGameEventsWithDate(string date){
            List<GameEvent> allGameEvents = _repo.GetAllGameEventsWithDate(date);
            return allGameEvents; 
        }


    }



}