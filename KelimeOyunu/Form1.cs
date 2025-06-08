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
            this.Load += girisYapma_Load;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAd = textBox1.Text.Trim();
            string sifre = textBox2.Text.Trim();

            if (kullaniciAd == "" || sifre == "")
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifreyi girin.");
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
                        MessageBox.Show(
                        "✅ Giriş başarılı!\n\nHoş geldin, iyi oyunlar! 🎮",
                        "🟢 Başarılı Giriş",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                         );
                        Form2 form2 = new Form2(10);
                        form2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(
                        "🚫 Kullanıcı adı veya şifre yanlış!\n\nLütfen tekrar deneyin.",
                        "🔴 Giriş Hatası",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
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
                MessageBox.Show(
                "⚠️ Lütfen hem kullanıcı adını hem şifreyi doldurun!",
                "Eksik Bilgi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
                return;
            }

            string connectionString = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kullanıcı var mı kontrolü
                string kontrolSorgu = "SELECT COUNT(*) FROM kullanicilar WHERE kullaniciAd = @kadi";
                using (SqlCommand cmdKontrol = new SqlCommand(kontrolSorgu, conn))
                {
                    cmdKontrol.Parameters.AddWithValue("@kadi", kullaniciAd);
                    int varMi = (int)cmdKontrol.ExecuteScalar();

                    if (varMi > 0)
                    {
                        MessageBox.Show(
                        "⚠️ Bu kullanıcı adı zaten alınmış!\n\nLütfen farklı bir kullanıcı adı deneyin.",
                        "🟡 Kayıt Hatası",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        );
                        return;
                    }
                }

                // Kullanıcıyı ekle
                string ekleSorgu = "INSERT INTO kullanicilar (kullaniciAd, sifre) VALUES (@kadi, @sifre)";
                using (SqlCommand cmdEkle = new SqlCommand(ekleSorgu, conn))
                {
                    cmdEkle.Parameters.AddWithValue("@kadi", kullaniciAd);
                    cmdEkle.Parameters.AddWithValue("@sifre", sifre);

                    int sonuc = cmdEkle.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show(
                        "✅ Kayıt başarılı!\n\nArtık giriş yapabilirsin 👌",
                        "🟢 Kayıt Tamamlandı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show("Kayıt sırasında bir hata oluştu.");
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
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string kullaniciAd = textBox1.Text.Trim();

            if (kullaniciAd == "")
            {
                MessageBox.Show(
                    "🔒 Lütfen kullanıcı adını gir!",
                    "Şifre Sıfırlama",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string connectionString = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string kontrolSorgu = "SELECT COUNT(*) FROM kullanicilar WHERE kullaniciAd = @kadi";
                using (SqlCommand cmdKontrol = new SqlCommand(kontrolSorgu, conn))
                {
                    cmdKontrol.Parameters.AddWithValue("@kadi", kullaniciAd);
                    int sonuc = (int)cmdKontrol.ExecuteScalar();

                    if (sonuc == 0)
                    {
                        MessageBox.Show(
                            "❌ Bu kullanıcı adı sistemde yok!",
                            "Şifre Sıfırlama",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }
                }

                // Yeni şifreyi oluştur
                string yeniSifre = Guid.NewGuid().ToString().Substring(0, 6); // örn: 6 haneli random şifre

                string sifreGuncelle = "UPDATE kullanicilar SET sifre = @yeni WHERE kullaniciAd = @kadi";
                using (SqlCommand cmdGuncelle = new SqlCommand(sifreGuncelle, conn))
                {
                    cmdGuncelle.Parameters.AddWithValue("@yeni", yeniSifre);
                    cmdGuncelle.Parameters.AddWithValue("@kadi", kullaniciAd);

                    cmdGuncelle.ExecuteNonQuery();

                    MessageBox.Show(
                        $"🔑 Yeni şifreniz: **{yeniSifre}**\n\nLütfen hemen giriş yapıp şifrenizi değiştirin.",
                        "Yeni Şifre Oluşturuldu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }
    }
}
