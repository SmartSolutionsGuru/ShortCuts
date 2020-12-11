using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShortCuts.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddUpdatePage.xaml
    /// </summary>
    public partial class AddUpdatePageView : UserControl
    {
        #region Private Members
        Core.ViewModels.Pages.AddUpdatePageViewModel ViewModel { get; set; }
        #endregion

        #region Cpnstructor

        public AddUpdatePageView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;

        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel = e.NewValue as Core.ViewModels.Pages.AddUpdatePageViewModel;
        }
        #endregion

        private void SoftwareTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var control = sender as ComboBox;
            if (control.SelectedItem != null)
            {
                var selecteItem = control.SelectedItem as DAL.Models.ProductTypeModel;
                if (selecteItem != null)
                    ViewModel.GetProducts(selecteItem);
            }
        }
    }
}
