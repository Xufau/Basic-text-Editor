using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        string selectedfile = "";
        bool currentlysaved = false;
        int getposrelX;
        int getposrelY;
        int filecount = 0;
        bool corr = false;
        bool baksaving = true;
        bool mouseup = true;

        public Form1()
        {
            InitializeComponent();
            groupBox2.Location = new Point(287, 81);
            groupBox3.Location = new Point(323, 169);
        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            groupBox2.Visible = false;
            currentlysaved = false;
        }

        private void button4_Click(object sender, EventArgs e)//Open file
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\";
            ofd.Title = "Select file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedfile = ofd.FileName;
                richTextBox2.Text = System.IO.File.ReadAllText(selectedfile);
            }
        }

        private void button6_Click(object sender, EventArgs e)//Stats
        {
            updatestats();
            groupBox3.Visible = false;
            if (groupBox2.Visible == true)
            {
                groupBox2.Visible = false;
            }
            else if (groupBox2.Visible == false)
            {
                groupBox2.Visible = true;
            }
        }

        private void updatestats()
        {
            int words = 0;
            List<string> currentword = new List<string>();
            bool space = true;
            int Palindromecount = 0;
            string text = richTextBox2.Text + " ";
            int charcount;
            int vowelnum;
            List<string> vowels = new List<string>();
            vowels.Add("A");
            vowels.Add("a");
            vowels.Add("E");
            vowels.Add("e");
            vowels.Add("I");
            vowels.Add("i");
            vowels.Add("O");
            vowels.Add("o");
            vowels.Add("U");
            vowels.Add("u");
            vowelnum = 0;
            charcount = 0;

            foreach (char letter in text)
            {
                if (vowels.Contains(Convert.ToString(letter)))
                {
                    vowelnum++;
                }
                charcount++;
                if (char.IsWhiteSpace(letter))
                {
                    string word = "";
                    space = true;
                    string wordreversed = "";
                    for (int i = 0; i < currentword.Count; i++)
                    {
                        word = word + currentword[i];
                    }
                    for (int i = currentword.Count - 1; i >= 0; i--)
                    {
                        wordreversed = wordreversed + currentword[i];
                    }
                    if (word.Equals(wordreversed))
                    {
                        if(word.Length > 1)
                        {
                            Palindromecount++;
                        }
                    }
                    currentword.Clear();
                }
                else
                {
                    currentword.Add(Convert.ToString(letter));
                    if (space == true)
                    {
                        words++;
                    }
                    space = false;
                }
            }  
            label5.Text = ("Word count: " + (Convert.ToString(words)));
            label6.Text = ("Character count: " + (Convert.ToString(charcount - 1)));
            label7.Text = ("Palindrome count: " + (Convert.ToString(Palindromecount)));
            label8.Text = ("Vowel count: " + (Convert.ToString(vowelnum)));
        }

        private void button5_Click(object sender, EventArgs e)//Save
        {
            groupBox2.Visible = false;
            currentlysaved = true;
            save();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseup = false;
            getposrelX = e.Location.X;
            getposrelY = e.Location.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseup == false)
            {
                Location = new Point(e.Location.X - getposrelX + Location.X, e.Location.Y - getposrelY + Location.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseup = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            if(currentlysaved == true)
            {
                Application.Exit();
            }
            if(currentlysaved == false)
            {
                groupBox3.Visible = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)//savecheck
        {
            save();
            if(groupBox3.Visible == true)
            {
                Application.Exit();
            }
        }

        private void button10_Click(object sender, EventArgs e)//exitcheck
        {
            Application.Exit();
        }
        private void save()
        {
            if(selectedfile == "")
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = "unknown.txt";
                savefile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    selectedfile = savefile.FileName;
                    File.WriteAllText(savefile.FileName, richTextBox2.Text);
                }
            }

            if (selectedfile != "")
            {
                filecount++;
                if (baksaving == true)
                {
                    while (corr == false)
                    {
                        try
                        {
                            if (File.Exists(selectedfile))
                            {
                                File.Move(selectedfile, selectedfile + "(" + filecount + ")" + ".bak");
                                corr = true;
                            }
                        }
                        catch
                        {
                            filecount++;
                        }
                    }
                    corr = false;
                    File.WriteAllText(selectedfile, richTextBox2.Text);
                }
            }
        }

        private void richTextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void richTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            groupBox2.Visible = false;
            currentlysaved = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
