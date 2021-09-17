using System;
using System.Collections.Generic;
using GameScheduler.Models;
using MySql.Data.MySqlClient;

namespace GameScheduler.Repository {
    
    
    
    public interface IGameEventRepository {

       
        // CRUD Methods for Game Events. 

        public IEnumerable<GameEvent> GetAllGameEvents();

        public GameEvent GetGameEventByID(int id);

        public GameGameEvent InsertGame(GameEvent newGameEvent);

        public void UpdateGameEvent(int id, GameEvent newGameEvent);

        public void DeleteGameEvent(int id);

        // GetGameEventsByGame will allow a user to search for all public events for a given game.
        public Game GetAllGameEventsByGame(Game inGame);

        // Possible Methods of the future
        /*
            
            public void addUserToGameEvent 

            public IEnumerable<User> GetAllUsersInGameEvent(int gameEventId);

        */
    }




    public class GameEventRepository : IGameEventRepository {

        public List<GameEvent> GameEvent {get;set;}
        
        
        
        private MySqlConnection _connection;
        public GameRepository() {
            string connectionString="server=game-scheduler.cm5lnq4oiwiw.us-east-2.rds.amazonaws.com;userid=admin;password=C$C149o#ndkuko1;database=GameScheduler";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }
        
        ~GameRepository() {
        
            _connection.Close();
        
        }



        public IEnumerable<GameEvent> GetAllGameEvents(){

            var statement = "SELECT * FROM GameEvents"
            var command = new MySqlCommand(statement, _connection);
            var results = command.ExecuteReader();


            List<GameEvent> newList = new List<GameEvent>

            while(results.Read()){

                GameEvent e = new GameEvent{

                    e.Id = (int)results[0],
                    e.Title = (string)results[1],
                    e.Users = (string)results[2], 
                    e.GameId = (string)results[3],
                    e.Capacity = (int)results[4], 
                    e.Time = (DateTime)results[5], 
                    e.Description = (String)results[6]
                
                }

            }
            results.Close();
            return newList;
        }



        public GameEvent GetGameEventByID(int id){

            var statement = "SELECT * GameEvents WHERE ID=@newId";
            
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@newId", id);
            command.executeReader();
            var results = command.ExecuteReader();

            if(results.Read()){
                GameEvent e = new GameEvent{

                    e.Id = (int)results[0],
                    e.Title = (string)results[1],
                    e.Users = (string)results[2], 
                    e.GameId = (string)results[3],
                    e.Capacity = (int)results[4], 
                    e.Time = (DateTime)results[5], 
                    e.Description = (String)results[6]
                
                }

            }
            results.Close();
            return e;
        }

        
        //public GameGameEvent InsertGame(GameEvent newGameEvent);

        //public void UpdateGameEvent(int id, GameEvent newGameEvent);

        //public void DeleteGameEvent(int id);

        // GetGameEventsByGame will allow a user to search for all public events for a given game.
        //public Game GetAllGameEventsByGame(Game inGame);





    }


}