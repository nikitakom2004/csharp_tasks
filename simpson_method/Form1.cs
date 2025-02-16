using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate double F(double x);
        F fun;
        double I1,I2,a,b,h,x,p1,p2,p3,eps;
        int n;
        string FileName,str;

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 about = new Form2();
            about.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            a = Convert.ToDouble(textBox1.Text);
            b = Convert.ToDouble(textBox2.Text);
            eps = Convert.ToDouble(textBox3.Text);
            if (comboBox1.SelectedIndex == 0) fun = fun1;
            else if (comboBox1.SelectedIndex == 1) fun = fun2;
            else if (comboBox1.SelectedIndex == 2) fun = fun3;
            else if (comboBox1.SelectedIndex == 3) fun = fun4;
            else
            {
                MessageBox.Show("select function");
                return;
            }
            n = 2; h = (b - a) + 2;
            I2 = h * (fun(a) + 4 * fun(a + h) + fun(b)) / 3;
            for (;;)
            {
                I1 = I2; I2 = 0; n = 2 * n; h = h / 2;
                for (int i = 1; i <= n - 1; i = i + 2)
                {
                    x = a + i * h;
                    I2 = I2 + fun(x);
                }
                I2 = 2 * I2;

                for (int i = 2; i <= n - 2; i = i + 2)
                {
                    x = a + i * h;
                    I2 = I2 + fun(x);
                }
                I2 = h * (fun(a) + 2 * I2 + fun(b)) / 3;
                if (Math.Abs(I1 - I2) < eps) break;
            }
                
            textBox4.Text = textBox4.Text + "I=" + I2.ToString() + "\r\n";
            textBox4.Text = textBox4.Text + "Проверка\r\n";
            p1 = Math.Cos(a) - Math.Cos(b);
            p2 = Math.Sin(b) - Math.Sin(a);
            p3 = ((8 * b * b + b * b * b * b) / 4) - ((8 * a * a + a* a* a * a) / 4);
            if (comboBox1.SelectedIndex == 0) textBox4.Text = textBox4.Text + "I=" + p1.ToString() + "\r\n";
            else if (comboBox1.SelectedIndex == 1) textBox4.Text = textBox4.Text + "I=" + p2.ToString() + "\r\n";
            else if (comboBox1.SelectedIndex == 2) textBox4.Text = textBox4.Text + "I=" + p3.ToString() + "\r\n";
            
        }
            
            double fun1(double x) { return Math.Sin(x); }
            double fun2(double x) { return Math.Cos(x); }
            double fun3(double x) { return Math.Pow(x, 3) + 4 * x; }

        double fun4(double x) { return 5*Math.Pow(x,4)+x; }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
if(MessageBox.Show("Open the file?","MethodSimpson",MessageBoxButtons.OKCancel,MessageBoxIcon.Information)==DialogResult.OK)
button3_Click(null,null);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save the file?", "MethodSimpson", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                button2_Click(null, null);
            }
        
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Close the file","MethodSimpson", MessageBoxButtons.OKCancel,MessageBoxIcon.Information)==DialogResult.OK)
                Close();
            else
               return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text file]*.txt/All files|*.*";

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                    FileName = saveFileDialog1.FileName;
                    File.WriteAllText(FileName, textBox4.Text);
                }
            else
                {
                    MessageBox.Show("No file", "MethodSimpson");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stream myStream;
            openFileDialog1.Filter="Text file|*.txt|All files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    FileName = openFileDialog1.FileName;
                    myStream.Close();
                }
                else
                {
                    MessageBox.Show("No file", "MethodSimpson");
                    return;
                }
            }
            else
            {
                MessageBox.Show("No file", "MethodSimpson");
                return;
            }

            str = File.ReadAllText(FileName);
            textBox4.Text = str;
            Text  = "Path: " + FileName;
        }
    }
}
