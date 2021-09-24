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
        

    }
    public class UserServices : IUserServices {
        private readonly IUserRepository _repo;
        public UserServices(IUserRepository userRepo) {
           _repo=userRepo;
        }
         public User AddUser(User newUser) {
            return _repo.AddUser(newUser);
        }
         public void DeleteUser(string name) {
             _repo.DeleteUser(name);
         }
         public IEnumerable<User> GetAllUsers() {
             return _repo.GetAllUsers();
         }
    }
}