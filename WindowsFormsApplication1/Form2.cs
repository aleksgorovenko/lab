using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form1 main_frm;

        public Form2()
        {
            InitializeComponent();
        }
        private void update()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            if (radioButton1.Checked)
            {
                comboBox1.Items.Add("pruzhina");
                comboBox1.Items.Add("perepl'ot");
            }
            else
            {
                comboBox1.Items.Add("mech");
                comboBox1.Items.Add("auto");

                comboBox2.Items.Add("red");
                comboBox2.Items.Add("black");
                comboBox2.Items.Add("blue");
                comboBox2.Items.Add("green");
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label4.Hide();
            comboBox2.Hide();

            textBox4.Show();
            label6.Show();

            update();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label4.Show();
            comboBox2.Show();

            textBox4.Hide();
            label6.Hide();

            update();
        }

        private int check()
        {
            if (textBox2.Text.Length == 0)
                return -1;
            for(int i = 0; i < textBox2.Text.Length; i++)
            {
                if ((textBox2.Text[i] < '0') || (textBox2.Text[i] > '9'))
                    return 1;
            }
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            List<Dictionary<string, string>> dat = new List<Dictionary<string, string>>();
            switch (check())
            {                
                case 0:
                    try
                    {
                        if (radioButton1.Checked)
                        {
                            res["parent_type"] = radioButton1.Text;
                            res["page_count"] = textBox4.Text;
                        }
                        else if (radioButton2.Checked)
                        {
                            res["parent_type"] = radioButton2.Text;
                            res["color"] = comboBox2.SelectedItem.ToString();
                        }
                        else MessageBox.Show("TEST", "uncorrect data", MessageBoxButtons.OK);

                        res["name"] = textBox1.Text;
                        res["price"] = textBox2.Text;
                        res["strih_code"] = textBox3.Text;
                        res["type"] = comboBox1.SelectedItem.ToString();

                        Hide();
                        main_frm.NewItem(res);
                    }
                    catch
                    {
                        MessageBox.Show("Uncorrect input data", "ERROR!!!!!", MessageBoxButtons.OK);
                    }
                    break;
                case 1:
                    MessageBox.Show("Uncorrect price", "uncorrect data", MessageBoxButtons.OK);
                    break;
                case -1:
                    MessageBox.Show("No price", "uncorrect data", MessageBoxButtons.OK);
                    break;
            }

        }

        public void edit_item(Dictionary<string, string> item)
        {
            try
            {
                if (item["parent_type"] == radioButton1.Text)
                {
                    comboBox2.Visible = false;
                    textBox4.Visible = true;
                    textBox4.Text = item["page_count"];
                }
                else if (item["parent_type"] == radioButton2.Text)
                {
                    comboBox2.Visible = true;
                    textBox4.Visible = false;
                }
                textBox1.Text = item["name"];
                textBox2.Text = item["price"];
                textBox3.Text = item["strih_code"];
                Show();
            }
            catch
            {
                MessageBox.Show("Cnnot edit element","ERROR!!!",MessageBoxButtons.OK);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}