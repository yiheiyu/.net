using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            string port = textBox2.Text;
            string pass = textBox3.Text;
            VNCObject vnc = new VNCObject(path,port,pass);
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            VNCObject vnc = new VNCObject();
            textBox1.Text = vnc.Path;
            textBox2.Text = vnc.Port;
            textBox3.Text = vnc.Password;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
