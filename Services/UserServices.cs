using System;
using System.Collections.Generic;
using GameScheduler.Models;
using GameScheduler.Repository;
using System.Linq;

namespace GameScheduler.Services {
    public interface IUserServices {
        public List<User> getUsers();
        public User insertUser(User g);
        public void deleteUser(string title);

    }
    public class UserServices : IUserServices {
        private readonly IUserRepository _repo;
        public UserServices(IUserRepository UserRepo) {
           _repo=UserRepo;
        }
        public List<User> getUsers() {
            List<User> theCourses = _repo.GetAllUsers().ToList<User>();
            return theCourses;
        }
         public User insertUser(User newUser) {
            return _repo.InsertUser(newUser);
        }
         public void deleteUser(string title) {
             _repo.DeleteUser(title);
         }
    }
}