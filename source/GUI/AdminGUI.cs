using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace I_Dream_of_Parking
{
    class AdminGUI : Form
    {
        private System.ComponentModel.IContainer components = null;

        public event EventHandler<IDCheckEventArgs> CompareID;
        public event EventHandler<AdminContinueEventArgs> AdminContinue;

        private AdminContinueEventArgs _adminContinue = new AdminContinueEventArgs();

        private Button[] _numPadButtons { get; set; }
        private TextBox _adminID { get; set; }
        private Label _instructions { get; set; }
        private string _localID { get; set; } = "";
        private List<Button> _initButtonList = new List<Button>();

        public AdminGUI()
        {
            CreateNumbers();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // GUI Initialization
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(584, 761);

            // NumPad Buttons
            _numPadButtons = new Button[_initButtonList.Count];
            foreach (var button in _initButtonList)
            {
                button.Click += new EventHandler(NumPad_Click);
                Controls.Add(button);
            }
            Controls.CopyTo(_numPadButtons, 0);

            // Admin ID TextBox
            _adminID = new TextBox()
            {
                Anchor = (AnchorStyles.Left | AnchorStyles.Right),
                Font = new Font("Microsoft Sans Serif", 60F, FontStyle.Regular, GraphicsUnit.Point, 0),
                Location = new Point(42, 119),
                MaxLength = 4,
                Name = "textBox1",
                ReadOnly = true,
                Size = new Size(500, 98),
                TabStop = false,
                TextAlign = HorizontalAlignment.Center
            };
            Controls.Add(_adminID);

            // Instructions Label
            _instructions = new Label()
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 30F),
                Location = new Point(48, 40),
                Name = "employeeID",
                Size = new Size(487, 46),
                Text = "Please Enter Employee ID",
                TextAlign = ContentAlignment.TopCenter
            };
            Controls.Add(_instructions);

            // GUI Initialization
            Name = "GUI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Access Panel";
            Load += new EventHandler(Admin_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        private void CreateNumbers()
        {
            int x = 42, y = 247;
            List<string> keypad = new List<string>() { "7", "8", "9", "4", "5", "6", "1", "2", "3", "CLEAR", "0", "ENTER" };
            foreach (var key in keypad)
            {
                var button = new Button()
                {
                    Font = new Font("Microsoft Sans Serif", 60F),
                    Location = new Point(x, y),
                    Margin = new Padding(0),
                    Name = key,
                    Size = new Size(150, 100),
                    TabStop = false,
                    Text = key
                };
                if (key == "CLEAR" || key == "ENTER") // CLEAR and ENTER buttons
                {
                    button.Font = new Font("Microsoft Sans Serif", 20F);
                }
                x += 175;
                if ((keypad.IndexOf(key) + 1) % 3 == 0) // END OF ROW
                {
                    x = 42;
                    y += 125;
                }
                _initButtonList.Add(button);
            }
        }

        private void NumPad_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (_localID.Length < _adminID.MaxLength && button.Text != "CLEAR" && button.Text != "ENTER")
            {
                _localID += button.Text;
                _adminID.Text = _localID;
                return;
            }
            if (button.Text == "CLEAR")
            {
                _localID = "";
                _adminID.Text = _localID;
                return;
            }
            if (button.Text == "ENTER")
            {
                var idCheck = new IDCheckEventArgs() { ID = _localID };
                CompareID?.Invoke(null, idCheck);
                if (idCheck.Valid)
                {
                    _adminContinue.LoggedIn = true;
                    Close();
                }
                else
                {
                    _localID = "";
                    _adminID.Text = _localID;
                }
                return;
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        protected override void Dispose(bool disposing)
        {
            AdminContinue?.Invoke(null, _adminContinue);

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
