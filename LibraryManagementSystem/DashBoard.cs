using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
            
        }

        //Log Out
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("You want to Log Out?", "Confirm", MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                
                Login SignIn = new Login();
                SignIn.Show();
                this.Hide();
            }
            

        }
        //Redirect to Add Book Page
        private void addNewBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBook AddBook = new AddBook();
            AddBook.Show();
            this.Hide();
        }
        //Redirect to View book Page
        private void viewBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewBook ViewBook = new ViewBook();
            ViewBook.Show();
            this.Hide();
        }

        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStudent AddStudent = new AddStudent();
            AddStudent.Show();
            this.Hide();
        }

        private void viewStudentInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewStudentInfo ViewStudentInfo = new ViewStudentInfo();
            ViewStudentInfo.Show();
            this.Hide();
        }

        private void issueBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueBook IssueBook = new IssueBook();
            IssueBook.Show();
            this.Hide();
        }

        private void returnBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReturnBook ReturnBook = new ReturnBook();
            ReturnBook.Show();
            this.Hide();
        }

        private void completeBooksDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompleteBookDetails CompleteBookDetails = new CompleteBookDetails();
            CompleteBookDetails.Show();
            this.Hide();
        }
    }
}
