using MCM.Core.Enum;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using MachineTagEditor.Infrastructure.Extensions;
using MachineTagEditor.Infrastructure.Extensions.XML;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;

namespace MachineTagEditor.Modules.TagManager.AddDataType
{

    //TODO: Add all the info you need to add a data type.
    // current data types, current files, etc.
    public class ViewModel: DependencyObject
    {

        public DelegateCommand AddToFile { get; set; }

        public TagManagerService TagService {get;set;}

        public ViewModel(TagManagerService _tm)
        {
            TagService = _tm;
            TagService.XmlFileList.CollectionChanged += XmlFileList_CollectionChanged;
            USMetricVisibility = Visibility.Collapsed;
            BaseTypesList = new ObservableCollection<string>();
            ParentsList = new ObservableCollection<string>();
            XmlFileList = TagService.XmlFileList;
            AddToFile = new DelegateCommand(OnAddToFile);

            foreach (DataType val in Enum.GetValues(typeof(DataType)))
                BaseTypesList.Add(val.ToString());

            //get all the base unit defs
            foreach (XmlNode val in TagService.AllTagsXML.Where(x => x.ContainsAttributeNonNull("dataType")))
                    ParentsList.Add(val.Attributes["name"].Value);

            //get all the derived unit defs
            //List<string> tempList = new List<string>();
            //foreach (string name in ParentsList)
                //foreach (XmlNode val in TagService.AllTagsXML.Values.Where(x => x.ContainsAttributeNonNull("parent") &&
                   //                                                          x.Attributes["parent"].Value.Contains(name)))
                    //tempList.Add(val.Attributes["name"].Value);

            //foreach (string name in tempList)
                //ParentsList.Add(name);

            //TODO: Create a custom converter so that "None" ends up at the top.
            ParentsList.Add("None");
            SelectedParentText = ParentsList.ElementAt(0);

            //foreach(XmlDataProvider name in TagService.AllXMLFiles.Values)

        }

        void XmlFileList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            XmlFileList = TagService.XmlFileList;
        }





        public XmlContainer SelectedFile
        {
            get { return (XmlContainer)GetValue(SelectedFileProperty); }
            set { SetValue(SelectedFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFileProperty =
            DependencyProperty.Register("SelectedFile", typeof(XmlContainer), typeof(ViewModel), new UIPropertyMetadata(null));

        

        


        private void OnAddToFile()
        {
            if(!TagService.XmlFileList.Contains(SelectedFile))
            {
                MessageBox.Show(SelectedFile + " file not found!");
                return;
            }

            Dictionary<string, string> attrList = new Dictionary<string, string>();


            if (ParentNode != null)
            {
                foreach (XmlAttribute attr in ParentNode.Attributes)
                    attrList.AddOrUpdate(attr.Name, attr.Value);

                attrList.AddOrUpdate("parent", ParentNode.AttributeValue("name"));
            }



                attrList.AddOrUpdate("name",Name);
            

            if (IsChecked)
            {
                attrList.AddOrUpdate("unitsMetric", UnitOneText);
                attrList.AddOrUpdate("roundMetric", UnitOneRounding);
                attrList.AddOrUpdate("toUs", UnitOneConversion);

                attrList.AddOrUpdate("unitsUs", UnitTwoText);
                attrList.AddOrUpdate("roundUs", UnitTwoRounding);
                attrList.AddOrUpdate("toMetric", UnitTwoConversion);
            }
            else
            {
                attrList.AddOrUpdate("units", UnitOneText);
                attrList.AddOrUpdate("rounding", UnitOneRounding);
            }

            TagService.AddNodeToFile(SelectedFile, "virtual", attrList);
        }

        public ObservableCollection<XmlContainer> XmlFileList
        {
            get { return (ObservableCollection<XmlContainer>)GetValue(XmlFileListProperty); }
            set { SetValue(XmlFileListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XmlFileList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XmlFileListProperty =
            DependencyProperty.Register("XmlFileList", typeof(ObservableCollection<XmlContainer>), typeof(ViewModel), new UIPropertyMetadata(null));


        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ViewModel), new UIPropertyMetadata(false, new PropertyChangedCallback(OnIsCheckedChanged)));

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue.Equals(false))
                (d as ViewModel).USMetricVisibility = Visibility.Collapsed;
            else
                (d as ViewModel).USMetricVisibility = Visibility.Visible;
        }

        

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));




