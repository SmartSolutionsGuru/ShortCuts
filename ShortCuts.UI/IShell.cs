using Caliburn.Micro;
using ShortCuts.DAL.Managers.Dialog;

namespace ShortCuts.UI
{
    public interface IShell   : IConductor
    {
         IDialogManager Dialog { get; }
    }
}
