﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Kursprojekt.ViewModel;

public partial class VerwaltungViewModel : BaseViewModel
{
    public VerwaltungViewModel()
    {
        Titel = "Verwaltung";
    }

    [RelayCommand]
    public void GetInitialData()
    {
      }
}
