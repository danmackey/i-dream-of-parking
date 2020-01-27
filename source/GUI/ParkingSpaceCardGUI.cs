using System;
using System.Windows.Forms;
using System.Drawing;

namespace I_Dream_of_Parking
{
    public class ParkingSpaceCardGUI : Form
    {
        private System.ComponentModel.IContainer components = null;

        private Label _spaceInstruction { get; set; }
        private Label _spaceName { get; set; }
        private Label _timeInstrution { get; set; }
        private Label _time { get; set; }

        public ParkingSpaceCardGUI(string space, DateTime time)
        {
            InitializeComponent(space, time);
        }

        private void InitializeComponent(string space, DateTime time)
        {
            // GUI Initialization
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(584, 761);

            // Space Name Instruction
            _spaceInstruction = new Label()
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 45F),
                Location = new Point(83, 40),
                Name = "SpaceNameInstruction",
                Size = new Size(418, 69),
                Text = "Parking Space",
                TextAlign = ContentAlignment.TopCenter
            };
            Controls.Add(_spaceInstruction);

            // Space Name
            _spaceName = new Label()
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 250F),
                Location = new Point(9, 148),
                Name = "SpaceName",
                Size = new Size(567, 378),
                Text = space,
                TextAlign = ContentAlignment.TopCenter
            };
            Controls.Add(_spaceName);

            // Time Instruction
            _timeInstrution = new Label()
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 45F),
                Location = new Point(89, 526),
                Name = "TimeInstruction",
                Size = new Size(389, 69),
                Text = "Time Entered",
                TextAlign = ContentAlignment.TopCenter
            };
            Controls.Add(_timeInstrution);

            // Time Entered
            _time = new Label()
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 35F),
                Location = new Point(52, 620),
                Name = "label4",
                Size = new Size(481, 54),
                Text = time.ToString()
            };
            Controls.Add(_time);


            // GUI Initialization
            Name = "GUI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Parking Space Card";
            Load += new EventHandler(ParkingCard_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        private void ParkingCard_Load(object sender, EventArgs e)
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
    }
}
