using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class CNotebookList : CCategorizedList
    {
        protected Dictionary<string, List<CNotebook>> lists;

        public CNotebookList()
        {
            lists = new Dictionary<string, List<CNotebook>>();
        }

        public override CItem Add(Dictionary<string, string> item)
        {
            List<CNotebook> list;
            CNotebook notebook = new CNotebook();

            if (!lists.ContainsKey(item["parent_type"]) )
            {
                lists.Add(item["parent_type"], new List<CNotebook>());
            }

            list = lists[item["parent_type"]];

            notebook.Set(item);
            list.Add(notebook);

            return notebook;
        }

        public override List<CItem> GetAll()
        {
            List<CItem> list = new List<CItem>();

            foreach(KeyValuePair<string, List<CNotebook>> i in lists)
            {
                list.AddRange(i.Value);
            }

            return list;
        }

        public override void Remove(CItem item)
        {
            Dictionary<string, string> t = item.Get();

            List<CNotebook> list = lists[item.Get()["parent_type"]];

            if (!lists.ContainsKey(item.Get()["parent_type"])) return;

            if (list.Contains(item))
            {
                list.Remove((CNotebook)item);
            }
        }

        public override List<CItem> GetByType(string type)
        {
            List<CItem> list = new List<CItem>();

            if (lists.ContainsKey(type))
            {
                list.AddRange(lists[type]);
            }
            return list;
        }

        public override List<string> GetTypes()
        {
            return new List<string>(lists.Keys);
        }
    }
}
