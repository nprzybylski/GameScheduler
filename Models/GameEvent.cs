using System;
using System.Collections.Generic;

namespace GameScheduler.Models {
    public class GameEvent {

        public int Id {get;set;}

        public string Title {get;set;}

        /*
        List of users that have joined a game event. 

        Value could be a string with User ID's split by comma. such as Users = "1332, 3323, 1234, 4432". Asplitter can be used to seperate these values
        and the resulting string could be used as a User ID to get information on each user. 
        
        */
        public String Users {get;set;}
        
        // Game associated with the game event.
        public String GameId {get; set;}

        // Limited number of users allowed to join a game event. 
        public int Capacity {get;set;}

        /*
            ||  var date1 = new DateTime(2008, 3, 1, 7, 0, 0);       ||
            vv  // For en-US culture, displays 3/1/2008 7:00:00 AM   vv
        */
        public DateTime Time {get;set;}
        
        public string Description{get;set}
    }
}