using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersediaanBarang
{
    public partial class dashboard_petugas : Form
    {
        public dashboard_petugas()
        {
            InitializeComponent();
        }

        private void dashboard_petugas_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult setuju = MessageBox.Show("Apakah mau keluar?", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (setuju == DialogResult.Yes)
            {
                login log = new login();
                log.Visible = true;
                this.Hide();
            }
        }

        private void guna2ComboBox1_DropDown(object sender, EventArgs e)
        {
        }
        public string id_user;
        private void dashboard_petugas_Load(object sender, EventArgs e)
        {

        }
    }
}