        public XmlNode ParentNode
        {
            get { return (XmlNode)GetValue(ParentNodeProperty); }
            set { SetValue(ParentNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ParentNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParentNodeProperty =
            DependencyProperty.Register("ParentNode", typeof(XmlNode), typeof(ViewModel), new UIPropertyMetadata(null));

        

        public string SelectedParentText
        {
            get { return (string)GetValue(SelectedParentTextProperty); }
            set { SetValue(SelectedParentTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedParentText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedParentTextProperty =
            DependencyProperty.Register("SelectedParentText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null,new PropertyChangedCallback(OnSelectedTextChanged)));

        private static void OnSelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
 	        ViewModel vm = (ViewModel)d;
            string newValue = (string)e.NewValue;


            if (!newValue.ToLower().Contains("none"))
            {
                vm.ClearValue(ParentNodeProperty);
 
                try{vm.ParentNode = vm.TagService.GetNodeByNameAttribute(newValue); }
                catch { MessageBox.Show("Couldn't find parent " + newValue); }

                vm.EnableOnNoParent = false;
                if (vm.ParentNode.ContainsAttributeNonNull("toMetric"))
                {
                    vm.USMetricVisibility = Visibility.Visible;
                    vm.IsChecked = true;
                    vm.UnitOneConversion = vm.ParentNode.AttributeValueOrDefault("toUs");
                    vm.UnitOneRounding = vm.ParentNode.AttributeValueOrDefault("roundMetric");
                    vm.UnitOneText = vm.ParentNode.AttributeValueOrDefault("unitsMetric");

                    vm.UnitTwoConversion = vm.ParentNode.AttributeValueOrDefault("toMetric");
                    vm.UnitTwoRounding = vm.ParentNode.AttributeValueOrDefault("roundUs");
                    vm.UnitTwoText = vm.ParentNode.AttributeValueOrDefault("unitsUs");
                    vm.SelectedSystemType = vm.ParentNode.AttributeValueOrDefault("dataType");
                }
                else
                {
                    vm.USMetricVisibility = Visibility.Collapsed;
                    vm.IsChecked = false;
                    vm.UnitOneRounding = vm.ParentNode.AttributeValueOrDefault("rounding");
                    vm.UnitOneText = vm.ParentNode.AttributeValueOrDefault("units");
                }
            }
            else
                vm.EnableOnNoParent = true;


        }





        public bool EnableOnNoParent
        {
            get { return (bool)GetValue(EnableOnNoParentProperty); }
            set { SetValue(EnableOnNoParentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnableOnNoParent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableOnNoParentProperty =
            DependencyProperty.Register("EnableOnNoParent", typeof(bool), typeof(ViewModel), new UIPropertyMetadata(null));

        

        

        public string SelectedSystemType
        {
            get { return (string)GetValue(SelectedSystemTypeProperty); }
            set { SetValue(SelectedSystemTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedSystemType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSystemTypeProperty =
            DependencyProperty.Register("SelectedSystemType", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));

        

        
        //TODO, make this an XMLNode List
        public ObservableCollection<string> ParentsList
        {
            get { return (ObservableCollection<string>)GetValue(ParentsListProperty); }
            set { SetValue(ParentsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ParentsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParentsListProperty =
            DependencyProperty.Register("ParentsList", typeof(ObservableCollection<string>), typeof(ViewModel), new UIPropertyMetadata(null));




        public ObservableCollection<string> BaseTypesList
        {
            get { return (ObservableCollection<string>)GetValue(BaseTypesListProperty); }
            set { SetValue(BaseTypesListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BaseTypesList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BaseTypesListProperty =
            DependencyProperty.Register("BaseTypesList", typeof(ObservableCollection<string>), typeof(ViewModel), new UIPropertyMetadata(null));

        

        public Visibility USMetricVisibility
        {
            get { return (Visibility)GetValue(USMetricVisibilityProperty); }
            set { SetValue(USMetricVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for USMetricVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty USMetricVisibilityProperty =
            DependencyProperty.Register("USMetricVisibility", typeof(Visibility), typeof(ViewModel), new UIPropertyMetadata(null));
     
       
      
        public string UnitOneText
        {
            get { return (string)GetValue(UnitOneTextProperty); }
            set { SetValue(UnitOneTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnitOneText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitOneTextProperty =
            DependencyProperty.Register("UnitOneText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));

    
        public string UnitOneConversion
        {
            get { return (string)GetValue(UnitOneConversionProperty); }
            set { SetValue(UnitOneConversionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnitOneConversion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitOneConversionProperty =
            DependencyProperty.Register("UnitOneConversion", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));



        public string UnitTwoText
        {
            get { return (string)GetValue(UnitTwoTextProperty); }
            set { SetValue(UnitTwoTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnitTwoText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitTwoTextProperty =
            DependencyProperty.Register("UnitTwoText", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));


        public string UnitTwoConversion
        {
            get { return (string)GetValue(UnitTwoConversionProperty); }
            set { SetValue(UnitTwoConversionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnitTwoConversion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitTwoConversionProperty =
            DependencyProperty.Register("UnitTwoConversion", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));



        public string UnitOneRounding
        {
            get { return (string)GetValue(UnitOneRoundingProperty); }
            set { SetValue(UnitOneRoundingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnitOneRounding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitOneRoundingProperty =
            DependencyProperty.Register("UnitOneRounding", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));


        public string UnitTwoRounding
        {
            get { return (string)GetValue(UnitTwoRoundingProperty); }
            set { SetValue(UnitTwoRoundingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnitTwoRounding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitTwoRoundingProperty =
            DependencyProperty.Register("UnitTwoRounding", typeof(string), typeof(ViewModel), new UIPropertyMetadata(null));



        

    }
}
