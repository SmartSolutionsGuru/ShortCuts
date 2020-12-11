using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;

namespace ShortCuts.UI
{
    public class AppBootstrapper : BootstrapperBase
    {
        public SimpleContainer container { get; private set; }

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<Core.ViewModels.Dialog.IDialogManager, Core.ViewModels.Dialog.DialogBaseViewModel>();
            container.PerRequest<Core.IShell, Core.ViewModels.ShellViewModel>();
            container.RegisterPerRequest(typeof(Core.ViewModels.MainWindowViewModel), null, typeof(Core.ViewModels.MainWindowViewModel));
            container.RegisterPerRequest(typeof(DAL.Managers.Product.IProductManager), null, typeof(DAL.Managers.Product.ProductManager));
            container.RegisterPerRequest(typeof(Core.ViewModels.Pages.AddUpdatePageViewModel), null, typeof(Core.ViewModels.Pages.AddUpdatePageViewModel));
            container.RegisterPerRequest(typeof(Core.ViewModels.Dialog.MessageBoxViewModel), null, typeof(Core.ViewModels.Dialog.MessageBoxViewModel));
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "ShortCuts.UI.Views",
                DefaultSubNamespaceForViewModels = "ShortCuts.Core.ViewModels",
            };
            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            try
            {
                base.OnStartup(sender,e);
                DisplayRootViewFor<Core.IShell>();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = base.SelectAssemblies().ToList();
            assemblies.Add(typeof(Core.ViewModels.MainWindowViewModel).GetTypeInfo().Assembly);
            return assemblies;
        }
    }
}

