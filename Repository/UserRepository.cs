using System;
using System.Collections.Generic;
using GameScheduler.Models;
// in terminal, run: dotnet add package MySql.Data
using MySql.Data.MySqlClient;

namespace GameScheduler.Repository {
    public interface IUserRepository {
        List<User> Users {get; set;}
        public IEnumerable<User> GetAllUsers();
        public User InsertUser(User u);
        public void UpdateUser(string name, User userIn);
        public void DeleteUser(string name);
        public bool loginUser(string name, string password);
    }
    public class UserRepository : IUserRepository {
        public List<User> Users {get; set;}
        private MySqlConnection _connection;
        public UserRepository() {
            string connectionString="server=game-scheduler.cm5lnq4oiwiw.us-east-2.rds.amazonaws.com;userid=admin;password=C$C149o#ndkuko1;database=GameScheduler";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }
        ~UserRepository() {
            _connection.Close();
        }
        public IEnumerable<User> GetAllUsers() {
            var statement = "Select * from user";
            var command = new MySqlCommand(statement,_connection);
            var results = command.ExecuteReader();

            List<User> newList = new List<User>(25);

            while(results.Read()) {
                User g = new User {
                    name = (string)results[0],
                    password = (string)results[1],
                    bio = (string)results[2]
                };
                newList.Add(g);
            }
            results.Close();
            return newList;
        }
        public User InsertUser(User u) {
            //left off with statement below
            var statement = "INSERT into user (name,password,bio) values (@newName,@newPassword,@newBio)";

            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newName", u.name);
            command.Parameters.AddWithValue("@newPassword", u.password);
            command.Parameters.AddWithValue("@newBio", u.bio);

            int result = command.ExecuteNonQuery();
            if(result == 1)
                return u;
            else
                return null;
        }
        public void UpdateUser(string name, User userIn) {
            var statement = "Update user Set Name=@newName, Password=@newPassword, Bio=@newBio Where Name = @updateName";
            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@newName", userIn.name);
            command.Parameters.AddWithValue("@newPassword", userIn.password);
            command.Parameters.AddWithValue("@newBio", userIn.bio);
            command.Parameters.AddWithValue("@updateName", name);

            int result = command.ExecuteNonQuery();
        }
        public void DeleteUser(string name) {
                
            var statement = "DELETE FROM user Where Name=@N";
            var command = new MySqlCommand(statement,_connection);
            command.Parameters.AddWithValue("@N", name);

            int result = command.ExecuteNonQuery();

        }

        public bool loginUser(string name, string password) {
            var statement = "Select * from user";
            //Name is null somehow

            var command = new MySqlCommand(statement,_connection);
            var result = command.ExecuteReader();
            Console.WriteLine(name);
            Console.WriteLine(password);
            bool passMatch = true;
            while(result.Read()) {  
                Console.WriteLine((string)result[0]);
                Console.WriteLine((string)result[1]);
                if((string.Equals(name, (string)result[0])) && (string.Equals(password, (string)result[1]))){
                    passMatch = true;
                    break;
                }else{
                    passMatch = false;
                }
            }
            result.Close();
            Console.WriteLine(passMatch);
            return passMatch;
        }
    }
}