using Impementation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Interface
{
    public partial class Form1 : Form
    {
        private List<Point> Points { get; set; } = new List<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(numericUpDown1.Value);
            int y = Convert.ToInt32(numericUpDown2.Value);
            Point point = new Point(x, y);
            if (!checkUnityPoint(point))
            {
                MessageBox.Show(
                "Данная точке уже существует!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            Points.Add(point);
            listBox1.Items.Insert(Points.Count - 1, point);
            if (Points.Count >= 2)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }

            MessageBox.Show(
                "Точка была успешно добавлена!",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            Points.RemoveAt(index);
            listBox1.Items.RemoveAt(index);
            if (Points.Count >= 2)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }

            MessageBox.Show(
               "Точка была успешно удалена!",
               "Сообщение",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DrawUtils.Draw(Points, panel1.CreateGraphics());
            button4.Enabled = true;

            MessageBox.Show(
               "Кривая Безье была успешно нарисована!",
               "Сообщение",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.CreateGraphics().Clear(Color.White);
            button4.Enabled = false;
            if (Points.Count >= 2)
            {
                button3.Enabled = true;
            }

            MessageBox.Show(
               "Панель для отрисовки была успешно очищена!",
               "Сообщение",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private bool checkUnityPoint(Point point)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                int x = point.X;
                int y = point.Y;
                if (Points[i].X == x && Points[i].Y == y)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
