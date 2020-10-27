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
        private Random _random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) // Открытие файла
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) // Метод возвращает открытие файла. Проверка, открылся ли файл
            {
                pictureBox1.Image = null; // удаление старой картинки. 
                _images.Clear(); // Непосредственно очистка
                var image = new Bitmap(openFileDialog1.FileName); // Открытие файла-картинки в новой переменной. Bitmap - двумерный массив из закрашенных пикселей. 
                Processing(image);
            }
        }

        private void Processing(Bitmap image) // Обработка картинки
        {
            var elements = GetElements(image); // Возвращает лист пикселей
            var step = (image.Width * image.Height) / 100; // Подсчет количества пикселей в 1 проценте ( итерации )
            var currentSet = new List<Element>(elements.Count - step); // Текущее состояние элементов

            for (int i = 1; i < trackBar1.Maximum; i++) // Выбор случайных элементов = процент. Отсчет с 1, так как последняя итерация показывает всю картинку
            {
                for (int j = 0; j < step; j++) // Для каждой итерации - количество пикселей в шаге
                {
                    var index = _random.Next(elements.Count); // Генератор случайных чисел от 0 до кол.ва пикселей, который остались
                    currentSet.Add(elements[index]); // Добавление элемента из всех пикселей под индексом
                    elements.RemoveAt(index); // Удаление пикселя из общего кол.ва, тк он уже присутствует в картинке
                }
                var currentImage = new Bitmap(image.Width, image.Height); // Создание нового изображения 

                foreach (var Element in currentSet)
                    currentImage.SetPixel(Element.Point.X, Element.Point.Y, Element.Color); // Установка пикселей в нужную точку и согласно нужному цвету
                    _images.Add(currentImage); // Добавление нового изображения в коллекцию 

                Text = $"{i} %";
            }
            _images.Add(image); //Отображение всей картинки при 100%
        }

        private List<Element> GetElements(Bitmap image) // Возвращение списка элементов. Задачей является взять информацию о каждом пикселе
        {
            var elements = new List<Element>(image.Width * image.Height); // элементов массива лист столько, сколько пикселей в картинке. Сильная экономия памяти

            for (int y = 0; y < image.Height; y++) // Анализ каждого пикселя
            {
                for (int x = 0; x < image.Width; x++)
                {
                    elements.Add(new Element() // Добавляется новый объект класса элемент 
                    {
                        Color = image.GetPixel(x, y), // Извлечение координаты
                        Point = new Point() { X = x, Y = y} // Хранение полученной координаты в точке
                    }) ;
                }
            }
            return elements;
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
