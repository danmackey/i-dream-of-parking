using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.IO;

namespace I_Dream_of_Parking
{
    public class ParkingLotGUI : Form
    {
        // ------------------------------------------------------------------------------------------------
        // BUSINESS LOGIC LAYER
        // ------------------------------------------------------------------------------------------------
        private DAL _dataAccessLayer { get; set; }

        private List<ParkingSpace> _parkingSpaces { get; set; }

        // ------------------------------------------------------------------------------------------------
        // GUI LAYER
        // ------------------------------------------------------------------------------------------------
        private System.ComponentModel.IContainer components = null;

        private readonly List<char> _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
        private string _path { get; set; }
        private List<Button> _initParkingSpaceButtons = new List<Button>();
        public Button[] _parkingSpaceButtons { get; set; }
        private Button[] _otherButtons { get; set; }
        private Button _getSpace { get; set; }
        private Button _leaveSpace { get; set; }
        private Button _admin { get; set; }
        private Button _adminClear { get; set; }
        private Button _demoMode { get; set; }
        private CarQueue _carQueue { get; set; }
        private ParkingSpaceCardGUI _card { get; set; }
        private bool _continue { get; set; } = false;
        private bool _spaceClicked { get; set; } = false;
        public ParkingLotGUI(string path, int spaces)
        {
            _path = path;
            _dataAccessLayer = new DAL(_path);
            _parkingSpaces = _dataAccessLayer.GetParkingSpaces();
            CreateEmployees(10);
            CreateParkingSpaces(spaces);
            InitializeComponent();
        }

        private void CreateParkingSpaces(int spaces)
        {
            int spaceNum = 0, spaceLetter = 0, spacesInRow = 10;
            int leftX = 100, leftY = 50, rightX = 1030, rightY = 50;

            for (int i = 0; i < spaces; i++)
            {
                var button = new Button()
                {
                    Enabled = false,
                    Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    Margin = new Padding(0),
                    Name = i.ToString(),
                    Size = new Size(70, 100),
                    Tag = i,
                    TabStop = false,
                    Text = _alphabet[spaceLetter].ToString() + spaceNum.ToString()
                };

                if (_parkingSpaces[i].Occupied) { button.BackColor = Color.Red; }
                else { button.BackColor = Color.Green; }

                if (spaceLetter % 2 == 0) // if this is the left column
                {
                    button.Location = new Point(leftX, leftY);
                    leftX += 80;
                }
                else // else this is the right column
                {
                    button.Location = new Point(rightX, rightY);
                    rightX += 80;
                }

                spaceNum++;
                if (spaceNum == spacesInRow) // end of spaces in row, need to increment to new row
                {
                    spaceNum = 0;
                    if (spaceLetter % 2 == 0) { leftX = 100; leftY += 110; } // end of left column
                    else { rightX = 1030; rightY += 110; } // end of right column
                    if ((spaceLetter - 2) % 4 == 0) { leftY += 30; } // end of two row grouping in left column
                    if ((spaceLetter - 3) % 4 == 0) { rightY += 30; } // end of two row grouping in right column
                    spaceLetter++;
                }

                _initParkingSpaceButtons.Add(button);
            }
        }

        private void InitializeComponent()
        {
            // GUI Initialization
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1904, 1041);

            // Parking Space Buttons
            _parkingSpaceButtons = new Button[_initParkingSpaceButtons.Count];
            foreach (var space in _initParkingSpaceButtons)
            {
                space.Click += new EventHandler(ParkingSpace_Click);
                Controls.Add(space);
            }
            Controls.CopyTo(_parkingSpaceButtons, 0);

            // Get Parking Space Button
            _getSpace = new Button()
            {
                Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Regular, GraphicsUnit.Point, 0),
                Location = new Point(810, 810),
                Name = "GetParking",
                Size = new Size(300, 200),
                TabStop = true,
                Text = "Press Here to Get Parking"
            };
            _getSpace.Click += new EventHandler(GetSpace_Click);
            Controls.Add(_getSpace);


