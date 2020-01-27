using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace I_Dream_of_Parking
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var _path = Path.GetDirectoryName(Application.ExecutablePath) + "\\ParkingLotDB.json";
            //  should load db from this directory:
            //  .\GitHub\ProjectDreams\I Dream of Parking\I Dream of Parking\I Dream of Parking\bin\Debug
            if (File.Exists(_path))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var GUI = new ParkingLotGUI(_path, 120);
                Application.Run(GUI);
            }
            else
            {
                InitalizeParkingLot(120);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var GUI = new ParkingLotGUI(_path, 120);
                Application.Run(GUI);
            }
        }

        static void InitalizeParkingLot(int rows)
        {
            var ParkingLot = new ParkingLotSystem(Path.GetDirectoryName(Application.ExecutablePath), "ParkingLotDB");
            ParkingLot.TruncateTable();
            ParkingLot.InsertSpaces(rows);
            ParkingLot.Save();
        }


    }
}
