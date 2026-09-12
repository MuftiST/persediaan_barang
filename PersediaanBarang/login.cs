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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string inputPass = txtpass.Text;

            DB.crud($"SELECT users.*, roles.nama_role FROM users INNER JOIN roles ON users.id_role = roles.id_role WHERE username = '{txtuser.Text}'");
            int cekbaris = DB.ds.Tables[0].Rows.Count;

            if (cekbaris == 1)
            {
                string hashedPassFromDb = DB.ds.Tables[0].Rows[0]["password"].ToString();
                string status = DB.ds.Tables[0].Rows[0]["status"].ToString();

                bool isValid = BCrypt.Net.BCrypt.Verify(inputPass, hashedPassFromDb);

                if (isValid)
                {
                    if (status == "aktif")
                    {
                        string role = DB.ds.Tables[0].Rows[0]["nama_role"].ToString();
                        DataRow baris = DB.ds.Tables[0].Rows[0];
                        string id_user = "" + baris["id_user"];

                        if (role == "admin")
                        {
                            dashboard_admin da = new dashboard_admin();
                            da.Visible = true;
                            da.id_user = id_user;
                            this.Hide();
                        }
                        else if (role == "petugas")
                        {
                            dashboard_petugas dp = new dashboard_petugas();
                            dp.Visible = true;
                            dp.id_user = id_user;
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Role tidak dikenali");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Akun nonaktif, hubungi admin");
                    }
                }
                else
                {
                    MessageBox.Show("Password salah");
                }
            }
            else
            {
                MessageBox.Show("Username tidak ditemukan");
            }

        }
    }
}
