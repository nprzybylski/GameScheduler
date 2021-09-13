using System;
using System.Collections.Generic;
using GameScheduler.Models;
// in terminal, run: dotnet add package MySql.Data
using MySql.Data.MySqlClient;

namespace GameScheduler.Repository {
    public interface IUserRepository {
        List<User> User {get;set;}
        public User AddUser(User u);
        public void DeleteUser(string name);
    }
    public class UserRepository : IUserRepository {
        public List<User> Users {get;set;}
        private MySqlConnection _connection;
        public UserRepository() {
            string connectionString="server=localhost;userid=csci490user;password=csci490pass;database=GameScheduler";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }
        ~UserRepository() {
            _connection.Close();
        }
        public User AddUser(User newUser) {
            var statement = "INSERT into User (Name,Id,FavoriteGames,Bio) values (@newName,@newId,@newFavoriteGames,@newBio)";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newName", newUser.Name);
            command.Parameters.AddWithValue("@newId", newUser.Id);
            command.Parameters.AddWithValue("@newFavoriteGames", newUser.FavoriteGames);
            command.Parameters.AddWithValue("@newBio", newUser.Bio);

            int result = command.ExecuteNonQuery();
            if(result == 1)
                return newUser;
            else
                return null;
        }
        public void DeleteUser(string Name) {
                
            var statement = "DELETE FROM Users Where Name=@N";
            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@N", Name);

            int result = command.ExecuteNonQuery();

        }
    }
}