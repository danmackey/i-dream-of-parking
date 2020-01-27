using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace I_Dream_of_Parking
{
    public class ParkingLotSystem
    {
        public List<ParkingSpace> _parkingSpaces { get; set; }
        private readonly List<char> _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
        private List<int> _occupiedSpaces = new List<int>();
        private static string _jsonPath { get; set; }
        public Dictionary<int, DateTime> Times { get; set; }

        public ParkingLotSystem(string path, string filename)
        {
            _parkingSpaces = new List<ParkingSpace>();
            _jsonPath = path + "\\" + filename + ".json";
        }

        public int FindParkingSpace(DateTime timeLeaving)
        {
            int res = FindParkingSpace();
            if (res != -1)
            {
                _parkingSpaces[res].TimeLeaving = timeLeaving;
            }
            return res;
        }

        public int FindParkingSpace()
        {
            int res = -1;

            int i = 1, k = 19;
            bool b = true;

            while (b && i < _parkingSpaces.Count && k < _parkingSpaces.Count)
            {
                if (_parkingSpaces[i - 1].Occupied == true)
                {
                    if (_parkingSpaces[k].Occupied == true)
                    {
                        if (i % 10 == 0) { i += 11; }
                        else { i++; }
                        if (k % 10 == 0) { k += 29; }
                        else { k--; }
                    }
                    else
                    {
                        SetSpaceOccupied(k);
                        res = k;
                        b = false;
                    }
                }
                else
                {
                    SetSpaceOccupied(i - 1);
                    res = i - 1;
                    b = false;
                }
            }
            return res;
        }

        public ParkingSpace SearchParkingLot(int uid)   // find a parking space by a given UID
        {
            var ret = new ParkingSpace(0);
            if (uid >= 0 && uid <= _parkingSpaces.Count())
            {
                var space = _parkingSpaces.Find(ps => ps.UID == uid);
                if (space != null)
                {
                    ret = space;
                }
            }
            else { throw new IndexOutOfRangeException("SearchParkingLot"); }
            return ret;
        }

        private void SetOccupied(int uid, bool state)   // set a parking space occupied/available by a given UID
        {
            if (uid >= 0 && uid <= _parkingSpaces.Count())
            {
                var space = SearchParkingLot(uid);
                if (space.UID >= 0)
                {
                    space.Occupied = state;
                    space.StartTime = DateTime.Now;
                    Save();
                }
                else { throw new IndexOutOfRangeException("SetOccupied"); }
            }
        }

        public void SetSpaceOccupied(int uid)   // set space occupied by a given UID
        {
            SetOccupied(uid, true);
        }

        public void SetSpaceAvailable(int uid)  // set space available by a given UID
        {
            SetOccupied(uid, false);
        }

        public void ClearSpaces()   // sets all spaces to available
        {
            _parkingSpaces.ForEach(ps => ps.Occupied = false);
        }

        public void InsertSpaces(int spaces)   // insert n number of spaces. primarily for initalizing system
        {
            if (spaces > 0)
            {
                var newUID = 0;
                if (_parkingSpaces.Any() == true)
                {
                    newUID = _parkingSpaces.Last().UID + 1;
                }
                for (int i = 0, j = 0; i < spaces; i++)
                {
                    if (i % 10 == 0 && i > 0) { j++; }
                    _parkingSpaces.Add(new ParkingSpace(newUID + i)
                    {
                        Occupied = false,
                        SectionLetter = _alphabet[j],
                        SpaceNumber = i % 10,
                        Column = j % 2,
                        StartTime = DateTime.Now
                    });
                }
                _parkingSpaces.Sort((x, y) => x.UID.CompareTo(y.UID));
            }
        }

        public void TruncateTable() // reset ParkingLot to new list, does not touch DB
        {
            _parkingSpaces = new List<ParkingSpace>();
        }

        public void Save()
        {
            var str = JsonConvert.SerializeObject(this);
            File.WriteAllText(_jsonPath, str);
        }

        public void Save(string filename)
        {
            var str = JsonConvert.SerializeObject(this);
            File.WriteAllText(filename, str);
        }

        public static ParkingLotSystem Load()
        {
            ParkingLotSystem ret = null;
            if (File.Exists(_jsonPath) == true)
            {
                var str = File.ReadAllText(_jsonPath);
                ret = JsonConvert.DeserializeObject<ParkingLotSystem>(str);
                ret._parkingSpaces.Sort((x, y) => x.UID.CompareTo(y.UID));
            }
            return ret;
        }

        public static ParkingLotSystem Load(string filename)
        {
            ParkingLotSystem ret = null;
            if (File.Exists(filename) == true)
            {
                var str = File.ReadAllText(filename);
                ret = JsonConvert.DeserializeObject<ParkingLotSystem>(str);
                ret._parkingSpaces.Sort((x, y) => x.UID.CompareTo(y.UID));
                _jsonPath = filename;

            }
            return ret;
        }

        public void AddOccupiedSpace(int uid)
        {
            _occupiedSpaces.Add(uid);
            _occupiedSpaces.Sort((x, y) => x.CompareTo(y));
        }

        public List<int> GetOccupiedSpaces()
        {
            return _occupiedSpaces;
        }

        public void RemoveOccupiedSpace(int uid)
        {
            _occupiedSpaces.Remove(uid);
        }

        public void Print()
        {
            foreach (ParkingSpace parkingSpace in _parkingSpaces)
            {
                Console.Write("Parking Space: " + parkingSpace.SectionLetter + parkingSpace.SpaceNumber.ToString() + " is ");
                if (parkingSpace.Occupied) { Console.Write("occupied."); }
                else { Console.Write("empty."); }
                Console.Write("  This space is in the ");
                if (parkingSpace.Column == 0) { Console.WriteLine("left column."); }
                else { Console.WriteLine("right column."); }
            }

        }

        public string DisplayParkingLot()
        {
            string res = "";
            for (int i = 1; i < 120; i++)
            {
                if (i % 40 == 0 && i > 0) { res = res.TrimEnd(' '); res += "\n\n"; }
                else if (i % 20 == 0 && i > 0) { res = res.TrimEnd(' '); res += "\n"; }
                else if (i % 10 == 0 && i > 0) { res = res.TrimEnd(' '); res += "\t"; }
                res = res + _parkingSpaces[i].SectionLetter + _parkingSpaces[i].SpaceNumber.ToString() + " ";
            }
            return res;
        }
    }
}
