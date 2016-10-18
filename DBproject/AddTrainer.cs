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
    public partial class AddTrainer : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        bool gender;
        public AddTrainer()
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
            textBox5.Text = "";
            textBox7.Text = "";
        }

        private void AddTrainer_Load(object sender, EventArgs e)
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
            if (radioButton2.Checked)
                gender = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkEmail())
            {
                label9.Visible = true;
                return;
            }
            try
            {
                cmd = new MySqlCommand("INSERT INTO Trainer VALUES (@firstname, @lastname, @id, @dateofbirth, @gender , @address ,@sallary,@email )", con);
                cmd.Parameters.AddWithValue("firstname", textBox1.Text);
                cmd.Parameters.AddWithValue("lastname", textBox2.Text);
                cmd.Parameters.AddWithValue("id", textBox3.Text);
                cmd.Parameters.AddWithValue("dateofbirth", textBox4.Text);
                cmd.Parameters.AddWithValue("gender", gender);
                cmd.Parameters.AddWithValue("address", textBox6.Text);
                cmd.Parameters.AddWithValue("sallary", textBox5.Text);
                cmd.Parameters.AddWithValue("email", textBox7.Text.ToString());

                cmd.ExecuteNonQuery();
                MessageBox.Show("Trainer inserted");
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            label9.Visible = false;
        }

        private bool checkEmail()
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(textBox7.Text);
                return true;
            }
            catch (FormatException)
            {
                //MessageBox.Show(ee.Message);
                return false;
            }
        }
    }
}
