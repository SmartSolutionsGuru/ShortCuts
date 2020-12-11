using System.Threading.Tasks;
using Caliburn.Micro;

namespace ShortCuts.DAL.Managers.Dialog
{
    public interface IDialogManager
    {
        /// <summary>
        /// Display The Screen in Dialog Form
        /// </summary>
        /// <param name="dialogModel"></param>
        /// <returns></returns>
        Task ShowDialogAsync(IScreen dialogModel);
    }
}
