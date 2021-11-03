using System;
using System.Collections.Generic;
using GameScheduler.Models;
// in terminal, run: dotnet add package MySql.Data
using MySql.Data.MySqlClient;

namespace GameScheduler.Repository {
    public interface IGameRepository {
        List<Game> Games {get;set;}
        public IEnumerable<Game> GetAllGames();
        public Game InsertGame(Game newGame);
        public void UpdateGame(string title, Game gameIn);
        public void DeleteGame(string title);
        public Game GetGameByTitle(string title);
    }
    public class GameRepository : IGameRepository {
        public List<Game> Games {get;set;}
        private MySqlConnection _connection;
        public GameRepository() {
            //string connectionString="server=localhost;userid=csci490user;password=csci490pass;database=GameScheduler";
            string connectionString="server=game-scheduler.cm5lnq4oiwiw.us-east-2.rds.amazonaws.com;userid=admin;password=C$C149o#ndkuko1;database=GameScheduler";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }
        ~GameRepository() {
            _connection.Close();
        }
        public IEnumerable<Game> GetAllGames() {
            var statement = "Select * from Games";
            var command = new MySqlCommand(statement,_connection);
            var results = command.ExecuteReader();

            List<Game> newList = new List<Game>(25);

            while(results.Read()) {
                Game g = new Game {
                    Title = (string)results[0],
                    Description = (string)results[1],
                    Genre = (string)results[2]
                };
                newList.Add(g);
            }
            results.Close();
            return newList;
        }
        public Game InsertGame(Game newGame) {
            var statement = "INSERT into Games (Title,Description,Genre) values (@newTitle,@newDescription,@newGenre)";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newTitle", newGame.Title);
            command.Parameters.AddWithValue("@newDescription", newGame.Description);
            command.Parameters.AddWithValue("@newGenre", newGame.Genre);
            
            int result = command.ExecuteNonQuery();
            if(result == 1)
                return newGame;
            else
                return null;
        }
        public void UpdateGame(string title, Game gameIn) {
            var statement = "Update Games Set Title=@newTitle, Description=@newDescription, Genre=@newGenre Where Title = @updateTitle";
            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newTitle", gameIn.Title);
            command.Parameters.AddWithValue("@newDescription", gameIn.Description);
            command.Parameters.AddWithValue("@newGenre", gameIn.Genre);
            command.Parameters.AddWithValue("@updateTitle", title);

            int result = command.ExecuteNonQuery();
        }
        public void DeleteGame(string title) {
                
            var statement = "DELETE FROM Games Where Title=@t";
            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@t", title);

            int result = command.ExecuteNonQuery();

        }

        public Game GetGameByTitle(string title) {
            var statement = "Select * from Games where Title=@newTitle";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newTitle", title);
            var results = command.ExecuteReader();

            if(results.Read()) {
                Game g = new Game {
                    Title = (string)results[0],
                    Description = (string)results[1],
                    Genre = (string)results[2]
                };
                results.Close();
                return g;
            }
            results.Close();
            return null;
        }
    }


}