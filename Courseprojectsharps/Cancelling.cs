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
    public partial class Cancelling : Form
    {
        public Cancelling()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\royal\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from CancelTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CancellationsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FillTicketId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TId from TicketTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TId", typeof(string));
            dt.Load(rdr);
            TIdCb.ValueMember = "TId";
            TIdCb.DataSource = dt;
            Con.Close();
        }
        private void FetchPassenger()
        {
            Con.Open();
            string query = "select * from TicketTbl where TId=" + TIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                FCodeTb.Text = dr["Fcode"].ToString();
            }
            Con.Close();
        }
        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Cancelling_Load(object sender, EventArgs e)
        {
            FillTicketId();
            populate();
        }

        private void TIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FetchPassenger();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
        private void DeleteTicket()
        {
                try
                {
                    Con.Open();
                    string query = "delete from TicketTbl where TId='" + TIdCb.SelectedValue.ToString() + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Flight Deleted Successfully");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CanId.Text == "" || FCodeTb.Text == "") //Если не введена информация
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into CancelTbl values(" + CanId.Text + "," + TIdCb.SelectedValue.ToString() + ",'" + FCodeTb.Text + "','" + CanDate.Value.Date + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ticket Cancelled Successfully");
                    Con.Close();
                    populate();
                    DeleteTicket();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CanId.Text = "";
            FCodeTb.Text = "";
        }
    }
}
