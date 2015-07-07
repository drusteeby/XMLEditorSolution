using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.Navigation.Splash
{
    public class CommandContainer
    {
        public string Name { get; set; }
        public DelegateCommand Command { get; set; }

        public CommandContainer(string name = null, DelegateCommand command = null)
        {
            this.Name = name;
            this.Command = command;
        }

    }
}
