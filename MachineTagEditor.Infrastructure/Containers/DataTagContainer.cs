using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Infrastructure.Containers
{
    public class DataTagContainer
    {
        public string Name { get; set; }
        public ObservableCollection<DataTagPropertyContainer> Properties { get; set; }
        public ObservableCollection<DataTagContainer> Children { get; set; }

        public DataTagContainer()
        {
            Properties = new ObservableCollection<DataTagPropertyContainer>();
            Children = new ObservableCollection<DataTagContainer>();
        }
    }
}
