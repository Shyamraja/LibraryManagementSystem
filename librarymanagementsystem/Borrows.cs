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
        }

        private void button1_Click(object sender, EventArgs e)
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
                string ID, MaterialID, CustomerID, BookedDate, DeliveryDate;
                ID = this.ID.Text;
                MaterialID = this.MaterialID.Text;
                CustomerID = this.CustomerID.Text;
                BookedDate = this.BookedDate.Text;
                DeliveryDate = this.DeliveryDate.Text;

                sql = "insert into BorrowTable (ID, MaterialID, CustomerID, BookedDate, DeliveryDate)values(@ID,@MaterialID,@CustomerID,@BookedDate,@DeliveryDate)";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MaterialID", MaterialID);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@BookedDate", BookedDate);
                cmd.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Added");
                con.Close();
                this.ID.Clear();
                this.MaterialID.Clear();
                this.CustomerID.Clear();
                this.BookedDate.Clear();
                this.DeliveryDate.Clear();
                //dataGridView1.Rows.Add(dataGridView1.SelectedRows[0]);
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
            ID.Text = "";
            MaterialID.Text = "";
            CustomerID.Text = "";
            BookedDate.Text = "";
            DeliveryDate.Text = "";

        }
        private void FillFields()
        {
            ID.Text = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            MaterialID.Text = dataGridView1.SelectedRows[0].Cells["MaterialID"].Value.ToString();
            CustomerID.Text = dataGridView1.SelectedRows[0].Cells["CustomerID"].Value.ToString();
            BookedDate.Text = dataGridView1.SelectedRows[0].Cells["BookedDate"].Value.ToString();
            DeliveryDate.Text = dataGridView1.SelectedRows[0].Cells["DeliveryDate"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int id =(int) dataGridView1.SelectedRows[0].Cells["ID"].Value;
            string deleteQuery = "Delete from BorrowTable where id = @ID" ;
            con.Open();
            OleDbCommand cmd = new OleDbCommand(deleteQuery, con);
             cmd.Parameters.AddWithValue("@ID", id);    
            cmd.ExecuteNonQuery();
            MessageBox.Show("Selected Borrow data row is Deleted!");
            con.Close();
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
             }

        private void button3_Click(object sender, EventArgs e)
        {
            string MaterialID, CustomerID, BookedDate, DeliveryDate;

            //ID = this.ID.Text;
            MaterialID = this.MaterialID.Text;
            CustomerID = this.CustomerID.Text;
            BookedDate = this.BookedDate.Text;
            DeliveryDate = this.DeliveryDate.Text;


            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            
            sql = "Update BorrowTable set  MaterialID = @MaterialID,CustomerID = @CustomerID,BookedDate = @BookedDate,DeliveryDate = @DeliveryDate where ID = @ID ";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(sql, con);
           
           
            cmd.Parameters.AddWithValue("@MaterialID", MaterialID);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            cmd.Parameters.AddWithValue("@BookedDate", BookedDate);
            cmd.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
            cmd.Parameters.AddWithValue("@ID", ID);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated!");
            con.Close();
            
            this.MaterialID.Clear();
            this.CustomerID.Clear();
            this.CustomerID.Clear();
            this.BookedDate.Clear();
            this.DeliveryDate.Clear();





        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customers formCustomers = new Customers();
            formCustomers.Show(this);
        }

        private void materialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materials formMaterial = new Materials();
            formMaterial.Show(this);

        }

        private void borrowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borrows formBorrow = new Borrows();
            formBorrow.Show(this);
        }
    }

}
