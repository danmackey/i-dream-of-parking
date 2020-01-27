using System;
using System.Collections.Generic;

namespace I_Dream_of_Parking
{
    public class DAL
    {
        private ParkingLotSystem _parkingLot { get; set; }

        public DAL()
        {

        }

        public DAL(string path)
        {
            _parkingLot = ParkingLotSystem.Load(path);
            foreach (var space in _parkingLot._parkingSpaces)
            {
                if (space.Occupied)
                {
                    AddOccupiedSpace(space.UID);
                }
            }
        }

        public List<ParkingSpace> GetParkingSpaces()
        {
            return _parkingLot._parkingSpaces;
        }

        public int FindParkingSpace()
        {
            return _parkingLot.FindParkingSpace();
        }

        public int FindParkingSpace(DateTime timeStaying)
        {
            return _parkingLot.FindParkingSpace(timeStaying);
        }

        public ParkingSpace SearchParkingLot(int uid)
        {
            return _parkingLot.SearchParkingLot(uid);
        }

        public void SetSpaceOccupied(int uid)
        {
            _parkingLot.SetSpaceOccupied(uid);
        }

        public void SetSpaceAvailable(int uid)
        {
            _parkingLot.SetSpaceAvailable(uid);
        }

        public void ClearSpaces()
        {
            _parkingLot.ClearSpaces();
        }

        public void InsertSpaces(int spaces)
        {
            _parkingLot.InsertSpaces(spaces);
        }

        public void TruncateTable()
        {
            _parkingLot.TruncateTable();
        }

        public void Save()
        {
            _parkingLot.Save();
        }

        public void Save(string filename)
        {
            _parkingLot.Save(filename);
        }

        public void AddOccupiedSpace(int uid)
        {
            _parkingLot.AddOccupiedSpace(uid);
        }

        public List<int> GetOccupiedSpaces()
        {
            return _parkingLot.GetOccupiedSpaces();
        }

        public void RemoveOccupiedSpace(int uid)
        {
            _parkingLot.RemoveOccupiedSpace(uid);
        }
        public void Print()
        {
            _parkingLot.Print();
        }

        public void DisplayParkingLot()
        {
            Console.WriteLine(_parkingLot.DisplayParkingLot());
        }
    }
}
