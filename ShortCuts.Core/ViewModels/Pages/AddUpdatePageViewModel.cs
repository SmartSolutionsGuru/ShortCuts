using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShortCuts.Core.ViewModels.Pages
{
    public class AddUpdatePageViewModel : BaseViewModel
    {
        #region Private Members
        private readonly DAL.Managers.Product.IProductManager _productManager;
        #endregion

        #region Constructor
        public AddUpdatePageViewModel(DAL.Managers.Product.IProductManager productManager)
        {
            _productManager = productManager;
        }
        #endregion

        #region Protected Methods
        protected override void OnActivate()
        {
            base.OnActivate();
            try
            {
                GetSuits();
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
        }
        public void Close()
        {
            TryClose();
        }
        public void Cancel()
        {
            TryClose();
        }

        public async void Submit()
        {
            try
            {
                if (SelectedShortCut == null)
                    return;
                if (string.IsNullOrEmpty(WinShortCutText) && string.IsNullOrEmpty(MacShortCutText))
                {
                    IsShortCutTextWritten = true;
                }
                else if (string.IsNullOrEmpty(ShortCutDescription))
                {
                    IsDescriptionTextWritten = true;
                }
                else
                {
                    //It's Means That User had Add new Record
                    if (IsDisplayProductListEditable)
                    {
                        FillSelectedShortCut();
                        var result = await _productManager.AddShortCutAsync(SelectedShortCut);
                        if (result)
                        {
                            // Opreation Completed SuccessFully So Close This Window
                            TryClose();
                        }
                    }
                    else
                    {
                        // We Consider That the User Is Only Edititng
                        FillSelectedShortCut();
                        var result = await _productManager.UpdateShortCutAsync(SelectedShortCut);
                        if (result)
                        {
                            // Opreation Completed SuccessFully So Close This Window
                            TryClose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public async void GetSuits()
        {
            try
            {
                ProductTypes = (await _productManager.GetProductTypesAsync()).ToList();
                if (SelectedShortCut != null)
                {
                    SelectedProductType = ProductTypes.Where(p => p.Id == SelectedShortCut?.ProductTypeId).FirstOrDefault();
                    GetProducts(SelectedProductType);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public async void GetProducts(DAL.Models.ProductTypeModel productType)
        {
            try
            {
                if (productType != null)
                {
                    if (SelectedShortCut != null)
                    {
                        Products = (await _productManager.LoadAppropriateSuitAsync(productType.Id)).ToList();
                        SelectedProduct = Products?.Where(x => x.Id == SelectedShortCut?.ProductId).FirstOrDefault();
                        FillSelectedShortCut();
                    }
                    else
                    {
                        IsDisplayProductListEditable = true;
                        Products = (await _productManager.LoadAppropriateSuitAsync(productType.Id)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }
        public void OnEditModeActive()
        {
            //if (SelectedShortCut != null)
            //{
            //    SelectedShortCut = new DAL.Models.ShortCutModel();
            //}
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Filled the Essantial Data For 
        /// Updating Or Eding Short Cut
        /// </summary>
        private void FillSelectedShortCut()
        {
            try
            {
                SelectedShortCut.ProductTypeId = SelectedProductType.Id.Value;
                SelectedShortCut.ProductId = SelectedProduct?.Id.Value ?? 0;
                //if (IsDisplayProductListEditable)
                //{
                    SelectedShortCut.WinKey = WinShortCutText;
                    SelectedShortCut.MacKey = MacShortCutText;
                    SelectedShortCut.Description = ShortCutDescription;
                //}
                //else
                //{
                //    WinShortCutText = SelectedShortCut.WinKey;
                //    MacShortCutText = SelectedShortCut.MacKey;
                //    ShortCutDescription = SelectedShortCut.Description;
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        #endregion

        #region Public Properties

        private string _MacShortCutText;
        /// <summary>
        /// New Or Edited Text For ShortCut
        /// </summary>
        public string MacShortCutText
        {
            get { return _MacShortCutText; }
            set { _MacShortCutText = value; NotifyOfPropertyChange(nameof(MacShortCutText)); }
        }

        private string _WinShortCutText;
        /// <summary>
        /// New Or Edited Text For ShortCut
        /// </summary>
        public string WinShortCutText
        {
            get { return _WinShortCutText; }
            set { _WinShortCutText = value; NotifyOfPropertyChange(nameof(WinShortCutText)); }
        }


        private string _ShortCutDescription;
        /// <summary>
        /// Description Text For New Or Edited ShortCut
        /// </summary>
        public string ShortCutDescription
        {
            get { return _ShortCutDescription; }
            set { _ShortCutDescription = value; NotifyOfPropertyChange(nameof(ShortCutDescription)); }
        }

        private List<DAL.Models.ProductTypeModel> _ProductTypes;
        /// <summary>
        /// List Of Product Type
        /// </summary>
        public List<DAL.Models.ProductTypeModel> ProductTypes
        {
            get { return _ProductTypes; }
            set { _ProductTypes = value; NotifyOfPropertyChange(nameof(ProductTypes)); }
        }

        private DAL.Models.ProductTypeModel _SelectedProductType;
        /// <summary>
        /// Selected ProductTYpe
        /// </summary>
        public DAL.Models.ProductTypeModel SelectedProductType
        {
            get { return _SelectedProductType; }
            set { _SelectedProductType = value; NotifyOfPropertyChange(nameof(SelectedProductType)); }
        }

        private List<DAL.Models.ProductModel> _Products;
        /// <summary>
        /// List Of Products
        /// </summary>
        public List<DAL.Models.ProductModel> Products
        {
            get { return _Products; }
            set { _Products = value; NotifyOfPropertyChange(nameof(Products)); }
        }

        private DAL.Models.ProductModel _SelectedProduct;
        /// <summary>
        /// Selected Product from Product Types
        /// </summary>
        public DAL.Models.ProductModel SelectedProduct
        {
            get { return _SelectedProduct; }
            set { _SelectedProduct = value; NotifyOfPropertyChange(nameof(SelectedProduct)); }
        }

        private DAL.Models.ShortCutModel _SelectedShortCut;
        /// <summary>
        /// For Edinting Selected ShortCut
        /// </summary>
        public DAL.Models.ShortCutModel SelectedShortCut
        {
            get { return _SelectedShortCut; }
            set { _SelectedShortCut = value; NotifyOfPropertyChange(nameof(SelectedShortCut)); }
        }

        private bool _IsDisplayProductListEditable;
        /// <summary>
        /// EditMode For Displaying List
        /// </summary>
        public bool IsDisplayProductListEditable
        {
            get { return _IsDisplayProductListEditable; }
            set { _IsDisplayProductListEditable = value; OnEditModeActive(); NotifyOfPropertyChange(nameof(IsDisplayProductListEditable)); }
        }
        private bool _IsShortCutTextWritten;
        /// <summary>
        /// Boolean Property For Error Message Handling of Shortcut
        /// </summary>
        public bool IsShortCutTextWritten
        {
            get { return _IsShortCutTextWritten; }
            set { _IsShortCutTextWritten = value; NotifyOfPropertyChange(nameof(IsShortCutTextWritten)); }
        }
        private bool _IsDescriptionTextWritten;
        /// <summary>
        /// Boolean Property For Error Message Handling of Description
        /// </summary>
        public bool IsDescriptionTextWritten
        {
            get { return _IsDescriptionTextWritten; }
            set { _IsDescriptionTextWritten = value; NotifyOfPropertyChange(nameof(IsDescriptionTextWritten)); }
        }

        #endregion
    }
}
