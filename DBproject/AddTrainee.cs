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
    public partial class AddTrainee : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        bool gender;
        int empid;
        string traineeType;
        string birthday;
        string regdate;
        string[] arrdepart = { "CS", "IS", "IT", "MM" };
        string[] arrlevel = { "1", "2", "3", "4" };
        public AddTrainee()
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
            textBox6.Text = "";
            textBox7.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            //textBox6.Text = "";
        }

        private void AddTrainee_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=localhost;database=coursessystem;uid=root;pwd=root");
            con.Open();
            //cmd = new MySqlCommand("select firstname", con);
            label14.Visible = false;
            cmd = new MySqlCommand("select * from employee", con);
            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                //dataGridView3.DataSource = dt;
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "firstname";
                comboBox1.ValueMember = "id";
            }
            catch
            {
            }
            comboBox2.DataSource = arrlevel;
            comboBox3.DataSource = arrdepart;

            groupBox3.Enabled = false;
            groupBox4.Enabled = false;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                traineeType = "student";
                groupBox3.Enabled = true;
                groupBox4.Enabled = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                traineeType = "graduated";
                groupBox4.Enabled = true;
                groupBox3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkEmail())
            {
                label14.Visible = true;
                return;
            }
            cmd = new MySqlCommand("select id from employee where firstname = '"+comboBox1.Text+"'",con);
            empid = (int)cmd.ExecuteScalar();

            DateTime dateTime = DateTime.Today ;
            string year = dateTime.Year.ToString();
            string month = dateTime.Month.ToString();
            string day = dateTime.Day.ToString();
            //MessageBox.Show(dateTime.ToShortDateString());
            regdate = year + "-" + month + "-" + day;
            
            //MessageBox.Show(regdate);
            birthday = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            //MessageBox.Show(birthday);

            cmd = con.CreateCommand();
            //cmd.Connection = con;
            MySqlTransaction myTrans;
            // Start a local transaction 
            myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction                         
            cmd.Transaction = myTrans;
            try
            {
                //MessageBox.Show("sfsdghukhweilfghsf");
                cmd.CommandText = ("INSERT INTO trainee values(@firstname,@lastname,@id,@dateofbirth,@gender,@address,@traineetype,@numberofbill,@empid,@dateofreg,@email) ");
                cmd.Parameters.AddWithValue("firstname", textBox1.Text.ToString());
                cmd.Parameters.AddWithValue("lastname", textBox2.Text.ToString());
                cmd.Parameters.AddWithValue("id", textBox3.Text.ToString());
                cmd.Parameters.AddWithValue("dateofbirth", birthday);
                cmd.Parameters.AddWithValue("gender", gender);
                cmd.Parameters.AddWithValue("address", textBox4.Text.ToString());
                cmd.Parameters.AddWithValue("traineetype", traineeType);
                cmd.Parameters.AddWithValue("numberofbill", textBox5.Text.ToString());
                cmd.Parameters.AddWithValue("empid", empid);
                cmd.Parameters.AddWithValue("dateofreg", regdate);
                cmd.Parameters.AddWithValue("email", textBox8.ToString());
                cmd.ExecuteNonQuery();

               // cmd.CommandText = "insert into trainee values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+birthday+"','"+gender+"','"+textBox4.Text+"','"+traineeType+"','"+textBox5.Text+"','"+empid+"''"+regdate+"')";

                //MessageBox.Show("sfsdghukhweilfghsf");
                //cmd.ExecuteNonQuery();
                //cmd.Parameters.Clear();
                //MessageBox.Show("sfsdghukhweilfghsf");
                if (traineeType.Equals("student"))
                {
                    //MessageBox.Show("sfsdghukhweilfghsf");
                    cmd.CommandText = "INSERT INTO student values(@_level,@department,@stid)";
                    cmd.Parameters.AddWithValue("_level", comboBox2.Text);
                    cmd.Parameters.AddWithValue("department", comboBox3.Text);
                    cmd.Parameters.AddWithValue("stid", textBox3.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student inserted");
                    //MessageBox.Show(traineeType.ToString());
                    //myTrans.Commit();
                }
                else
                {
                    //MessageBox.Show("sfsdghukhweilfghsf");
                    cmd.CommandText = "INSERT INTO graduated values(@university,@college,@stid)";
                    cmd.Parameters.AddWithValue("university", textBox6.Text);
                    cmd.Parameters.AddWithValue("college", textBox7.Text);
                    cmd.Parameters.AddWithValue("stid", textBox3.Text);
                    cmd.ExecuteNonQuery();
                    //myTrans.Commit();
                    MessageBox.Show("Graduated inserted");
                }
                myTrans.Commit();

            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
                myTrans.Rollback();
                MessageBox.Show("Cant insert");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                gender = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                gender = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            Hide();
            f.ShowDialog();
        }

        private bool checkEmail()
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(textBox8.Text);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            label14.Visible = false;
        }
    }
}
