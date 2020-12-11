using ShortCuts.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShortCuts.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : UserControl
    {
        #region Private Members
        MainWindowViewModel ViewModel { get; set; }
        #endregion

        #region Constructor
        public MainWindowView()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods

        private void SoftwareTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var control = sender as ComboBox;
                if (control?.SelectedItem != null)
                {
                    (DataContext as MainWindowViewModel).ShortCuts = null;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            VideoGrid.Visibility = Visibility.Collapsed;
            ShortCutGrid.Visibility = Visibility.Visible;
        }

        private void OnViewLoaded(object sender, RoutedEventArgs e)
        {
            VideoGrid.Visibility = Visibility.Visible;
            ShortCutGrid.Visibility = Visibility.Collapsed;
            mediaElement.Play();
        }

        private void OnStopMediaClick(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            Task.Delay(2000);
            VideoGrid.Visibility = Visibility.Collapsed;
            ShortCutGrid.Visibility = Visibility.Visible;
        }

        private void UpdateShortCutClick(object sender, RoutedEventArgs e)
        {
            ViewModel = DataContext as MainWindowViewModel;
            if (ViewModel.IsEditModeOn)
            {
                ViewModel.IsEditModeOn = false;
            }  
            else
            {
                ViewModel.IsEditModeOn = true;
            }
               
        }

        private void ShortCutListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                //EditButtonText.Visibility = Visibility.Collapsed;
                //EditButton.Width = 100;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion

    }
}
