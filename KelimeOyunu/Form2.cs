using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KelimeOyunu
{

    public partial class Form2 : Form
    {
        string dogruCevap = "";
        string connectionString = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True";
        public Form2()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ingilizceKelime = EngTextBox.Text.Trim();
            string turkceKelime = TurTextBox.Text.Trim();
            string resimYolu = ImageTextBox.Text.Trim();

            string connectionString = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kullanıcı var mı kontrolü
                string kontrolSorgu = "SELECT COUNT(*) FROM kelimeler WHERE EngWordName = @kadi";
                using (SqlCommand cmdKontrol = new SqlCommand(kontrolSorgu, conn))
                {
                    cmdKontrol.Parameters.AddWithValue("@kadi", ingilizceKelime);
                    int varMi = (int)cmdKontrol.ExecuteScalar();

                    if (varMi > 0)
                    {
                        MessageBox.Show("Bu kelime zaten eklenmis!");
                        return;
                    }
                }

                // Kullanıcıyı ekle
                string ekleSorgu = "INSERT INTO kelimeler (EngWordName, TurWordName,Picture) VALUES (@kadi, @turu , @pic)";
                using (SqlCommand cmdEkle = new SqlCommand(ekleSorgu, conn))
                {
                    cmdEkle.Parameters.AddWithValue("@kadi", ingilizceKelime);
                    cmdEkle.Parameters.AddWithValue("@turu", turkceKelime);
                    cmdEkle.Parameters.AddWithValue("@pic", resimYolu);

                    int sonuc = cmdEkle.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Kayıt başarılı!");
                    }
                    else
                    {
                        MessageBox.Show("Kayıt sırasında bir hata oluştu.");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = true;
            soruSorma();
        }
        public void soruSorma()
        {

            string ingilizceKelime = "";

            List<string> secenekler = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Rastgele 1 kelime çek
                string rastgeleKelimeSorgu = "SELECT TOP 1 * FROM kelimeler ORDER BY NEWID()";
                using (SqlCommand cmd = new SqlCommand(rastgeleKelimeSorgu, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ingilizceKelime = reader["EngWordName"].ToString();
                        dogruCevap = reader["TurWordName"].ToString();
                        secenekler.Add(dogruCevap);
                    }
                }

                // 3 tane yanlış cevap çek
                string yanlislarSorgu = "SELECT TOP 3 TurWordName FROM kelimeler WHERE TurWordName != @dogru ORDER BY NEWID()";
                using (SqlCommand cmd2 = new SqlCommand(yanlislarSorgu, conn))
                {
                    cmd2.Parameters.AddWithValue("@dogru", dogruCevap);
                    using (SqlDataReader reader2 = cmd2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            secenekler.Add(reader2.GetString(0));
                        }
                    }
                }
            }

            // Karıştır (shuffle)
            Random rnd = new Random();
            secenekler = secenekler.OrderBy(x => rnd.Next()).ToList();

            // Label’a İngilizce kelimeyi yaz
            labelKelime.Text = ingilizceKelime;

            // Butonlara şıkları yerleştir
            buttonA.Text = secenekler[0];
            buttonB.Text = secenekler[1];
            buttonC.Text = secenekler[2];
            buttonD.Text = secenekler[3];

        }

        private void buttonA_Click(object sender, EventArgs e)
        {

        }

        private void buttonB_Click(object sender, EventArgs e)
        {

        }

        private void buttonC_Click(object sender, EventArgs e)
        {

        }

        private void buttonD_Click(object sender, EventArgs e)
        {

        }
        void YeniSoruGetir()
        {
            string ingilizceKelime = "";
            List<string> secenekler = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1 tane rastgele kelime al
                string rastgeleKelimeSorgu = "SELECT TOP 1 * FROM kelimeler ORDER BY NEWID()";
                using (SqlCommand cmd = new SqlCommand(rastgeleKelimeSorgu, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ingilizceKelime = reader["EngWordName"].ToString();
                        dogruCevap = reader["TurWordName"].ToString();
                        secenekler.Add(dogruCevap);
                    }
                }

                // 3 yanlış cevap al
                string yanlislarSorgu = "SELECT TOP 3 TurWordName FROM kelimeler WHERE TurWordName != @dogru ORDER BY NEWID()";
                using (SqlCommand cmd2 = new SqlCommand(yanlislarSorgu, conn))
                {
                    cmd2.Parameters.AddWithValue("@dogru", dogruCevap);
                    using (SqlDataReader reader2 = cmd2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            secenekler.Add(reader2.GetString(0));
                        }
                    }
                }
            }

            // Şıkları karıştır
            Random rnd = new Random();
            secenekler = secenekler.OrderBy(x => rnd.Next()).ToList();

            // Ekrana bas
            labelKelime.Text = ingilizceKelime;
            buttonA.Text = secenekler[0];
            buttonB.Text = secenekler[1];
            buttonC.Text = secenekler[2];
            buttonD.Text = secenekler[3];
        }

        private async void buttonSecenek_Click(object sender, EventArgs e)
        {
            Button tiklananButon = sender as Button;

            // Doğru mu yanlış mı kontrol et ve renk ver
            if (tiklananButon.Text == dogruCevap)
            {
                tiklananButon.BackColor = Color.Green;
            }
            else
            {
                tiklananButon.BackColor = Color.Red;

                // Doğru olanı da yeşil yapalım
                foreach (var btn in new[] { buttonA, buttonB, buttonC, buttonD })
                {
                    if (btn.Text == dogruCevap)
                    {
                        btn.BackColor = Color.Green;
                        break;
                    }
                }
            }

            // 1 saniye bekle, sonra yeni soru gelsin
            await Task.Delay(1000);
            YeniSoruGetir();

            // Buton renklerini sıfırla
            foreach (var btn in new[] { buttonA, buttonB, buttonC, buttonD })
            {
                btn.BackColor = SystemColors.Control;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void ayarlar_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3(this);
            frm3.Show();
            this.Hide();
        }
    }
}
