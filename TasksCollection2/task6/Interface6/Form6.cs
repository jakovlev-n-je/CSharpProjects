using Implementation6;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Interface6
{
    public partial class Form6 : Form
    {
        private readonly List<IFabric> _suits;

        public Form6()
        {
            InitializeComponent();
            _suits = new List<IFabric>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fabricType = textBox2.Text.Trim();
            if (!IsCorrectnessText(fabricType))
            {
                ShowErrorBox("Некорректно указан тип ткани!");
                return;
            }
            string color = textBox3.Text.Trim();
            if (!IsCorrectnessText(color))
            {
                ShowErrorBox("Некорректно указан цвет ткани!");
                return;
            }
            _suits.Add(new Suit(fabricType, color, Convert.ToInt32(numericUpDown1.Value)));
            ClearInputData();
            UpdateListBox1();
            ShowSuccessBox("Новый элемент был успешно добавлен в список!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string resultText = _suits[listBox1.SelectedIndex].SewUp();
            UpdateListBox1();
            ShowSuccessBox(resultText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string resultText = ((Suit)_suits[listBox1.SelectedIndex]).Discard();
            _suits.RemoveAt(listBox1.SelectedIndex);
            UpdateListBox1();
            ShowSuccessBox(resultText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string resultText = ((Suit)_suits[listBox1.SelectedIndex]).GiveAwayToTailor();
            UpdateListBox1();
            ShowSuccessBox(resultText);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string resultText = _suits[listBox1.SelectedIndex].WearOut();
            UpdateListBox1();
            ShowSuccessBox(resultText);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string resultText = ((Suit)_suits[listBox1.SelectedIndex]).Sell();
            _suits.RemoveAt(listBox1.SelectedIndex);
            UpdateListBox1();
            ShowSuccessBox(resultText);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCorrectIndex = listBox1.SelectedIndex > -1;
            textBox1.Text = isCorrectIndex ? _suits[listBox1.SelectedIndex].GetInforamtion().Replace("\n", Environment.NewLine) : "Не выбрана одежда";
            ChangeButtonsEnabledState(isCorrectIndex);
        }

        private void ClearInputData()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            numericUpDown1.Value = 0;
        }

        private void ChangeButtonsEnabledState(bool isEnabled)
        {
            button2.Enabled = isEnabled;
            button3.Enabled = isEnabled;
            button4.Enabled = isEnabled;
            button5.Enabled = isEnabled;
            button6.Enabled = isEnabled;
        }

        private bool IsCorrectnessText(string text)
        {
            foreach (char symbol in text)
            {
                if (!char.IsLetter(symbol))
                {
                    return false;
                }
            }
            return text != "";
        }

        private void UpdateListBox1()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = _suits;
        }

        private void ShowSuccessBox(string text)
        {
            MessageBox.Show(
                text,
                "Успех",
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
