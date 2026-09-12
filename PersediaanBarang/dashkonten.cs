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
    public partial class dashkonten : Form
    {
        public dashkonten()
        {
            InitializeComponent();
        }

        private void dashkonten_Load(object sender, EventArgs e)
        {
            DB.crud($"select count(*) as total from users");
            lbluser.Text = DB.ds.Tables[0].Rows[0]["total"].ToString() + " orang";

            DB.crud($"select count(*) as total from barang");
            lblstok.Text = DB.ds.Tables[0].Rows[0]["total"].ToString() + " barang";

            DB.crud($"select count(*) as total from supplier");
            lblsupp.Text = DB.ds.Tables[0].Rows[0]["total"].ToString() + " supplier";

            DB.crud($"select count(*) as total from stok_barang");
            lbltrans.Text = DB.ds.Tables[0].Rows[0]["total"].ToString() + " transaksi";
        }
    }
}
