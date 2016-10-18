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
    public partial class AddGroup : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        char[] gname = {'A','B','C','D'};
        int crscode;
        string grpname;
        int trainerid,classroomid;
        string startdate, enddate;
        public AddGroup()
        {
            InitializeComponent();
        }

        private void checkedListBox4_SelectedIndexChanged(object sender, EventArgs e)
        {                                    
        }
        private void checkedListBox4_ItemCheck(object sender, ItemCheckEventArgs e)
        {            
        }


        private void AddGroup_Load(object sender, EventArgs e)
        {
            con = new MySqlConnection("server=localhost;database=coursessystem;uid=root;pwd=root");
            con.Open();

            cmd = new MySqlCommand("select couresname,coursecode from course", con);

            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                //dataGridView1.DataSource = dt;
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "couresname";
                comboBox1.ValueMember = "coursecode";
            }
            catch
            {
            }

            cmd = new MySqlCommand("select firstname,id from trainer", con);
            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                //dataGridView2.DataSource = dt;
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "firstname";
                comboBox2.ValueMember = "id";
            }
            catch
            {
            }

            cmd = new MySqlCommand("select * from classroom", con);
            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                //dataGridView3.DataSource = dt;
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "id";
                comboBox3.ValueMember = "id";
            }
            catch
            {
            }



            for(int i = 0 ; i <  gname.Length; i++ )
            {
                listBox4.Items.Add(gname[i]);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*listBox1.ClearSelected();
            listBox2.ClearSelected();
            listBox3.ClearSelected();
            listBox4.ClearSelected();  */          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("select coursecode from course where couresname = '"+comboBox1.Text+"'",con);
            crscode = (int)cmd.ExecuteScalar();
            //MessageBox.Show(crscode.ToString());

            cmd = new MySqlCommand("select id from trainer where firstname = '"+comboBox2.Text+"'",con);
            trainerid = (int)cmd.ExecuteScalar();
            //MessageBox.Show(trainerid.ToString());

            classroomid = int.Parse(comboBox3.Text);
            //MessageBox.Show(classroomid.ToString());

            grpname = listBox4.SelectedItem.ToString();
            //MessageBox.Show(grpname.ToString());

            

            try
            {                
                /*string year1 = dateTimePicker1.Value.Date.Year.ToString();
                string month1= dateTimePicker1.Value.Date.Month.ToString();
                string day1 = dateTimePicker1.Value.Date.Day.ToString();
                 * */
                startdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                //MessageBox.Show(startdate);


                /*string year2 = dateTimePicker2.Value.Date.Year.ToString();
                string month2= dateTimePicker2.Value.Date.Month.ToString();
                string day2 = dateTimePicker2.Value.Date.Day.ToString();
                 * */
                enddate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                //MessageBox.Show(enddate);
                //MessageBox.Show("INSERT INTO _group VALUES (@groupname, @crscode, @trainerid, @startdate, @enddate)");

                //MessageBox.Show("'" + year1 + '-' + month1 + '-' + day1 + "','" + year2 + '-' + month2 + '-' + day2 + "' ");
                //string str = "'" + year1 + '-' + month1 + '-' + day1 + "','" + year2 + '-' + month2 + '-' + day2 + "' ";
                cmd = new MySqlCommand("insert into _group values (@groupname, @crscode, @trainerid, @startdate, @enddate,@clsroomid)", con);
                //cmd.Parameters.Add("@groupname",MySqlDbType.VarChar,1);
                //cmd.Parameters["@groupname"].Value = grpname;

                cmd.Parameters.AddWithValue("groupname", grpname);
                cmd.Parameters.AddWithValue("crscode", crscode);
                cmd.Parameters.AddWithValue("trainerid", trainerid);
                cmd.Parameters.AddWithValue("startdate", startdate);
                cmd.Parameters.AddWithValue("enddate", enddate);
                cmd.Parameters.AddWithValue("clsroomid", classroomid);
                
                //cmd.Parameters.AddWithValue("startdate", dateTimePicker1.Value.Date);
                //cmd.Parameters.AddWithValue("enddate", dateTimePicker2.Value.Date);
                //MessageBox.Show("Group opsdfjposfjdsopfdspo inserted");
                //MessageBox.Show("sgjfdkgds'g");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Group inserted");
                cmd = new MySqlCommand("select totalhours from course where coursecode = " + crscode, con);
                int totalHours = int.Parse(cmd.ExecuteScalar().ToString());

                cmd = new MySqlCommand("select hoursperweek from course where coursecode = " + crscode, con);
                int hoursPerWeek = int.Parse(cmd.ExecuteScalar().ToString());

                int numOfSessions = totalHours / hoursPerWeek;
                for (int i = 1; i <= numOfSessions; i++)
                {
                    cmd = new MySqlCommand("insert into _session values ("+ i +", "+crscode+",'"+grpname+"')", con);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(i.ToString());
                }
                MessageBox.Show("Number of sessions : "+numOfSessions.ToString());
            }
            catch
            {
                MessageBox.Show("Cant insert");
            }

        }

        private void dataGridView1_RowContentClick(object sender, DataGridViewCellEventArgs e)
        {                       
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            Hide();
            f.ShowDialog();
        }


        /*private void datagridview1_SelectionChanged(object sender, EventArgs e)
        {                     
            int selectedrowindex = dataGridView1.SelectedCells[1].RowIndex;            
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];            
            string a = Convert.ToString(selectedRow.Cells["coursecode"].Value);            
            crscode = int.Parse(a);            
            MessageBox.Show(crscode.ToString());          
            
        }
        */
    }
}
