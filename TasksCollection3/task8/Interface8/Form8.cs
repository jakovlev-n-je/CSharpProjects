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
        private ReflectionInfo _reflectionInfo;

        private ProcessEmulator _processEmulator;

        private Image _builder, _car1, _car2, _car3, _car4,
                      _house1_1, _house1_2, _house1_3, _house1_4, _house1_5,
                      _house2_1, _house2_2, _house2_3, _house2_4, _house2_5,
                      _house3_1, _house3_2, _house3_3, _house3_4, _house3_5;

        private Thread _repaintThread;

        private List<Type> _types;

        public Form8()
        {
            InitializeImages();
            InitializeComponent();
            panel1.Paint += panel1_Paint;
            _reflectionInfo = new ReflectionInfo(Directory.GetCurrentDirectory() + @"\Implementation8.dll");
            InitializeRoofTypes();
            _repaintThread = null;
        }

        private Image GetLorryImage()
        {
            Type type = _processEmulator.ConstructionEquipment.GetType();
            if (type == typeof(MetalTiles))
            {
                return _car2;
            }
            else if (type == typeof(Ondulin))
            {
                return _car3;
            }
            else
            {
                return _car4;
            }
        }

        private Image GetHouseImage(int imageId)
        {
            Type type = _processEmulator.ConstructionEquipment.GetType();
            if (type == typeof(MetalTiles))
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
                    default:
                        return _house1_1;
                }
            }
            else if (type == typeof(Ondulin))
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
                    default:
                        return _house3_1;
                }
            }
        }

        private int GetHouseWidth(IConstructionEquipment constructionEquipment)
        {
            Type type = constructionEquipment.GetType();
            if (type == typeof(MetalTiles))
            {
                return 150;
            }
            else if (type == typeof(Ondulin))
            {
                return 133;
            }
            else
            {
                return 150;
            }
        }

        private int GetHouseHeight(IConstructionEquipment constructionEquipment)
        {
            Type type = constructionEquipment.GetType();
            if (type == typeof(MetalTiles))
            {
                return 182;
            }
            else if (type == typeof(Ondulin))
            {
                return 188;
            }
            else
            {
                return 142;
            }
        }

        private void InitializeRoofTypes()
        {
            _types = _reflectionInfo.GetTypesOfImplementingClasses(typeof(IConstructionEquipment));
            UpdateComboBox();
        }

        private void InitializeImages()
        {
            string basePath = Directory.GetCurrentDirectory() + @"\Resources\";
            _builder = Image.FromFile(basePath + "builder.png");
            _car1 = Image.FromFile(basePath + "car.png");
            _car2 = Image.FromFile(basePath + "carHouse1.png");
            _car3 = Image.FromFile(basePath + "carHouse2.png");
            _car4 = Image.FromFile(basePath + "carHouse3.png");
            _house1_1 = Image.FromFile(basePath + "house1.1.png");
            _house1_2 = Image.FromFile(basePath + "house1.2.png");
            _house1_3 = Image.FromFile(basePath + "house1.3.png");
            _house1_4 = Image.FromFile(basePath + "house1.4.png");
            _house1_5 = Image.FromFile(basePath + "house1.5.png");
            _house2_1 = Image.FromFile(basePath + "house2.1.png");
            _house2_2 = Image.FromFile(basePath + "house2.2.png");
            _house2_3 = Image.FromFile(basePath + "house2.3.png");
            _house2_4 = Image.FromFile(basePath + "house2.4.png");
            _house2_5 = Image.FromFile(basePath + "house2.5.png");
            _house3_1 = Image.FromFile(basePath + "house3.1.png");
            _house3_2 = Image.FromFile(basePath + "house3.2.png");
            _house3_3 = Image.FromFile(basePath + "house3.3.png");
            _house3_4 = Image.FromFile(basePath + "house3.4.png");
            _house3_5 = Image.FromFile(basePath + "house3.5.png");
        }

        private void PaintImage(Graphics graphics)
        {
            if (_processEmulator == null)
            {
                return;
            }
            if (_processEmulator.ConstructionEquipment.CarInWarehouse())
            {
                graphics.DrawImage(_car1, _processEmulator.ConstructionEquipment.BaseCoordinates.X, _processEmulator.ConstructionEquipment.BaseCoordinates.Y, 100, 46);
            }
            else
            {
                graphics.DrawImage(GetLorryImage(), _processEmulator.ConstructionEquipment.NextCoordinates.X, _processEmulator.ConstructionEquipment.NextCoordinates.Y, 100, 46);
            }
            for (int i = 0; i < _processEmulator.Builders.Count; i++)
            {
                graphics.DrawImage(GetHouseImage(_processEmulator.Builders[i].House.ImageId), _processEmulator.Builders[i].House.Coordinates.X, _processEmulator.Builders[i].House.Coordinates.Y,
                                   GetHouseWidth(_processEmulator.ConstructionEquipment), GetHouseHeight(_processEmulator.ConstructionEquipment));
                if (_processEmulator.Builders[i].InHouse())
                {
                    graphics.DrawImage(_builder, _processEmulator.Builders[i].BaseCoordinates.X, _processEmulator.Builders[i].BaseCoordinates.Y, 43, 57);
                }
                else
                {
                    graphics.DrawImage(_builder, _processEmulator.Builders[i].NextCoordinates.X, _processEmulator.Builders[i].NextCoordinates.Y, 43, 57);
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
                Random random = new Random();
                IConstructionEquipment constructionEquipment = (IConstructionEquipment)Activator.CreateInstance(_types[comboBox1.SelectedIndex],
                    new Coordinates(panel1.Width - 150, panel1.Height / 2 + 20, 30),
                    new Coordinates(panel1.Width - 150, panel1.Height / 2 + 20, 30));
                List<Builder> builders = new List<Builder>();
                for (int i = 0; i < Convert.ToInt32(numericUpDown1.Value); i++)
                {
                    builders.Add(new Builder(new Coordinates(10 + GetHouseWidth(constructionEquipment) * i + i * 30 + GetHouseWidth(constructionEquipment) / 2, panel1.Height - 70, 20),
                               new Coordinates(10 + GetHouseWidth(constructionEquipment) * i + i * 30 + GetHouseWidth(constructionEquipment) / 2, panel1.Height - 70, 20),
                               new House(new Coordinates(10 + GetHouseWidth(constructionEquipment) * i + i * 30, 20, 0), constructionEquipment.GetType(), random)));
                }
                _processEmulator = new ProcessEmulator(constructionEquipment, builders);
                _processEmulator.Run();
                _repaintThread = new Thread(PanelRepaint)
                {
                    IsBackground = true
                };
                _repaintThread.Start();
                button1.Text = "Остановить";
                comboBox1.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                _processEmulator.Abort();
                _repaintThread.Abort();
                _repaintThread = null;
                button1.Text = "Запустить";
                comboBox1.Enabled = true;
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
