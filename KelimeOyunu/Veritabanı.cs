using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace KelimeOyunu
{
    public static class Veritabanı
    {
        private static string connectionString = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True"; // Burayı kendine göre ayarla

        public static DataTable GetKelimeIstatistik()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM KelimeIstatistik";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static int GetToplamKelimeSayisi()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM kelimeler";
                SqlCommand cmd = new SqlCommand(query, conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public static int GetOgrenilenKelimeSayisi()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM KelimeIstatistik WHERE DogruSayisi >= 6";
                SqlCommand cmd = new SqlCommand(query, conn);
                return (int)cmd.ExecuteScalar();
            }
        }
        public static void GuncelleKelimeIstatistik(string ingilizceKelime)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Kelime ID'sini al
                string kelimeIdSorgu = "SELECT KelimeId FROM kelimeler WHERE EngWordName = @engKelime";
                int kelimeId = -1;

                using (SqlCommand cmdKelimeId = new SqlCommand(kelimeIdSorgu, conn))
                {
                    cmdKelimeId.Parameters.AddWithValue("@engKelime", ingilizceKelime);
                    object result = cmdKelimeId.ExecuteScalar();

                    if (result != null)
                    {
                        kelimeId = Convert.ToInt32(result);
                    }
                    else
                    {
                        // Kelime bulunamadı, çık
                        return;
                    }
                }

                // 2. İstatistik tablosunda kontrol ve ekle/güncelle
                string kontrolQuery = "SELECT DogruSayisi FROM KelimeIstatistik WHERE KelimeId = @kelimeId";
                using (SqlCommand cmdKontrol = new SqlCommand(kontrolQuery, conn))
                {
                    cmdKontrol.Parameters.AddWithValue("@kelimeId", kelimeId);
                    object result = cmdKontrol.ExecuteScalar();

                    if (result == null)
                    {
                        // Yeni kayıt ekle
                        string insertQuery = "INSERT INTO KelimeIstatistik (KelimeId, DogruSayisi, YanlisSayisi, SonDogruTarihi, TestZamanlari) VALUES (@kelimeId, 1, 0, @tarih, 1)";
                        using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                        {
                            cmdInsert.Parameters.AddWithValue("@kelimeId", kelimeId);
                            cmdInsert.Parameters.AddWithValue("@tarih", DateTime.Now);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        int mevcutSayac = Convert.ToInt32(result);
                        if (mevcutSayac < 6)
                        {
                            string updateQuery = "UPDATE KelimeIstatistik SET DogruSayisi = DogruSayisi + 1, SonDogruTarihi = @tarih, TestZamanlari = TestZamanlari + 1 WHERE KelimeId = @kelimeId";
                            using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@kelimeId", kelimeId);
                                cmdUpdate.Parameters.AddWithValue("@tarih", DateTime.Now);
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                        // 6 ise artık öğrenilmiş sayılır, sayaç artırılmaz
                    }
                }
            }
        }
    }
}
