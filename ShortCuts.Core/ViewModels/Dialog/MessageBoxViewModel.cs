using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortCuts.Core.ViewModels.Dialog
{
   [Export(typeof(MessageBoxViewModel)),PartCreationPolicy( CreationPolicy.Shared)]
    public class MessageBoxViewModel : BaseViewModel
    {
        #region Private Members
        private readonly DAL.Managers.Product.IProductManager _productManager;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public MessageBoxViewModel(DAL.Managers.Product.IProductManager productManager)
        {
            _productManager = productManager;
        }
        #endregion

        #region Public Methods
        public void Cancel()
        {
            TryClose();
        }
        public void Close()
        {
            TryClose();
        }
        public async void Submit()
        {
            try
            {
                if (SelectedShortCut != null)
                    IsOpreationSuccessfull = await _productManager.RemoveShortCutAsync(SelectedShortCut.Id.Value);
                if (IsOpreationSuccessfull)
                    TryClose();
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }
        #endregion

        #region Properties
        public DAL.Models.ShortCutModel SelectedShortCut { get; set; }
        public bool IsOpreationSuccessfull { get; set; }
        #endregion
    }
}
