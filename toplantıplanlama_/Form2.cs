using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace toplantıplanlama_
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string mesaj;
        

        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = mesaj;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kod_Kontrol kod_Kontrol = new Kod_Kontrol();
            kod_Kontrol.Show();
            this.Hide();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            TopTarKesinleştirmeSayfası topTarKesinleştirmeSayfası = new TopTarKesinleştirmeSayfası();
            topTarKesinleştirmeSayfası.ShowDialog();
            this.Hide();
            
        }
    }
}
