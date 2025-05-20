using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KelimeOyunu
{
    public partial class Form3 : Form
    {
        

        public Form3()
        {
            InitializeComponent();
            YukleIstatistikler();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {


        }
        private void YukleIstatistikler()
        {
            DataTable dt = Veritabanı.GetKelimeIstatistik();
            dgvIstatistik.DataSource = dt;


            int toplam = Veritabanı.GetToplamKelimeSayisi();
            int ogrenilen = Veritabanı.GetOgrenilenKelimeSayisi();

            progressBar.Maximum = toplam;
            progressBar.Value = Math.Min(ogrenilen, toplam); // Hata almamak için
            lblSoruDurumu.Text = $"Öğrenilen: {ogrenilen} / {toplam}";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
