﻿using Kursprojekt.View.Services;
using Kursprojekt.ViewModel;
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

namespace Kursprojekt.View.Pages
{
    /// <summary>
    /// Interaktionslogik für UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserViewModel UserViewModel { get; }

        public UserView(UserViewModel appUserViewModel)
        {
            InitializeComponent();
            UserViewModel = appUserViewModel;
            UserViewModel.InitBaseViewModelDelegateAndEvents();
            DataContext = UserViewModel;
        }

    }
}
