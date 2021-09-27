using System;
using System.Collections.Generic;
using GameScheduler.Models;
using MySql.Data.MySqlClient;

namespace GameScheduler.Repository {
    
    
    
    public interface IGameEventRepository {

       
        // CRUD Methods for Game Events. 

        public IEnumerable<GameEvent> GetAllGameEvents();

        public GameEvent GetGameEventById(int id);

        public GameEvent InsertGameEvent(GameEvent newGameEvent);

        public void UpdateGameEvent(int id, GameEvent newGameEvent);

        public void DeleteGameEvent(int id);

        // GetGameEventsByGame will allow a user to search for all public events for a given game.
        public IEnumerable<GameEvent> GetAllGameEventsWithGameId(string Id);

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

            var statement = "SELECT * FROM GameEvents";
            var command = new MySqlCommand(statement, _connection);
            var results = command.ExecuteReader();


            List<GameEvent> newList = new List<GameEvent>();

            while(results.Read()){

                GameEvent e = new GameEvent{

                    e.Id = (int)results[0],
                    e.Title = (string)results[1],
                    e.Users = (string)results[2], 
                    e.GameId = (string)results[3],
                    e.Capacity = (int)results[4], 
                    e.Time = (DateTime)results[5], 
                    e.Description = (String)results[6]
                
                };

            }
            results.Close();
            return newList;
        }




        public GameEvent GetGameEventById(int id){

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
                
                };

            }
            results.Close();
            return e;
        }

        

        public GameEvent InsertGameEvent(GameEvent newGameEvent){

            var statement = "INSERT INTO GameEvents (Id, Title, Users, GameId, Capacity, Time, Description) VALUES (@newId, @newTitle, @newUsers, @newGameId, @newCapacity, @newTime, @newDescription)";

            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@newId", newGameEvent.Id);
            command.Parameters.AddWithValue("@newTitle", newGameEvent.Title);
            command.Parameters.AddWithValue("@newUsers", newGameEvent.Users);
            command.Parameters.AddwithValue("@newGameId", newGameEvent.GameId);
            command.Parameters.AddwithValue("@newCapacity", newGameEvent.Capacity);
            command.Parameters.AddwithValue("@newTime", newGameEvent.Time);
            command.Parameters.AddwithValue("@newDescription", newGameEvent.Description);
            
            int result = command.ExecuteNonQuery();

            if(result == 1){
                return nweGameEvent;
            }else{
                return null;
            }
        }




        public void UpdateGameEvent(int id, GameEvent newGameEvent){

            var statement = "UPDATE GameEvent SET Title=@newTitle, Users=@newUsers, GameId=@newGameId, Capacity=@newCapacity, Time=@newTime, Description=@newDescription WHERE Id=@newId";

            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@newId", id);
            command.Parameters.AddWithValue("@newTitle", newGameEvent.Title);
            command.Parameters.AddWithValue("@newUsers", newGameEvent.Users);
            command.Parameters.AddwithValue("@newGameId", newGameEvent.GameId);
            command.Parameters.AddwithValue("@newCapacity", newGameEvent.Capacity);
            command.Parameters.AddwithValue("@newTime", newGameEvent.Time);
            command.Parameters.AddwithValue("@newDescription", newGameEvent.Description);

            int result = command.ExecuteNonQuery();

            if(result == 1){
                return newGameEvent;
            }else{
                return null;
            }

        }



        
        public void DeleteGameEvent(int id){

            var statement = "DELETE FROM GameEvents WHERE Id = @newId";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newId", id);

            result = command.ExecuteNonQuery();

        }




        // GetGameEventsByGameTitle will allow a user to search for all public events for a given game.
        public IEnumerable<GameEvent> GetAllGameEventsWithGameId(String id){

            var statement = "SELECT * FROM GameEvents WHERE GameId = @newGameId";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddwithValue("@newGameId", id);

            results = command.ExecuteReader();

            List<GameEvent> newList = new List<GameEvent>();

            while(results.Read()){

                GameEvent e = new GameEvent{

                    e.Id = (int)results[0],
                    e.Title = (string)results[1],
                    e.Users = (string)results[2], 
                    e.GameId = (string)results[3],
                    e.Capacity = (int)results[4], 
                    e.Time = (DateTime)results[5], 
                    e.Description = (String)results[6]
                
                };

            }
            results.Close();
            return newList;


        }





    }


}