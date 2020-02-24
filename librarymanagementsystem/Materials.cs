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
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Owner\Source\repos\Librarysystem\Databaselibrarym.mdb");
        OleDbCommand cmd;
        OleDbDataReader dr;
        string sql;

        public Materials()
        {
            InitializeComponent();
            load();
        }
        public void load()
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from MaterialTable";
            DataTable dt = new DataTable();
            //creating oledb data adapter using cmd
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            timer1.Start();
            con.Close();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                string Name, Type, Status, Author, ISBN;
               
                Name = this.Name.Text;
                Type= this.Type.Text;
                Status = this.Status.Text;
                Author = this.Author.Text;
                ISBN = this.ISBN.Text;
               


                sql = "insert into MaterialTable (Name,Type,Status,Author,ISBN,PublicationDate)values(@Name,@Type,@Status,@Author,@ISBN,@PublicationDate)";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                
               
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Author", Author);
                cmd.Parameters.AddWithValue("@ISBN", ISBN);
                cmd.Parameters.AddWithValue("@PublicationDate", PublicationDate);
                            
                cmd.ExecuteNonQuery();                
                MessageBox.Show("Values are Added to the Data Table");
                con.Close();
            
                load();
                this.MaterialID.Clear();
                this.Name.Clear();
                this.Author.Clear();
                this.ISBN.Clear();

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
            MaterialID.Text = "";
            Name.Text = "";
            Status.Text = "";
            Author.Text = "";
            ISBN.Text = "";
            PublicationDate.Text = "";

        }
        private void FillFields()
        {

            
            Name.Text = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            Type.Text = dataGridView1.SelectedRows[0].Cells["Type"].Value.ToString();
            Status.Text = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
            Author.Text = dataGridView1.SelectedRows[0].Cells["Author"].Value.ToString();
            ISBN.Text = dataGridView1.SelectedRows[0].Cells["ISBN"].Value.ToString();
            PublicationDate.Text = dataGridView1.SelectedRows[0].Cells["PublicationDate"].Value.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string  Name,Type,Status, Author, ISBN;
 
            Name = this.Name.Text;
            Type = this.Type.Text;
            Status = this.Status.Text;
            Author = this.Author.Text;
            ISBN = this.ISBN.Text;
           

            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int MaterialID = (int)dataGridView1.SelectedRows[0].Cells["MaterialID"].Value;

            sql = "Update MaterialTable set Name = @Name, Type = @Type, Status =@Status, Author =@Author, ISBN = @ISBN, PublicationDate = @PublicationDate  where MaterialID = @MaterialID ";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(sql, con);

            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Author", Author);
            cmd.Parameters.AddWithValue("@ISBN", ISBN);
            cmd.Parameters.AddWithValue("@PublicationDate", PublicationDate);
            cmd.Parameters.AddWithValue("@MaterialID", MaterialID);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Data values from Selected ID is Updated!");
            con.Close();
            load();
            this.Name.Clear();
            this.Author.Clear();
            this.ISBN.Clear();
         

        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            int MaterialID = (int)dataGridView1.SelectedRows[0].Cells["MaterialID"].Value;
            string deleteQuery = "Delete from MaterialTable where MaterialID = @MaterialID";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(deleteQuery, con);
            cmd.Parameters.AddWithValue("@MaterialID", MaterialID);
            DialogResult ans = MessageBox.Show("Want to Delete This Row?", "Confirmation", MessageBoxButtons.YesNo);
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
       

        private void button1_Click(object sender, EventArgs e)
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
            {
                DateTime time = DateTime.Now;
                this.timer.Text = time.ToString();
            }

        }
    }
}
