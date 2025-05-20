
namespace KelimeOyunu
{
    partial class Form3
    {
        /// <summary>
        /// Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        /// <param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Tasarımcı desteği için gerekli yöntem - 
        /// Bu yöntemin içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            dgvIstatistik = new DataGridView();
            progressBar = new ProgressBar();
            lblSoruDurumu = new Label();
            btnKapat = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvIstatistik).BeginInit();
            SuspendLayout();
            // 
            // dgvIstatistik
            // 
            dgvIstatistik.AllowUserToAddRows = false;
            dgvIstatistik.AllowUserToDeleteRows = false;
            dgvIstatistik.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIstatistik.Location = new Point(14, 13);
            dgvIstatistik.Margin = new Padding(3, 4, 3, 4);
            dgvIstatistik.Name = "dgvIstatistik";
            dgvIstatistik.ReadOnly = true;
            dgvIstatistik.RowHeadersWidth = 51;
            dgvIstatistik.Size = new Size(754, 400);
            dgvIstatistik.TabIndex = 0;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(14, 440);
            progressBar.Margin = new Padding(3, 4, 3, 4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(571, 31);
            progressBar.TabIndex = 1;
            // 
            // lblSoruDurumu
            // 
            lblSoruDurumu.AutoSize = true;
            lblSoruDurumu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSoruDurumu.Location = new Point(594, 440);
            lblSoruDurumu.Name = "lblSoruDurumu";
            lblSoruDurumu.Size = new Size(149, 23);
            lblSoruDurumu.TabIndex = 2;
            lblSoruDurumu.Text = "0 / 0 soru tamam";
            // 
            // btnKapat
            // 
            btnKapat.Location = new Point(682, 487);
            btnKapat.Margin = new Padding(3, 4, 3, 4);
            btnKapat.Name = "btnKapat";
            btnKapat.Size = new Size(86, 40);
            btnKapat.TabIndex = 3;
            btnKapat.Text = "Kapat";
            btnKapat.UseVisualStyleBackColor = true;
            btnKapat.Click += btnKapat_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 548);
            Controls.Add(btnKapat);
            Controls.Add(lblSoruDurumu);
            Controls.Add(progressBar);
            Controls.Add(dgvIstatistik);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form3";
            StartPosition = FormStartPosition.CenterParent;
            Text = "İstatistik Ekranı";
            ((System.ComponentModel.ISupportInitialize)dgvIstatistik).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["Form2"].Show();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvIstatistik;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblSoruDurumu;
        private System.Windows.Forms.Button btnKapat;
    }
}