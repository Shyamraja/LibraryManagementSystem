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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customers category = new Customers();
            category.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Borrows category = new Borrows();
            category.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Materials category = new Materials();
            category.Show();
        }
    }
}
