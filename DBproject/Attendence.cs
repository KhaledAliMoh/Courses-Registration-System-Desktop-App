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
    public partial class Attendence : Form
    {

        MySqlConnection con;
        MySqlCommand cmd;        
        public Attendence()
        {
            InitializeComponent();
        }

        private void Attendence_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=localhost;database=coursessystem;uid=root;pwd=root");
            con.Open();
            cmd = new MySqlCommand("select couresname,coursecode from course;", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "couresname";
            comboBox1.ValueMember = "coursecode";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("select groupname from _Group where crscode='" + comboBox1.SelectedValue + "';", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "groupname";
            comboBox2.ValueMember = "groupname";
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("select sessid from _session where crscode='" + comboBox1.SelectedValue + "' and grpname='" + comboBox2.SelectedValue + "';", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            //dataGridView3.DataSource = dt;
            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "sessid";
            comboBox3.ValueMember = "sessid";

            cmd = new MySqlCommand("select id,firstname,lastname from trainee  where id in (select traineeid from traineeingroup where crscode='" + comboBox1.SelectedValue + "' and grpname='" + comboBox2.SelectedValue + "');", con);
            MySqlDataReader dr2 = cmd.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(dr2);
            dataGridView1.DataSource = dt2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            Hide();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool attend;
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {                    
                    if (dataGridView1.Rows[i].Selected)
                        attend = true;
                    else
                        attend = false;

                    int id = int.Parse(dataGridView1.Rows[i].Cells["id"].Value.ToString());                 

                    cmd = new MySqlCommand("insert into attendence values (@traineeid ,@crscode,@grpname,@sessionid,@attend)", con);
                    cmd.Parameters.AddWithValue("traineeid", id);
                    cmd.Parameters.AddWithValue("crscode", comboBox1.SelectedValue);                    
                    cmd.Parameters.AddWithValue("grpname", comboBox2.SelectedValue.ToString());                          
                    cmd.Parameters.AddWithValue("sessionid", comboBox3.SelectedValue);
                    cmd.Parameters.AddWithValue("attend", attend);
                                  
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Attendence taken ! ");
            }
            catch (Exception es) { MessageBox.Show(es.Message); }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.SelectedRows[0].Cells["id"].Value.ToString());
            
            cmd = new MySqlCommand("select count(*) from attendence where traineeid = " + id+" and crscode = "+comboBox1.SelectedValue+" and grpname = '"+comboBox2.SelectedValue+ "' and attend = true ", con);
            try
            {
                textBox1.Text = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ee) {
                MessageBox.Show(ee.Message);
            }
        }



        

    }
}
