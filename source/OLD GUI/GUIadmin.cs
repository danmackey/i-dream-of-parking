using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I_Dream_of_Parking
{
    public partial class GUIadmin : Form
    {
        static string pswd = "1234";
        static int pswdAttempts = 0;
        public GUIadmin()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "1";
                pswdAttempts += 1;

            }
            else
            {
                textBox3.Text = textBox3.Text + "1";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "2";
            }
            else
            {
                textBox3.Text = textBox3.Text + "2";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "3";
            }
            else
            {
                textBox3.Text = textBox3.Text + "3";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "4";
            }
            else
            {
                textBox3.Text = textBox3.Text + "4";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "5";
            }
            else
            {
                textBox3.Text = textBox3.Text + "5";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "6";
            }
            else
            {
                textBox3.Text = textBox3.Text + "6";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "7";
            }
            else
            {
                textBox3.Text = textBox3.Text + "7";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "8";
            }
            else
            {
                textBox3.Text = textBox3.Text + "8";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "9";
            }
            else
            {
                textBox3.Text = textBox3.Text + "9";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4 && textBox3.Text != null)
            {
                textBox3.Text = "0";
            } 
            else
            {
                textBox3.Text = textBox3.Text + "0";
            }
            if (textBox3.Text == pswd)
            {
                this.Close();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
