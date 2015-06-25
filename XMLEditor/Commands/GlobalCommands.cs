using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLEditor.Commands
{
    public static class GlobalCommands
    {
        public static DelegateCommand saveAs { get; set; }
        public static DelegateCommand save { get; set; }
        public static DelegateCommand AddXMLFile { get; set; }
        public static DelegateCommand DeleteNode { get; set; }
        public static DelegateCommand RemoveXMLFile { get; set; }
        public static DelegateCommand ClearSettings { get; set; }
        public static DelegateCommand AlarmView { get; set; }
        public static DelegateCommand AddAlarm { get; set; }
        public static DelegateCommand ViewDataTags { get; set; }

    }
}
