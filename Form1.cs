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

namespace Мокг_лб_вкладки
{
    public partial class Form1 : Form
    {
        double x, xn, xk, xh, a,fx, min, max;
        int N;
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            N = (int)Math.Ceiling((Convert.ToDouble(textBox2.Text) - Convert.ToDouble(textBox1.Text)) / Convert.ToDouble(textBox3.Text));
            N++;
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            string filename = saveFileDialog1.FileName;
            for (int i = 0; i < N; i++)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filename, true))
                    {
                        writer.Write(Convert.ToString(dataGridView1[0, i].Value) + "    " + Convert.ToString(dataGridView1[1, i].Value) + "\n");
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void сохранитьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = " *.bmp | *.bmp;| *.png | *.png;| *.jpg | *.jpg| All files(*.*) | *.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            string filename = saveFileDialog1.FileName;
            
            switch (saveFileDialog1.FilterIndex)
            {
                case 1: chart1.SaveImage(filename, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Bmp); break;
                case 2: chart1.SaveImage(filename, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png); break;
                case 3: chart1.SaveImage(filename, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Jpeg); break;
            }
        }

        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            int i = 0, count = 1;
            while (i < fileText.Length)
            {
                if (count == 1)
                {
                    if (fileText[i] != ' ' && fileText[i] != '\n')
                    {
                        textBox1.Text += fileText[i];
                    }
                    else
                    {
                        count=2;i++;
                    }
                }
                if (count == 2)
                {
                    if (fileText[i] != ' ' && fileText[i] != '\n' )
                    {
                        textBox2.Text += fileText[i];
                    }
                    else
                    {
                        count=3;i++;
                    }
                }
                if (count == 3)
                {
                    if (fileText[i] != ' ' && fileText[i] != '\n')
                    { 
                        textBox3.Text += fileText[i];
                    }
                    else
                    {
                        count=4;i++;
                    }
                }
                if (count == 4)
                {
                    if (fileText[i] != ' ' || fileText[i] != '\n' && fileText[i] != Convert.ToChar(""))
                    {
                        textBox4.Text += fileText[i];
                    }
                    else
                    {
                        count++;
                    }
                }
                i++;
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int n;
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                
                xn = Convert.ToDouble(textBox1.Text);
                xk = Convert.ToDouble(textBox2.Text);
                xh = Convert.ToDouble(textBox3.Text);
                a = Convert.ToDouble(textBox4.Text);
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 2;
                n = (int)Math.Ceiling((xk - xn) / xh);
                dataGridView1.Rows.Add(n);
                dataGridView1.Columns[0].Name = "   X";
                dataGridView1.Columns[1].Name = "   Y";
                x = xn;
                min = 1;
                max = Math.Pow(x, 4.0) + 2 * Math.Pow(x, 3.0) - x;
                int k = 0;
                while (x <= xk)
                {
                    if (x < 0)
                    {
                        fx = Math.Pow(x, 4.0) + 2 * Math.Pow(x, 3.0) - x;
                    }
                    else if (x > 0 && x < a)
                    {
                        fx = Math.Exp(-x) + Math.Pow(x, 0.25);
                    }
                    else if (x > a)
                    {
                        fx = Math.Log(Math.Pow(x, 3.0) + Math.Pow(x, 2.0));
                    }
                    dataGridView1[0, k].Value = x;
                    dataGridView1[1, k].Value = fx;
                    chart1.Series[0].Points.AddXY(x, fx);
                    if (fx > max) max = fx;
                    if (fx < min) min = fx;
                    x += xh;
                    k++;
                }
                textBox5.Text = Convert.ToString(max);
                textBox6.Text = Convert.ToString(min);
            }
            else MessageBox.Show("Заполните все поля!");
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.Font = dataGridView1.Font;
            fontDialog1.Color = dataGridView1.ForeColor;
            if(fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                dataGridView1.Font = fontDialog1.Font;
                dataGridView1.ForeColor = fontDialog1.Color;
            }
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.BackgroundColor = colorDialog1.Color;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int i;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
    }
}
