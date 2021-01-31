﻿using Interface2;
using Interface6;
using Interface8;
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
            new Form2().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form6().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form8().Show();
        }
    }
}
