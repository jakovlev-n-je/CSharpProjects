using System;
using System.Windows.Forms;
using Implementation1;

namespace Interface1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(numericUpDown1.Value);
            int m = Convert.ToInt32(numericUpDown2.Value);
            TableUtils.CustomizeTable(dataGridView1, n, m);
            TableUtils.FillTable(ArrayUtils.GenerateRandomArray(n, m, Convert.ToInt32(numericUpDown3.Value),
                Convert.ToInt32(numericUpDown4.Value) + 1), dataGridView1);
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;

            MessageBox.Show(
            "Случайный массив был успешно сгенерирован!",
            "Сообщение",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TableUtils.FillTable(Streamline.StreamlineArray(TableUtils.ReadTable(dataGridView1)),
                dataGridView1);
            button2.Enabled = false;
            button1.Enabled = true;

            MessageBox.Show(
            "Массив был успешно уплотнен!",
            "Сообщение",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            button2.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = true;

            MessageBox.Show(
            "Таблица была успешно очищена!",
            "Сообщение",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        }
    }
}
