using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSnake_Click(object sender, EventArgs e)
        {
            SnakeGame snakeForm = new SnakeGame();
            snakeForm.Show();
            this.Hide(); 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnPlatformer_Click(object sender, EventArgs e)
        {
            PlatformerGame platformerForm = new PlatformerGame();
            platformerForm.Show();
            this.Hide();
        }
    }
}
