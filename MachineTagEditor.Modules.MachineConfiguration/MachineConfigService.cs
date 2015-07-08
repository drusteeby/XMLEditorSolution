using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Modules.MachineConfiguration
{
    public class MachineConfigContainer
    {
        public string Name { get; set; }
        public List<string> _filePaths = new List<string>();

        public void AddExisitingFile(string path)
        {
            _filePaths.Add(path);
        }

        public void AddExisitingFile(string dir, string fileName)
        {
            if (!Directory.Exists(dir) || !File.Exists(fileName))
                throw new FileNotFoundException();

            _filePaths.Add(dir + fileName);
        }

    }
    public class MachineConfigService
    {
        Dictionary<string,MachineConfigContainer> _configurationList;

        public MachineConfigService()
        {
            _configurationList = new Dictionary<string,MachineConfigContainer>();

            //Load the saved templates
            foreach(string configName in Properties.Settings.Default.MachineTemplates)
            {
                IEnumerable<string> fileLines = File.ReadLines(Properties.Settings.Default.configPath + configName);

                _configurationList.Add(fileLines.First(),new MachineConfigContainer());
                

                for(int i = 1; i < fileLines.Count(); i++)
                    _configurationList.Values.Last().AddExisitingFile(fileLines.ElementAt(i));
            }
        }

        
        public void AddExistingFile(string configName,string path)
        {
            MachineConfigContainer _container = null;
            if (_configurationList.ContainsKey(configName))
                _configurationList.TryGetValue(configName, out _container);

            if (_container != null)
                _container.AddExisitingFile(path);
        }

        public void AddNewConfig(string configName)
        {
            if (_configurationList.ContainsKey(configName))
                throw new Exception();

            if (Properties.Settings.Default.MachineTemplates == null)
                Properties.Settings.Default.MachineTemplates = new System.Collections.Specialized.StringCollection();

            Properties.Settings.Default.MachineTemplates.Add(configName);
            Properties.Settings.Default.Save();

            _configurationList.Add(configName, new MachineConfigContainer());
        }
        

    }
}
