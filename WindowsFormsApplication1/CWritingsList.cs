using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class CWritingsList : CCategorizedList
    {
        protected Dictionary<string, List<CWritings>> lists;

        public CWritingsList()
        {
            lists = new Dictionary<string, List<CWritings>>();
        }

        public override CItem Add(Dictionary<string, string> item)
        {
            List<CWritings> list;
            CWritings writings = new CWritings();

            if (!lists.ContainsKey(item["parent_type"]))
            {
                lists.Add(item["parent_type"], new List<CWritings>());
            }

            list = lists[item["parent_type"]];

            writings.Set(item);
            list.Add(writings);

            return writings;
        }

        public override List<CItem> GetAll()
        {
            List<CItem> list = new List<CItem>();

            foreach (KeyValuePair<string, List<CWritings>> i in lists)
            {
                list.AddRange(i.Value);
            }

            return list;
        }

        public override void Remove(CItem item)
        {
            Dictionary<string, string> t = item.Get();
            string st = item.Get()["parent_type"];

            List <CWritings> list = lists[item.Get()["parent_type"]];

            if (!lists.ContainsKey(item.Get()["parent_type"])) return;

            if (list.Contains(item))
            {
                list.Remove((CWritings)item);
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
