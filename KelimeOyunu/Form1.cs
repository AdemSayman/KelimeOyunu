using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace KelimeOyunu
{
    public partial class girisYapma : Form
    {
        


        public girisYapma()
        {


            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAd = textBox1.Text.Trim();
            string sifre = textBox2.Text.Trim();

            if (kullaniciAd == "" || sifre == "")
            {
                MessageBox.Show("L�tfen kullan�c� ad� ve �ifreyi girin.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sorgu = "SELECT COUNT(*) FROM kullanicilar WHERE kullaniciAd = @kadi AND sifre = @sifre";
                using (SqlCommand cmd = new SqlCommand(sorgu, conn))
                {
                    cmd.Parameters.AddWithValue("@kadi", kullaniciAd);
                    cmd.Parameters.AddWithValue("@sifre", sifre);

                    int sonuc = (int)cmd.ExecuteScalar();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Giri� ba�ar�l�!");
                        Form2 form2 = new Form2(10);
                        form2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Hatal� kullan�c� ad� veya �ifre.");
                    }
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {


            string kullaniciAd = textBox1.Text.Trim();
            string sifre = textBox2.Text.Trim();

            if (kullaniciAd == "" || sifre == "")
            {
                MessageBox.Show("L�tfen kullan�c� ad� ve �ifreyi girin.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kullan�c� var m� kontrol�
                string kontrolSorgu = "SELECT COUNT(*) FROM kullanicilar WHERE kullaniciAd = @kadi";
                using (SqlCommand cmdKontrol = new SqlCommand(kontrolSorgu, conn))
                {
                    cmdKontrol.Parameters.AddWithValue("@kadi", kullaniciAd);
                    int varMi = (int)cmdKontrol.ExecuteScalar();

                    if (varMi > 0)
                    {
                        MessageBox.Show("Bu kullan�c� ad� zaten kay�tl�!");
                        return;
                    }
                }

                // Kullan�c�y� ekle
                string ekleSorgu = "INSERT INTO kullanicilar (kullaniciAd, sifre) VALUES (@kadi, @sifre)";
                using (SqlCommand cmdEkle = new SqlCommand(ekleSorgu, conn))
                {
                    cmdEkle.Parameters.AddWithValue("@kadi", kullaniciAd);
                    cmdEkle.Parameters.AddWithValue("@sifre", sifre);

                    int sonuc = cmdEkle.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Kay�t ba�ar�l�!");
                    }
                    else
                    {
                        MessageBox.Show("Kay�t s�ras�nda bir hata olu�tu.");
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void girisYapma_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
