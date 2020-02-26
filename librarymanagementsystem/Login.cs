using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms;
//using System.Data;
using System.Data.OleDb;
using System.Configuration;


namespace Librarysystem
{
    public partial class Login : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Owner\Source\repos\Librarysystem\Databaselibrarym.mdb");

        string sql;
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
            //while (reader.Read())
                int count = 0;
            while  (reader.Read())
            {
                count = count + 1;

            }
            //Checking single entry for application only one at a time
            if (count == 1)
            {
                MessageBox.Show("You are successfully Logged in");
            }
            //
            if (count > 1)
            {
                MessageBox.Show("Can't be duplicate Admin");
            }

            con.Close();

            MessageBox.Show("You are logged in Successfully");
                 Librarymdiparent mdiparent = new Librarymdiparent();
                 mdiparent.Show();
                 this.Hide();

            

            //   string username, password;
            //   username = textBox1.Text;
            //   password = textBox2.Text;
            //   if(username=="Admin"&& password=="Admin1290")
            //   {
            //       MessageBox.Show("You are logged in Successfully");
            //       Librarymdiparent mdiparent = new Librarymdiparent();
            //       mdiparent.Show();
            //       this.Hide();
            //   }
            //else
            //   {
            //       MessageBox.Show("Username or Password is incorrect");
            //   }
            //connection.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Application.Exit();
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
