using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Infrastructure.Containers
{
    public class SaveSettingContainer
    {
        public object setting { get; set; }
        public string settingName { get; set; }
        public Type settingType { get; set; }

        public SaveSettingContainer(string Name = null, object Setting = null, Type type = null)
        {
            if(setting != null) setting = Setting;
            if(settingName != null) settingName = Name;
            if(type != null) settingType = type;
        }    
        
        
    }
}
