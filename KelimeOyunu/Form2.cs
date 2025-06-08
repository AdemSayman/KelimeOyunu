using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using KelimeOyunu;
using System.Speech.Synthesis;



namespace KelimeOyunu
{

    public partial class Form2 : Form
    {

        int dogruSayac = 0;
        int yanlisSayac = 0;
        string dogruCevap = "";
        int hedefSoruSayisi = 10; // default
        int mevcutSoruSayaci = 0;
        string connectionString = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True";
        public Form2(int soruSayisi)
        {
            hedefSoruSayisi = soruSayisi;
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab= tabPage1;
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBoxAyarlar.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ingilizceKelime = EngTextBox.Text.Trim();
            string turkceKelime = TurTextBox.Text.Trim();
            string resimYolu = ImageTextBox.Text.Trim();
            string OrnekCumle = richTextBox1.Text.Trim();

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
                string ekleSorgu = "INSERT INTO kelimeler (EngWordName, TurWordName,Picture,OrnekCumle) VALUES (@kadi, @turu , @pic, @ornek)";
                using (SqlCommand cmdEkle = new SqlCommand(ekleSorgu, conn))
                {
                    cmdEkle.Parameters.AddWithValue("@kadi", ingilizceKelime);
                    cmdEkle.Parameters.AddWithValue("@turu", turkceKelime);
                    cmdEkle.Parameters.AddWithValue("@pic", resimYolu);
                    cmdEkle.Parameters.AddWithValue("@ornek", OrnekCumle);

                    int sonuc = cmdEkle.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Kayıt başarılı!");
                        EngTextBox.Text = "";
                        TurTextBox.Text = "";
                        ImageTextBox.Text = "";
                        richTextBox1.Text = "";
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

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen çalışılacak kelime sayısını seç.");
                return;
            }
            else
            {
                hedefSoruSayisi = int.Parse(comboBox1.SelectedItem.ToString()); // Seçilen sayı hedefe atanıyor

                tabControl1.SelectedTab = tabPage2;
                groupBox2.Visible = true;
                groupBox1.Visible = false;
                groupBoxAyarlar.Visible = false;
                progressBarSorular.Minimum = 0;
                progressBarSorular.Maximum = hedefSoruSayisi;
                progressBarSorular.Value = 0;

                mevcutSoruSayaci = 0; // Sayacı sıfırla ki düzgün saysın
                YeniSoruGetir(); // Soru başlat
            }


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

            // Karıştır 
            Random rnd = new Random();
            secenekler = secenekler.OrderBy(x => rnd.Next()).ToList();

            // Label İngilizce kelimeyi yaz
            labelKelime.Text = ingilizceKelime;
            Oku(ingilizceKelime);


            // Butonlara şıkları yerleştir
            buttonA.Text = secenekler[0];
            buttonB.Text = secenekler[1];
            buttonC.Text = secenekler[2];
            buttonD.Text = secenekler[3];

        }

        private void Oku(string kelime)
        {
            SpeechSynthesizer okuyucu = new SpeechSynthesizer();
            okuyucu.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            okuyucu.Rate = 0; // Hız (0 normal)
            okuyucu.Volume = 100; // Ses seviyesi
            okuyucu.SpeakAsync(kelime); // Asenkron şekilde okur
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
            if (mevcutSoruSayaci >= hedefSoruSayisi)
            {
                string mesaj =
                "🎉 *Test Tamamlandı!*\n\n" +
                $"✅ Doğru Cevap Sayısı: {dogruSayac}\n" +
                $"❌ Yanlış Cevap Sayısı: {yanlisSayac}\n\n" +
                "🔁 Yeni bir test başlatmak ister misin?";
                Oku(mesaj);
                DialogResult cevap = MessageBox.Show(mesaj, "🧠 Sonuçlar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    mevcutSoruSayaci = 0;
                    dogruSayac = 0;
                    yanlisSayac = 0;
                    YeniSoruGetir();
                }
                else
                {
                    this.Close();
                }
                return;
            }

            // burada veritabanından rastgele kelime çekme işlemi olacak
            mevcutSoruSayaci++;
            progressBarSorular.Value = mevcutSoruSayaci;
            labelSoruSayac.Text = $"Soru {mevcutSoruSayaci}/{hedefSoruSayisi}";

            string ingilizceKelime = "";
            List<string> secenekler = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                
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

            Random rnd = new Random();
            secenekler = secenekler.OrderBy(x => rnd.Next()).ToList();

            labelKelime.Text = ingilizceKelime;
            Oku(ingilizceKelime);
            buttonA.Text = secenekler[0];
            buttonB.Text = secenekler[1];
            buttonC.Text = secenekler[2];
            buttonD.Text = secenekler[3];
        }

        private async void buttonSecenek_Click(object sender, EventArgs e)
        {
            Button tiklananButon = sender as Button;

            // Doğru mu yanlış mı 
            if (tiklananButon.Text == dogruCevap)
            {
                tiklananButon.BackColor = Color.Green;
                dogruSayac++;

                Veritabanı.GuncelleKelimeIstatistik(labelKelime.Text);

                //using (SqlConnection conn = new SqlConnection(connectionString))
                //{
                //    conn.Open();

                //    // Kelimenin istatistikte olup olmadığını kontrol et
                //    string kontrolQuery = "SELECT COUNT(*) FROM KelimeIstatistik WHERE Kelime = @kelime";
                //    using (SqlCommand cmdKontrol = new SqlCommand(kontrolQuery, conn))
                //    {
                //        cmdKontrol.Parameters.AddWithValue("@kelime", labelKelime.Text);
                //        int kayitVarMi = (int)cmdKontrol.ExecuteScalar();

                //        if (kayitVarMi > 0)
                //        {
                //            // Varsa, DogruSayac değerini artır
                //            string updateQuery = "UPDATE KelimeIstatistik SET DogruSayac = DogruSayac + 1 WHERE Kelime = @kelime";
                //            using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                //            {
                //                cmdUpdate.Parameters.AddWithValue("@kelime", labelKelime.Text);
                //                cmdUpdate.ExecuteNonQuery();
                //            }
                //        }
                //        else
                //        {
                //            // Yoksa, yeni kayıt olarak ekle
                //            string insertQuery = "INSERT INTO KelimeIstatistik (Kelime, DogruSayac) VALUES (@kelime, 1)";
                //            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                //            {
                //                cmdInsert.Parameters.AddWithValue("@kelime", labelKelime.Text);
                //                cmdInsert.ExecuteNonQuery();
                //            }
                //        }
                //    }
                //}
            }
            else
            {
                tiklananButon.BackColor = Color.Red;
                yanlisSayac++;
                // yeşil yapalım
                foreach (var btn in new[] { buttonA, buttonB, buttonC, buttonD })
                {
                    if (btn.Text == dogruCevap)
                    {
                        btn.BackColor = Color.Green;
                        break;
                    }
                }
            }

            
            await Task.Delay(1000);
            YeniSoruGetir();

            
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
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ayarlar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            groupBoxAyarlar.Visible = true;
            groupBox2.Visible = false;
            groupBox1.Visible = false;
          
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 istatistikForm = new Form3();
            istatistikForm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.Hide();
            form5.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();

            // girisYapma
            girisYapma girisForm = new girisYapma();
            girisForm.Show();
        }
       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
