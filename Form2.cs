using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombeeShooter
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Location = new Point(150, 150);

            button1.BackColor = Color.Transparent;
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            using (Form1 form1 = new Form1())
                form1.ShowDialog();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
