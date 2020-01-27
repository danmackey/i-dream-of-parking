using System;

namespace I_Dream_of_Parking
{
    public class IDCheckEventArgs : EventArgs
    {
        public string ID { get; set; }
        public bool Valid { get; set; }
    }
}
