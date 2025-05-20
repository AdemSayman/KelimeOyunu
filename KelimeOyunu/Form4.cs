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

            DotNetEnv.Env.Load();

            string openRouterApiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");
            string hfToken = Environment.GetEnvironmentVariable("HUGGINGFACE_TOKEN");
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string kelimeler = hikayeTextBox.Text;
            string prompt = $"Write a short and fun story using these English words: {kelimeler}";

            try
            {
                string hikaye = await HikayeOlustur(prompt);
                richTextBox1.Text = hikaye;

                string jsonResponse = await ResimOlustur(prompt);

                
                var base64Array = JsonSerializer.Deserialize<string[]>(jsonResponse);

                if (base64Array != null && base64Array.Length > 0)
                {
                    Base64ToPictureBox(base64Array[0], pictureBox1);
                }
                else
                {
                    MessageBox.Show("Resim oluşturulamadı.");
                }
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
        private async Task<string> ResimOlustur(string prompt)
        {
            using var client = new HttpClient();
            string huggingFaceApiKey = EnvHelper.GetEnv("HUGGINGFACE_TOKEN");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", huggingFaceApiKey);

            var requestBody = new
            {
                inputs = prompt,
                options = new { wait_for_model = true }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api-inference.huggingface.co/models/stabilityai/stable-diffusion-3.5-large", content);
            var json = await response.Content.ReadAsStringAsync();

            var imageUrls = JsonSerializer.Deserialize<string[]>(json);

            if (imageUrls != null && imageUrls.Length > 0)
            {
                var imageUrl = imageUrls[0];
                var imageBytes = await client.GetByteArrayAsync(imageUrl);
                using var ms = new MemoryStream(imageBytes);
                var image = Image.FromStream(ms);
                pictureBox1.Image = image;

                return "Resim oluşturuldu.";
            }
            else
            {
                return "Resim oluşturulamadı.";
            }
        }
    }
}
