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
    public partial class Librarymdiparent : Form
    {
        //private int childFormNumber = 0;

        public Librarymdiparent()
        {
            InitializeComponent();
        }

        //private void ShowNewForm(object sender, EventArgs e)
        //{
        //    Form childForm = new Form();
        //    childForm.MdiParent = this;
        //    childForm.Text = "Window " + childFormNumber++;
        //    childForm.Show();
        //}

        //private void OpenFile(object sender, EventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        //    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        //    {
        //        string FileName = openFileDialog.FileName;
        //    }
        //}

        //private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        //    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
        //    {
        //        string FileName = saveFileDialog.FileName;
        //    }
        //}

        //private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //}

        //private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //}

        //private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //}

        //private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        //}

        //private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        //}

        //private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    LayoutMdi(MdiLayout.Cascade);
        //}

        //private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    LayoutMdi(MdiLayout.TileVertical);
        //}

        //private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    LayoutMdi(MdiLayout.TileHorizontal);
        //}

        //private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    LayoutMdi(MdiLayout.ArrangeIcons);
        //}

        //private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    foreach (Form childForm in MdiChildren)
        //    {
        //        childForm.Close();
        //    }
        //}

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Customers = new Customers();
            Customers.MdiParent = this;
            Customers.Show();
           
        }

        private void materialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Materials = new Materials();
            Materials.MdiParent = this;
            Materials.Show();
          
        }

        private void borrowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Borrows = new Borrows();
            Borrows.MdiParent = this;
            Borrows.Show();
           

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("Do you Want to Logout?", "Confirmation", MessageBoxButtons.YesNo);
            if (ans == DialogResult.Yes)
            {

                MessageBox.Show("You are Successfully Logout please add Username AND password to Login!");
                Login formLogin = new Login();
                formLogin.Show(this);
                this.Hide();
            }
            
                else
                {
                Librarymdiparent mdiparent = new Librarymdiparent();
                mdiparent.Show();
                this.Hide();    
                }

            
        }
    }
}
