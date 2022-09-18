using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Courseprojectsharps
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Flighttbl flight = new Flighttbl();
            flight.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddPassenger addpass = new AddPassenger();
            addpass.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Ticket tick = new Ticket();
            tick.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cancelling cancel = new Cancelling();
            cancel.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
