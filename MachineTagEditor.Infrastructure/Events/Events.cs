using MachineTagEditor.Infrastructure.Containers;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Infrastructure.Events
{
    public class TagsUpdated: PubSubEvent<bool> {}
    public class SaveSetting : PubSubEvent<SaveSettingContainer> { }
}
