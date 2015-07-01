//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
namespace XMLEditor
{
    using System;
    using System.Windows;
    using Microsoft.Practices.Prism.Logging;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.UnityExtensions;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Prism.Regions;
    using WRAExample.Utility;
    using System.Reflection;
    using MachineTagEditor.Modules.Alarms;
    using MachineTagEditor.Modules.XMLDocument;
    using MachineTagEditor.Modules.Toolbar;
    using MachineTagEditor.Modules.Navigation;
    using MachineTagEditor.Modules.Units;

    /// <summary>
    /// Initializes Prism to start this quickstart Prism application to use Unity.
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="DependencyObject"/>, the
        /// <see cref="Bootstrapper"/> will attach the default <seealso cref="Microsoft.Practices.Composite.Regions.IRegionManager"/> of
        /// the application in its <see cref="Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionManagerProperty"/> attached property
        /// in order to be able to add regions by using the <seealso cref="Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionNameProperty"/>
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implemention ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }


        /// <summary>
        /// Configures the <see cref="IUnityContainer"/>. May be overwritten in a derived class to add specific
        /// type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        /// <summary>
        /// Returns the module catalog that will be used to initialize the modules.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IModuleCatalog"/> that will be used to initialize the modules.
        /// </returns>
        /// <remarks>
        /// When using the default initialization behavior, this method must be overwritten by a derived class.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            base.CreateModuleCatalog();
            base.ModuleCatalog = new ModuleCatalog();

            Type XMLDocumentModuleType = typeof(XMLDocumentModule);

            ModuleCatalog.AddModule(
                new ModuleInfo()
                {
                    ModuleName = XMLDocumentModuleType.Name,
                    ModuleType = XMLDocumentModuleType.AssemblyQualifiedName,
                });


            Type AlarmsModuleType = typeof(AlarmsModule);

            ModuleCatalog.AddModule(
                new ModuleInfo()
                {
                    ModuleName = AlarmsModuleType.Name,
                    ModuleType = AlarmsModuleType.AssemblyQualifiedName,
                });




            Type ToolbarModuleType = typeof(ToolbarModule);

            ModuleCatalog.AddModule(
                new ModuleInfo()
                {
                    ModuleName = ToolbarModuleType.Name,
                    ModuleType = ToolbarModuleType.AssemblyQualifiedName,
                });

            Type NavigationModuleType = typeof(NavigationModule);

            ModuleCatalog.AddModule(
                new ModuleInfo()
                {
                    ModuleName = NavigationModuleType.Name,
                    ModuleType = NavigationModuleType.AssemblyQualifiedName,
                });

            Type UnitsModuleType = typeof(UnitsModule);

            ModuleCatalog.AddModule(
                new ModuleInfo()
                {
                    ModuleName = UnitsModuleType.Name,
                    ModuleType = UnitsModuleType.AssemblyQualifiedName,
                });

            return base.ModuleCatalog;
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            
            RegionAdapterMappings regionAdapterMappings = Container.TryResolve<RegionAdapterMappings>();

            if (regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(Window), new WindowRegionAdapter(Container.TryResolve<IRegionBehaviorFactory>()));
            }

            return base.ConfigureRegionAdapterMappings();
        }
    }
}
