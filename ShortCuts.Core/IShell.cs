using Caliburn.Micro;
using ShortCuts.Core.ViewModels.Dialog;

namespace ShortCuts.Core
{
    public interface IShell : IConductor
    {
        IDialogManager Dialog { get; }
    }
}
