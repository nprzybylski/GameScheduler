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
        public GameEventRepository() {
            string connectionString="server=game-scheduler.cm5lnq4oiwiw.us-east-2.rds.amazonaws.com;userid=admin;password=C$C149o#ndkuko1;database=GameScheduler";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        
        }
        
        ~GameEventRepository() {
        
            _connection.Close();
        
        }



        public IEnumerable<GameEvent> GetAllGameEvents(){

            var statement = "SELECT * FROM GameEvents";
            var command = new MySqlCommand(statement, _connection);
            var results = command.ExecuteReader();


            List<GameEvent> newList = new List<GameEvent>();

            while(results.Read()){

                GameEvent e = new GameEvent{

                    Id = (int)results[0],
                    Title = (string)results[1],
                    Users = (string)results[2], 
                    GameId = (string)results[3],
                    Capacity = (int)results[4], 
                    Time = (DateTime)results[5], 
                    Description = (String)results[6]
                
                };
                newList.Add(e);

            }
            results.Close();
            return newList;
        }




        public GameEvent GetGameEventById(int id){

            var statement = "SELECT * GameEvents WHERE ID=@newId";
            
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@newId", id);
            command.ExecuteReader();
            var results = command.ExecuteReader();

            if(results.Read()){
                GameEvent e = new GameEvent{

                    Id = (int)results[0],
                    Title = (string)results[1],
                    Users = (string)results[2], 
                    GameId = (string)results[3],
                    Capacity = (int)results[4], 
                    Time = (DateTime)results[5], 
                    Description = (String)results[6]
                
                };
                results.Close();
                return e;
            }
            results.Close();
            return null;
            
        }

        

        public GameEvent InsertGameEvent(GameEvent newGameEvent){

            var statement = "INSERT INTO GameEvents (Id, Title, Users, GameId, Capacity, Time, Description) VALUES (@newId, @newTitle, @newUsers, @newGameId, @newCapacity, @newTime, @newDescription)";

            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@newId", newGameEvent.Id);
            command.Parameters.AddWithValue("@newTitle", newGameEvent.Title);
            command.Parameters.AddWithValue("@newUsers", newGameEvent.Users);
            command.Parameters.AddWithValue("@newGameId", newGameEvent.GameId);
            command.Parameters.AddWithValue("@newCapacity", newGameEvent.Capacity);
            command.Parameters.AddWithValue("@newTime", newGameEvent.Time);
            command.Parameters.AddWithValue("@newDescription", newGameEvent.Description);
            
            int result = command.ExecuteNonQuery();

            if(result == 1){
                return newGameEvent;
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
            command.Parameters.AddWithValue("@newGameId", newGameEvent.GameId);
            command.Parameters.AddWithValue("@newCapacity", newGameEvent.Capacity);
            command.Parameters.AddWithValue("@newTime", newGameEvent.Time);
            command.Parameters.AddWithValue("@newDescription", newGameEvent.Description);

            int result = command.ExecuteNonQuery();

        }



        
        public void DeleteGameEvent(int id){

            var statement = "DELETE FROM GameEvents WHERE Id = @newId";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newId", id);

            int result = command.ExecuteNonQuery();

        }




        // GetGameEventsByGameTitle will allow a user to search for all public events for a given game.
        public IEnumerable<GameEvent> GetAllGameEventsWithGameId(String id){

            var statement = "SELECT * FROM GameEvents WHERE GameId = @newGameId";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newGameId", id);

            var results = command.ExecuteReader();

            List<GameEvent> newList = new List<GameEvent>();

            while(results.Read()){

                GameEvent e = new GameEvent{

                    Id = (int)results[0],
                    Title = (string)results[1],
                    Users = (string)results[2], 
                    GameId = (string)results[3],
                    Capacity = (int)results[4], 
                    Time = (DateTime)results[5], 
                    Description = (String)results[6]
                
                };
                results.Close();
                return newList;
            }
            results.Close();
            return null;
            


        }





    }


}