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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        //Close button
        private void SignIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Register
        private void BtnRegister_Click(object sender, EventArgs e)
        {
           //Validate each Textbox
            if (txtFirstname.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtLastname.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtAge.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtGender.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtHome.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtUsername.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else
            {
                string connection = @"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa";
                SqlConnection con = new SqlConnection(connection);
                try
                {
                    //save in Users DB Table
                    SqlCommand cmd = new SqlCommand("Insert into UsersDetails(Firstname,Lastname,Age,Gender,Home,Email,Username,Password) Values(@Firstname,@Lastname,@Age,@Gender,@Home,@Email,@Username,@Password)", con);

                    cmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("@Lastname", txtLastname.Text);
                    cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                    cmd.Parameters.AddWithValue("@Home", txtHome.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    //save in Login DB Table
                    SqlCommand cmd2 = new SqlCommand("Insert into Login(Firstname,Lastname,Username,Password) Values(@Firstname,@Lastname,@Username,@Password)", con);
                    cmd2.Parameters.AddWithValue("@Firstname", txtFirstname.Text);
                    cmd2.Parameters.AddWithValue("@Lastname", txtLastname.Text);
                    cmd2.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd2.Parameters.AddWithValue("@Password", txtPassword.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Account created successfully..");
                    ClearData();
                }
                catch
                {
                    MessageBox.Show("Error occured...");
                }
                finally
                {
                    con.Close();
                }

            }
        
        }
           
        //Clear data after submit
        private void ClearData()
        {
            txtFirstname.Text = "";
            txtLastname.Text = "";
            txtAge.Text = "";
            txtGender.Text = "";
            txtHome.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
        //Button Back
        private void BtnBack_Click(object sender, EventArgs e)
        {

            Login SignIn = new Login();
            SignIn.Show();
            this.Hide();
        }

       
    }
}
