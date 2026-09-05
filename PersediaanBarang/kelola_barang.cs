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
    public partial class kelola_barang : Form
    {
        public kelola_barang()
        {
            InitializeComponent();
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void bersih()
        {
            txtnama.Text = "";
            cmbkat.Text = "";
            cmbsat.Text = "";
            txtstokmin.Text = "";
            txtbeli.Text = "";
            txtjual.Text = "";
        }
        public void tampildata()
        {
            dataGridView1.Rows.Clear();
            DB.crud("select * from barang");
            foreach (DataRow baris in DB.ds.Tables[0].Rows)
            {
                string idb = "" + baris["idb"];
                string nm = "" + baris["nama_barang"];
                string kat = "" + baris["kategori_id"];
                string sat = "" + baris["satuan_id"];
                string stok = "" + baris["stok"];
                string beli = "" + baris["harga_beli"];
                string jual = "" + baris["harga_jual"];
                dataGridView1.Rows.Add(idb, nm, kat, sat, stok, beli, jual);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtnama.Text != "" || cmbkat.Text != "" || cmbsat.Text != "" || txtstokmin.Text != "" || txtbeli.Text != "" || txtjual.Text != "")
            {
                string nm = txtnama.Text;
                string kat = cmbkat.Text;
                string sat = cmbsat.Text;
                string stok = txtstokmin.Text;
                string beli = txtbeli.Text;
                string jual = txtjual.Text;
                DB.crud($"insert into barang values(null, '{nm}', '{kat}', '{sat}', '{stok}', '{beli}', '{jual}')");
                tampildata();
                bersih();
            }
            else
            {
                MessageBox.Show("Lengkapi Data!");
            }
        }

        private void kelola_barang_Load(object sender, EventArgs e)
        {
            tampildata();
        }

        private void cmbkat_DropDown(object sender, EventArgs e)
        {
            string sqlKategori = "SELECT id_kategori, nama_kategori FROM kategori";
            DB.crud(sqlKategori);
            cmbkat.DataSource = DB.ds.Tables[0];
            cmbkat.DisplayMember = "nama_kategori";
            cmbkat.ValueMember = "id_kategori";
        }

        private void cmbsat_DropDown(object sender, EventArgs e)
        {
            string sqlSatuan = "SELECT id_satuan, nama_satuan FROM satuan";
            DB.crud(sqlSatuan);
            cmbkat.DataSource = DB.ds.Tables[0];
            cmbkat.DisplayMember = "nama_satuan";
            cmbkat.ValueMember = "id_satuan";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int brs = e.RowIndex;
            int kolom = e.ColumnIndex;
            string idb = dataGridView1.Rows[brs].Cells[0].Value.ToString();
            if (kolom == 7)
            {

                DB.crud($"select * from barang where idb = '{idb}'");
                foreach (DataRow baris in DB.ds.Tables[0].Rows)
                {
                    string idbrg = "" + baris["idb"];
                    string nm = "" + baris["nama_barang"];
                    string kat = "" + baris["kategori_id"];
                    string sat = "" + baris["satuan_id"];
                    string stok = "" + baris["stok"];
                    string beli = "" + baris["harga_beli"];
                    string jual = "" + baris["harga_jual"];
                    label1.Text = idbrg;
                    txtnama.Text = nm;
                    cmbkat.SelectedItem = kat;
                    cmbsat.SelectedItem = sat;
                    txtstokmin.Text = stok;
                    txtbeli.Text = beli;
                    txtjual.Text = jual;
                }
            }
            if (kolom == 8)
            {

                DialogResult setuju = MessageBox.Show("Apakah mau hapus? ", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (setuju == DialogResult.Yes)
                {
                    DB.crud($"delete from barang where idb = '{idb}'");
                }
                tampildata();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string nm = txtnama.Text;
            string kat = cmbkat.Text;
            string sat = cmbsat.Text;
            string stok = txtstokmin.Text;
            string beli = txtbeli.Text;
            string jual = txtjual.Text;
            DB.crud($"update barang set nama_barang='{nm}', kategori_id='{kat}', satuan_id='{sat}', stok_minimum='{stok}', harga_beli='{beli}', harga_jual='{jual}' where idb = '{label1.Text}'");
            bersih();
            tampildata();
        }

        private void txtstokmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtbeli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // blok input
            }
        }

        private void txtjual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // blok input
            }
        }

        private void cmbkat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
