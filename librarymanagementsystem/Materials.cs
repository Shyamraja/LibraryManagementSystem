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
            load();
        }
        public void load()
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
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    con.Open();
        //    OleDbCommand cmd = con.CreateCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "select * from MaterialTable";
        //    DataTable dt = new DataTable();
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //    da.Fill(dt);
        //    dataGridView1.DataSource = dt;
        //    con.Close();
        //}
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                string Name, Type, Status, Author, ISBN, PublicationDate;
               
                Name = this.Name.Text;
                Type= this.Type.Text;
                Status = this.Status.Text;
                Author = this.Author.Text;
                ISBN = this.ISBN.Text;
                PublicationDate = this.PublicationDate.Text;


                sql = "insert into MaterialTable (Name,Type,Status,Author,ISBN,PublicationDate)values(@Name,@Type,@Status,@Author,@ISBN,@PublicationDate)";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                
                //cmd.Parameters.AddWithValue("@MaterialID", MaterialID);
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
                this.PublicationDate.Clear();


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

            MaterialID.Text = dataGridView1.SelectedRows[0].Cells["MaterialID"].Value.ToString();
            Name.Text = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
            Type.Text = dataGridView1.SelectedRows[0].Cells["Type"].Value.ToString();
            Status.Text = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
            Author.Text = dataGridView1.SelectedRows[0].Cells["Author"].Value.ToString();
            ISBN.Text = dataGridView1.SelectedRows[0].Cells["ISBN"].Value.ToString();
            PublicationDate.Text = dataGridView1.SelectedRows[0].Cells["PublicationDate"].Value.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string  Name,Type,Status, Author, ISBN, PublicationDate;
 
            Name = this.Name.Text;
            Type = this.Type.Text;
            Status = this.Status.Text;
            Author = this.Author.Text;
            ISBN = this.ISBN.Text;
            PublicationDate = this.PublicationDate.Text;

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
           
            con.Close();
            load();
            this.Name.Clear();
            this.Author.Clear();
            this.ISBN.Clear();
            this.PublicationDate.Clear();

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
        private void materialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materials formMaterial = new Materials();
            formMaterial.Show(this);
            this.Hide();
        }
        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customers formCustomers = new Customers();
            formCustomers.Show(this);
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
    }
}
