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
    public partial class Form1 : Form
    {
        private static Form1 form;
        public Form1()
        {
            InitializeComponent();
            form = this;
            //Form1_Load(new object(), new EventArgs());
            //MessageBox.Show("sdfaf");
        }

        static public Form1 getBack()
        {
            return form;
        }

        public void Form1_Load(object sender, EventArgs e)
        {            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Course f = new Add_Course();
            Hide();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEmployee f = new AddEmployee();
            Hide();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddTrainer f = new AddTrainer();
            Hide();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddLap f = new AddLap();
            Hide();
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddTrainee f = new AddTrainee();
            Hide();
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddGroup f = new AddGroup();
            Hide();
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FillGroup f = new FillGroup();
            Hide();
            f.ShowDialog();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            Attendence f = new Attendence();
            Hide();
            f.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Search f = new Search();
            Hide();
            f.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            Hide();
            login.ShowDialog();
        }
    }
}
