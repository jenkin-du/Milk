using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace milk
{


    public partial class LaunchForm : Form
    {

       

        public LaunchForm()
        {
            InitializeComponent();

          
            
        }

      

       

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Thread.Sleep(10000);

            Main form1 = new Main();
            form1.Show();

            this.Hide();
            //this.Enabled = false;
            //this.Visible = false;
        }
    }
}
