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

        public SaveSettingContainer(string Name, object Setting, Type type)
        {
            setting = Setting;
            settingName = Name;
            settingType = type;
        }    
        
        
    }
}
