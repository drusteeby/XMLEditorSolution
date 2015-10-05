using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Infrastructure.Interfaces
{
    public interface IDragable
    {
        Type DataType { get; }

        void Remove(object i);
        
    }
}
