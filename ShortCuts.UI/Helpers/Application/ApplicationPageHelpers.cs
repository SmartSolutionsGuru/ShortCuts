using System.Diagnostics;
using ShortCuts.Core.Models;
using ShortCuts.UI.Pages;

namespace ShortCuts.UI.Helpers.Application
{
    public static class ApplicationPageHelpers
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                //case ApplicationPage.Main:
                //    return new MainWindow(viewModel as ShortCuts.Core.ViewModels.MainWindowViewModel);

                //case ApplicationPage.Edit:
                //    return new RegisterPage(viewModel as RegisterViewModel);

                default:
                   Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches the base page
            //if (page is MainWindow)
            //    return ApplicationPage.Main;

            //if (page is LoginPage)
            //    return ApplicationPage.Edit;

            
            // Alert developer of issue
            Debugger.Break();
            return default;
        }
    }
}
