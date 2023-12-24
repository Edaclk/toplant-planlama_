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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Threading;

namespace toplantıplanlama_
{
    public partial class Form5 : Form
    {
        
        public Form5()
        {
            InitializeComponent();

            
        }

        

        private void Form5_Load(object sender, EventArgs e)
        {
            List<DateTime> dateList = ReadDatesFromFile("dates.txt");

            if (dateList.Count > 0)
            {
                for (int i = 0; i < dateList.Count; i++)
                {
                    AddDateTimePickerAndSetValue(dateList[i]);

                }
            }
            else
            {
                MessageBox.Show("Tarih dosyası boş veya geçersiz.");
            }
            // Metin dosyasındaki değerleri oku ve Label'lara yaz
            ReadFromFile();

        }
        private void ReadFromFile()
        {
            // Okunacak metin dosyasının yolu
            string dosyaYolu = Path.Combine(Application.StartupPath, "File.txt");

            // Dosya var mı kontrol et
            if (File.Exists(dosyaYolu))
            {
                // Dosyadan değerleri oku
                string[] lines = File.ReadAllLines(dosyaYolu);

                // Label'lara değerleri yaz
                if (lines.Length >= 2)
                {
                    label3.Text = lines[0];
                    label5.Text = lines[1];
                }
            }
            else
            {
                MessageBox.Show("Dosya bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<DateTime> ReadDatesFromFile(string filePath)
        {

            List<DateTime> dateList = new List<DateTime>();

            if (File.Exists(filePath))
            {
                try
                {
                    // Dosyadan tarihleri oku
                    string[] lines = File.ReadAllLines(filePath);

                    // Her satırdaki tarihi DateTime tipine çevir ve listeye ekle
                    foreach (string line in lines)
                    {
                        if (DateTime.TryParse(line, out DateTime date))
                        {
                            dateList.Add(date);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dosya okuma hatası: " + ex.Message);
                }
            }

            return dateList;

           
        }
        

        private void AddDateTimePickerAndSetValue(DateTime date)
        {
            // Yeni DateTimePicker oluştur
            DateTimePicker yeniDateTimePicker = new DateTimePicker();
            yeniDateTimePicker.Location = new System.Drawing.Point(336, 40 * Controls.Count);
            yeniDateTimePicker.Format = DateTimePickerFormat.Short;
            
            yeniDateTimePicker.Name = "dateTimePicker" + Controls.Count;
            Controls.Add(yeniDateTimePicker);
            
            yeniDateTimePicker.ShowCheckBox = true;

            // DateTimePicker'ın değerini belirtilen tarihe ayarla
            yeniDateTimePicker.Value = date;

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
                        // Dosyaya yazarken belirli bir tarih formatını kullan
                        writer.WriteLine(dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
        }
        


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Toplantınız Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
       


        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
    }

        

