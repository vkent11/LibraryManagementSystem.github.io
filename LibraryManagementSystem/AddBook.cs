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
    public partial class AddBook : Form
    {
        public AddBook()
        {
            InitializeComponent();
           
        }
        
        //Back to Dasboard
        private void BtnBack_Click(object sender, EventArgs e)
        {
            DashBoard SignIn = new DashBoard();
            SignIn.Show();
            this.Hide();
        }

        //Save/register the book
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //Validate each Textbox
            if (txtBookname.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtAuthor.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtPublication.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtPrice.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter something in the textbox");
                return; // return because we don't want to run normal code of buton click
            }
            else if (txtQuantity.Text.Trim() == string.Empty)
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
                    SqlCommand cmd = new SqlCommand("Insert into Book(Name,Author,Publication,Price,Quantity,Date,BTime) Values(@Name,@Author,@Publication,@Price,@Quantity,@Date,@BTime)", con);

                    cmd.Parameters.AddWithValue("@Name", txtBookname.Text);
                    cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                    cmd.Parameters.AddWithValue("@Publication", txtPublication.Text);
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                    cmd.Parameters.AddWithValue("@BTime", LblTime.Text);
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = dateTimePicker1.Value.ToString();
                    //cmd.Parameters.AddWithValue("@Date", DTPDate.Value.ToString("hh:mm tt"));


                    con.Open();
                    cmd.ExecuteNonQuery();
                   
                    MessageBox.Show("Book Registered successfully..");
                    
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

        //Clear Textboxes
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtBookname.Text = "";
            txtAuthor.Text = "";
            txtPublication.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            
        }

        private void AddBook_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblTime.Text = DateTime.Now.ToShortTimeString();
        }
    }
}
