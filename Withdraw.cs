using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMsys
{
    public partial class Withdraw : Form
    {
        public Withdraw()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\OneDrive\\Documents\\ATMDb.mdf;Integrated Security=True;Connect Timeout=30");
        string Acc = Login.AccNumber;
        int bal;
        private void getbalance(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(" select Balance from AccountTbl where AccNum = '" + Acc + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Balancelbl.Text = "Balance = Rs. " + dt.Rows[0][0].ToString();
            bal = Convert.ToInt32(dt.Rows[0][0].ToString());
            Con.Close();
        }
        private void addtransaction()
        {
            string Type = "Withdraw";

            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values ('" + Acc + "','" + Type + "'," + Withdrawtb.Text + ",'" + DateTime.Today.Date.ToString() + "')";
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
        private void Withdraw_Load(object sender, EventArgs e)
        {
            getbalance(sender, e);
        }
        int newbalance;
        private void button1_Click(object sender, EventArgs e)
        {
            if(Withdrawtb.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else if (Convert.ToInt32(Withdrawtb.Text) <= 0)
            {
                MessageBox.Show("Enter a Valid Amount!");
            }
            else if (Convert.ToInt32(Withdrawtb.Text) > bal)
            {
                MessageBox.Show("Balance can not be Negative");
            }
            else
            {
                
                    newbalance = bal - Convert.ToInt32(Withdrawtb.Text);
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance =" + newbalance + " where AccNum='" + Acc + "';";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Success Withdrawal!");
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
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }

