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
    public partial class Materials : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Owner\Desktop\Library\Databaselibrarym.mdb");
        OleDbCommand cmd;
        OleDbDataReader dr;
        string sql;

        public Materials()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from MaterialTable";
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

                string ID, Name, Type, Status;
                ID = this.ID.Text;
                Name = this.Name.Text;
                Type= this.Type.Text;
                Status = this.Status.Text;

                sql = "insert into MaterialTable (ID,Name,Type,Status)values(@ID,@Name,@Type,@Status)";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.ExecuteNonQuery();
               
                MessageBox.Show("Value Added to Data Table");
                con.Close();
                this.ID.Clear();
                this.Name.Clear();
                this.Type.Clear();
                this.Status.Clear();              

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
            Name.Text = "";
            Type.Text = "";
            Status.Text = "";
        }
        private void FillFields()
        {
           
            ID.Text = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            Name.Text = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            Type.Text = dataGridView1.SelectedRows[0].Cells["Type"].Value.ToString();
            Status.Text = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string  Name, Type, Status;
 
            Name = this.Name.Text;
            Type = this.Type.Text;
            Status = this.Status.Text;

            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;

            sql = "Update MaterialTable set Name = @Name,Type = @Type,Status = @Status where ID = @ID ";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(sql, con);

            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@ID", ID);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Select Material ID is Updated!");
            con.Close();
   
            this.Name.Clear();
            this.Type.Clear();
            this.Status.Clear();        
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            string deleteQuery = "Delete from MaterialTable where ID = @ID";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(deleteQuery, con);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Selected Material data is Deleted!");
            con.Close();
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
        }
    }
}
