using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ATMsys
{
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\OneDrive\\Documents\\ATMDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            int bal =0;
            if (AccNameTb.Text == "" || AccNumTb.Text == "" || FaNameTb.Text == "" || PhoneTb.Text == "" || AddressTb.Text == "" || OccupationTb.Text == "" || PinTb.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else {
                try { 
                        Con.Open();
                    string query = "insert into AccountTbl values ('" + AccNumTb.Text + "','" + AccNameTb.Text + "','" + FaNameTb.Text + "','" + DobTb.Value.Date + "','" + PhoneTb.Text + "','" + AddressTb.Text + "','" + EducationB.SelectedItem.ToString() + "','" + OccupationTb.Text + "','" + PinTb.Text + "',"+bal+")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Created Successfuly!");
                    Con.Close();
                    Login log = new Login();
                    log.Show();
                    this.Hide();
                }
                catch ( Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.DarkSlateGray;
        }

        private void button_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor= Color.SeaGreen;
        }

        private void label13_MouseHover(object sender, EventArgs e)
        {
            label13.BackColor = Color.Red;
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            label13.BackColor = Color.White;
        }
    }
}
