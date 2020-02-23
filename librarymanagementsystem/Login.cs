using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Librarysystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            string username, password;
            username = textBox1.Text;
            password = textBox2.Text;
            if(username=="Admin"&& password=="Admin1290")
            {
                MessageBox.Show("You are logged in Successfully");    
                Main Main = new Main();
                Main.Show();               
            }
         else
            {
                MessageBox.Show("Username or Password is incorrect");
            }
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    }
}
