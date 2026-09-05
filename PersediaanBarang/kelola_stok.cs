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
    public partial class kelola_stok : Form
    {
        public kelola_stok()
        {
            InitializeComponent();
        }

        public void bersih()
        {
            cmbidb.SelectedIndex = -1;
            txtket.Text = "";
            txtbeli.Text = "";
            cmbids.SelectedIndex = -1;
            cmbjenis.SelectedIndex = -1;
            txtmasuk.Text = "";
            txtkeluar.Text = "";
            txtsaldo.Text = "";
        }
        public void tampildata()
        {
            dataGridView1.Rows.Clear();
            DB.crud("select * from stok_barang");
            foreach (DataRow baris in DB.ds.Tables[0].Rows)
            {
                string idtr = "" + baris["id_transaksi"];
                string idb = "" + baris["idb"];
                string idsup = "" + baris["supplier_id"];
                string beli = "" + baris["harga_beli"];
                string tgl = "" + baris["tanggal_transaksi"];
                string jns = "" + baris["jenis_transaksi"];
                string msk = "" + baris["jumlah_masuk"];
                string klr = "" + baris["jumlah_keluar"];
                string sld = "" + baris["saldo_akhir"];
                string ket = "" + baris["keterangan"];
                dataGridView1.Rows.Add(idtr, idb, idsup, beli, tgl, jns, msk, klr, sld, ket);
            }
        }
        private void kelola_stok_Load(object sender, EventArgs e)
        {
            id_user.Text = idu;
            tampildata();

            lbltrans.Text = DB.GetNewIDFromProcedure();

            string sqlBarang = "SELECT idb, nama_barang FROM barang";
            DB.crud(sqlBarang);
            cmbidb.DataSource = DB.ds.Tables[0];
            cmbidb.DisplayMember = "nama_barang";
            cmbidb.ValueMember = "idb";
            cmbidb.SelectedIndex = -1;


            cmbjenis.Items.Clear();
            cmbjenis.Items.Add("-- Pilih Jenis --");
            cmbjenis.SelectedIndex = 0;

            cmbids.Items.Clear();
            cmbids.Items.Add("-- Opsional (barang masuk) --");
            cmbids.SelectedIndex = 0;

        }
        private static int idBarang;

        public string idu;
        private static int GetStokBarang(object idBarang)
        {
            int stok = 0;

            DB.crud($"SELECT stok FROM barang WHERE idb = '{idBarang}'");

            if (DB.ds.Tables.Count > 0 && DB.ds.Tables[0].Rows.Count > 0)
            {
                stok = Convert.ToInt32(DB.ds.Tables[0].Rows[0]["stok"]);
            }

            return stok;
        }

        private void txtkeluar_TextChanged(object sender, EventArgs e)
        {
            if (cmbidb.SelectedValue == null) return;

            int stokAwal = GetStokBarang(idBarang);

            if (!string.IsNullOrWhiteSpace(txtkeluar.Text))
            {
                if (int.TryParse(txtkeluar.Text, out int jumlahKeluar))
                {
                    if (jumlahKeluar > stokAwal)
                    {
                        MessageBox.Show("Stok tidak mencukupi!");
                        txtkeluar.Text = "";
                        return;
                    }
                    txtsaldo.Text = (stokAwal - jumlahKeluar).ToString();
                    txtmasuk.Enabled = false;
                }
            }
            else
            {
                txtmasuk.Enabled = true;
                txtsaldo.Text = stokAwal.ToString();
            }
        }

        private void txtmasuk_TextChanged(object sender, EventArgs e)
        {
            int stokAwal = GetStokBarang(Convert.ToInt32(idBarang));

            if (!string.IsNullOrWhiteSpace(txtmasuk.Text))
            {
                int jumlahMasuk = int.Parse(txtmasuk.Text);
                txtsaldo.Text = (stokAwal + jumlahMasuk).ToString();
                txtkeluar.Enabled = false;
            }
            else
            {
                txtkeluar.Enabled = true;
                txtsaldo.Text = stokAwal.ToString();
            }
        }

        private void txtsaldo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbidb_DropDown(object sender, EventArgs e)
        {
            string sqlBarang = "SELECT idb, nama_barang FROM barang";
            DB.crud(sqlBarang);
            cmbidb.DataSource = DB.ds.Tables[0];
            cmbidb.DisplayMember = "nama_barang";
            cmbidb.ValueMember = "idb";
        }

        private void cmbids_DropDown(object sender, EventArgs e)
        {
            string sqlSupplier = "SELECT id_supplier, nama_supplier FROM supplier";
            DB.crud(sqlSupplier);
            cmbids.DataSource = DB.ds.Tables[0];
            cmbids.DisplayMember = "nama_supplier";
            cmbids.ValueMember = "id_supplier";
        }

        private void cmbjenis_DropDown(object sender, EventArgs e)
        {
            cmbjenis.Items.Clear();
            cmbjenis.Items.Add("masuk");
            cmbjenis.Items.Add("keluar");
            cmbjenis.Items.Add("penyesuaian");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(txtmasuk.Text) || !string.IsNullOrWhiteSpace(txtkeluar.Text)) // salah satu wajib isi
              && !string.IsNullOrWhiteSpace(cmbjenis.Text)
              && !string.IsNullOrWhiteSpace(cmbidb.Text)
)
            {
                string idtrans = lbltrans.Text;
                string idb = cmbidb.SelectedValue.ToString();
                string jns = cmbjenis.Text;
                string ids = (jns == "masuk" && cmbids.SelectedValue != null && !string.IsNullOrWhiteSpace(cmbids.SelectedValue.ToString()))
                                ? $"'{cmbids.SelectedValue}'"
                                : "NULL";
                string beli = (jns == "masuk" && !string.IsNullOrWhiteSpace(txtbeli.Text))
                                ? txtbeli.Text
                                : "NULL";
                string msk = string.IsNullOrWhiteSpace(txtmasuk.Text) ? "0" : txtmasuk.Text;
                string klr = string.IsNullOrWhiteSpace(txtkeluar.Text) ? "0" : txtkeluar.Text;
                string ket = txtket.Text;
                string idu = id_user.Text;
                int stokAwal = GetStokBarang(Convert.ToInt32(idb));
                int saldoAkhir = stokAwal;
                if (!string.IsNullOrWhiteSpace(msk) && msk != "0")
                {
                    saldoAkhir = stokAwal + Convert.ToInt32(msk);
                }
                else if (!string.IsNullOrWhiteSpace(klr) && klr != "0")
                {
                    saldoAkhir = stokAwal - Convert.ToInt32(klr);
                }
                DB.crud($"INSERT INTO stok_barang (id_transaksi, idb, supplier_id, harga_beli, jenis_transaksi, jumlah_masuk, jumlah_keluar, saldo_akhir, keterangan, id_user) " +
                        $"VALUES ('{idtrans}', '{idb}', {ids}, {beli}, '{jns}', {msk}, {klr}, {saldoAkhir}, '{ket}', '{idu}')");
                DB.crud($"UPDATE barang SET stok = {saldoAkhir} WHERE idb = '{idb}'");
                tampildata();
                bersih();
                // ambil ID transaksi baru dari procedure
                lbltrans.Text = DB.GetNewIDFromProcedure();
            }
            else
            {
                MessageBox.Show("Data belum lengkap, isi semua field wajib!");
            }

            lbltrans.Text = DB.GetNewIDFromProcedure();
        }
        int stokAwal = GetStokBarang(idBarang);
        private void cmbidb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbidb.SelectedValue != null && int.TryParse(cmbidb.SelectedValue.ToString(), out idBarang))
            {
                int stokAwal = GetStokBarang(idBarang);
                txtsaldo.Text = stokAwal.ToString();
            }
        }

        private void cmbids_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int brs = e.RowIndex;
            int kolom = e.ColumnIndex;
            string idstok = dataGridView1.Rows[brs].Cells[0].Value.ToString();
            if (kolom == 10)
            {
                DB.crud($"select stok_barang.*, barang.nama_barang from barang INNER JOIN stok_barang ON barang.idb = stok_barang.idb where id_transaksi = '{idstok}'");
                foreach (DataRow baris in DB.ds.Tables[0].Rows)
                {
                    string idt = "" + baris["id_transaksi"];
                    string idb = "" + baris["idb"];
                    string supp = "" + baris["supplier_id"];
                    string beli = "" + baris["harga_beli"];
                    string jenis = "" + baris["jenis_transaksi"];
                    string msk = "" + baris["jumlah_masuk"];
                    string klr = "" + baris["jumlah_keluar"];
                    string akhir = "" + baris["saldo_akhir"];
                    string ket = "" + baris["keterangan"];

                    lbltrans.Text = idt;
                    txtbeli.Text = beli;
                    txtmasuk.Text = msk;
                    txtkeluar.Text = klr;
                    txtsaldo.Text = akhir;
                    txtket.Text = ket;

                    int idxDb = cmbidb.FindStringExact(idb);
                    if (idxDb >= 0) cmbidb.SelectedIndex = idxDb;

                    int idxSup = cmbids.FindStringExact(supp);
                    if (idxSup >= 0) cmbids.SelectedIndex = idxSup;

                    int idxJenis = cmbjenis.FindStringExact(jenis);
                    if (idxJenis >= 0) cmbjenis.SelectedIndex = idxJenis;
                }
            }
            if (kolom == 11)
            {

                DialogResult setuju = MessageBox.Show("Apakah mau hapus? ", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (setuju == DialogResult.Yes)
                {
                    DB.crud($"delete from users where id_user = '{idstok}'");
                }
                tampildata();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void txtmasuk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtkeluar_KeyPress(object sender, KeyPressEventArgs e)
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
                e.Handled = true;
            }
        }

        private void id_user_Click(object sender, EventArgs e)
        {
            id_user.Text = idu;
        }

        private void cmbjenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stokAwal = GetStokBarang(Convert.ToInt32(idBarang));

            if (cmbjenis.Text == "masuk")
            {
                txtkeluar.Enabled = false;
                txtmasuk.Enabled = true;
                txtsaldo.Text = stokAwal.ToString();
            }
            else if (cmbjenis.Text == "keluar" || cmbjenis.Text == "penyesuaian")
            {
                txtmasuk.Enabled = false;
                txtkeluar.Enabled = true;
                txtsaldo.Text = stokAwal.ToString();
            }
            else
            {
                txtmasuk.Enabled = false;
                txtkeluar.Enabled = false;
                txtsaldo.Text = stokAwal.ToString();
            }

        }
    }
}
