﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.ViewModel;

public partial class RoleViewModel : BaseViewModel
{
    public RoleViewModel()
    {
        Titel = "Rolle";
    }

    public override void GetInitialData()
    {
        base.GetInitialData();
    }
}
