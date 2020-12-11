using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using ShortCuts.Core.Commands;
using ShortCuts.Core.ViewModels.Dialog;
using ShortCuts.DAL.DatabaseHelpers;
using SmartBooks.UI.Helpers;

namespace ShortCuts.Core.ViewModels
{
    [Export(typeof(IShell)), PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellViewModel : Conductor<Screen>, IShell, IHandle<Screen>
    {

        #region Constructor
        public ShellViewModel() { }

        public ShellViewModel(Window window)
        {
            mWindow = window;
        }

        [ImportingConstructor]
        public ShellViewModel(IWindowManager windowManager
                              , IEventAggregator eventAggregator
                              , IDialogManager dialogManager
                              ,NLog.ILogger log)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _dialog = dialogManager;
            _log = log;
            CreateVideoPath();

        }
        #endregion

        #region Protected Methods
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            // Listen out for the window resizing
            mWindow = view as Window;
            if (mWindow != null)
            {
                mWindow.StateChanged += (sender, e) =>
                {
                    // Fire off events for all properties that are affected by a resize
                    WindowResized();
                };

                mWindowResizer = new WindowResizer(mWindow);

                // Listen out for dock changes
                mWindowResizer.WindowDockChanged += (dock) =>
                {
                    // Store last position
                    mDockPosition = dock;

                    // Fire off resize events
                    WindowResized();
                };

                // On window being moved/dragged
                mWindowResizer.WindowStartedMove += () =>
                {
                    // Update being moved flag
                    BeingMoved = true;
                };

                // Fix dropping an undocked window at top which should be positioned at the
                // very top of screen
                mWindowResizer.WindowFinishedMove += () =>
                {
                    // Update being moved flag
                    BeingMoved = false;

                    // Check for moved to top of window and not at an edge
                    if (mDockPosition == WindowDockPosition.Undocked && mWindow.Top == mWindowResizer.CurrentScreenSize.Top)
                        // If so, move it to the true top (the border size)
                        mWindow.Top = -OuterMarginSize.Top;
                };
            }
            else
                return;
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();
           
            CreateConnectionString();
            CreateDbConnection();
            Handle(IoC.Get<MainWindowViewModel>());
        }
        protected override void OnActivate()
        {
            try
            {
                base.OnActivate();
                
                MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));
                _eventAggregator?.Subscribe(this);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator?.Unsubscribe(this);
        }
        #endregion

        #region Private Members
        private readonly IDialogManager _dialog;
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly NLog.ILogger _log;

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// The window resizer helper that keeps the window size correct in various states
        /// </summary>
        private WindowResizer mWindowResizer;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private Thickness mOuterMarginSize = new Thickness(5);

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int mWindowRadius = 10;

        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;
        #endregion

        #region Public Properties
        public IDialogManager Dialog => _dialog;

        private bool _IsOsShortCutSelected;
        /// <summary>
        /// Verify is it OS shortcuts or Not
        /// </summary>
        public bool IsOsShortCutSelected
        {
            get { return _IsOsShortCutSelected; }
            set { _IsOsShortCutSelected = value; NotifyOfPropertyChange(nameof(IsOsShortCutSelected)); }
        }
        private bool _IsDisplayWindowShortcuts;
        /// <summary>
        /// Verify That Windows Shortcut Display or Mac
        /// </summary>
        public bool IsDisplayWindowShortcuts
        {
            get { return _IsDisplayWindowShortcuts; }
            set { _IsDisplayWindowShortcuts = value; NotifyOfPropertyChange(nameof(IsDisplayWindowShortcuts)); UpdateWindowShortCut(); }
        }
        private double _WindowMinimumWidth = 1000;
        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth
        {
            get { return _WindowMinimumWidth; }
            set { _WindowMinimumWidth = value; NotifyOfPropertyChange(nameof(WindowMinimumWidth)); }
        }

        private double _WindowMinimumHeight = 725;
        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight
        {
            get { return _WindowMinimumHeight; }
            set { _WindowMinimumHeight = value; NotifyOfPropertyChange(nameof(WindowMinimumHeight)); }
        }


        private bool _BeingMoved;
        /// <summary>
        /// True if the window is currently being moved/dragged
        /// </summary>
        public bool BeingMoved
        {
            get { return _BeingMoved; }
            set { _BeingMoved = value; NotifyOfPropertyChange(nameof(BeingMoved)); }
        }


        private bool _IsLoggedIn;
        /// <summary>
        /// flage that checks that is it loggeed in or not
        /// </summary>
        public bool IsLoggedIn
        {
            get { return _IsLoggedIn; }
            set { _IsLoggedIn = value; NotifyOfPropertyChange(nameof(IsLoggedIn)); }
        }


        private bool m_IsLoading;
        public bool IsLoading
        {
            get { return m_IsLoading; }
            set { m_IsLoading = value; NotifyOfPropertyChange(nameof(IsLoading)); }
        }
        /// <summary>
        /// True if the window should be borderless because it is docked or maximized
        /// </summary>
        public bool Borderless => (mWindow?.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked);

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder => mWindow?.WindowState == WindowState.Maximized ? 0 : 4;

        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness
        {
            get => new Thickness(OuterMarginSize.Left + ResizeBorder,
                           OuterMarginSize.Top + ResizeBorder,
                           OuterMarginSize.Right + ResizeBorder,
                           OuterMarginSize.Bottom + ResizeBorder);
            set => NotifyOfPropertyChange(nameof(ResizeBorderThickness));
        }

        private Thickness _InnerContentPadding;
        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding
        {
            get { return _InnerContentPadding = new Thickness(0); }
            set { _InnerContentPadding = value; NotifyOfPropertyChange(nameof(InnerContentPadding)); }
        }


        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginSize
        {
            // If it is maximized or docked, no border
            get => mWindow?.WindowState == WindowState.Maximized ? mWindowResizer.CurrentMonitorMargin : (Borderless ? new Thickness(0) : mOuterMarginSize);
            set => mOuterMarginSize = value;
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            // If it is maximized or docked, no border
            get => Borderless ? 0 : mWindowRadius;
            set => mWindowRadius = value;
            
        }

        /// <summary>
        /// The rectangle border around the window when docked
        /// </summary>
        public int FlatBorderThickness => Borderless && mWindow.WindowState != WindowState.Maximized ? 1 : 0;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        private int _TitleHeight = 42;
        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight
        {
            get { return _TitleHeight; }
            set { _TitleHeight = value; NotifyOfPropertyChange(nameof(TitleHeight)); }
        }

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

        private bool _DimmableOverlayVisible;
        /// <summary>
        /// True if we should have a dimmed overlay on the window
        /// such as when a popup is visible or the window is not focused
        /// </summary>
        public bool DimmableOverlayVisible
        {
            get { return _DimmableOverlayVisible; }
            set { _DimmableOverlayVisible = value; NotifyOfPropertyChange(nameof(DimmableOverlayVisible)); }
        }

        private string _VideoPath;
         /// <summary>
         /// String for Video Path
         /// </summary>
        public string VideoPath
        {
            get { return _VideoPath; }
            set { _VideoPath = value; NotifyOfPropertyChange(nameof(VideoPath)); }
        }


        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        public Point GetMousePosition()
        {
            return mWindowResizer.GetCursorPosition();
        }

        /// <summary>
        /// If the window resizes to a special position (docked or maximized)
        /// this will update all required property change events to set the borders and radius values
        /// </summary>
        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            NotifyOfPropertyChange(nameof(Borderless));
            NotifyOfPropertyChange(nameof(FlatBorderThickness));
            NotifyOfPropertyChange(nameof(ResizeBorderThickness));
            NotifyOfPropertyChange(nameof(OuterMarginSize));
            NotifyOfPropertyChange(nameof(WindowRadius));
            NotifyOfPropertyChange(nameof(WindowCornerRadius));
        }


        #endregion

        #region Public Methods
        /// <summary>
        /// Create the Connection with database
        /// </summary>
        /// <returns></returns>
        private string CreateConnectionString()
        {
            string constring = string.Empty;
            try
            {
                string fileName = @"\Assets\ShortCuts.db";
                string fullPath = Path.GetFullPath(fileName);
                if (!string.IsNullOrEmpty(fullPath))
                {
                    //ConnectionString = fullPath;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }

            return constring;
        }

        private void CreateVideoPath()
        {
            var path = Environment.CurrentDirectory;
            var fileName = "Assets\\SortCuts.mp4";
            string[] appPath = path.Split(new string[] { "bin" }, StringSplitOptions.None);
            VideoPath = string.Concat(appPath[0], fileName);

        }
        /// <summary>
        /// Helper Method that get the Cource Path
        /// And create Connection string for Sqlite
        /// </summary>
        public void CreateDbConnection()
        {
            string path = Environment.CurrentDirectory;
            var fileName = "Assets\\ShortCuts.db";
            string[] appPath = path.Split(new string[] { "bin" }, StringSplitOptions.None);
            ConnectionInfo.Instance.ConnectionString = string.Concat("URI=file:", appPath[0], fileName);

        }
        public void Handle(Screen screen)
        {
            if (screen is MainWindowViewModel)
            {
                (screen as MainWindowViewModel).IsDisplayWindowShortcuts = IsDisplayWindowShortcuts;
                (screen as MainWindowViewModel).IsOsShortCutSelected = IsOsShortCutSelected;
                (screen as MainWindowViewModel).VideoPath = VideoPath;
                ActivateItem(screen);
            }

        }
        public void UpdateWindowShortCut()
        {
            try
            {
                if (ActiveItem is MainWindowViewModel)
                {

                    var resultScreen = ActiveItem;
                    (resultScreen as MainWindowViewModel).IsDisplayWindowShortcuts = IsDisplayWindowShortcuts;
                    (resultScreen as MainWindowViewModel).IsOsShortCutSelected = IsOsShortCutSelected;
                    (resultScreen as MainWindowViewModel).LoadSoftwareShortCuts((resultScreen as MainWindowViewModel).SelectedSoftware);
                }

            }
            catch (System.Exception ex)
            {

                ex.ToString();
            }
        }

        public void MinimizeWindow()
        {
            try
            {
                mWindow.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion

        #region Commnads
        public RelayCommand MenuCommand { get; set; }
        public RelayCommand MaximizeCommand { get; set; }
        public RelayCommand MinimizeCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        #endregion

    }
}
