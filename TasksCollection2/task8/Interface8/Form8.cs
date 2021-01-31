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
        private ClassInformation _classInfo;

        private Emulator _emulator;

        private Image _mason, _lorry1, _lorry2, _lorry3, _lorry4,
                      _house1_1, _house1_2, _house1_3, _house1_4, _house1_5, _house1_6, _house1_7,
                      _house2_1, _house2_2, _house2_3, _house2_4, _house2_5, _house2_6, _house2_7,
                      _house3_1, _house3_2, _house3_3, _house3_4, _house3_5, _house3_6, _house3_7;

        private Thread _repaintThread;

        private List<Type> _types;

        public Form8()
        {
            InitializeImages();
            InitializeComponent();
            panel1.Paint += panel1_Paint;
            _classInfo = new ClassInformation(Directory.GetCurrentDirectory() + @"\Implementation8.dll");
            InitializeRoofTypes();
            _repaintThread = null;
        }

        private Image GetLorryImage()
        {
            Type type = _emulator.ConstructionMachinery.GetType();
            if (type == typeof(CorrugatedSheet))
            {
                return _lorry2;
            }
            else if (type == typeof(Ruberoid))
            {
                return _lorry3;
            }
            else
            {
                return _lorry4;
            }
        }

        private Image GetHouseImage(int imageId)
        {
            Type type = _emulator.ConstructionMachinery.GetType();
            if (type == typeof(CorrugatedSheet))
            {
                switch (imageId)
                {
                    case 2:
                        return _house1_2;
                    case 3:
                        return _house1_3;
                    case 4:
                        return _house1_4;
                    case 5:
                        return _house1_5;
                    case 6:
                        return _house1_6;
                    case 7:
                        return _house1_7;
                    default:
                        return _house1_1;
                }
            }
            else if (type == typeof(Ruberoid))
            {
                switch (imageId)
                {
                    case 2:
                        return _house2_2;
                    case 3:
                        return _house2_3;
                    case 4:
                        return _house2_4;
                    case 5:
                        return _house2_5;
                    case 6:
                        return _house2_6;
                    case 7:
                        return _house2_7;
                    default:
                        return _house2_1;
                }
            }
            else
            {
                switch (imageId)
                {
                    case 2:
                        return _house3_2;
                    case 3:
                        return _house3_3;
                    case 4:
                        return _house3_4;
                    case 5:
                        return _house3_5;
                    case 6:
                        return _house3_6;
                    case 7:
                        return _house3_7;
                    default:
                        return _house3_1;
                }
            }
        }

        private int GetHouseWidth(IConstructionMachinery constructionMachinery)
        {
            Type type = constructionMachinery.GetType();
            if (type == typeof(CorrugatedSheet))
            {
                return 159;
            }
            else if (type == typeof(Ruberoid))
            {
                return 246;
            }
            else
            {
                return 213;
            }
        }

        private int GetHouseHeight(IConstructionMachinery constructionMachinery)
        {
            Type type = constructionMachinery.GetType();
            if (type == typeof(CorrugatedSheet))
            {
                return 190;
            }
            else if (type == typeof(Ruberoid))
            {
                return 135;
            }
            else
            {
                return 175;
            }
        }

        private void InitializeRoofTypes()
        {
            _types = _classInfo.GetTypesOfImplementingClasses(typeof(IConstructionMachinery));
            UpdateComboBox();
        }

        private void InitializeImages()
        {
            string basePath = Directory.GetCurrentDirectory() + @"\Resources\";
            _mason = Image.FromFile(basePath + "mason.png");
            _lorry1 = Image.FromFile(basePath + "lorry.png");
            _lorry2 = Image.FromFile(basePath + "lorry_h1.png");
            _lorry3 = Image.FromFile(basePath + "lorry_h2.png");
            _lorry4 = Image.FromFile(basePath + "lorry_h3.png");
            _house1_1 = Image.FromFile(basePath + "house1.1.png");
            _house1_2 = Image.FromFile(basePath + "house1.2.png");
            _house1_3 = Image.FromFile(basePath + "house1.3.png");
            _house1_4 = Image.FromFile(basePath + "house1.4.png");
            _house1_5 = Image.FromFile(basePath + "house1.5.png");
            _house1_6 = Image.FromFile(basePath + "house1.6.png");
            _house1_7 = Image.FromFile(basePath + "house1.7.png");
            _house2_1 = Image.FromFile(basePath + "house2.1.png");
            _house2_2 = Image.FromFile(basePath + "house2.2.png");
            _house2_3 = Image.FromFile(basePath + "house2.3.png");
            _house2_4 = Image.FromFile(basePath + "house2.4.png");
            _house2_5 = Image.FromFile(basePath + "house2.5.png");
            _house2_6 = Image.FromFile(basePath + "house2.6.png");
            _house2_7 = Image.FromFile(basePath + "house2.7.png");
            _house3_1 = Image.FromFile(basePath + "house3.1.png");
            _house3_2 = Image.FromFile(basePath + "house3.2.png");
            _house3_3 = Image.FromFile(basePath + "house3.3.png");
            _house3_4 = Image.FromFile(basePath + "house3.4.png");
            _house3_5 = Image.FromFile(basePath + "house3.5.png");
            _house3_6 = Image.FromFile(basePath + "house3.6.png");
            _house3_7 = Image.FromFile(basePath + "house3.7.png");
        }

        private void PaintImage(Graphics graphics)
        {
            if (_emulator == null)
            {
                return;
            }
            if (_emulator.ConstructionMachinery.TruckInWarehouse())
            {
                graphics.DrawImage(_lorry1, _emulator.ConstructionMachinery.BaseCoordinates.X, _emulator.ConstructionMachinery.BaseCoordinates.Y, 100, 46);
            }
            else
            {
                graphics.DrawImage(GetLorryImage(), _emulator.ConstructionMachinery.NextCoordinates.X, _emulator.ConstructionMachinery.NextCoordinates.Y, 100, 46);
            }
            for (int i = 0; i < _emulator.Masons.Count; i++)
            {
                graphics.DrawImage(GetHouseImage(_emulator.Masons[i].House.ImageId), _emulator.Masons[i].House.Coordinates.X, _emulator.Masons[i].House.Coordinates.Y,
                                   GetHouseWidth(_emulator.ConstructionMachinery), GetHouseHeight(_emulator.ConstructionMachinery));
                if (_emulator.Masons[i].InHouse())
                {
                    graphics.DrawImage(_mason, _emulator.Masons[i].BaseCoordinates.X, _emulator.Masons[i].BaseCoordinates.Y, 43, 57);
                }
                else
                {
                    graphics.DrawImage(_mason, _emulator.Masons[i].NextCoordinates.X, _emulator.Masons[i].NextCoordinates.Y, 43, 57);
                }
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
                int count = Convert.ToInt32(textBox1.Text.Trim());
                if (count > 5)
                {
                    throw new ArgumentException();
                }
                Random random = new Random();
                IConstructionMachinery constructionMachinery = (IConstructionMachinery)Activator.CreateInstance(_types[comboBox1.SelectedIndex],
                    new Coordinates(panel1.Width - 150, panel1.Height / 2 + 20, 30),
                    new Coordinates(panel1.Width - 150, panel1.Height / 2 + 20, 30));
                List<Mason> masons = new List<Mason>();
                for (int i = 0; i < count; i++)
                {
                    masons.Add(new Mason(new Coordinates(10 + GetHouseWidth(constructionMachinery) * i + i * 30 + GetHouseWidth(constructionMachinery) / 2, panel1.Height - 70, 20),
                               new Coordinates(10 + GetHouseWidth(constructionMachinery) * i + i * 30 + GetHouseWidth(constructionMachinery) / 2, panel1.Height - 70, 20),
                               new House(new Coordinates(10 + GetHouseWidth(constructionMachinery) * i + i * 30, 20, 0), constructionMachinery.GetType(), random)));
                }
                _emulator = new Emulator(constructionMachinery, masons);
                _emulator.Run();
                _repaintThread = new Thread(PanelRepaint)
                {
                    IsBackground = true
                };
                _repaintThread.Start();
                button1.Text = "Закончить демонстрацию процесса";
                textBox1.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                _emulator.Abort();
                _repaintThread.Abort();
                _repaintThread = null;
                button1.Text = "Начать демонстрацию процесса";
                textBox1.Enabled = true;
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
