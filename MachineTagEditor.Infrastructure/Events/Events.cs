using MachineTagEditor.Infrastructure.Containers;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineTagEditor.Infrastructure.Events
{
    public class TagsUpdated: PubSubEvent<bool> {}
    public class SaveSetting : PubSubEvent<SaveSettingContainer> { }
    public class ChangeWizardVisibility : PubSubEvent<Visibility> { }

}
