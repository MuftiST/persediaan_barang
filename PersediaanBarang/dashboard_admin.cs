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
    public partial class dashboard_admin : Form
    {

        
        public dashboard_admin()
        {
            InitializeComponent();
        }

        private void dashboard_admin_FormClosing(object sender, FormClosingEventArgs e)
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            kelola_users user = new kelola_users() { TopLevel = false, TopMost = true };
            KF.untukform(user, pnlkonten);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            kelola_roles role = new kelola_roles() { TopLevel = false, TopMost = true };
            KF.untukform(role, pnlkonten);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (pnlside.Visible == true)
            {
                pnlside.Visible = false;
            }
            else
            {
                pnlside.Visible = true;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            kelola_barang brg = new kelola_barang() { TopLevel = false, TopMost = true };
            KF.untukform(brg, pnlkonten);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            kelola_supplier sup = new kelola_supplier() { TopLevel = false, TopMost = true };
            KF.untukform(sup, pnlkonten);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            kelola_stok stok = new kelola_stok() { TopLevel = false, TopMost = true };
            stok.idu = id_user;
            KF.untukform(stok, pnlkonten);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            guna2Button8.Location = new Point(12, 309);
            if (pnluser.Visible == true)
            {
                pnluser.Visible = false;
            }
            else
            {
                pnluser.Visible = true;
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (pnlbrg.Visible == true)
            {
                pnlbrg.Visible = false;
            }
            else
            {
                pnlbrg.Visible = true;
            }
        }
        public string id_user;
        private void dashboard_admin_Load(object sender, EventArgs e)
        {
            pnluser.Visible = false;
            pnlbrg.Visible = false;
            idu.Text = id_user;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
           
        }
    }
}
