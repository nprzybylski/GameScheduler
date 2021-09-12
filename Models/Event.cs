using System;
using System.Collections.Generic;

namespace GameScheduler.Models {
    public class Event {
        public string Title {get;set;}
        public int Id {get;set;}
        public int NumUsers {get;set;}
        /*
            ||  var date1 = new DateTime(2008, 3, 1, 7, 0, 0);       ||
            vv  // For en-US culture, displays 3/1/2008 7:00:00 AM   vv
        */
        public DateTime Time {get;set;}
        
    }
}