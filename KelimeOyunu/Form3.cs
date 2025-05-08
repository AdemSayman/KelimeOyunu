using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KelimeOyunu
{
    public partial class Form3 : Form
    {
        Form2 form2Ref;

        public Form3(Form2 gelenForm)
        {
            form2Ref = gelenForm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form2Ref.Show();
            this.Close();
        }
    }
}
