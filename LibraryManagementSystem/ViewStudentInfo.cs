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
    public partial class ViewStudentInfo : Form
    {
        //Connection String  
        string cs = @"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa";
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        public ViewStudentInfo()
        {
            InitializeComponent();
        }

        //Load the data and display in gridview
        private void ViewStudentInfo_Load(object sender, EventArgs e)
        {
            BindingDta();
        }
        //Binding Data
        public void BindingDta()
        {
            con = new SqlConnection(cs);
            con.Open();
            adapt = new SqlDataAdapter("select * from Student", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        //Search from database in display in data gridview
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(cs);
            con.Open();
            adapt = new SqlDataAdapter("select * from Student where (StudentID like '%" + txtSearch.Text + "%') or (Name like '%" + txtSearch.Text + "%') or (Email like '%" + txtSearch.Text + "%') ", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        //get ther data from specific cell
        int bID;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                bID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
            SqlCommand cmd = new SqlCommand("Select * from Student where ID= " + bID + " ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LblID.Text = ds.Tables[0].Rows[0][0].ToString();
            txtName.Text = ds.Tables[0].Rows[0][1].ToString();
            txtDept.Text = ds.Tables[0].Rows[0][3].ToString();
            txtContact.Text = ds.Tables[0].Rows[0][4].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0][5].ToString();
            txtGender.Text = ds.Tables[0].Rows[0][6].ToString();
        }
        //Update Stundent Data
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
                SqlCommand cmd = new SqlCommand("update Student set Name=@Name,Dept=@Dept,Contact=@Contact,Email=@Email,Gender=@Gender  where ID='" + LblID.Text + "' ", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Dept", txtDept.Text);
                cmd.Parameters.AddWithValue("@Contact", txtContact.Text);          
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Updated!");

                txtName.Text = "";
                txtDept.Text = "";
                txtContact.Text = "";
                txtEmail.Text = "";
                txtGender.Text = "";

                BindingDta();
            }
        }
        //Delete the Student Data
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Deleted. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
                SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE ID = '" + LblID.Text + "' ", con);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Updated!");

                txtName.Text = "";
                txtDept.Text = "";
                txtContact.Text = "";
                txtEmail.Text = "";
                txtGender.Text = "";

                BindingDta();
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            DashBoard Back = new DashBoard();
            Back.Show();
            this.Hide();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtDept.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            txtGender.Text = "";
            LblID.Text = "";
        }
    }
}
