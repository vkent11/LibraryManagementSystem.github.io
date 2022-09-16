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
    public partial class CompleteBookDetails : Form
    {
        public CompleteBookDetails()
        {
            InitializeComponent();
        }
        //Load/display the database details in data grid view
        private void CompleteBookDetails_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-DQT5U75\\SQLEXPRESS;Initial Catalog=LibrarySystem;User ID=virnil;Password=sa");
            SqlCommand cmd = new SqlCommand("Select * from IssueBooks where ReturnDate is null", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

           
            SqlCommand cmd1 = new SqlCommand("Select * from IssueBooks where ReturnDate is not null", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
            dataGridView2.DataSource = ds1.Tables[0];
        }
        //Back to Dashboard
        private void BtnBack_Click(object sender, EventArgs e)
        {
            DashBoard DashBoard = new DashBoard();
            DashBoard.Show();
            this.Hide();
        }
    }
}
