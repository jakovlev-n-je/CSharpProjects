using Implementation8;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Interface8
{
    public partial class Form8 : Form
    {
        private ClassInformation _classInformation;

        private Emulator _emulator;

        private Image _forkLoder, _amateurEquipment, _cheapEquipment, _professionalEquipment, _truck, _factoryFirst, _factorySecond, _factoryThird;

        private Thread _repaintThread;

        private List<Type> _types;

        public Form8()
        {
            InitializeImages();
            InitializeComponent();
            panel1.Paint += panel1_Paint;
            _classInformation = new ClassInformation(Directory.GetCurrentDirectory() + @"\Implementation8.dll");
            InitializeForklifts();
            _repaintThread = null;
        }

        private Image GetImage(int status, bool isFactory)
        {
            if (isFactory)
            {
                switch (status)
                {
                    case 0:
                        return _factoryFirst;
                    case 1:
                        return _factorySecond;
                    case 2:
                        return _factoryThird;
                    default:
                        throw new ArgumentException();
                }
            }
            else
            {
                Type type = _emulator.Truck.GetType();
                if (type == typeof(AmateurEquipment))
                {
                    return _amateurEquipment;
                }
                else if (type == typeof(CheapEquipment))
                {
                    return _cheapEquipment;
                }
                else
                {
                    return _professionalEquipment;
                }
            }
        }

        private void InitializeForklifts()
        {
            _types = _classInformation.GetTypesOfImplementingClasses(typeof(IEquipment));
            UpdateComboBox();
        }

        private void InitializeImages()
        {
            string basePath = Directory.GetCurrentDirectory() + @"\Resources\";
            _forkLoder = Image.FromFile(basePath + "forkLoader.png");
            _amateurEquipment = Image.FromFile(basePath + "amateurTruck.png");
            _cheapEquipment = Image.FromFile(basePath + "cheapTruck.png");
            _professionalEquipment = Image.FromFile(basePath + "professionalTruck.png");
            _truck = Image.FromFile(basePath + "truck.png");
            _factoryFirst = Image.FromFile(basePath + "factoryFirst.png");
            _factorySecond = Image.FromFile(basePath + "factorySecond.png");
            _factoryThird = Image.FromFile(basePath + "factoryThird.png");
        }

        private void PaintImage(Graphics graphics)
        {
            if (_emulator == null)
            {
                return;
            }
            if (_emulator.Truck.TruckInWarehouse())
            {
                graphics.DrawImage(_truck, _emulator.Truck.BaseCoordinates.X, _emulator.Truck.BaseCoordinates.Y, 100, 55);
            }
            else
            {
                graphics.DrawImage(GetImage(_emulator.Truck.ImageId, false), _emulator.Truck.NextCoordinates.X, _emulator.Truck.NextCoordinates.Y, 100, 55);
            }
            for (int i = 0; i < _emulator.ForkLoaders.Count; i++)
            {
                Image image = GetImage(_emulator.ForkLoaders[i].Factory.ImageId, true);
                if (_emulator.ForkLoaders[i].InWarehouse())
                {
                    graphics.DrawImage(_forkLoder, _emulator.ForkLoaders[i].BaseCoordinates.X, _emulator.ForkLoaders[i].BaseCoordinates.Y, 75, 50);
                }
                else
                {
                    graphics.DrawImage(_forkLoder, _emulator.ForkLoaders[i].NextCoordinates.X, _emulator.ForkLoaders[i].NextCoordinates.Y, 75, 50);
                }
                graphics.DrawImage(image, _emulator.ForkLoaders[i].Factory.Coordinates.X, _emulator.ForkLoaders[i].Factory.Coordinates.Y, 150, 157);
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
                Random random = new Random();
                IEquipment equipment = (IEquipment)Activator.CreateInstance(_types[comboBox1.SelectedIndex], new Coordinates(panel1.Width / 2 - 50, panel1.Height / 2, 20),
                    new Coordinates(panel1.Width / 2 - 50, panel1.Height / 2, 30));
                List<ForkLoader> forkLoaders = new List<ForkLoader>();
                for (int i = 0; i < Convert.ToInt32(numericUpDown1.Value); i++)
                {
                    forkLoaders.Add(new ForkLoader(new Coordinates(10 + 155 * i + 37, panel1.Height - 70, 20), new Coordinates(10 + 155 * i + 37, panel1.Height - 70, 20),
                                  new Factory(new Coordinates(10 + 155 * i, 20, 0), (int)numericUpDown2.Value, equipment.GetType(), random)));
                }
                _emulator = new Emulator(equipment, forkLoaders);
                _emulator.Run();
                _repaintThread = new Thread(PanelRepaint)
                {
                    IsBackground = true
                };
                _repaintThread.Start();
                button1.Text = "Закончить демонстрацию процесса";
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                _emulator.Abort();
                _repaintThread.Abort();
                _repaintThread = null;
                button1.Text = "Начать демонстрацию процесса";
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
