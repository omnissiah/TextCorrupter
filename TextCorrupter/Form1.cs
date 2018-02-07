using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; 
using System.Text; 
using System.Windows.Forms;

namespace TextCorrupter
{
    public partial class Form1 : Form
    {
        private String translatorFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "translatorLicence"); 
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            if (checkUsability())
            {
                txtModified.Text = SpecialCharToModified(txtOriginal.Text, chkIsFromRunic.Checked);
            }
            else
            {
                if (!string.IsNullOrEmpty(txtOriginal.Text) && txtOriginal.Text.Length > 5)
                {
                    MessageBox.Show(null, "This demo version can only convert 5 characters at a time. Obtain a license and place in: \"" + translatorFolder + "\" folder. :)", "No.", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                    txtModified.Text = SpecialCharToModified(txtOriginal.Text, chkIsFromRunic.Checked);
            }
        }

        private Boolean checkUsability()
        {  
            if (!Directory.Exists(translatorFolder))
                Directory.CreateDirectory(translatorFolder);

            string licenceFilePath=translatorFolder + @"\lic.txt";

            if (!File.Exists(licenceFilePath))
            {
                File.Create(licenceFilePath).Close();
            }

            using (StreamReader sr = new StreamReader(licenceFilePath, true))
            {
                string result = sr.ReadToEnd();
                if (result.Contains("this is a licence"))
                    return true;
            }  
            return false;
        }

        private static string SpecialCharToModified(string inString, Boolean decode)
        {

            String[] olds = { "T", "B", "S", "D", "H", "G", "A", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "y", "z", "ç", "ı", "ş", "ü", "ğ" };
            String[] news = { "ᛪ", "ᛟ", "ᛠ", "ᛢ", "ᛰ", "ᛯ", "ᛮ", "ᚠ", "ᚡ", "ᚢ", "ᚣ", "ᚤ", "ᚥ", "ᚦ", "ᚧ", "ᛥ", "ᚩ", "ᛤ", "ᚫ", "ᚰ", "ᚱ", "ᚲ", "ᚳ", "ᚴ", "ᚵ", "ᚶ", "ᚷ", "ᚸ", "ᚹ", "ᚻ", "ᚺ", "ᚻ", "ᛄ", "ᛉ", "ᛋ", "ᛁ" };

            for (int i = 0; i < olds.Length; i++)
            {
                if (!decode)
                    inString = inString.Replace(olds[i], news[i]);
                else
                    inString = inString.Replace(news[i], olds[i]);
            }

            return inString; 
        }   
    }
}
