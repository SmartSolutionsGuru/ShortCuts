using Caliburn.Micro;
using ShortCuts.DAL.Managers.Dialog;
using System.ComponentModel.Composition;

namespace ShortCuts.UI
{
    [Export(typeof(IShell)),PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellViewModel : Conductor<Screen>,IShell
    {
        #region Private Members
        private readonly IDialogManager _dialog;
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Constructor
        public ShellViewModel()
        {
            ActivateItem(new Core.ViewModels.MainWindowViewModel());
        }
        public ShellViewModel(IWindowManager windowManager,IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
        }
        #endregion

        #region Public Properties
        public IDialogManager Dialog => _dialog;
        #endregion
      
    }
}
