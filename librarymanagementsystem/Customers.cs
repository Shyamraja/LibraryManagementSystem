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
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Owner\Source\repos\Librarysystem\Databaselibrarym.mdb");
   
        string sql;
        public Customers()
        {
            InitializeComponent();
            load();

        }
        public void load()
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from CustomerTable";
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            timer1.Start();
            con.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                string  CustomerName, Phone, Age,Address, Gender, MaterialName;
               
                CustomerName = this.CustomerName.Text;
                Phone = this.Phone.Text;
                Age = this.Age.Text;
                Address = this.Address.Text;
                Gender = this.Gender.Text;
                MaterialName = this.MaterialName.Text;

                sql = "insert into CustomerTable (CustomerName,Phone,Age,Address,Gender,MaterialName)values(@CustomerName,@Phone,@Age,@Address,@Gender,@MaterialName)";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
             
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Age", Age);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@MaterialName", MaterialName);
               
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Added to the Customer Table");
                con.Close();
                load();
                this.CustomerID.Clear();
                this.CustomerName.Clear();
                this.Phone.Clear();
                this.Age.Clear();
                this.Address.Clear();
                this.MaterialName.Clear();
                
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
          
            CustomerName.Text = "";
            Phone.Text = "";
            Age.Text = "";
            Address.Text = "";
            Gender.Text = "";
            MaterialName.Text = "";
        }
        private void FillFields()
        {
            CustomerID.Text = dataGridView1.SelectedRows[0].Cells["CustomerID"].Value.ToString();
            CustomerName.Text = dataGridView1.SelectedRows[0].Cells["CustomerName"].Value.ToString();
            Phone.Text = dataGridView1.SelectedRows[0].Cells["Phone"].Value.ToString();
            Age.Text = dataGridView1.SelectedRows[0].Cells["Age"].Value.ToString();
            Address.Text = dataGridView1.SelectedRows[0].Cells["Address"].Value.ToString();
            Gender.Text = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();
            MaterialName.Text = dataGridView1.SelectedRows[0].Cells["MaterialName"].Value.ToString();

        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int CustomerID = (int)dataGridView1.SelectedRows[0].Cells["CustomerID"].Value;
            string deleteQuery = "Delete from CustomerTable where CustomerID = @CustomerID";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(deleteQuery, con);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            
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
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            load();
            

        }

        private void EDIT_Click(object sender, EventArgs e)
        {
            

            string CustomerName, Phone, Age, Address ,Gender, MaterialName;
          
            CustomerName = this.CustomerName.Text;
            Phone = this.Phone.Text;
            Age = this.Age.Text;
            Address = this.Address.Text;
            Gender = this.Gender.Text;

            MaterialName = this.MaterialName.Text;
          
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int CustomerID = (int)dataGridView1.SelectedRows[0].Cells["CustomerID"].Value;
            
            sql = "Update CustomerTable set CustomerName = @CustomerName,Phone = @Phone,Age = @Age, Address =@Address,Gender = @Gender,MaterialName = @MaterialName where CustomerID = @CustomerID ";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(sql, con);

            cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Age", Age);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Gender", Gender);          
            cmd.Parameters.AddWithValue("@MaterialName", MaterialName);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Updated from Selected Customer ID!");
            con.Close();
            load();
           
             this.CustomerName.Clear();
             this.Phone.Clear();
             this.Age.Clear();
             this.Address.Clear();
             this.MaterialName.Clear();
   
        }

      
       
        private void CANCEL_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Do you Want to Cancel This Operation?", "Confirmation", MessageBoxButtons.YesNo);
            if (ans == DialogResult.Yes)
            {
                ClearField();
                MessageBox.Show("Cancelled Operation!");
            }
            else
            {
                FillFields();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            this.timer.Text = time.ToString();
        }

     
    }
  
}

    
    
