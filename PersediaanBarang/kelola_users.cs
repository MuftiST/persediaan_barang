using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;

namespace PersediaanBarang
{
    public partial class kelola_users : Form
    {
        public kelola_users()
        {
            InitializeComponent();
        }

        private void kelola_users_Load(object sender, EventArgs e)
        {
            DB.crud("SELECT id_role, nama_role FROM roles");

            if (DB.ds.Tables.Count > 0)
            {
                cmbrole.DataSource = DB.ds.Tables[0];
                cmbrole.DisplayMember = "nama_role";
                cmbrole.ValueMember = "id_role"; 
            }

            tampildata();
        }
        public void bersih()
        {
            txtnama.Text = "";
            txtemail.Text = "";
            txtpass.Text = "";
            txtuser.Text = "";
            txttelp.Text = "";
            cmbrole.Text = "";
        }
        public void tampildata()
        {
            dataGridView1.Rows.Clear();
            DB.crud("select * from users inner join roles on roles.id_role = users.id_role");
            foreach (DataRow baris in DB.ds.Tables[0].Rows)
            {
                string idu = "" + baris["id_user"];
                string nm = "" + baris["nama"];
                string user = "" + baris["username"];
                string pass = "" + baris["password"];
                string role = "" + baris["nama_role"];
                string email = "" + baris["email"];
                string telp = "" + baris["no_telp"];
                string status = "" + baris["status"];
                dataGridView1.Rows.Add(idu, nm, user, pass, role, email, telp, status);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtnama.Text != "" || txtemail.Text!=""||txtpass.Text!=""||txtuser.Text!=""||txttelp.Text!=""||cmbrole.Text!="")
            {
                string nm = txtnama.Text;
                string email = txtemail.Text;
                string hashedpass = BCrypt.Net.BCrypt.HashPassword(txtpass.Text);
                string user = txtuser.Text;
                string telp = txttelp.Text;
                string role = cmbrole.SelectedValue.ToString();
                DB.crud($"INSERT INTO users (nama, username, password, id_role, email, no_telp, status) VALUES ('{nm}', '{user}', '{hashedpass}', '{role}', '{email}', '{telp}', DEFAULT)");
                tampildata();
            }
        }

        private void guna2ComboBox1_DropDown(object sender, EventArgs e)
        {
            cmbstatus.Items.Clear();
            cmbstatus.Items.Add("aktif");
            cmbstatus.Items.Add("nonaktif");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int brs = e.RowIndex;
            int kolom = e.ColumnIndex;
            string iduser = dataGridView1.Rows[brs].Cells[0].Value.ToString();
            if (kolom == 8)
            {
                
                DB.crud($"select users.*, roles.nama_role from users INNER JOIN roles ON users.id_role = roles.id_role where id_user = '{iduser}'");
                foreach (DataRow baris in DB.ds.Tables[0].Rows)
                {
                    string idu = "" + baris["id_user"];
                    string nm = "" + baris["nama"];
                    string user = "" + baris["username"];
                    string pass = "" + baris["password"];
                    string role = "" + baris["nama_role"];
                    string email = "" + baris["email"];
                    string telp = "" + baris["no_telp"];
                    string status = "" + baris["status"];
                    txtid.Text = idu;
                    txtnama.Text = nm;
                    txtpass.Text = pass;
                    txtuser.Text = user;
                    txtemail.Text = email;
                    cmbrole.Text = role;
                    txttelp.Text = telp;
                    cmbstatus.Text = status;
                }
            }
            if (kolom == 9)
            {
                
                DialogResult setuju = MessageBox.Show("Apakah mau hapus? ", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (setuju == DialogResult.Yes)
                {
                    DB.crud($"delete from users where id_user = '{iduser}'");
                }
                tampildata();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string nm = txtnama.Text;
            string email = txtemail.Text;
            string hashedpass = BCrypt.Net.BCrypt.HashPassword(txtpass.Text);
            string user = txtuser.Text;
            string telp = txttelp.Text;
            string role = cmbrole.SelectedValue.ToString();
            string status = cmbstatus.Text;
            DB.crud($"UPDATE users SET nama='{nm}', username='{user}', password='{hashedpass}', id_role='{role}', email='{email}', no_telp='{telp}', status='{status}' WHERE id_user='{txtid.Text}'");
            bersih();
            tampildata();
        }

        private void cmbrole_DropDown(object sender, EventArgs e)
        {

           

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            
        }

        private void cmbrole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtnama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtnama.Text != "")
                {
                    txtuser.Select();
                }
            }
        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtuser.Text != "")
                {
                    txtpass.Select();
                }
            }
        }

        private void txttelp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txttelp.Text != "")
                {
                    txtemail.Select();
                }
            }
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtpass.Text != "")
                {
                    txttelp.Select();
                }
            }
        }

        private void txtgudang_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
