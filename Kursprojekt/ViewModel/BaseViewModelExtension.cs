using Kursprojekt.View.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.ViewModel;

public static class BaseViewModelExtension
{
    public static void InitBaseViewModelDelegateAndEvents(this BaseViewModel baseViewModel)
    {
        ViewManager.InitBaseDelEvents(baseViewModel);
    }
}