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
        private List<Bitmap> _images = new List<Bitmap>(); // Создание листа с хранящимися изображениями и его экземпляра

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

        private void trackBar1_Scroll(object sender, EventArgs e) // Скрол бар
        {
            // Text = trackBar1.Value.ToString();
            if (_images==null || _images.Count == 0) // Защита от ошибки отсутствия изображения
            {
                return;
            }
            pictureBox1.Image = _images[trackBar1.Value-1]; // Присваивание итерации боксу. Значение -1, тк лист начинается с 0, а сам скрол с 1
        }
    }
}
