using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Unicode2Code
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int code = 0;

            string str1 =richTextBox1.Text;
            string str2 = "";
            StringInfo ss = new StringInfo(str1);
            int m = ss.LengthInTextElements;//字符长度

            string word = "";
            for (int i = 0; i < m; i++)
            {
                word = ss.SubstringByTextElements(i, 1);
                    code = Char.ConvertToUtf32(word, 0);
                    str2 = str2+ "\\" + Convert.ToString(code,16);

            }
            richTextBox2.Text = str2;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int code = 0;

            string str1 = richTextBox1.Text;
            string str2 = "";
            str1 = str1.Replace("\n", "");
            str1 = str1.Replace("\u200b", "");
            StringInfo ss = new StringInfo(str1);

            int m = ss.LengthInTextElements;//字符长度

            string word = "";
            string strKey = "";
            for (int i = 0; i < m; i++)
            {
                word = ss.SubstringByTextElements(i, 1);

                if (word == "\\")
                {
                    if (strKey != "")
                    {
                        try
                        {
                            code = Convert.ToInt32(strKey, 16);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("有非法字符！");
                        }

                        str2 = str2 + Char.ConvertFromUtf32(code);
                    }
                    strKey = "";
                }
                else
                {
                    strKey += word;
                }
            }
            if (strKey != "")
            {
                try { 
                code = Convert.ToInt32(strKey, 16);
                }
                catch (FormatException)
                {
                    MessageBox.Show("有非法字符！");
                }
                str2 = str2 + Char.ConvertFromUtf32(code);

            }

            richTextBox2.Text = str2;

        }
    }
}
