using MachineTagEditor.Infrastructure.Extensions;
using MachineTagEditor.Infrastructure.Extensions.XML;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace MachineTagEditor.Modules.TagManager.AddParameter
{
    public class ViewModel: AddTagViewModelBase
    {

        public DelegateCommand<DragEventArgs> dropCommand { get; set; }



        public ViewModel(TagManagerService _tm):base(_tm)
        {
            dropCommand = new DelegateCommand<DragEventArgs>(OnDropCommand);

            foreach (XmlNode node in base.TagService.DataTypesList)
                base.ParentsList.Add(node.Attributes["name"].Value);

            base.Add = new DelegateCommand(OnAddParameter, CanAddParameter);

            Group = "partEdit";
            Page = "Auto Part";
            Name = "Part.";


        }


        private void OnDropCommand(DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnAddParameter()
        {
            if (!base.TagService.XmlFileList.Contains(SelectedFile))
            {
                MessageBox.Show(SelectedFile + " file not found!");
                return;
            }

            Dictionary<string, string> attrList = new Dictionary<string, string>();


            if (base.ParentNode != null)
            {
                //foreach (XmlAttribute attr in ParentNode.Attributes)
                    //attrList.AddOrUpdate(attr.Name, attr.Value);

                attrList.AddOrUpdate("parent", ParentNode.AttributeValue("name"));
            }



            attrList.AddOrUpdate("name", Name);
            attrList.AddOrUpdate("group", Group);

            if (!String.IsNullOrEmpty(base.Text)) attrList.AddOrUpdate("text", base.Text);
            if (!String.IsNullOrEmpty(base.Page)) attrList.AddOrUpdate("page", base.Page);
            if (!String.IsNullOrEmpty(base.Min)) attrList.AddOrUpdate("page", base.Min);
            if (!String.IsNullOrEmpty(base.Max)) attrList.AddOrUpdate("prepend", base.Max);

            base.TagService.AddNodeToFile(base.SelectedFile, "tag", attrList);

            base.CloseWindow();
        }

        private bool CanAddParameter()
        {
            //TODO: Implement
            return true;
        }
    }
}
