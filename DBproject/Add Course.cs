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
    public partial class Add_Course : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        public Add_Course()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            try
            {                              
                cmd = new MySqlCommand("INSERT INTO course VALUES (@coursecode, @coursename, @totalhours, @hoursperweek, @credits)", con);                 
                cmd.Parameters.AddWithValue("coursecode", textBox1.Text);
                cmd.Parameters.AddWithValue("coursename", textBox2.Text);
                cmd.Parameters.AddWithValue("totalhours", textBox3.Text);
                cmd.Parameters.AddWithValue("hoursperweek", textBox4.Text);
                cmd.Parameters.AddWithValue("credits", textBox5.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Course inserted");
            }
            catch
            {
                MessageBox.Show("Cant insert");
            }
        }

        private void Add_Course_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=localhost;database=coursessystem;uid=root;pwd=root");
            con.Open();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            Hide();
            f.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
