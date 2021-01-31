using Implementation9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Interface9
{
    public partial class Form9 : Form
    {
        private ClassInformation _classInformation;

        private Emulator _emulator;

        private Image _forklift, _mechanic, _oilDerrickUp, _oilDerrickDown, _oilDerrickDownBurning, _oilDerrickBlownUp, _oilDerrickBroken;

        private Thread _repaintThread;

        private List<Type> _types;

        public Form9()
        {
            InitializeImages();
            InitializeComponent();
            panel1.Paint += panel1_Paint;
            _classInformation = new ClassInformation(Directory.GetCurrentDirectory() + @"\Implementation9.dll");
            InitializeForklifts();
            _repaintThread = null;
        }

        private Image GetImage(int status)
        {
            switch (status)
            {
                case 1:
                    return _oilDerrickDown;
                case 2:
                    return _oilDerrickUp;
                case 3:
                    return _oilDerrickDownBurning;
                case 4:
                    return _oilDerrickBlownUp;
                case 5:
                    return _oilDerrickBroken;
                default:
                    throw new ArgumentException();
            }
        }

        private void InitializeForklifts()
        {
            _types = _classInformation.GetTypesOfImplementingClasses(typeof(IForklift));
            UpdateComboBox();
        }

        private void InitializeImages()
        {
            string basePath = Directory.GetCurrentDirectory() + @"\Resources\";
            _forklift = Image.FromFile(basePath + "forklift.png");
            _mechanic = Image.FromFile(basePath + "mechanic.png");
            _oilDerrickUp = Image.FromFile(basePath + "oilDerrickUp.png");
            _oilDerrickDown = Image.FromFile(basePath + "oilDerrickDown.png");
            _oilDerrickDownBurning = Image.FromFile(basePath + "oilDerrickDownBurning.png");
            _oilDerrickBlownUp = Image.FromFile(basePath + "oilDerrickBlownUp.png");
            _oilDerrickBroken = Image.FromFile(basePath + "oilDerrickBroken.png");
        }

        private void PaintImage(Graphics graphics)
        {
            if (_emulator == null)
            {
                return;
            }
            bool inWarehouse = _emulator.Forklift.InWarehouse();
            graphics.DrawImage(_forklift, inWarehouse ? _emulator.Forklift.BaseCoordinates.X : _emulator.Forklift.NextCoordinates.X,
                                          inWarehouse ? _emulator.Forklift.BaseCoordinates.Y : _emulator.Forklift.NextCoordinates.Y, 81, 59);
            bool inHouse = _emulator.Mechanic.InHouse();
            graphics.DrawImage(_mechanic, inHouse ? _emulator.Mechanic.BaseCoordinates.X : _emulator.Mechanic.NextCoordinates.X,
                                          inHouse ? _emulator.Mechanic.BaseCoordinates.Y : _emulator.Mechanic.NextCoordinates.Y, 19, 27);
            for (int i = 0; i < _emulator.OilDerricks.Count; i++)
            {
                graphics.DrawImage(GetImage(_emulator.OilDerricks[i].Status), _emulator.OilDerricks[i].Coordinates.X, _emulator.OilDerricks[i].Coordinates.Y, 108, 108);

            }
        }

        private void PanelRepaint()
        {
            while (true)
            {
                Thread.Sleep(100);
                panel1.Invalidate();
            }
        }

        private void UpdateComboBox()
        {
            comboBox1.DataSource = null;
            List<string> typeNames = new List<string>();
            foreach (Type type in _types)
            {
                typeNames.Add(type.Name);
            }
            comboBox1.DataSource = typeNames;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_repaintThread == null)
            {
                IForklift forklift = (IForklift)Activator.CreateInstance(_types[comboBox1.SelectedIndex], new Coordinates(panel1.Width / 2 - 27, panel1.Height - 75, 30),
                    new Coordinates(panel1.Width / 2 - 27, panel1.Height - 75, 30));
                Mechanic mechanic = new Mechanic(new Coordinates(panel1.Width / 2 - 13, panel1.Height / 2 - 186, 15), new Coordinates(panel1.Width / 2 - 13, panel1.Height / 2 - 186, 15));
                Random random = new Random();
                List<OilDerrick> oilDerricks = new List<OilDerrick>();
                for (int i = 0; i < Convert.ToInt32(numericUpDown1.Value); i++)
                {
                    oilDerricks.Add(new OilDerrick(new Coordinates(17 + 124 * i, panel1.Height / 2 - 124, 0), (int)numericUpDown2.Value, random));

                }
                _emulator = new Emulator(forklift, mechanic, oilDerricks);
                _emulator.Run();
                _repaintThread = new Thread(PanelRepaint)
                {
                    IsBackground = true
                };
                _repaintThread.Start();
                button1.Text = "Завершить";
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                _emulator.Abort();
                _repaintThread.Abort();
                _repaintThread = null;
                button1.Text = "Запустить";
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                comboBox1.Enabled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = panel1.CreateGraphics();
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            Graphics bitmapGraphics = Graphics.FromImage(bitmap);
            bitmapGraphics.Clear(Color.White);
            PaintImage(bitmapGraphics);
            graphics.DrawImage(bitmap, 0, 0);
            bitmapGraphics.Dispose();
            bitmap.Dispose();
        }
    }
}
