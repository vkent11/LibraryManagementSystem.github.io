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
    public partial class IssueBook : Form
    {
        public IssueBook()
        {
            InitializeComponent();
        }


        private void IssueBook_Load(object sender, EventArgs e)
        {
            //Load the time
            timer1.Start();
            //For Combo Box/Dropdown
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Name from Book", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    comboBox1.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
            con.Close();
        }
        //Search the Student I.D
        int count;
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //check if the student id exist
            if (txtSearch.Text == null)
            {
                MessageBox.Show("Please enter something in the Search Box");
                return;
            }
            else
            {
                string sID = txtSearch.Text;
                string connection = @"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa";
                SqlConnection con = new SqlConnection(connection);
                try
                {
                    SqlCommand cmd = new SqlCommand("Select * from Student where StudentID= '" + sID + "' ", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    //Check adn count if how many book has already issued in Student
                    SqlCommand cmd1 = new SqlCommand("Select count (StudentID) from IssueBooks where StudentID= '" + sID + "' and ReturnDate is null ", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    sda1.Fill(ds1);
                    count = int.Parse(ds1.Tables[0].Rows[0][0].ToString());

                    con.Open();
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        txtSName.Text = ds.Tables[0].Rows[0][1].ToString();
                        txtStudentID.Text = ds.Tables[0].Rows[0][2].ToString();
                        txtDept.Text = ds.Tables[0].Rows[0][3].ToString();
                        txtContact.Text = ds.Tables[0].Rows[0][4].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0][5].ToString();
                        
                    }
                    else
                    {
                        txtSearch.Text = "";
                        txtSName.Text = "";
                        txtStudentID.Text = "";
                        txtDept.Text = "";
                        txtContact.Text = "";
                        txtEmail.Text = "";
                        MessageBox.Show("Student I.D doesn't exist");
                    }
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
        //Button Back
        private void BtnBack_Click(object sender, EventArgs e)
        {
            DashBoard SignIn = new DashBoard();
            SignIn.Show();
            this.Hide();
        }
        //Clear all textbox
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtSName.Text = "";
            txtStudentID.Text = "";
            txtDept.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            comboBox1.SelectedIndex = -1;
            
        }
        //insert the data
        private void BtnIssue_Click(object sender, EventArgs e)
        {
           //check if student is existing in the database
           string sID = txtSearch.Text;
           string connections = @"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa";
           SqlConnection con1 = new SqlConnection(connections);
           SqlCommand cmd1 = new SqlCommand("Select * from IssueBooks where StudentID= '" + sID + "' ", con1);
           SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
           DataTable ds1 = new DataTable();
           sda1.Fill(ds1);
           if (ds1.Rows.Count > 0 && MessageBox.Show("Data will be Updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
           {
                if (comboBox1.SelectedIndex != -1 && count <= 2 )
                {
                    string connection = @"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa";
                    SqlConnection con = new SqlConnection(connection);
                    try
                    {
                        //save in Users DB Table
                        SqlCommand cmd = new SqlCommand("Insert into IssueBooks(StudentID,StudentName,Department,Contact,Email,BookName,IssueDate,IssueTime) Values(@StudentID,@StudentName,@Department,@Contact,@Email,@BookName,@IssueDate,@IssueTime)", con);

                        cmd.Parameters.AddWithValue("@StudentName", txtSName.Text);
                        cmd.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
                        cmd.Parameters.AddWithValue("@Department", txtDept.Text);
                        cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@BookName", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@IssueDate", SqlDbType.DateTime).Value = DtpDate.Value.ToString();
                        cmd.Parameters.AddWithValue("@IssueTime", LblTime.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Book Issued successfully..");
                        
                    }
                    catch
                    {
                        MessageBox.Show("Error occured...");
                        txtSearch.Text = "";
                        txtSName.Text = "";
                        txtStudentID.Text = "";
                        txtDept.Text = "";
                        txtContact.Text = "";
                        txtEmail.Text = "";
                        comboBox1.SelectedIndex = -1;
                    }
                    finally
                    {
                        con.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Maximum Book has been Issued...");
                    txtSearch.Text = "";
                    txtSName.Text = "";
                    txtStudentID.Text = "";
                    txtDept.Text = "";
                    txtContact.Text = "";
                    txtEmail.Text = "";
                    comboBox1.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show("Error occured...");
                return;
            }
                    
               
        }
    //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblTime.Text = DateTime.Now.ToShortTimeString();
        }
    }
}

