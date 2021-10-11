using System;
using System.Collections.Generic;
using GameScheduler.Models;
using GameScheduler.Repository;
using System.Linq;

namespace GameScheduler.Services {
    public interface IUserServices {
        public User AddUser(User u);
        public void DeleteUser(string name);
        public IEnumerable<User> GetAllUsers();
        public User getUserByName(string name);
        public void updateUser(string name, User userIn);
        

    }
    public class UserServices : IUserServices {
        private readonly IUserRepository _repo;
        public UserServices(IUserRepository userRepo) {
           _repo=userRepo;
        }
         public User AddUser(User newUser) {
             Console.WriteLine("Trying Services...");
             return _repo.AddUser(newUser);
        }
         public void DeleteUser(string name) {
             _repo.DeleteUser(name);
         }
         public IEnumerable<User> GetAllUsers() {
             return _repo.GetAllUsers();
         }
         public User getUserByName(string name) {
            User u = _repo.GetUserByName(name);
            return u;
         }
         public void updateUser(string name, User userIn) {
            _repo.UpdateUser(name, userIn);
         }
    }
}