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
    public partial class kelola_roles : Form
    {
        public kelola_roles()
        {
            InitializeComponent();
        }
        public void bersih()
        {
            txtnama_role.Text = "";
            txtketerangan.Text = "";
        }
        public void tampildata()
        {
            dataGridView1.Rows.Clear();
            DB.crud("select * from roles");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
            {
                string idr = "" + brs["id_role"];
                string nmr = "" + brs["nama_role"];
                string ket = "" + brs["keterangan"];
                dataGridView1.Rows.Add(idr, nmr, ket);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtnama_role.Text != ""|| txtketerangan.Text != "")
            {
                DB.crud($"insert into roles values(null, '{txtnama_role.Text}', '{txtketerangan.Text}')");
                tampildata();
                bersih();
            }
            else
            {
                MessageBox.Show("Lengkapi data");
            }
        }

        private void kelola_roles_Load(object sender, EventArgs e)
        {
            tampildata();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int brs = e.RowIndex;
            int kolom = e.ColumnIndex;
            string idrole = dataGridView1.Rows[brs].Cells[0].Value.ToString();
            if (kolom == 3)
            {

                DB.crud($"select * from roles where id_role = '{idrole}'");
                foreach (DataRow baris in DB.ds.Tables[0].Rows)
                {
                    string nm = "" + baris["nama_role"];
                    string ket = "" + baris["keterangan"];
                    txtnama_role.Text = nm;
                    txtketerangan.Text = ket;
                    
                }
            }
            if (kolom == 4)
            {

                DialogResult setuju = MessageBox.Show("Apakah mau hapus? ", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (setuju == DialogResult.Yes)
                {
                    DB.crud($"delete from roles where id_role = '{idrole}'");
                }
                tampildata();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string nm = txtnama_role.Text;
            string ket = txtketerangan.Text;
            DB.crud($"UPDATE users SET nama_role='{nm}', keterangan='{ket}' WHERE id_role='{lblid.Text}'");
            bersih();
            tampildata();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
