using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Courseprojectsharps
{
    public partial class AddPassenger : Form
    {
        public AddPassenger()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\royal\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label10_Click(object sender, EventArgs e)  //Функционал крестика
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) //Запись пассажира
        {
            if (Passid.Text == "" || Passad.Text == "" || Passname.Text == "" || Passporttb.Text == "" || Phonetb.Text == "") //Если не введена информация
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into PassengerTbl values(" + Passid.Text + ",'" + Passname.Text + "','" + Passporttb.Text + "','" + Passad.Text + "','" + Nationalitycb.SelectedItem.ToString() + "','" + Gendercb.SelectedItem.ToString() + "','" + Phonetb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger Recorded Successfully");
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) //Открытие окна со списком пассажиров
        {
            ViewPassengers viewpass = new ViewPassengers();
            viewpass.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
