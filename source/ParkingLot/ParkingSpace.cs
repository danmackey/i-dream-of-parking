using System;

namespace I_Dream_of_Parking
{
    public class ParkingSpace
    {
        public int UID { get; set; }    // Unique ID : UID = 0, 1, 2, ..., -> total spaces - 1
        public bool Occupied { get; set; } // true if a car is in space, false if space is empty
        public char SectionLetter { get; set; } // letter associated with section of spaces
        public int SpaceNumber { get; set; } // number of space in given section
        public int Column { get; set; } // 0 if left column, 1 if right column
        public DateTime StartTime { get; set; } // time that space becomes occupied
                                                // this will be set every time the space becomes 
        public DateTime TimeLeaving { get; set; }

        public ParkingSpace(int uid) { UID = uid; }

    }
}
