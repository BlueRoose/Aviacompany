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
    public partial class ViewFlights : Form
    {
        public ViewFlights()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\royal\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from FlightTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FlightsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ViewFlights_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Flighttbl Addfl = new Flighttbl();
            Addfl.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            Seatnum.Text = "";
            Ticketpr.Text = "";
        }

        private void FlightsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                FcodeTb.Text = FlightsDGV.SelectedRows[0].Cells[0].Value.ToString();
                SrcCb.SelectedItem = FlightsDGV.SelectedRows[0].Cells[1].Value.ToString();
                DestCb.SelectedItem = FlightsDGV.SelectedRows[0].Cells[2].Value.ToString();
                Seatnum.Text = FlightsDGV.SelectedRows[0].Cells[4].Value.ToString();
                Ticketpr.Text = FlightsDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "")
            {
                MessageBox.Show("Enter The Flight To Delete");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from FlightTbl where Fcode='" + FcodeTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Deleted Successfully");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "" || Seatnum.Text == "" || Ticketpr.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update FlightTbl set Fcode='" + FcodeTb.Text + "',Fsrc='" + SrcCb.SelectedItem.ToString() + "',Fdest='" + DestCb.SelectedItem.ToString() + "',Fdate='" + Fdate.Value.Date.ToString() + "',Fcap='" + Seatnum.Text + "',Fticket='" + Ticketpr.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Updated Successfully");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Missing Information");
                }
            }
        }
    }
}
