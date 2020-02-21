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
                CustomerID = this.CustomerID.Text;
                Name = this.Name.Text;
                Phone = this.Phone.Text;
                Age = this.Age.Text;
                Gender = this.Gender.Text;
                Material = this.Material.Text;

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
                this.CustomerID.Clear();
                this.Name.Clear();
                this.Phone.Clear();
                this.Age.Clear();
                //this.Gender.SelectedItem.Clear();
                this.Material.Clear();
                dataGridView1.Rows.Add(dataGridView1.SelectedRows[0]);
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
            CustomerID.Text = "";
            Name.Text = "";
            Phone.Text = "";
            Age.Text = "";
            Gender.Text = "";
            Material.Text = "";
        }
        private void FillFields()
        {
            CustomerID.Text = dataGridView1.SelectedRows[0].Cells["CustomerID"].Value.ToString();
            Name.Text = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            Phone.Text = dataGridView1.SelectedRows[0].Cells["Phone"].Value.ToString();
            Age.Text = dataGridView1.SelectedRows[0].Cells["Age"].Value.ToString();
            Gender.Text = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();
            Material.Text = dataGridView1.SelectedRows[0].Cells["Material"].Value.ToString();

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

        private void EDIT_Click(object sender, EventArgs e)
        {
            //try
            //{

            string Name, Phone, Age, Gender, Material;
           // CustomerID = this.CustomerID.Text;
            Name = this.Name.Text;
            Phone = this.Phone.Text;
            Age = this.Age.Text;
            Gender = this.Gender.Text;
            Material = this.Material.Text;

            //    sql = "Update CustomerTable set Name = @Name,Phone = @Phone,Age = @Age,Gender = @Gender,Material = @Material where CustomerID = @CustomerID ";
            //    con.Open();
            //    OleDbCommand cmd = new OleDbCommand(sql, con);
            //    cmd.Parameters.AddWithValue("@Name", Name);
            //    cmd.Parameters.AddWithValue("@Phone", Phone);
            //    cmd.Parameters.AddWithValue("@Age", Age);
            //    cmd.Parameters.AddWithValue("@Gender", Gender);
            //    cmd.Parameters.AddWithValue("@Material", Material);
            //    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("Added");
            //    con.Close();
            //    this.CustomerID.Clear();
            //    this.Name.Clear();
            //    this.Phone.Clear();
            //    this.Age.Clear();
            //    this.Gender.Clear();
            //    this.Material.Clear();

            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    if (con.State != ConnectionState.Closed) con.Close();
            //}
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int CustomerID = (int)dataGridView1.SelectedRows[0].Cells["CustomerID"].Value;
            //string updateQuery = "Update from CustomerTable where Customerid = @CustomerID";

            sql = "Update CustomerTable set Name = @Name,Phone = @Phone,Age = @Age,Gender = @Gender,Material = @Material where CustomerID = @CustomerID ";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@Material", Material);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated!");
            con.Close();
            this.CustomerID.Clear();
              this.Name.Clear();
             this.Phone.Clear();
             this.Age.Clear();
             //this.Gender.Clear();
             this.Material.Clear();

   
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

    
    
