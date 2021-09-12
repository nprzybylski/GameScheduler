using System;
using System.Collections.Generic;
using GameScheduler.Models;
using GameScheduler.Repository;
using System.Linq;

namespace GameScheduler.Services {
    public interface IGameServices {
        public List<Game> getGames();
        public Game insertGame(Game g);
        public void updateGame(string title, Game gameIn);
        public void deleteGame(string title);
        public Game getGameByTitle(string title); 

    }
    public class GameServices : IGameServices {
        private readonly IGameRepository _repo;
        public GameServices(IGameRepository gameRepo) {
           _repo=gameRepo;
        }
        public List<Game> getGames() {
            List<Game> theCourses = _repo.GetAllGames().ToList<Game>();
            return theCourses;
        }
         public Game insertGame(Game newGame) {
            return _repo.InsertGame(newGame);
        }
        public void updateGame(string title, Game gameIn) {
            _repo.UpdateGame(title, gameIn);
         }
         public void deleteGame(string title) {
             _repo.DeleteGame(title);
         }
         public Game getGameByTitle(string title) {
            Game g = _repo.GetGameByTitle(title);
            return g;
         }
    }
}