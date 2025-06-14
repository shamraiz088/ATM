using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMsys
{
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\OneDrive\\Documents\\ATMDb.mdf;Integrated Security=True;Connect Timeout=30");
        string Acc = Login.AccNumber;
        private void addtransaction()
        {
            string Type = "Deposit";

            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values ('" + Acc + "','" + Type + "'," + Amounttb.Text + ",'" + DateTime.Today.Date.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Account Created Successfuly!");
                Con.Close();
                Login log = new Login();
                log.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            if (Amounttb.Text == "" || Convert.ToInt32(Amounttb.Text) <= 0)
            {
                MessageBox.Show("Enter The Amount To Deposit!");
            }
            else
            {

                newbalance = oldbalance + Convert.ToInt32(Amounttb.Text);
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set Balance =" + newbalance + " where AccNum='" + Acc + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                   cmd.ExecuteNonQuery();
                     MessageBox.Show("Success Deposit");
                    Con.Close();
                    addtransaction(); 
                    Home home = new Home();
                    home.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
        int oldbalance, newbalance;
        private void getbalance(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(" select Balance from AccountTbl where AccNum = '" + Acc + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            oldbalance = Convert.ToInt32(dt.Rows[0][0].ToString());
            Con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Deposit_Load(object sender, EventArgs e)
        {
            getbalance(null, null);
        }
    }
}

