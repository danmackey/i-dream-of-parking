using System;

namespace I_Dream_of_Parking
{
    public class Car
    {
        public Random ran = new Random();
        public DateTime TimeStaying { get; set; }
        public int Name { get; set; }

        public Car() { }

        public Car(int maxTime, int N)
        {
            var now = DateTime.Now;
            TimeSpan addTime = new TimeSpan(0, 0, ran.Next(maxTime));
            TimeStaying = now.Add(addTime);
            Name = N;
        }
    }
}
