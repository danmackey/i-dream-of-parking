using System;
using System.Collections.Concurrent;
using System.Threading;

namespace I_Dream_of_Parking
{
    public class CarQueue
    {
        private readonly Random _ran = new Random();
        private readonly ConcurrentQueue<Car> _carQ = new ConcurrentQueue<Car>();
        private ParkingLotGUI _parkingLotGUI { get; set; }
        private DAL _dataAccessLayer = new DAL();
        private int _carsToCreate { get; set; }
        private int _timeBetweenArrivals { get; set; }
        private int _carMaxTime { get; set; }

        public CarQueue(int CarsToCreate, int TimeBetweenArrivals, int CarMaxTime, DAL DataAccessLayer, ParkingLotGUI parkingLotGUI)
        {
            _dataAccessLayer = DataAccessLayer;
            _carsToCreate = CarsToCreate;
            _timeBetweenArrivals = TimeBetweenArrivals;
            _carMaxTime = CarMaxTime;
            _parkingLotGUI = parkingLotGUI;
        }

        public void Enqueue()
        {
            for (int i = 0; i < _carsToCreate; i++)
            {
                Thread.Sleep(_ran.Next(0, _timeBetweenArrivals));
                Car car = new Car(_carMaxTime, i);
                _carQ.Enqueue(car);
                Console.WriteLine("Car {0} will stay for: " + car.TimeStaying + " milliseconds.", i);
            }
        }

        public void Dequeue()
        {
            Car car = new Car();
            int i = 0;
            while (i < _carsToCreate)
            {
                if (_carQ.TryPeek(out car))
                {
                    Console.WriteLine("Car {0} is staying in the lot for " + car.TimeStaying + " milliseconds.", car.Name);
                    Thread.Sleep(_ran.Next(0, _timeBetweenArrivals));
                    _carQ.TryDequeue(out car);
                    var space = _dataAccessLayer.FindParkingSpace(car.TimeStaying);
                    _parkingLotGUI._parkingSpaceButtons[space].BackColor = System.Drawing.Color.Red;
                    Console.WriteLine("Car {0} has entered the parking lot.", car.Name);
                    i++;
                }
            }
            while (!_carQ.IsEmpty)
            {
                _carQ.TryDequeue(out car);
                Console.WriteLine("Car {0} has entered the parking lot.", car.Name);
            }
            if (_carQ.IsEmpty) { Console.WriteLine("There are no cars waiting to enter the parking lot."); }
        }
    }
}
