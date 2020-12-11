using Caliburn.Micro;
using NLog;
using ShortCuts.Core.Commands;
using ShortCuts.Core.Helpers;
using ShortCuts.Core.Models;
using ShortCuts.Core.ViewModels.Dialog;
using ShortCuts.DAL.DatabaseHelpers;
using ShortCuts.DAL.Managers.Product;
using ShortCuts.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace ShortCuts.Core.ViewModels
{
    [Export(typeof(MainWindowViewModel)), PartCreationPolicy(CreationPolicy.Shared)]
    public class MainWindowViewModel : Conductor<Screen>
    {
        #region Managers
        private IProductManager _productManager;
        Logger LogMessage = NLog.LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor

        [ImportingConstructor]
        public MainWindowViewModel(IProductManager productManager)
        {
            _productManager = productManager;
        }
        #endregion

        #region Private & Protected Methods
        protected async override void OnActivate()
        {
            try
            {
                base.OnActivate();
                SoftwareTypeList = (await _productManager.GetProductTypesAsync()).OrderBy(x => x.ShortName).ToList();
                IsShortCutListAvailable = true;
                ShortCutSuggetions = new ShortCutSuggetionProvider();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load the Specific Software suit 
        /// </summary>
        /// <param name="productType"></param>
        public async void LoadSoftwareSuit(ProductTypeModel productType)
        {
            try
            {
                SoftwareNameList = (await _productManager.LoadAppropriateSuitAsync(productType.Id)).OrderBy(x => x.ShortName).ToList();
            }
            catch (Exception ex)
            {
                LogMessage.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Load Selected Software ShortCuts
        /// </summary>
        /// <param name="software"></param>
        public async void LoadSoftwareShortCuts(ProductModel software)
        {
            try
            {
                string key = string.Empty;
                if (software == null)
                    return;
                if (IsDisplayWindowShortcuts)
                    ShortCuts = (await _productManager.LoadProductShortCutsAsync(software.Id)).OrderBy(x => x.WinKey).ToList();
                else
                    ShortCuts = (await _productManager.LoadProductShortCutsAsync(software.Id)).OrderBy(x => x.MacKey).ToList();
                ShortCutSuggetions.ShortCutList = ShortCuts;
                ShortCuts = ShortCuts.Where(x => x.WinKey != string.Empty || x.WinKey != null).ToList();
                ShortCutSuggetions.IsWinShortCut = IsDisplayWindowShortcuts;
                if (SelectedSoftwareType?.Id == 1)
                {
                    //Here The ShortCut are Returning is For OS
                    //We are checking Which Os is Selected from List
                    if (software.ShortName.Equals("Windows") == true)
                    {
                        key = "Windows";
                        IsDisplayWindowShortcuts = true;
                    }
                    else if (software.ShortName.Equals("Mac") == true)
                    {
                        key = "Mac";
                        IsDisplayWindowShortcuts = false;
                    }
                    else
                    {
                        if (IsDisplayWindowShortcuts)
                        {
                            key = "Windows";
                            IsDisplayWindowShortcuts = true;
                        }
                        else
                            key = "Mac";
                    }
                }
                else
                {
                    if (IsDisplayWindowShortcuts)
                        key = "Windows";
                    else
                        key = "Mac";
                }

                ShortCutSuggetions.ShortCutList = RemoveEmptyKeys(key, ShortCutSuggetions.ShortCutList.ToList());
                //filter the shortcuts emty shortcuts of Mac and Windows 
                ShortCuts = ShortCutSuggetions.ShortCutList.ToList();

                if (ShortCutSuggetions?.ShortCutList?.Count() == 0)
                {
                    IsShortCutListAvailable = false;
                    ShortCuts = null;
                    NotifyOfPropertyChange(nameof(IsShortCutListAvailable));
                }
                else
                {
                    IsShortCutListAvailable = true;
                    NotifyOfPropertyChange(nameof(IsShortCutListAvailable));
                }
            }
            catch (Exception ex)
            {
                LogMessage.Error(ex.ToString());
            }
        }

        #region CRUD Opreations
        public async void AddShortCut(ShortCutModel model)
        {
            try
            {
                if (model != null)
                {
                    var viewModel = IoC.Get<Pages.AddUpdatePageViewModel>();
                    viewModel.SelectedShortCut = model;
                    viewModel.IsDisplayProductListEditable = true;
                    IDialogManager dlg = IoC.Get<IDialogManager>();
                    await dlg.ShowDialogAsync(viewModel);
                    await _productManager.LoadProductShortCutsAsync(viewModel.SelectedShortCut.ProductId);
                }
            }
            catch (Exception ex)
            {

                LogMessage.Error(ex.ToString());
            }
        }
        /// <summary>
        /// Perform The Remove Opreation
        /// </summary>
        /// <param name="model"></param>
        public async void RemoveShortCut(ShortCutModel model)
        {
            try
            {
                var viewModel = IoC.Get<MessageBoxViewModel>();
                viewModel.SelectedShortCut = model;
                IDialogManager dlg = IoC.Get<IDialogManager>();
                await dlg.ShowDialogAsync(viewModel);
                if (viewModel.IsOpreationSuccessfull)
                    viewModel.Close();
                await _productManager.LoadProductShortCutsAsync(SelectedSoftware.Id);
            }
            catch (Exception ex)
            {
                LogMessage.Error(ex.ToString());
            }
        }
        public async void UpdateShortCut(ShortCutModel model)
        {
            try
            {
                if (model != null)
                {
                    var viewModel = IoC.Get<Pages.AddUpdatePageViewModel>();
                    viewModel.SelectedShortCut = model;
                    IDialogManager dlg = IoC.Get<IDialogManager>();
                    await dlg.ShowDialogAsync(viewModel);
                }
            }
            catch (Exception ex)
            {
                LogMessage.Error(ex.ToString());
            }
        }
        #endregion

        #endregion

        #region Public Properties
        private string _VideoPath;
        /// <summary>
        /// String for Video Path
        /// </summary>
        public string VideoPath
        {
            get { return _VideoPath; }
            set { _VideoPath = value; NotifyOfPropertyChange(nameof(VideoPath)); }
        }
        /// <summary>
        /// 
        /// </summary>
        private bool _IsEditModeOpreational;

        public bool IsEditModeOpreational
        {
            get { return _IsEditModeOpreational; }
            set { _IsEditModeOpreational = value; NotifyOfPropertyChange(nameof(IsEditModeOpreational)); }
        }



        private ShortCutSuggetionProvider _ShortCutSuggetions;
        /// <summary>
        /// Property that will be used to Give suggetion in Search Box
        /// </summary>
        public ShortCutSuggetionProvider ShortCutSuggetions
        {
            get { return _ShortCutSuggetions; }
            set { _ShortCutSuggetions = value; NotifyOfPropertyChange(nameof(ShortCutSuggetions)); }
        }

        private List<ProductTypeModel> _SoftwareTypeList;
        /// <summary>
        /// Software List To Choose your list Like Microsoft Office,Adobe Suit, Corel Suit etc...
        /// </summary>
        public List<ProductTypeModel> SoftwareTypeList
        {
            get { return _SoftwareTypeList; }
            set { _SoftwareTypeList = value; NotifyOfPropertyChange(nameof(SoftwareTypeList)); }
        }

        private bool _IsWinKeyAvailable;
        /// <summary>
        /// Verify that Windows Shortcut are available in Db
        /// </summary>
        public bool IsWinKeyAvailable
        {
            get { return _IsWinKeyAvailable; }
            set { _IsWinKeyAvailable = value; NotifyOfPropertyChange(nameof(IsWinKeyAvailable)); }
        }

        private bool _IsMacKeyAvailable;
        /// <summary>
        /// Verify that Mac Shortcut are available in Db
        /// </summary>
        public bool IsMacKeyAvailable
        {
            get { return _IsMacKeyAvailable; }
            set { _IsMacKeyAvailable = value; NotifyOfPropertyChange(nameof(IsMacKeyAvailable)); }
        }

        private ProductTypeModel _SelectedSoftwareType;
        /// <summary>
        /// Selected Software Type Like Adobe,Microsoft Office etc...
        /// </summary>
        public ProductTypeModel SelectedSoftwareType
        {
            get { return _SelectedSoftwareType; }
            set
            {
                _SelectedSoftwareType = value;
                if (value != null)
                {
                    LoadSoftwareSuit(value);
                }
                NotifyOfPropertyChange(nameof(SelectedSoftwareType));
            }
        }

        private bool _IsEditModeOn;

        public bool IsEditModeOn
        {
            get { return _IsEditModeOn; }
            set { _IsEditModeOn = value; NotifyOfPropertyChange(nameof(IsEditModeOn)); }
        }

        private List<ProductModel> _SoftwareNameList;
        /// <summary>
        /// Display the software from Selected Suit Like Adobe Illistrater, Adobe photoshop etc...
        /// </summary>
        public List<ProductModel> SoftwareNameList
        {
            get { return _SoftwareNameList; }
            set { _SoftwareNameList = value; NotifyOfPropertyChange(nameof(SoftwareNameList)); }
        }


        private ProductModel _SelectedSoftware;
        /// <summary>
        /// Select the software from Selected Suit Like Illustrater,Or photoshop etc..
        /// </summary>
        public ProductModel SelectedSoftware
        {
            get { return _SelectedSoftware; }
            set
            {
                _SelectedSoftware = value;
                if (value != null)
                {
                    LoadSoftwareShortCuts(SelectedSoftware);
                }
                NotifyOfPropertyChange(nameof(SelectedSoftware));
            }
        }

        private List<ShortCutModel> _ShortCuts;
        /// <summary>
        /// List Of Short Cuts for for Specific Tool
        /// </summary>
        public List<ShortCutModel> ShortCuts
        {
            get { return _ShortCuts; }
            set { _ShortCuts = value; NotifyOfPropertyChange(nameof(ShortCuts)); }
        }

        private ShortCutModel _SelectedShortCut;
        /// <summary>
        /// Selected ShortCut 
        /// </summary>
        public ShortCutModel SelectedShortCut
        {
            get { return _SelectedShortCut; }
            set { _SelectedShortCut = value; NotifyOfPropertyChange(nameof(SelectedShortCut)); }
        }



        private bool? _IsShortCutListAvailable;

        public bool? IsShortCutListAvailable
        {
            get { return _IsShortCutListAvailable; }
            set { _IsShortCutListAvailable = value; NotifyOfPropertyChange(nameof(_IsShortCutListAvailable)); }
        }
        private bool _EditPageVisible;

        public bool EditPageVisible
        {
            get { return _EditPageVisible; }
            set
            {
                if (_EditPageVisible == value)
                    return;
                _EditPageVisible = value;
                NotifyOfPropertyChange(nameof(EditPageVisible));
            }
        }
        private bool _IsDisplayWindowShortcuts;
        /// <summary>
        /// Verify That Windows Shortcut Display or Mac
        /// </summary>
        public bool IsDisplayWindowShortcuts
        {
            get { return _IsDisplayWindowShortcuts; }
            set { _IsDisplayWindowShortcuts = value; NotifyOfPropertyChange(nameof(IsDisplayWindowShortcuts)); }
        }

        private bool _IsOsShortCutSelected;
        /// <summary>
        /// Verify is it OS shortcuts or Not
        /// </summary>
        public bool IsOsShortCutSelected
        {
            get { return _IsOsShortCutSelected; }
            set { _IsOsShortCutSelected = value; NotifyOfPropertyChange(nameof(IsOsShortCutSelected)); }
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Remove the Keys if they are null or Empty
        /// </summary>
        /// <param name="key"> the key in the loop</param>
        /// <param name="sourceList"> shortcut list of selected item</param>
        /// <returns></returns>
        private List<ShortCutModel> RemoveEmptyKeys(string key, List<ShortCutModel> sourceList)
        {
            List<ShortCutModel> filterdList = new List<ShortCutModel>();
            try
            {
                if (string.Equals(key, "Windows"))
                {
                    foreach (var item in sourceList)
                    {
                        string s = item.WinKey;
                        if (!string.IsNullOrEmpty(s))
                        {
                            string replacement = s.Replace(@"\t|\n|\r", "").Replace("\t\t", "");

                            if (!string.IsNullOrEmpty(replacement))
                                filterdList.Add(item);
                        }
                    }
                    if (filterdList.Count == 0)
                        IsWinKeyAvailable = false;
                }
                else if (string.Equals(key, "Mac"))
                {
                    foreach (var item in sourceList)
                    {
                        string s = item.MacKey;
                        if (!string.IsNullOrEmpty(s))
                        {
                            string replacement = s.Replace(@"\t|\n|\r", "").Replace("\t\t", "");
                            if (!string.IsNullOrEmpty(replacement))
                                filterdList.Add(item);
                        }
                    }
                    if (filterdList.Count == 0)
                        IsMacKeyAvailable = false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return filterdList;
        }

        #endregion

    }
}