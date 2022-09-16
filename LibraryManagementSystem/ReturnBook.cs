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
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from IssueBooks where StudentID= '" + txtSearch.Text + "' ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            if (ds.Tables[0].Rows.Count !=0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Invalid Student I.D","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        Int64 bID;
        string bname;
        string sdate;
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                bID = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bname = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                sdate = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                
                
            }
            else
            {
                MessageBox.Show("Error Occured...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lblID.Text = bID.ToString();
            txtBName.Text = bname;
            txtIDate.Text = sdate;
            
        }
        //Update/Return the book
        private void BtnReturnBook_Click(object sender, EventArgs e)
        {
            //check if student ID is existing in the database
            string sID = txtSearch.Text;
            string connections = @"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa";
            SqlConnection con1 = new SqlConnection(connections);
            SqlCommand cmd1 = new SqlCommand("Select * from IssueBooks where StudentID= '" + sID + "' ", con1);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            sda1.Fill(ds1);
            if (ds1.Rows.Count > 0 && MessageBox.Show("Data will be Updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
               
               SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-DQT5U75\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
               con.Open();
               SqlCommand cmd = new SqlCommand("Update IssueBooks set ReturnDate=@ReturnDate,ReturnTime=@ReturnTime where ID= '" + lblID.Text + "' ", con);
               cmd.Parameters.AddWithValue("@ReturnDate", SqlDbType.DateTime).Value = DtpReturnDate.Value.ToString();
               cmd.Parameters.AddWithValue("@ReturnTime", lblTime.Text);
               cmd.ExecuteNonQuery();
               con.Close();
               MessageBox.Show("Updated Successfully...");
                

            }
            else
            { 
                MessageBox.Show("Invalid Student I.D...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }

        private void ReturnBook_Load(object sender, EventArgs e)
        {
            //Load the time
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToShortTimeString();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            DashBoard Back = new DashBoard();
            Back.Show();
            this.Hide();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtBName.Text = "";
            txtIDate.Text = "";

            dataGridView1.Columns.Clear();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtBName.Text = "";
            txtIDate.Text = "";

            dataGridView1.Columns.Clear();
        }
    }
}
