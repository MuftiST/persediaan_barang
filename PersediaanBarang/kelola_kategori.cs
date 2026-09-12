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
                guna2Button1.Enabled = false;
                DB.crud($"select * from kategori where id_kategori = '{idkat}'");
                foreach (DataRow baris in DB.ds.Tables[0].Rows)
                {
                    string id = "" + baris["id_kategori"];
                    string kat = "" + baris["nama_kategori"];
                    txtkat.Text = kat;
                    lblid.Text = id;
                }
            }
            if (kolom == 3)
            {

                DialogResult setuju = MessageBox.Show("Apakah mau hapus? ", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (setuju == DialogResult.Yes)
                {
                    DB.crud($"delete from kategori where id_kategori = '{idkat}'");
                }
                tampildata();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string id = lblid.Text;
            string kat = txtkat.Text;
            DB.crud($"UPDATE kategori SET nama_kategori='{kat}' WHERE id_kategori='{id}'");
            guna2Button1.Enabled = true;
            bersih();
            tampildata();
        }

        private void kelola_kategori_Load(object sender, EventArgs e)
        {
            tampildata();
        }
    }
}
