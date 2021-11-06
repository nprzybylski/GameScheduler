using System;
using System.Collections.Generic;

namespace GameScheduler.Models {
    public class User {
        public string name {get;set;}
        public string password {get;set;}

        public List<Game> FavoriteGames{get; set;}
        public string bio {get;set;}
    }
}