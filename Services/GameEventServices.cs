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

        public Game GetAllGameEventsWithGameId(string id);

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

            return _repo.UpdateGameEvent(id, newGameEvent);
        }

        public void DeleteGameEvent(int id){

            return _repo.DeleteGameEvent(id);
        }

        public Game GetAllGameEventsWithGameId(string id){
            List<GameEvent> allGameEvents = _repo.GetAllGameEventsWithGameId(id);
            return allGameEvents;
        }

    }



}