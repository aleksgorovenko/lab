using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class CNotebook : CItem
    {
        protected string page_count = null;

        public override Dictionary<string, string> Get()
        {
            Dictionary<string, string> props = new Dictionary<string, string>();
            props = base.Get();
            props.Add("page_count", page_count);
            return props;
        }

        public override void Set(Dictionary<string, string> props)
        {
            base.Set(props);

            if (props.ContainsKey("page_count"))
            {
                page_count = props["page_count"];
            }
        }
    }
}
