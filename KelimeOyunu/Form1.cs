namespace KelimeOyunu
{
    public partial class girisYapma : Form
    {
        Form2 form2 = new Form2();
        

        public girisYapma()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            form2.Show();
            this.Hide();
        }
    }
}
