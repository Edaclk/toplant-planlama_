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
    public partial class TopTarBelirleme : Form
    {
        public TopTarBelirleme()
        {
            InitializeComponent();
        }

        private void TopTarBelirleme_Load(object sender, EventArgs e)
        {
            //Metni Dosyadan Oku ve labele yaz
            string readText = ReadTextFromFile();

            if( !string.IsNullOrEmpty( readText ) )
            {
                label1.Text = readText;
            }
            
        }
        private string ReadTextFromFile()
        {
            string filePath = "textFile.txt";
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return null;
        }
    }
}
