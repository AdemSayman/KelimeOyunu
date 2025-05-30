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
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;


namespace KelimeOyunu
{
    public partial class Form3 : Form
    {


        public Form3()
        {
            InitializeComponent();
            YukleIstatistikler();

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Veritabanından veri çekme kodun...

            // Stil ayarları
            
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

        private void btnDisari_Click(object sender, EventArgs e)
        {
            Document doc = new Document();
            Section section = doc.AddSection();

            Paragraph paragraph = section.AddParagraph("İstatistik Raporu");
            paragraph.Format.Font.Name = "Segoe UI";
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Bold = true;

            // Örnek tablo (DataGridView'deki verilerle doldur)
            Table table = section.AddTable();
            table.Borders.Width = 0.75;

            // Sütunlar
            foreach (DataGridViewColumn col in dgvIstatistik.Columns)
            {
                Column column = table.AddColumn(Unit.FromCentimeter(4));
                column.Format.Alignment = ParagraphAlignment.Left;
            }

            // Başlık satırı
            Row headerRow = table.AddRow();
            headerRow.Shading.Color = Colors.LightGray;
            for (int i = 0; i < dgvIstatistik.Columns.Count; i++)
            {
                headerRow.Cells[i].AddParagraph(dgvIstatistik.Columns[i].HeaderText);
                headerRow.Cells[i].Format.Font.Bold = true;
            }

            // Veriler
            foreach (DataGridViewRow dgvRow in dgvIstatistik.Rows)
            {
                if (dgvRow.IsNewRow) continue;

                Row row = table.AddRow();
                for (int i = 0; i < dgvIstatistik.Columns.Count; i++)
                {
                    row.Cells[i].AddParagraph(dgvRow.Cells[i].Value?.ToString() ?? "");
                }
            }

            // PDF oluştur
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
            renderer.Document = doc;
            renderer.RenderDocument();

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "IstatistikRaporu.pdf");
            renderer.PdfDocument.Save(path);

            MessageBox.Show("PDF dışa aktarıldı:\n" + path);
        }
    }
}
