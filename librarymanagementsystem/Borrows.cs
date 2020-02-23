using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace Librarysystem
{
    public partial class Borrows : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Owner\Desktop\Library\Databaselibrarym.mdb");
        OleDbCommand cmd;
        OleDbDataReader dr;
        string sql;
        public Borrows()
        {
            InitializeComponent();
            load();
        }
        public void load()
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from BorrowTable";
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string  MaterialID, CustomerID;
                //BorrowID = this.BorrowID.Text;
                MaterialID = this.MaterialID.Text;
                CustomerID = this.CustomerID.Text;
                //BookedDate = this.BookedDate.Text;
                //ReturnedDate = this.ReturnedDate.Text;

                sql = "insert into BorrowTable ( MaterialID, CustomerID,BookedDate, ReturnedDate)values(@MaterialID,@CustomerID,@BookedDate,@ReturnedDate)";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                //cmd.Parameters.AddWithValue("@BorrowID", BorrowID);
                cmd.Parameters.AddWithValue("@MaterialID", MaterialID);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@BookedDate", BookedDate);
                cmd.Parameters.AddWithValue("@ReturnedDate", ReturnedDate);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Added");
                con.Close();
                load();
                //this.BorrowID.Clear();
                this.MaterialID.Clear();
                this.CustomerID.Clear();
                //this.BookedDate.Clear();
                //this.ReturnedDate.Clear();
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State != ConnectionState.Closed) con.Close();
            }
        }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                ClearField();
            else
            {
                FillFields();
            }
        }

        private void ClearField()
        {
            //BorrowID.Text = "";
            MaterialID.Text = "";
            CustomerID.Text = "";
            BookedDate.Text = "";
            ReturnedDate.Text = "";

        }
        private void FillFields()
        {
            //BorrowID.Text = dataGridView1.SelectedRows[0].Cells["BorrowID"].Value.ToString();
            MaterialID.Text = dataGridView1.SelectedRows[0].Cells["MaterialID"].Value.ToString();
            CustomerID.Text = dataGridView1.SelectedRows[0].Cells["CustomerID"].Value.ToString();
            BookedDate.Text = dataGridView1.SelectedRows[0].Cells["BookedDate"].Value.ToString();
            ReturnedDate.Text = dataGridView1.SelectedRows[0].Cells["ReturnedDate"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int BorrowID = (int) dataGridView1.SelectedRows[0].Cells["BorrowID"].Value;
            string deleteQuery = "Delete from BorrowTable where BorrowID = @BorrowID";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(deleteQuery, con);
             cmd.Parameters.AddWithValue("@BorrowID", BorrowID);    
            cmd.ExecuteNonQuery();
            DialogResult ans = MessageBox.Show("Do you Want to Delete This Row?", "Confirmation", MessageBoxButtons.YesNo);
            if (ans == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Selected data row is Deleted!");
            }
            else
            {
                MessageBox.Show("Selected Data Row not deleted!");
            }
            con.Close();
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string MaterialID, CustomerID;

            MaterialID = this.MaterialID.Text;
            CustomerID = this.CustomerID.Text;
            //BookedDate = this.BookedDate.Text;
            //ReturnedDate = this.ReturnedDate.Text;


            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int BorrowID = (int)dataGridView1.SelectedRows[0].Cells["BorrowID"].Value;
            
            sql = "Update BorrowTable set  MaterialID = @MaterialID,CustomerID = @CustomerID,BookedDate = @BookedDate,ReturnedDate = @ReturnedDate where BorrowID = @BorrowID ";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(sql, con);
           
           
            cmd.Parameters.AddWithValue("@MaterialID", MaterialID);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            cmd.Parameters.AddWithValue("@BookedDate", BookedDate);
            cmd.Parameters.AddWithValue("@ReturnedDate", ReturnedDate);
            cmd.Parameters.AddWithValue("@BorrowID", BorrowID);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Data values from Selected ID is Updated!");
            con.Close();
            load();
            this.MaterialID.Clear();
            this.CustomerID.Clear();
            this.CustomerID.Clear();
            //this.BookedDate.Clear();
            //this.ReturnedDate.Clear();
        }
        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customers formCustomers = new Customers();
            formCustomers.Show(this);
            this.Hide();
        }
        private void materialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materials formMaterial = new Materials();
            formMaterial.Show(this);
            this.Hide();
        }
        private void borrowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borrows formBorrow = new Borrows();
            formBorrow.Show(this);
            this.Hide();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You are successfully Logout");
            Login formLogin = new Login();
            formLogin.Show(this);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Do you Want to Cancel This Window?", "Confirmation", MessageBoxButtons.YesNo);
            if (ans == DialogResult.Yes)
            {
                this.Hide();
                MessageBox.Show("Cancelled!");
            }
            else
            {
                Borrows formBorrow = new Borrows();
                formBorrow.Show(this);
                this.Hide();
            }
        }
    }

}
