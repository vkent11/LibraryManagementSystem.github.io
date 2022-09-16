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
    public partial class ViewBook : Form
    {
        public ViewBook()
        {
            InitializeComponent();
        }

        private void ViewBook_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
            SqlCommand cmd = new SqlCommand("Select * from Book", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            
        }
        public void BindingDta()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
            SqlCommand cmd = new SqlCommand("Select * from Book", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        //Back Button
        private void BtnBack_Click(object sender, EventArgs e)
        {
            DashBoard Back = new DashBoard();
            Back.Show();
            this.Hide();
        }
        //get the details from specific cell
        int bID;
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                bID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            panel2.Visible = true;
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
            SqlCommand cmd = new SqlCommand("Select * from Book where ID= " + bID + " ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LblID.Text = ds.Tables[0].Rows[0][0].ToString();
            txtName.Text = ds.Tables[0].Rows[0][1].ToString();
            txtAuthor.Text = ds.Tables[0].Rows[0][2].ToString();
            txtPub.Text = ds.Tables[0].Rows[0][3].ToString();
            txtPrice.Text = ds.Tables[0].Rows[0][4].ToString();
            txtQuantity.Text = ds.Tables[0].Rows[0][5].ToString();
        }

        //Clear the textboxes
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtAuthor.Text = "";
            txtPub.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
        }
        //Update
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
                SqlCommand cmd = new SqlCommand("update Book set Name=@Name,Author=@Author,Publication=@Publication,Price=@Price,Quantity=@Quantity,Date=@Date  where ID='" + LblID.Text + "' ", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@Publication", txtPub.Text);
                cmd.Parameters.AddWithValue("@Date", DTPDate.Value.ToString());
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Updated!");

                txtName.Text = "";
                txtAuthor.Text = "";
                txtPub.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";

                BindingDta();
            }
           
        }
        //Delete
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Deleted. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
                SqlCommand cmd = new SqlCommand("DELETE FROM Book WHERE ID = '" + LblID.Text + "' ", con);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Updated!");

                txtName.Text = "";
                txtAuthor.Text = "";
                txtPub.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";

                BindingDta();
            }
           
                
            
        }
        //Search
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
            SqlDataAdapter sda = new SqlDataAdapter("select * from Book where(Name like '%" + txtSearch.Text + "%') or (Author like '%" + txtSearch.Text + "%')", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No Result");
            }
        }
        //Refresh
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
            SqlCommand cmd = new SqlCommand("Select * from Book", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            txtSearch.Text = "";
        }
    }
}
