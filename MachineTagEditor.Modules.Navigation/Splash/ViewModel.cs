using MachineTagEditor.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineTagEditor.Modules.Navigation.Splash
{
    public class ViewModel: DependencyObject
    {
        [Dependency]
        public IRegionManager regionManager { get; set; }

        public ViewModel()
        {
            Options = new ObservableCollection<CommandContainer>();

            Options.Add(new CommandContainer("Exit",new DelegateCommand(OnExitCommand)));
        }

        public void OnExitCommand()
        {
            regionManager.RequestNavigate(RegionNames.OverlayRegion, "test");
        }

        public ObservableCollection<CommandContainer> Options
        {
            get { return (ObservableCollection<CommandContainer>)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Options.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register("Options", typeof(ObservableCollection<CommandContainer>), typeof(ViewModel), new UIPropertyMetadata(null));

        

    }
}
