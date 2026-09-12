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
    public partial class kelola_supplier : Form
    {
        public kelola_supplier()
        {
            InitializeComponent();
        }

        public void bersih()
        {
            txtnama.Text = "";
            txtalamat.Text = "";
            txtemail.Text = "";
            txttelp.Text = "";
        }
        public void tampildata()
        {
            dataGridView1.Rows.Clear();
            DB.crud("select * from supplier");
            foreach (DataRow baris in DB.ds.Tables[0].Rows)
            {
                string ids = "" + baris["id_supplier"];
                string nm = "" + baris["nama_supplier"];
                string ala = "" + baris["alamat"];
                string telp = "" + baris["telepon"];
                string email = "" + baris["email"];
                dataGridView1.Rows.Add(ids, nm, ala, telp, email);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtnama.Text != "" || txtalamat.Text != "" || txttelp.Text != "" || txtemail.Text != "")
            {
                string nm = txtnama.Text;
                string ala = txtalamat.Text;
                string telp = txttelp.Text;
                string email = txtemail.Text;
                DB.crud($"insert into supplier values(null, '{nm}', '{ala}', '{telp}', '{email}')");
                tampildata();
                bersih();
            }
            else
            {
                MessageBox.Show("Lengkapi Data!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int brs = e.RowIndex;
            int kolom = e.ColumnIndex;
            string ids = dataGridView1.Rows[brs].Cells[0].Value.ToString();
            if (kolom == 6)
            {

                DB.crud($"select * from supplier where id_supplier = '{ids}'");
                foreach (DataRow baris in DB.ds.Tables[0].Rows)
                {
                    guna2Button1.Enabled = false;
                    string idsp = "" + baris["id_supplier"];
                    string nm = "" + baris["nama_supplier"];
                    string ala = "" + baris["alamat"];
                    string telp = "" + baris["telepon"];
                    string email = "" + baris["email"];
                    lblid.Text = idsp;
                    txtnama.Text = nm;
                    txtalamat.Text = ala;
                    txttelp.Text = telp;
                    txtemail.Text = email;
                }
            }
            if (kolom == 7)
            {

                DialogResult setuju = MessageBox.Show("Apakah mau hapus? ", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (setuju == DialogResult.Yes)
                {
                    DB.crud($"delete from barang where idb = '{ids}'");
                }
                tampildata();
            }
        }

        private void kelola_supplier_Load(object sender, EventArgs e)
        {
            tampildata();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string nm = txtnama.Text;
            string ala = txtalamat.Text;
            string telp = txttelp.Text;
            string email = txtemail.Text;
            DB.crud($"update supplier set nama_supplier ='{nm}', alamat='{ala}', telepon='{telp}', email='{email}' where id_supplier='{lblid.Text}'");
            bersih();
            tampildata();
            guna2Button1.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
