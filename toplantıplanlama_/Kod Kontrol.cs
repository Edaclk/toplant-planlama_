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
using System.Diagnostics.Eventing.Reader;

namespace toplantıplanlama_
{
    public partial class Kod_Kontrol : Form
    {
        public Kod_Kontrol()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            



            //Güvenlik kodunu metin dosyasına oku
            string storedCode = ReadSecurityCodeFromFile();

            if(storedCode != null)
            {
                //kullanıcının girdiği güvenlik kodunu al
                string enteredcode = textBox1.Text.Trim();

                // güvenlik kodunu kontrol et
                if(enteredcode ==  storedCode)
                {
                    MessageBox.Show("Güvenlik Kodu Doğru");
                    Form5 form5 = new Form5();
                    form5.Show();
                    this.Hide();
                }
                else  
                {
                    MessageBox.Show("Güvenlik Kodu Yanlış");

                }
                
            }
            else
            {
                MessageBox.Show("Güvenlik Kodu Bulunmadı");
            }
            
        }
        
        private string ReadSecurityCodeFromFile()
        {
            string filePath = "securityCode.txt";
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return null;
        }
    }
}
