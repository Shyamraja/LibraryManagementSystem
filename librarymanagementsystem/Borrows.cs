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
                ID = txtID.Text;
                MaterialID = textBox2.Text;
                CustomerID = textBox3.Text;
                BookedDate = textBox4.Text;
                DeliveryDate = textBox5.Text;

                sql = "insert into BorrowTable (ID, MaterialID, CustomerID, BookedDate, DeliveryDate)values(@ID,@CustomerID,@MaterialID,@BookedDate,@DeliveryDate)";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@MaterialID", MaterialID);
                cmd.Parameters.AddWithValue("@Booked Date", BookedDate);
                cmd.Parameters.AddWithValue("@Delivery Date", DeliveryDate);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Added");
                con.Close();
                txtID.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
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
            textBox3.Text = "";
            textBox2.Text = "";

        }
        private void FillFields()
        {
            textBox2.Text = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();

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
            MessageBox.Show("Deleted!");
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
             }
    }
}
