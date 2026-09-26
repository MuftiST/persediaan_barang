using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using ClosedXML.Excel;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PersediaanBarang
{
    public partial class cetak_laporan : Form
    {
        public cetak_laporan()
        {
            InitializeComponent();
        }

        public void tampildata()
        {
            dataGridView1.Rows.Clear();
            DB.crud("select * from stok_barang order by id_transaksi desc");
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
                string user = "" + baris["id_user"];
                dataGridView1.Rows.Add(idtr, idb, idsup, beli, tgl, jns, msk, klr, sld, ket, user);
            }
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

        private void txtsaldo_TextChanged(object sender, EventArgs e)
        {

        }

        
        int stokAwal = GetStokBarang(idBarang);
       

        private void cmbids_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
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


       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbltrans_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        public static void ExportWithClosedXML(DataGridView dgv, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Laporan");

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                    worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                    worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                    worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (!dgv.Rows[i].IsNewRow)
                    {
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = dgv.Rows[i].Cells[j].Value?.ToString();
                            worksheet.Cell(i + 2, j + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                    }
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
            var result = MessageBox.Show("Data berhasil dicetak!\nLaporan disimpan di: " + filePath +
                                         "\n\nApakah ingin membuka file sekarang?",
                                         "Export Sukses",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Process.Start(filePath);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ExportWithClosedXML(dataGridView1, @"D:\laporan.xlsx");
        }

        private void cetak_laporan_Load(object sender, EventArgs e)
        {
            tampildata();
        }
        public static void ExportToPDF(DataGridView dgv, string filePath)
        {
            // Buat dokumen PDF
            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                PdfWriter.GetInstance(doc, stream);
                doc.Open();

                // Judul
                Paragraph title = new Paragraph("Laporan Data",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph("\n"));

                // Buat tabel sesuai jumlah kolom DataGridView
                PdfPTable table = new PdfPTable(dgv.Columns.Count);
                table.WidthPercentage = 100;

                // Header
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText,
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                // Data
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            table.AddCell(cell.Value?.ToString() ?? "");
                        }
                    }
                }

                doc.Add(table);
                doc.Close();
            }

            // Pesan sukses
            var result = MessageBox.Show("Data berhasil dicetak!\nLaporan disimpan di: " + filePath +
                                         "\n\nApakah ingin membuka file sekarang?",
                                         "Export Sukses",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Process.Start(filePath);
            }
        }
        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            ExportToPDF(dataGridView1, @"D:\laporan.pdf");
        }
    }
}
