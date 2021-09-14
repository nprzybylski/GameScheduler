using System;
using System.Collections.Generic;

namespace GameScheduler.Models {
    public class User {
        public string Name {get;set;}
        public int Id {get;}
        public string Password {get;set;}
        public List<Game> FavoriteGames {get;set;}
        public string Bio {get;set;}
    }
}