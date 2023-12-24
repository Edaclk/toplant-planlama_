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
using System.Globalization;




namespace toplantıplanlama_
{
    public partial class TopTarKesinleştirmeSayfası : Form
    {
        
        public TopTarKesinleştirmeSayfası()
        {
            InitializeComponent();

        }



        private void TopTarKesinleştirmeSayfası_Load(object sender, EventArgs e)
        {
            // Kaydedilen tarihi oku
            string selectedDateFilePath = "selectedDate.txt";

            if (File.Exists(selectedDateFilePath))
            {
                try
                {
                    // Dosyadan tarihi oku
                    string selectedDate = File.ReadAllText(selectedDateFilePath);

                    // Tarihi DateTimePicker kontrolüne yansıt
                    if (DateTime.TryParseExact(selectedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDateTime))
                    {
                        dateTimePicker1.Value = selectedDateTime;
                        dateTimePicker1.Format = DateTimePickerFormat.Short;
                    }
                    else if (DateTime.TryParse(selectedDate, CultureInfo.GetCultureInfo("tr-TR"), DateTimeStyles.None, out selectedDateTime))
                    {
                        // İlk format başarısız olursa, Türk kültürü ile çözümleme deneyin
                        dateTimePicker1.Value = selectedDateTime;
                        dateTimePicker1.Format = DateTimePickerFormat.Short;
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz tarih formatı: " + selectedDate);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dosya okuma hatası: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seçilen tarih dosyası bulunamadı.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

    }
    }
    
    
    

