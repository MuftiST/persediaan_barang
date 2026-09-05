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
    public partial class kelola_kategori : Form
    {
        public kelola_kategori()
        {
            InitializeComponent();
        }
        public void bersih()
        {
            txtkat.Text = "";
        }
        public void tampildata()
        {
            dataGridView1.Rows.Clear();
            DB.crud("select * from kategori");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
            {
                string idk = "" + brs["id_kategori"];
                string nmk = "" + brs["nama_kategori"];
                dataGridView1.Rows.Add(idk, nmk);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtkat.Text != "")
            {
                DB.crud($"insert into kategori values(null, '{txtkat.Text}')");
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
            string idkat = dataGridView1.Rows[brs].Cells[0].Value.ToString();
            if (kolom == 2)
            {

                DB.crud($"select * from kategori where id_kategori = '{idkat}'");
                foreach (DataRow baris in DB.ds.Tables[0].Rows)
                {
                    string kat = "" + baris["nama_kategori"];
                    txtkat.Text = kat;

                }
            }
            if (kolom == 3)
            {

                DialogResult setuju = MessageBox.Show("Apakah mau hapus? ", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (setuju == DialogResult.Yes)
                {
                    DB.crud($"delete from roles where id_role = '{idkat}'");
                }
                tampildata();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string kat = txtkat.Text;
            DB.crud($"UPDATE users SET nama_kategori='{kat}' WHERE id_kategori='{lblid.Text}'");
            bersih();
            tampildata();
        }
    }
}
