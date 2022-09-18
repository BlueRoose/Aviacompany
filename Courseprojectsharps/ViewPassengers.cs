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
    public partial class ViewPassengers : Form
    {
        public ViewPassengers()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\royal\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from PassengerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PassengerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ViewPassengers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e) //Открытие окна с записью пассажира
        {
            AddPassenger addpass = new AddPassenger();
            addpass.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PidTb.Text == "")
            {
                MessageBox.Show("Enter The Passenger To Delete");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from PassengerTbl where PassId=" + PidTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger Deleted Successfully");
                    Con.Close();
                    populate();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        /*private void PassengerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) //Заплнение ячеек при клике на пассажира
        {
            PidTb.Text = PassengerDGV.SelectedRows[0].Cells[0].Value.ToString();
            PnameTb.Text = PassengerDGV.SelectedRows[0].Cells[1].Value.ToString();
            PpassTb.Text = PassengerDGV.SelectedRows[0].Cells[2].Value.ToString();
            PadTb.Text = PassengerDGV.SelectedRows[0].Cells[3].Value.ToString();
            NatCb.SelectedItem = PassengerDGV.SelectedRows[0].Cells[4].Value.ToString();
            GendCb.SelectedItem = PassengerDGV.SelectedRows[0].Cells[5].Value.ToString();
            PhTb.Text = PassengerDGV.SelectedRows[0].Cells[6].Value.ToString();
        }*/

        private void button3_Click(object sender, EventArgs e) //Обнуление информации
        {
            PidTb.Text = "";
            PnameTb.Text = "";
            PpassTb.Text = "";
            PadTb.Text = "";
            NatCb.SelectedItem = "";
            GendCb.SelectedItem = "";
            PhTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e) // Обновляем информацию о конкретном пассажире
        {
            if (PidTb.Text == "" || PnameTb.Text == "" || PpassTb.Text == "" || PadTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update PassengerTbl set PassName='" + PnameTb.Text + "',Passport='" + PpassTb.Text + "',PassAd='" + PadTb.Text + "',PassNat='" + NatCb.SelectedItem.ToString() + "',PassGend='" + GendCb.SelectedItem.ToString() + "',PassPhone='" + PhTb.Text + "'where PassId=" + PidTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger Updated Successfully");
                    Con.Close();
                    populate();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show("Missing Information");
                }
            }
        }
    }
}
