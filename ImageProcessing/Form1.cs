using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) // Открытие файла
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) // Метод возвращает открытие файла. Проверка, открылся ли файл
            { 
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName); // Формирование картинке в boxe из взятого файла. Bitmap - двумерный массив из закрашенных пикселей. 
            }
        }
    }
}
