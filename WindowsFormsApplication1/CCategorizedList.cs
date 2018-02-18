using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    abstract class CCategorizedList
    {
        abstract public CItem Add(Dictionary<string, string> item);

        abstract public List<CItem> GetAll();

        abstract public void Remove(CItem item);

        abstract public List<CItem> GetByType(string type);

        abstract public List<string> GetTypes();
    }
}
