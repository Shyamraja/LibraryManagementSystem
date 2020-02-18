using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace Librarysystem
{
    public partial class Customers : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Owner\Desktop\Library\Databaselibrarym.mdb");
        OleDbCommand cmd;
        OleDbDataReader dr;
        string sql;
        public Customers()
        {
            InitializeComponent();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                string CustomerID, Name, Phone, Age, Gender, Material;
                CustomerID = textBox1.Text;
                Name = textBox2.Text;
                Phone = textBox3.Text;
                Age = textBox4.Text;
                Gender = textBox5.Text;
                Material = textBox6.Text;

                sql = "insert into CustomerTable (CustomerID,Name,Phone,Age,Gender,Material)values(@CustomerID,@Name,@Phone,@Age,@Gender,@Material)";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Age", Age);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Material", Material);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added");
                con.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

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
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
        private void FillFields()
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells["CustomerID"].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells["Phone"].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells["Age"].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells["Material"].Value.ToString();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from CustomerTable";
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int Customerid = (int)dataGridView1.SelectedRows[0].Cells["CustomerID"].Value;
            string deleteQuery = "Delete from CustomerTable where Customerid = @CustomerID";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(deleteQuery, con);
            cmd.Parameters.AddWithValue("@CustomerID", Customerid);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted!");
            con.Close();
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
        }
    }
}
