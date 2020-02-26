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
    public partial class Login : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Owner\Source\repos\Librarysystem\Databaselibrarym.mdb");
    
        public Login()
        {
            InitializeComponent();
        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            string username, password;
             username = textBox1.Text;
             password = textBox2.Text;
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Admin where username = '"+textBox1.Text+"' and password = '" + textBox2.Text+"'";
            OleDbDataReader reader = cmd.ExecuteReader();
          
                int count = 0;
            while  (reader.Read())
            {
                count = count + 1;

            }
            //Checking If the Admins username and password is unique or not  in the database 
            if (count == 1)
            {
                MessageBox.Show("You are successfully Logged in");
                Librarymdiparent mdiparent = new Librarymdiparent();
                mdiparent.Show();
                this.Hide();
            }
            //If username and password is more than 1 then 
            if (count > 1)
            {
                MessageBox.Show("Can't be duplicate Admin");              
            }                 
            else
            {
                MessageBox.Show("Username and password is incorrect");
            }
            con.Close();                 
        }
        private void button2_Click(object sender, EventArgs e)
        {           
            DialogResult conform = MessageBox.Show("Do you Want to Exit This Application?", "Confirmation", MessageBoxButtons.YesNo);
            if (conform == DialogResult.Yes)
            {
                Application.Exit();
                            
            }
            else
            {
                MessageBox.Show("Please input your Username And Password to Login");
                Login formLogin = new Login();
                formLogin.Show(this);
                this.Hide();
            }
        }      
    }
}
