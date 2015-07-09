using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Infrastructure.Interfaces
{
    public interface INamedView: IView
    {
        string Name { get; }
    }
}
