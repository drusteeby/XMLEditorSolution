using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Infrastructure.Containers
{
    public class NameTypeContainer
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public NameTypeContainer(string name = null, Type type = null)
        {
            if (name != null) Name = name;
            if (type != null) Type = type;
        }
    }
}
