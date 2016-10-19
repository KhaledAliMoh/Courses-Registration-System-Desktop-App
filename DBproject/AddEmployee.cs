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
    public partial class AddEmployee : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        bool gender;
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBox6.Text = "";
            textBox7.Text = "";
            textBox5.Text = "";
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=localhost;database=coursessystem;uid=root;pwd=root");
            con.Open();
            label9.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                gender = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                gender = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!checkEmail())
            {
                label9.Visible = true;
                return;
            }
            try
            {
                cmd = new MySqlCommand("INSERT INTO employee VALUES (@firstname, @lastname, @id, @dateofbirth, @gender , @address,@email,@pword)", con);
                cmd.Parameters.AddWithValue("firstname", textBox1.Text);
                cmd.Parameters.AddWithValue("lastname", textBox2.Text);
                cmd.Parameters.AddWithValue("id", textBox3.Text);
                cmd.Parameters.AddWithValue("dateofbirth", textBox4.Text);
                cmd.Parameters.AddWithValue("email", textBox5.Text);
                cmd.Parameters.AddWithValue("pword", textBox7.Text);
                cmd.Parameters.AddWithValue("gender", gender);
                cmd.Parameters.AddWithValue("address", textBox6.Text);                

                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee inserted");
            }
            catch
            {
                MessageBox.Show("Cant insert");
            }
        }

        private bool checkEmail()
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(textBox5.Text);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            Hide();
            f.ShowDialog();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            label9.Visible = false;
        }
    }
}
