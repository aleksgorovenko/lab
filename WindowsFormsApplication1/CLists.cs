using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class CLists
    {
        protected Dictionary<string, CCategorizedList> lists;

        public CLists()
        {
            lists = new Dictionary<string, CCategorizedList>();
            lists.Add("Writings", new CWritingsList());
            lists.Add("Notebooks", new CNotebookList());
        }

        public void Clear()
        {
            lists.Clear();
            lists.Add("Writings", new CWritingsList());
            lists.Add("Notebooks", new CNotebookList());
        }

        public void Add(List<Dictionary<string, string>> items)
        {
            foreach (Dictionary<string, string> item in items)
            {
                string type = item["parent_type"];

                if (lists.ContainsKey(type))
                {
                    var list = lists[type];
                    list.Add(item);
                }
            }
        }

        public void Remove(CItem item)
        {
            string type = item.Get()["parent_type"];

            if (!lists.ContainsKey(type)) return;

            CCategorizedList list;

            list = lists[type];
            list.Remove(item);
        }

        public List<CItem> GetList(string type = null)
        {
            List<CItem> list = new List<CItem>();

            if (type == null)
            {
                list.AddRange(lists["Writings"].GetAll());
                list.AddRange(lists["Notebooks"].GetAll());
            }
            else
            {
                list.AddRange(lists[type].GetAll());
            }


            return list;
        }

        public CCategorizedList GetCatList(string type)
        {
            if (lists.ContainsKey(type))
            {
                return lists[type];
            }

            return null;
        }

        public List<string> GetTypes()
        {
            List<string> types = new List<string>();

            types.Add("Writings");
            types.Add("Notebooks");

            return types;
        }
    }
}
