using System;
using System.Windows.Forms;

namespace Runner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Interface1.Form1().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Interface2.Form2().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Interface3.Form3().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Interface4.Form4().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Interface6.Form6().Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Interface7.Form7().Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new Interface9.Form9().Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/id160746710/");
        }
    }
}
