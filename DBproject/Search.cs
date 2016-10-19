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
    public partial class Search : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        public Search()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
        }

        private void Search_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=localhost;database=coursessystem;uid=root;pwd=root");
            con.Open();

            label1.Text = "";
            label2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new MySqlCommand("select count(*) from course", con);
                textBox2.Text = cmd.ExecuteScalar().ToString();
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked == true)
            {
                label1.Text = "Trainee information";
                cmd = new MySqlCommand("select * from trainee where firstname = '" + textBox1.Text+"'", con);
                try
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                }
                catch
                {
                }

            }

            else if (radioButton2.Checked == true)
            {
                label1.Text = "Course information";
                cmd = new MySqlCommand("select couresname from course where coursecode in (select crscode from _group where trainerid = (select id from trainer where firstname =  '" + textBox1.Text + "'))", con);
                try
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                }
                catch
                {
                }

            }

            else if (radioButton3.Checked == true)
            {
                label1.Text = "Trainee info";
                label2.Text = "Trainer info";
                int crscode;
                cmd = new MySqlCommand("select coursecode from course where couresname = '"+textBox1.Text+"'",con);
                crscode = (int)cmd.ExecuteScalar();

                cmd = new MySqlCommand("select firstname, lastname from trainee where id in (select traineeid from traineeingroup where crscode = '"+crscode+"')", con);
                try
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                }
                catch
                {
                }

                cmd = new MySqlCommand("select firstname , lastname from trainer where id in(select trainerid from _group where crscode = '"+crscode+"')",con);
                try
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView2.DataSource = dt;
                }
                catch
                {
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            Hide();
            f.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
        }
    }
}

