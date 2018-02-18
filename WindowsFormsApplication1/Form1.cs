using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataGridView1.Visible = true;
            treeView1.Visible = false;

            button2.Enabled = button3.Enabled = false;
        }

        private CLists lists = new CLists();
        private Form2 new_frm = new Form2();
        public Dictionary<string, string> tov = new Dictionary<string, string>();
        
        public void update_data()//Обновление сведений
        {
            dataGridView1.Rows.Clear();
            listBox1.Items.Clear();

            List<CItem> items = lists.GetList();
            
            foreach(CItem item in items)
            {
                string[] row = {null, null, null};

                row[0] = item.Get()["name"].ToString();
                row[1] = item.Get()["price"].ToString();
                row[2] = item.Get()["strih_code"].ToString();

                listBox1.Items.Add(row[0] + "  -  " + row[1] + "  -  " + row[2]);
                
                dataGridView1.Rows.Add(row);
            }
        }

        public void update_tree()//Обновление дерева объектов
        {            
            TreeNode node = new TreeNode();
            TreeNode sub_node = new TreeNode();
            TreeNode typeNode = new TreeNode("Tovary");
            treeView1.Nodes.Clear();

            foreach (string type in lists.GetTypes())
            {                
                CCategorizedList cat_list = lists.GetCatList(type);
                List<string> subtypes = cat_list.GetTypes();

                foreach (string subtype in subtypes)
                {
                    TreeNode subtypeNode = new TreeNode(subtype);
                    List<CItem> items = cat_list.GetByType(subtype);

                    foreach (CItem item in items)
                    {
                        TreeNode itemNode = new TreeNode(item.ToString());
                        itemNode.Text = item.Get()["name"];

                        subtypeNode.Nodes.Add(itemNode);
                    }
                    typeNode.Nodes.Add(subtypeNode);                    
                }                
            }
            treeView1.Nodes.Add(typeNode);
        }

        private void button1_Click(object sender, EventArgs e)//Добавление новых объектов
        {
            new_frm.main_frm = this;        
            new_frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)//Изменение товара
        {
            Dictionary<string, string> param = new Dictionary<string, string>();//Спсок полей редактируемого обьекта
            TreeNode s_node = treeView1.SelectedNode;
            if (s_node != null)
            {
                param["name"] = s_node.Text.ToString();

                foreach (string type in lists.GetTypes())
                {
                    CCategorizedList cat_list = lists.GetCatList(type);
                    List<string> subtypes = cat_list.GetTypes();

                    foreach (string subtype in subtypes)
                    {
                        TreeNode subtypeNode = new TreeNode(subtype);
                        List<CItem> items = cat_list.GetByType(subtype);

                        foreach (CItem item in items)
                        {
                            var t = item.Get();
                            if (t.ContainsValue(param["name"]))
                            {
                                lists.Remove(item);
                                new_frm.main_frm = this;
                                new_frm.edit_item(t);                                
                            }
                        }
                    }
                }
                update_tree();
                update_data();
            }           
        }

        private void treeToolStripMenuItem_Click(object sender, EventArgs e)//Отображение дерева объектов
        {
            dataGridView1.Visible = false;
            treeView1.Visible = true;

            button2.Enabled = button3.Enabled = true;
        }

        public void NewItem(Dictionary<string, string> props)//Добавление нового объекта
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            list.Add(props);
            lists.Add(list);

            update_data();
            update_tree();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)//Загрузка списка объектов
        {
            lists = new CLists();
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C://";
            dialog.ShowDialog();
            String file_name = dialog.FileName;
            try
            {
                SQLiteConnection m_dbConnection;
                m_dbConnection = new SQLiteConnection("Data Source=" + file_name + ";Version=3;");
                m_dbConnection.Open();


                string sql = "SELECT * FROM Table1 ORDER BY Name";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Dictionary<string, string> read = new Dictionary<string, string>();
                    read["name"] = reader["name"].ToString();
                    read["price"] = reader["price"].ToString();
                    read["strih_code"] = reader["shtrih_kod"].ToString();
                    read["parent_type"] = reader["parent_type"].ToString();
                    read["type"] = reader["type"].ToString();
                    if (read.ContainsValue("Writings"))
                    {
                        read["color"] = reader["color"].ToString();
                    }else
                    {
                        read["page_count"] = reader["page_count"].ToString();
                    }
                    NewItem(read);
                }

                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Uncorrect file format", "ERROR!!!!!", MessageBoxButtons.OK);
            }
            button2.Enabled = button3.Enabled = false;
            lists.Add(data);
            update_data();
            update_tree();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)//Отображение спииска объектов
        {
            dataGridView1.Visible = true;
            treeView1.Visible = false;

            button2.Enabled = button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)//Удаление объекта
        {
            Dictionary<string, string> param = new Dictionary<string, string>();//Спсок полей редактируемого обьекта
            TreeNode s_node = treeView1.SelectedNode;
            if (s_node != null)
            {
                param["name"] = s_node.Text.ToString();

                foreach (string type in lists.GetTypes())
                {
                    CCategorizedList cat_list = lists.GetCatList(type);
                    List<string> subtypes = cat_list.GetTypes();

                    foreach (string subtype in subtypes)
                    {
                        TreeNode subtypeNode = new TreeNode(subtype);
                        List<CItem> items = cat_list.GetByType(subtype);

                        foreach (CItem item in items)
                        {
                            var t = item.Get();
                            if (t.ContainsValue(param["name"]))
                            {
                                lists.Remove(item);
                            }
                        }
                    }
                }
                update_tree();
                update_data();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)//Сохранение списка объектв
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = "c:\\";
            save.ShowDialog();
            string file_name = save.FileName;
            try
            {
                SQLiteConnection m_dbConnection;
                m_dbConnection = new SQLiteConnection("Data Source=" + file_name + ";Version=3;");
                m_dbConnection.Open();

                foreach (string type in lists.GetTypes())
                {
                    CCategorizedList cat_list = lists.GetCatList(type);
                    List<string> subtypes = cat_list.GetTypes();

                    foreach (string subtype in subtypes)
                    {
                        TreeNode subtypeNode = new TreeNode(subtype);
                        List<CItem> items = cat_list.GetByType(subtype);

                        foreach (CItem item in items)
                        {
                            if (item.Get()["parent_type"].CompareTo("Writings") == 0)
                            {
                                string sql = "UPDATE Table1 SET parent_type = " + item.Get()["parent_type"] + "name = " + item.Get()["name"] + "price = " + item.Get()["price"] + 
                                    "type = " + item.Get()["type"] + "type = " + item.Get()["color"] + "WHERE strih_kod = " + item.Get()["strih_code"];
                            }
                            else
                            {
                                string sql = "UPDATE Table1 SET paret = " + item.Get()["parent_type"] + "name = " + item.Get()["name"] + "price = " 
                                    + item.Get()["price"] + "type = " + item.Get()["type"] + "page_count = " + item.Get()["page_count"] 
                                    + "WHERE strih_kod = " + item.Get()["strih_code"];
                        }
                        }
                    }
                }
                m_dbConnection.Close();
            }
            catch
            {
                MessageBox.Show("Uncorrect data", "ERROR!!!", MessageBoxButtons.OK);
            }

}
    }
}