using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class CItem
    {
        protected string name;
        protected string price;
        protected string strih_code;
        protected string parent_type;
        protected string type;

        public virtual Dictionary<string, string> Get()
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            props.Add("parent_type", parent_type);
            props.Add("name", name);
            props.Add("price", price);
            props.Add("strih_code", strih_code);
            props.Add("type", type);

            return props;
        }

        public virtual void Set(Dictionary<string, string> props)
        {
            if (props.ContainsKey("type"))
            {
                try
                {
                    type = props["type"];
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Неверный формат числа");//error!
                }
            }

            if (props.ContainsKey("parent_type"))
            {
                parent_type = props["parent_type"];
            }

            if (props.ContainsKey("name"))
            {
                name = props["name"];
            }

            if (props.ContainsKey("price"))
            {
                price = props["price"];
            }

            if (props.ContainsKey("strih_code"))
            {
                strih_code = props["strih_code"];
            }
        }
    }
}
