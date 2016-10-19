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
    public partial class FillGroup : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        public FillGroup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int snn = int.Parse( textBox1.Text);
            cmd = new MySqlCommand("select count(*) from trainee where id="+snn, con);
            try
            {
                int count = int.Parse(cmd.ExecuteScalar().ToString());
                //MessageBox.Show(" trainee is added ");
                cmd = new MySqlCommand("select capacity from classroom where id=(select clsroomid from _Group where crscode=@crscode and groupname=@groupname)", con);
            cmd.Parameters.AddWithValue("crscode", comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("groupname", comboBox2.SelectedValue);
            //MessageBox.Show(" trainee is added ");
            int capacity= int.Parse(cmd.ExecuteScalar().ToString());
                //MessageBox.Show(" trainee is added ");
            cmd = new MySqlCommand("select count(*) from Traineeingroup",con);
            int total=int.Parse(cmd.ExecuteScalar().ToString());

            if (count != 1)
            { MessageBox.Show(" invalide trainee"); }
            else if(total==capacity)
            {

                MessageBox.Show(" Group is Full");
            }
            else
            {
                cmd = new MySqlCommand("Insert into TraineeinGroup Values(@snn,@crs,@gname,0)", con);
                cmd.Parameters.AddWithValue("snn", textBox1.Text);
                cmd.Parameters.AddWithValue("crs", comboBox1.SelectedValue);
                cmd.Parameters.AddWithValue("gname", comboBox2.SelectedValue);
                cmd.ExecuteNonQuery();

                MessageBox.Show(" trainee is added ");

          }
        }catch{

            MessageBox.Show("cant insert");
        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void FillGroup_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=localhost;database=coursessystem;uid=root;pwd=root");
            con.Open();
            cmd = new MySqlCommand("select couresname,coursecode from course;", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            //dataGridView3.DataSource = dt;
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "couresname";
            comboBox1.ValueMember = "coursecode";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("select groupname from _Group where crscode='"+comboBox1.SelectedValue+"';", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            //dataGridView3.DataSource = dt;
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "groupname";
            comboBox2.ValueMember = "groupname";

        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            Hide();
            f.ShowDialog();
        }
    }
}
