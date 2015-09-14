﻿using MachineTagEditor.Infrastructure.Containers;
using MachineTagEditor.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Mvvm;
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
    public class OpenWizard : PubSubEvent<List<NameTypeContainer>> { }
    public class MachineConfigWizard : PubSubEvent<bool> { }
    public class AddAlarmConfigWizard : PubSubEvent<bool> { }
    public class NavigateToPage : PubSubEvent<string> { };
    public class DisplayMessage : PubSubEvent<string> { };
    public class LoadXMLFile : PubSubEvent<bool> { };
    public class SaveXMLFile : PubSubEvent<bool> { };

    public class RibbonEvent : PubSubEvent<string> { }
}
