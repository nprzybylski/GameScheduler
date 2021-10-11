using System;
using System.Collections.Generic;
using GameScheduler.Models;
// in terminal, run: dotnet add package MySql.Data
using MySql.Data.MySqlClient;
using System.Linq;


namespace GameScheduler.Repository {
    public interface IUserRepository {
        List<User> Users {get;set;}
        public User AddUser(User u);
        public bool AddGameToUser(string id, Game newGame);
        public void DeleteUser(string name);
        public IEnumerable<User> GetAllUsers();
        public User GetUserByName(string name);
        public void UpdateUser(string name, User userIn);
    }
    public class UserRepository : IUserRepository {
        public List<User> Users {get;set;}
        private static MySqlConnection _connection;
        public UserRepository() {
            //string connectionString="server=localhost;userid=csci490user;password=csci490pass;database=GameScheduler";
            string connectionString="server=game-scheduler.cm5lnq4oiwiw.us-east-2.rds.amazonaws.com;userid=admin;password=C$C149o#ndkuko1;database=GameScheduler";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }
        ~UserRepository() {
            _connection.Close();
        }
        
        public bool AddGameToUser(string id, Game newGame) {
            var statement = "INSERT INTO UserGames (UserId,GameTitle) Values (@newUserId, @newGameTitle)";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@newUserId", id);
            command.Parameters.AddWithValue("@newGameTitle", newGame.Title);

            int result = command.ExecuteNonQuery();
            if(result == 1)
                return true;
            else
                return false;
        }
        public void DeleteUser(string Name) {
                
            var statement = "DELETE FROM Users Where Name=@N";
            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@N", Name);

            int result = command.ExecuteNonQuery();

        }
        public IEnumerable<User> GetAllUsers() {
            Console.WriteLine("Get Repo...");
            var statement = "Select * from Users";
            var command = new MySqlCommand(statement,_connection);
            var results = command.ExecuteReader();
            Console.WriteLine("Get Repo Execute...");
            List<User> newList = new List<User>(25);
            

            while(results.Read()) {
                Console.WriteLine("test...");
                User u = new User {
                    Name = (string)results[0],
                    // Id = (int)results[1],
                    Password = (string)results[1]
                    // FavoriteGames = (List<Game>)results[3],
                    // Bio = (string)results[4]

                };
                newList.Add(u);
                
            }
            results.Close();
            Console.WriteLine("Done Get Repo...");
            return newList;
        }

        public User GetUserByName(string name) {
            var statement = "Select * from Users where Name=@newName";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newName", name);
            var results = command.ExecuteReader();

            if(results.Read()) {
                User u = new User {
                    Name = (string)results[0],
                    Password = (string)results[1]
                };
                results.Close();
                return u;
            }
            results.Close();
            return null;
        }

        public User AddUser(User newUser) {
            //var statement = "INSERT into User (Name,Id,Bio) values (@newName,@newId,@newBio)";
            Console.WriteLine("Trying Repo...");
            var statement = "INSERT into Users (Name,Password) values (@newName,@newPass)";

            // UserRepository UserRepo = new UserRepository();
            // List<User> users = UserRepo.GetAllUsers().ToList<User>();

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newName", newUser.Name);
            command.Parameters.AddWithValue("@newPass", newUser.Password);
            //command.Parameters.AddWithValue("@newId",users.Count()-1);
            //command.Parameters.AddWithValue("@newBio", newUser.Bio);
            Console.WriteLine("Mid Repo...");
            int result = command.ExecuteNonQuery();
            Console.WriteLine("Post Mid Repo...");
            if(result == 1) {
                Console.WriteLine(newUser.Name);
                Console.WriteLine(newUser.Password);
                return newUser;
            }
            else {
                Console.WriteLine("Null Repo...");
                return null;
            }
        }
        // public IEnumerable<Game> GetGamesForUserById(string id) {
        //     var statement = "Select * from Users where Id=@newId";

        //     var command = new MySqlCommand(statement,_connection);
        //     command.Parameters.AddWithValue("@newId", id);
        //     var results = command.ExecuteReader();

        //     if(results.Read()) {
        //         User u = new User {
        //             Name = (string)results[0],
        //             Id = (int)results[1],
        //             Password = (string)results[2],
        //             Bio = (string)results[3]
        //         };
        //         results.Close();
        //         //return c;

        //         var _statement = "Select * from UserGames where UserId=@newUserId";
        //         var _command = new MySqlCommand(_statement,_connection);
        //         _command.Parameters.AddWithValue("@newUserId",u.Id);
        //         var _results = _command.ExecuteReader();
                    
        //         List<Game> _newList = new List<Game>(25);
                    
        //         while(_results.Read()) {
        //             Game _g = new Game {
        //                 Title = (string)_results[1]
        //             };
        //             _newList.Add(_g);
        //             u.FavoriteGames = _newList;
        //         }
        //         _results.Close();
        //         return u.FavoriteGames;
        //     }
        //     results.Close();
        //     return null;
        // }

        public void UpdateUser(string name, User userIn) {
            var statement = "Update Users Set Name=@newName, Password=@newPassword Where Name = @updateName";
            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newName", userIn.Name);
            command.Parameters.AddWithValue("@newPassword", userIn.Password);
            command.Parameters.AddWithValue("@updateName", name);

            int result = command.ExecuteNonQuery();
        }
    }
}