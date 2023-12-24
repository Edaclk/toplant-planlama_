using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace toplantıplanlama_
{
    public partial class Form4 : Form
    {
       
        public Form4()
        {
            InitializeComponent();
            
        }
       

        private void button2_Click_1(object sender, EventArgs e)
        {
            DateTimePicker yeniDateTimePicker = new DateTimePicker();
            yeniDateTimePicker.MinDate = DateTime.Now.AddYears(-100);
            yeniDateTimePicker.MaxDate = DateTime.Now.AddYears(100);
            yeniDateTimePicker.Value = new DateTime(2000, 1, 1);
            yeniDateTimePicker.Location = new System.Drawing.Point(247, 20 * Controls.Count);
            yeniDateTimePicker.Format = DateTimePickerFormat.Short;
            yeniDateTimePicker.Name = "dateTimePicker" + Controls.Count;
            Controls.Add(yeniDateTimePicker);
            yeniDateTimePicker.ValueChanged += YeniDateTimePicker_ValueChanged;
        }
        private void YeniDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // Tüm DateTimePicker değerlerini bir dosyaya kaydet
            using (StreamWriter writer = new StreamWriter("dates.txt"))
            {
                foreach (Control control in Controls)
                {
                    if (control is DateTimePicker dateTimePicker)
                    {
                        writer.WriteLine(dateTimePicker.Value.ToString());
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Toplantınız Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Rasgele 4 haneli güvenlik kodu türet
            Random random = new Random();
            int securitycode = random.Next(1000, 10000);

            //güvenlik kodunu metin dosyasına kaydet
            SaveSecurityCodeToFile(securitycode.ToString());

            MessageBox.Show("Güvenlik Kodu Oluşturuldu: " + securitycode.ToString());
            // TextBox'lardan değerleri al
            string deger1 = textBox2.Text;
            string deger2 = textBox1.Text;

            // Değerleri metin dosyasına yaz
            WriteToFile(deger1, deger2);

            MessageBox.Show("Değerler başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void WriteToFile(string deger1, string deger2)
        {
            // Uygulamanın çalışma dizini
            string uygulamaDizini = AppDomain.CurrentDomain.BaseDirectory;

            // Dosya yolu oluştur
            string dosyaAdi = "File.txt";
            string dosyaYolu = Path.Combine(uygulamaDizini, dosyaAdi);

            try
            {
                // Değerleri dosyaya yaz
                using (StreamWriter writer = new StreamWriter(dosyaYolu))
                {
                    writer.WriteLine(deger1);
                    writer.WriteLine(deger2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya yazma hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

    }
            private void SaveSecurityCodeToFile(string code)
        {
            string filePath = "securityCode.txt";
            File.WriteAllText(filePath, code);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Sunucuyu başlat
            Thread serverThread = new Thread(new ThreadStart(Server.Instance.Start));
            serverThread.Start();

            // İstemciyi başlat
            Thread clientThread = new Thread(new ThreadStart(Client.Instance.Start));
            clientThread.Start();
        }
        

    }
}
