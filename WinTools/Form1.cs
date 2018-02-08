using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace WinTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string pat = "*";
            string path = textBox1.Text.Trim();
            string[] arrfileName = Directory.GetFiles(path, pat);
            foreach (string item in arrfileName)
            {
                string str = item.Replace(path + @"\", "");
                listBox1.Items.Add(str);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var items = listBox1.Items;
            foreach (var item in items)
            {
                string oldname = item.ToString();
                string newname = oldname.Replace(textBox2.Text.Trim(), "");
                modifyFileName(oldname, newname);
            }
        }
        void modifyFileName(string oldname, string newname)
        {
            try
            {
                if (File.Exists(textBox1.Text.Trim() + @"\" + oldname))
                {
                    FileInfo fi = new FileInfo(textBox1.Text.Trim() + @"\" + oldname);
                    fi.MoveTo(textBox1.Text.Trim() + @"\" + newname);
                }
            }
            catch { }
        }

        ChineseChangeToNumber chinese = new ChineseChangeToNumber();
        
        private void button4_Click(object sender, EventArgs e)
        {
            var items = listBox1.Items;
            foreach (var item in items)
            {
                string oldname = item.ToString();
                string type = oldname.Substring(oldname.LastIndexOf('.'));
                string newname = oldname.Replace("第", "").Replace("讲", "").Replace(type, "");
                try
                {
                  int number = chinese.ChineseToNumber(newname);
                    modifyFileName(oldname, "第" + number+ "讲" + type);
                }
                catch { }
            }

        }
    }


    public class ChineseChangeToNumber
    {
        Tools tool = new Tools();
        public int ChineseToNumber(String str)
        {
            String str1 = "";
            String str2 = "";
            String str3 = "";
            int k = 0;
            bool dealflag = true;
            for (int i = 0; i < str.Length; i++)
            {//先把字符串中的“零”除去
                if ('零' == str.ToCharArray()[i])
                {
                    str = str.Substring(0, i) + str.Substring(i + 1);
                }
            }

            String chineseNum = str;
            for (int i = 0; i < chineseNum.Length; i++)
            {
                if (chineseNum.ToCharArray()[i] == '亿')
                {
                    str1 = chineseNum.Substring(0, i);//截取亿前面的数字，逐个对照表格，然后转换
                    k = i + 1;
                    dealflag = false;//已经处理
                }
                if (chineseNum.ToCharArray()[i] == '万')
                {
                    str2 = chineseNum.Substring(k, i);
                    str3 = str.Substring(i + 1);
                    dealflag = false;//已经处理
                }
            }
            if (dealflag)
            {//如果没有处理
                str3 = chineseNum;
            }
            int result = sectionChinese(str1) * 100000000 +
                    sectionChinese(str2) * 10000 + sectionChinese(str3);
            return result;
        }

        public int sectionChinese(String str)
        {
            int value = 0;
            int sectionNum = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int v = (int)tool.intList[str.ToCharArray()[i]];
                if (v == 10 || v == 100 || v == 1000)
                {//如果数值是权位则相乘
                    sectionNum = v * sectionNum;
                    value = value + sectionNum;
                }
                else if (i == str.Length - 1)
                {
                    value = value + v;
                }
                else
                {
                    sectionNum = v;
                }
            }
            return value;
        }
    }

    public class Tools
    {
        //数字位  
        public  String[] chnNumChar = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        public  char[] chnNumChinese = { '零', '一', '二', '三', '四', '五', '六', '七', '八', '九' };
        //节权位  
        public  String[] chnUnitSection = { "", "万", "亿", "万亿" };
        //权位  
        public  String[] chnUnitChar = { "", "十", "百", "千" };
        public  Dictionary<char, int> intList = new Dictionary<char, int>();

        public Tools()
        {
            for (int i = 0; i < chnNumChar.Length; i++)
            {
                intList.Add(chnNumChinese[i], i);
            }
            intList.Add('十', 10);
            intList.Add('百', 100);
            intList.Add('千', 1000);
        }


    }



}
