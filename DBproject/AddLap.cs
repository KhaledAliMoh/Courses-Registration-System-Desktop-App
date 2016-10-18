using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DBproject
{
    public partial class AddLap : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        public AddLap()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void AddLap_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=localhost;database=coursessystem;uid=root;pwd=root");
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new MySqlCommand("INSERT INTO classroom VALUES (@id, @location, @capacity)", con);
                cmd.Parameters.AddWithValue("id", textBox1.Text);
                cmd.Parameters.AddWithValue("location", textBox2.Text);
                cmd.Parameters.AddWithValue("capacity", textBox3.Text);                

                cmd.ExecuteNonQuery();
                MessageBox.Show("Lap inserted");
            }
            catch
            {
                MessageBox.Show("Cant insert");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            Hide();
            f.ShowDialog();
        }
    }
}