            // Leave Parking Space Button
            _leaveSpace = new Button()
            {
                Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Regular, GraphicsUnit.Point, 0),
                Location = new Point(1520, 810),
                Name = "LeaveParking",
                Size = new Size(300, 200),
                TabStop = true,
                Text = "Press Here to Leave the Parking Lot"
            };
            if (_dataAccessLayer.GetOccupiedSpaces().Count == 0)
            {
                _leaveSpace.Enabled = false;
            }
            _leaveSpace.Click += new EventHandler(Leave_Click);
            Controls.Add(_leaveSpace);

            // Admin Button
            _admin = new Button()
            {
                Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Regular, GraphicsUnit.Point, 0),
                Location = new Point(100, 810),
                Name = "Admin",
                Size = new Size(300, 200),
                TabStop = false,
                Text = "Administrator Login"
            };
            _admin.Click += new EventHandler(Admin_Click);
            Controls.Add(_admin);

            //// Enter Demo Mode
            //_demoMode = new Button()
            //{
            //    Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Regular, GraphicsUnit.Point, 0),
            //    Location = new Point(810, 810),
            //    Name = "DemoMode",
            //    Size = new Size(300, 200),
            //    TabStop = true,
            //    Text = "Start Demo Mode"
            //};
            //_demoMode.Click += new EventHandler(DemoMode_Click);
            //Controls.Add(_demoMode);

            // Admin Clear
            _adminClear = new Button()
            {
                Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Regular, GraphicsUnit.Point, 0),
                Location = new Point(1520, 810),
                Name = "AdminClear",
                Size = new Size(300, 200),
                TabStop = false,
                Visible = false,
                Text = "Clear All Parking"
            };
            _adminClear.Click += new EventHandler(Admin_Clear);
            Controls.Add(_adminClear);

            // GUI Initialization
            Name = "GUI";
            Text = "I Dream of Parking";
            WindowState = FormWindowState.Maximized;
            Load += new EventHandler(ParkingGUI_Load);
            ResumeLayout(false);
        }


        public void GetSpace_Click(object sender, EventArgs e)
        {
            if (_spaceClicked == false)
            {
                _getSpace.Text = "Press Here to Continue";
                _continue = false;
                _spaceClicked = true;
                _admin.Enabled = false;
                _leaveSpace.Enabled = false;
                FindOpenSpace(e);
            }
            else
            {
                _getSpace.Text = "Press Here to Get Parking";
                _continue = true;
                _spaceClicked = false;
                _admin.Enabled = true;
                _leaveSpace.Enabled = true;
            }
        }

        private void FindOpenSpace(EventArgs e)
        {
            int space = _dataAccessLayer.FindParkingSpace();
            if (space != -1)
            {
                ParkingSpace_Wait(_parkingSpaceButtons[space], e);
                _dataAccessLayer.AddOccupiedSpace(space);
                _card = new ParkingSpaceCardGUI(_parkingSpaceButtons[space].Text, _parkingSpaces[space].StartTime);
                _card.Show();
            }
            else
            {
                _getSpace.Text = "PARKING LOT FULL";
                _getSpace.Enabled = false;
                _admin.Enabled = true;
                _leaveSpace.Enabled = true;

            }
        }

        private async void ParkingSpace_Wait(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            while (!_continue)
            {
                await Task.Delay(500);
                button.BackColor = button.BackColor == Color.Yellow ? Color.Green : Color.Yellow;
            }
            _card.Close();
            button.BackColor = Color.Yellow;
            await Task.Delay(1000);
            button.BackColor = Color.Red;
        }

        private void ParkingSpace_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.BackColor != Color.Red)
            {
                button.BackColor = Color.Red;
                _dataAccessLayer.SetSpaceOccupied((int)button.Tag);
                _dataAccessLayer.Save();
            }
            else
            {
                button.BackColor = Color.Green;
                _dataAccessLayer.SetSpaceAvailable((int)button.Tag);
                _dataAccessLayer.Save();
            }
        }
        private void Leave_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            var randomSpace = random.Next(_dataAccessLayer.GetOccupiedSpaces().Count);
            var randomSpaceUID = _dataAccessLayer.GetOccupiedSpaces()[randomSpace];
            _dataAccessLayer.SetSpaceAvailable(randomSpaceUID);
            _parkingSpaceButtons[randomSpaceUID].BackColor = Color.Green;
            _dataAccessLayer.Save();
            _dataAccessLayer.RemoveOccupiedSpace(randomSpaceUID);
            _getSpace.Enabled = true;
            _getSpace.Text = "Press Here to Get Parking";
            if (_dataAccessLayer.GetOccupiedSpaces().Count == 0)
            {
                _leaveSpace.Enabled = false;
            }
        }

        private void ParkingGUI_Load(object sender, EventArgs e)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // ------------------------------------------------------------------------------------------------
        // ADMIN ACCESS
        // ------------------------------------------------------------------------------------------------

        private bool _adminIsLoggedIn { get; set; }
        private List<string> _adminList = new List<string>();

        // TODO : Read/Write to disk
        private void CreateEmployees(int employees)
        {
            string[] arr = { "4762", "2661", "8810", "4430", "5361", "1423", "5044", "8402", "8224", "8401" };
            _adminList = arr.ToList();
            //Random random = new Random();
            //for (int i = 0; i < employees; i++)
            //{
            //    var id = random.Next(1111, 9999).ToString();
            //    _adminList.Add(id);
            //    Console.WriteLine(id);
            //}
        }

        private void Admin_Click(object sender, EventArgs e)
        {
            if (_adminIsLoggedIn) // logout
            {
                GiveAdminAccess(!_adminIsLoggedIn);
                _adminIsLoggedIn = false;
            }
            else // login
            {
                _admin.Enabled = false;
                var admin = new AdminGUI();
                admin.CompareID += Admin_CompareID;
                admin.AdminContinue += Admin_Continue;
                admin.Show();
            }
        }

        private void Admin_CompareID(object sender, IDCheckEventArgs e)
        {
            e.Valid = _adminList.Contains(e.ID);
            GiveAdminAccess(e.Valid);
        }

        private void Admin_Continue(object sender, AdminContinueEventArgs e)
        {
            _admin.Enabled = true;
            _adminIsLoggedIn = e.LoggedIn;
        }

        private void Admin_Clear(object sender, EventArgs e)
        {
            //var OccupiedSpaces = _dataAccessLayer.GetOccupiedSpaces();
            //for (int uid = 0; uid < OccupiedSpaces.Count; uid++)
            //{
            //    var space = OccupiedSpaces[uid];
            //    _parkingSpaceButtons[space].BackColor = Color.Green;
            //    _dataAccessLayer.SetSpaceAvailable(space);
            //    _dataAccessLayer.Save();
            //    OccupiedSpaces.Remove(space);
            //}
            foreach (var btn in _parkingSpaceButtons)
            {
                if (btn.BackColor == Color.Red)
                {
                    btn.BackColor = Color.Green;
                    _dataAccessLayer.SetSpaceAvailable((int)btn.Tag);
                }
            }

            _getSpace.Enabled = true;
            _getSpace.Text = "Press Here to Get Parking";
        }

        public void GiveAdminAccess(bool state)
        {
            if (state)
            {
                Text = "I Dream of Parking (ADMINISTRATOR MODE IN USE)";
                _admin.Text = "Administrator Logout";
                _adminClear.Visible = true;
                _leaveSpace.Visible = false;
                _getSpace.Visible = false;
            }
            else
            {
                Text = "I Dream of Parking";
                _admin.Text = "Administrator Login";
                _adminClear.Visible = false;
                _leaveSpace.Visible = true;
                _getSpace.Visible = true;

            }
            foreach (var button in _parkingSpaceButtons)
            {
                if (state) { button.Cursor = Cursors.Hand; }
                else { button.Cursor = Cursors.Default; }
                button.Enabled = state;
            }
        }

        private void DemoMode_Click(object sender, EventArgs e)
        {
            // Car Wait Queue
            _carQueue = new CarQueue(15, 500, 500, _dataAccessLayer, this);
            Thread CarIn = new Thread(new ThreadStart(_carQueue.Enqueue));
            Thread CarOut = new Thread(new ThreadStart(_carQueue.Dequeue));
            CarIn.Start();
            CarOut.Start();
            CarIn.Join();
            CarOut.Join();
        }
    }
}
