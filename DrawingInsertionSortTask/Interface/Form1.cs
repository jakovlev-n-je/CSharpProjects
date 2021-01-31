using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Implementation;

namespace Interface
{
    public partial class Form1 : Form
    {
        private Bitmap _bitmap;

        private Element[] _elements;

        private Graphics _graphicsBitmap;

        private Graphics _graphicsScreen;

        private Painter _painter;

        public Form1()
        {
            InitializeComponent();
            _graphicsBitmap = panel1.CreateGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _graphicsScreen = panel1.CreateGraphics();
            _bitmap = new Bitmap(panel1.Width, panel1.Height);
            _graphicsBitmap = Graphics.FromImage(_bitmap);
        }

        private async void async_Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            await Task.Run(() => button1_Click(sender, e));
            button2.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_elements != null)
            {
                ArrayUtils utils = new ArrayUtils(_painter);
                utils.Sort(_elements);
            }
            else
                ShowErrorBox("Отсутствует массив!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _elements = ArrayUtils.GenerateElements();
            _painter = new Painter(new PainterData(_bitmap, panel1.ClientRectangle, _graphicsBitmap, _graphicsScreen), _elements);
            _painter.Paint(0, 0);
            ShowInformationBox("Случайный массив был успешно отрисован!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _elements = null;
            panel1.CreateGraphics().Clear(panel1.BackColor);
            ShowInformationBox("Панель для отрисовки успешно очищенна!");
        }

        private void ShowInformationBox(string text)
        {
            MessageBox.Show(
                text,
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }

        private void ShowErrorBox(string text)
        {
            MessageBox.Show(
                text,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
        }
    }
}
