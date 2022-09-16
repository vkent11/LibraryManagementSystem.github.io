using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
       
        //Login
            private void button1_Click(object sender, EventArgs e)
        {
            if(txtUser.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return;
            }
            else if(txtPass.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
                SqlCommand cmd = new SqlCommand("Select * from Login where Username=@Username and Password=@Password", con);
                cmd.Parameters.AddWithValue("@Username", txtUser.Text);
                cmd.Parameters.AddWithValue("@Password", txtPass.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Successfully Login your Account !!!");
                    DashBoard Dash = new DashBoard();
                    Dash.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Login please check username and password");
                    txtUser.Text = "";
                    txtPass.Text = "";
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
        }

        //Close Icon
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Redirect to Sign up
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignIn SignIn = new SignIn();
            SignIn.Show();
            this.Hide();
        }

        //Show/Hide Password
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = checkBox1.Checked ? char.MinValue : '*';
        }
    }
}
