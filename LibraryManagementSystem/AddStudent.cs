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
namespace LibraryManagementSystem
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }
        //Back Button
        private void BtnBack_Click(object sender, EventArgs e)
        {
            DashBoard SignIn = new DashBoard();
            SignIn.Show();
            this.Hide();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            //Validate each Textbox
            if (txtName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtStudentID.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtDept.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtContact.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (RdbMale.Checked == false && RdbFemale.Checked == false)
            {
                MessageBox.Show("Please pick something");
                return;
            }
            else
            {
                string connection = @"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa";
                SqlConnection con = new SqlConnection(connection);
                try
                {
                    //save in Users DB Table
                    SqlCommand cmd = new SqlCommand("Insert into Student(Name,StudentID,Dept,Contact,Email,Gender) Values(@Name,@StudentID,@Dept,@Contact,@Email,@Gender)", con);

                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
                    cmd.Parameters.AddWithValue("@Dept", txtDept.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    
                    


                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Book Registered successfully..");
                    Clear();
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
        public void Clear()
        {
            txtName.Text = "";
            txtStudentID.Text = "";
            txtDept.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            RdbMale.Checked = false;
            RdbFemale.Checked = false;
        }
        string Gender = string.Empty;
        private void RdbMale_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Male";
        }

        private void RdbFemale_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Female";
        }
    }
}
