using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace KelimeOyunu
{
    public partial class Form5 : Form
    {
        private int tahminSayisi = 0;
        private string gizliKelime = "";
        private List<string> ogrenilenKelimeler = new List<string>();
        private Random rnd = new Random();

        public Form5()
        {
            InitializeComponent();
            this.Load += Form5_Load;
            
        }

        private void Form5_Resize(object sender, EventArgs e)
        {
            
            

        }

        private void Form5_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.FixedDialog; // ya da FixedSingle, sana kalmış
            this.StartPosition = FormStartPosition.CenterScreen; // ortala ekran ortasında açılır
            this.Size = new Size(1000, 800); // istediğin boyut, mesela 800x600
            this.MaximizeBox = false;
            panelContainer.Left = (this.ClientSize.Width - panelContainer.Width) / 2;
            panelContainer.Top = (this.ClientSize.Height - panelContainer.Height) / 2;

            // Veritabanı bağlantısı (kendine göre ayarla)
            string connStr = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string sql = @"SELECT k.EngWordName
                           FROM KelimeIstatistik ki
                           JOIN Kelimeler k ON ki.KelimeId = k.KelimeId
                           WHERE LEN(k.EngWordName) = 5 AND ki.DogruSayisi >= 6";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ogrenilenKelimeler.Clear();
                        while (reader.Read())
                        {
                            ogrenilenKelimeler.Add(reader["EngWordName"].ToString().ToUpper());
                        }
                    }
                }

                if (ogrenilenKelimeler.Count > 0)
                {
                    gizliKelime = ogrenilenKelimeler[rnd.Next(ogrenilenKelimeler.Count)];
                    System.Diagnostics.Debug.WriteLine("Seçilen kelime: " + gizliKelime);
                }
                else
                {
                    MessageBox.Show("Öğrenilmiş 5 harfli kelime bulunamadı!");
                    btnTahmin.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                btnTahmin.Enabled = false;
            }
        }

        private void btnTahmin_Click(object sender, EventArgs e)
        {
            string tahmin = textBoxTahmin.Text.Trim().ToUpper();
            gizliKelime = gizliKelime.Trim().ToUpper();

            if (tahmin.Length != 5)
            {
                MessageBox.Show("5 harfli bir kelime gir!");
                return;
            }

            if (tahminSayisi >= 4)
            {
                MessageBox.Show("Tahmin hakkın bitti!");
                return;
            }



            char[] tahminChars = tahmin.ToCharArray();
            char[] gizliChars = gizliKelime.ToCharArray();

            Color[] renkler = new Color[5];
            bool[] gizliKullanildi = new bool[5];

            // 1. PASS → YEŞİL (doğru harf ve doğru yer)
            for (int i = 0; i < 5; i++)
            {
                if (tahminChars[i] == gizliChars[i])
                {
                    renkler[i] = Color.Green;
                    gizliKullanildi[i] = true;
                }
                else
                {
                    renkler[i] = Color.Gray; // Şimdilik gri, sonra belki sarıya döner
                }
            }

            // 2. PASS → SARI (doğru harf ama yanlış yer)
            for (int i = 0; i < 5; i++)
            {
                if (renkler[i] == Color.Green) continue; // Zaten doğru yerde

                for (int j = 0; j < 5; j++)
                {
                    if (!gizliKullanildi[j] && tahminChars[i] == gizliChars[j])
                    {
                        renkler[i] = Color.Gold;
                        gizliKullanildi[j] = true;
                        break;
                    }
                }
            }

            // UI'ya uygula (panel ve label'lara renklendirme)
            for (int i = 0; i < 5; i++)
            {
                string panelName = $"panel{tahminSayisi + 1}_{i + 1}";
                Panel pnl = this.Controls.Find(panelName, true).FirstOrDefault() as Panel;

                if (pnl != null)
                {
                    Label lbl = pnl.Controls.OfType<Label>().FirstOrDefault();
                    if (lbl != null)
                    {
                        lbl.Text = tahminChars[i].ToString();
                        lbl.ForeColor = Color.White;
                        lbl.TextAlign = ContentAlignment.MiddleCenter;
                        lbl.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                    }

                    pnl.BackColor = renkler[i];
                }
            }


            tahminSayisi++;

            if (tahmin == gizliKelime)
            {
                MessageBox.Show("Tebrikler! Doğru bildin!");
                btnTahmin.Enabled = false;
            }
            else if (tahminSayisi >= 4)
            {
                MessageBox.Show($"Kaybettin! Kelime: {gizliKelime}");
                btnTahmin.Enabled = false;
            }

            textBoxTahmin.Text = "";
            textBoxTahmin.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 frm2 = new Form2(10);
            frm2.Show();
        }

        private void btnTekrar_Click(object sender, EventArgs e)
        {
            tahminSayisi = 0;
            btnTahmin.Enabled = true;
            textBoxTahmin.Text = "";

            // Tüm panel ve label'ları temizle
            for (int satir = 1; satir <= 4; satir++)
            {
                for (int sutun = 1; sutun <= 5; sutun++)
                {
                    string panelName = $"panel{satir}_{sutun}";
                    Panel pnl = this.Controls.Find(panelName, true).FirstOrDefault() as Panel;

                    if (pnl != null)
                    {
                        pnl.BackColor = Color.Gainsboro; // Burayı değiştirdik
                        Label lbl = pnl.Controls.OfType<Label>().FirstOrDefault();
                        if (lbl != null)
                            lbl.Text = "";
                    }
                }
            }


            // Yeni kelimeyi rastgele çek
            try
            {
                string connStr = "Data Source=DESKTOP-A5JV8RA\\SQLEXPRESS;Initial Catalog=KelimeOyun_db;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"SELECT TOP 1 k.EngWordName
                           FROM KelimeIstatistik ki
                           JOIN Kelimeler k ON ki.KelimeId = k.KelimeId
                           WHERE LEN(k.EngWordName) = 5 AND ki.DogruSayisi >= 6
                           ORDER BY NEWID()";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            gizliKelime = reader["EngWordName"].ToString().ToUpper();
                            System.Diagnostics.Debug.WriteLine("Yeni kelime: " + gizliKelime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kelime çekilirken hata oluştu: " + ex.Message);
                btnTahmin.Enabled = false;
            }

            textBoxTahmin.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
