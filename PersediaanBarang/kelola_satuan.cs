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
    public partial class kelola_satuan : Form
    {
        public kelola_satuan()
        {
            InitializeComponent();
        }
        public void bersih()
        {
            txtsat.Text = "";
        }
        public void tampildata()
        {
            dataGridView1.Rows.Clear();
            DB.crud("select * from satuan");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
            {
                string idk = "" + brs["id_satuan"];
                string nmk = "" + brs["nama_satuan"];
                dataGridView1.Rows.Add(idk, nmk);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtsat.Text != "")
            {
                DB.crud($"insert into satuan values(null, '{txtsat.Text}')");
                bersih();
                tampildata();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int brs = e.RowIndex;
            int kolom = e.ColumnIndex;
            string idsat = dataGridView1.Rows[brs].Cells[0].Value.ToString();
            if (kolom == 2)
            {

                DB.crud($"select * from satuan where id_satuan = '{idsat}'");
                foreach (DataRow baris in DB.ds.Tables[0].Rows)
                {
                    string kat = "" + baris["nama_kategori"];
                    txtsat.Text = kat;

                }
            }
            if (kolom == 3)
            {

                DialogResult setuju = MessageBox.Show("Apakah mau hapus? ", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (setuju == DialogResult.Yes)
                {
                    DB.crud($"delete from roles where id_role = '{idsat}'");
                }
                tampildata();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string sat = txtsat.Text;
            DB.crud($"UPDATE users SET nama_satuan='{sat}' WHERE id_satuan='{lblid.Text}'");
            bersih();
            tampildata();
        }
    }
}
