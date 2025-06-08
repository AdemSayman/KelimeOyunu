using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DotNetEnv;

namespace KelimeOyunu
{
    public partial class Form4 : Form
    {

        public Form4()
        {
            InitializeComponent();

            
            this.Resize += Form4_Resize;

            DotNetEnv.Env.Load();

            string openRouterApiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");
            string hfToken = Environment.GetEnvironmentVariable("HUGGINGFACE_TOKEN");
        }

        private void Form4_Resize(object sender, EventArgs e)
        {
            panelContainer.Left = (this.ClientSize.Width - panelContainer.Width) / 2;
            panelContainer.Top = (this.ClientSize.Height - panelContainer.Height) / 2;

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            this.StartPosition = FormStartPosition.CenterScreen; 
            this.Size = new Size(1000, 800); 
            this.MaximizeBox = false;
            panelContainer.Left = (this.ClientSize.Width - panelContainer.Width) / 2;
            panelContainer.Top = (this.ClientSize.Height - panelContainer.Height) / 2;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string kelimeler = hikayeTextBox.Text;
            string prompt = $"Write a short and fun story using these English words: {kelimeler}";

            try
            {
                string hikaye = await HikayeOlustur(prompt);
                richTextBox1.Text = hikaye;

                await ResimOlustur(prompt); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private async Task<string> HikayeOlustur(string prompt)
        {
            using var client = new HttpClient();
            string openRouterApiKey = EnvHelper.GetEnv("OPENROUTER_API_KEY");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openRouterApiKey);

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
            new { role = "user", content = prompt }
        }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }

        private void Base64ToPictureBox(string base64Image, PictureBox pb)
        {
            var base64Data = base64Image.Contains(",") ? base64Image.Substring(base64Image.IndexOf(",") + 1) : base64Image;

            byte[] bytes = Convert.FromBase64String(base64Data);
            using var ms = new System.IO.MemoryStream(bytes);
            pb.Image = Image.FromStream(ms);
        }
        private async Task ResimOlustur(string prompt)
        {
            using var client = new HttpClient();
            string deepaiApiKey = Environment.GetEnvironmentVariable("DEEP_AI");


            client.DefaultRequestHeaders.Add("api-key", deepaiApiKey);
            MessageBox.Show("KEY: " + deepaiApiKey);
            var body = new Dictionary<string, string>
            {
                { "text", prompt }
            };

            var content = new FormUrlEncodedContent(body);

            var response = await client.PostAsync("https://api.deepai.org/api/text2img", content);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("DeepAI isteği başarısız: " + response.StatusCode);
                return;
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var url = doc.RootElement.GetProperty("output_url").GetString();

            var imageBytes = await client.GetByteArrayAsync(url);
            using var ms = new MemoryStream(imageBytes);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void buttonCikis_Click(object sender, EventArgs e)
        {
            this.Close();

            Form2 frm2 = new Form2(10);
            frm2.Show();
        }
    }
}
