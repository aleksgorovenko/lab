using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class CWritings : CItem
    {
        protected string color;

        public override Dictionary<string, string> Get()
        {
            Dictionary<string, string> props = new Dictionary<string, string>();
            props = base.Get();
            
            props.Add("color", color);
            return props;
        }

        public override void Set(Dictionary<string, string> props)
        {
            base.Set(props);

            if (props.ContainsKey("color"))
            {
                color = props["color"];
            }
        }
    }
}
