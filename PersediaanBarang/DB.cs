using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace PersediaanBarang
{
    class DB
    {
        public static MySqlConnection koneksi = new MySqlConnection
            ("server=127.0.0.1; username=root; password=; database=persediaan_barang");
        public static DataSet ds = new DataSet();
        public static MySqlDataAdapter da;
        public static MySqlCommand perintah;
        public static MySqlDataReader read;

        public static MySqlConnection getConnection()
        {
            if (koneksi.State == System.Data.ConnectionState.Closed)
            {
                koneksi.Open();
            }
            return koneksi;
        }

        public static MySqlDataReader query(string sql)
        {
            perintah = new MySqlCommand(sql, getConnection());
            return perintah.ExecuteReader();
        }

        public static void crud(string query)
        {
            ds.Tables.Clear();
            perintah = new MySqlCommand(query, getConnection());
            da = new MySqlDataAdapter(perintah);
            da.Fill(ds);
        }

        public static string GetNewIDFromProcedure()
        {
            string newID = "";
            MySqlConnection conn = DB.koneksi;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand("IDTransaksi", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlParameter outParam = new MySqlParameter("p_new_id", MySqlDbType.VarChar, 25);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.ExecuteNonQuery();

                    newID = outParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error ID Auto: " + ex.Message);
            }
            finally
            {
                // Tutup koneksi kembali agar aman untuk proses berikutnya
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return newID;
        }
    }
}
