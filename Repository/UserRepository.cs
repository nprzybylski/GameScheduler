using System;
using System.Collections.Generic;
using GameScheduler.Models;
// in terminal, run: dotnet add package MySql.Data
using MySql.Data.MySqlClient;

namespace GameScheduler.Repository {
    public interface IUserRepository {
        public User AddUser(User u);
        public void DeleteUser(string name);
    }
    public class UserRepository : IUserRepository {
        private MySqlConnection _connection;
        public UserRepository() {
            string connectionString="server=localhost;userid=csci490user;password=csci490pass;database=GameScheduler";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }
        ~UserRepository() {
            _connection.Close();
        }
        public User AddUser(User u) {
            var statement = "INSERT into User (Name,Id,Password,FavoriteGames,Bio) values (@newName,@newId,@newPassword,@newFavoriteGames,@newBio)";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newName", u.Name);
            command.Parameters.AddWithValue("@newId", u.Id);
            command.Parameters.AddWithValue("@newPassword", u.Password);
            command.Parameters.AddWithValue("@newFavoriteGames", u.FavoriteGames);
            command.Parameters.AddWithValue("@newBio", u.Bio);

            int result = command.ExecuteNonQuery();
            if(result == 1)
                return u;
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