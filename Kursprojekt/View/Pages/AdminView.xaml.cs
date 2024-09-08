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

namespace Kursprojekt.View.Pages;

/// <summary>
/// Interaktionslogik für AdminView.xaml
/// </summary>
public partial class AdminView : UserControl
{
    public AdminViewModel AdminViewModel { get; }

    public AdminView(AdminViewModel adminViewModel)
    {
        InitializeComponent();
        AdminViewModel = adminViewModel;
        DataContext = AdminViewModel;
    }

    private void OpenPage(object sender, RoutedEventArgs e)
    {
        if (sender is Button objButton)
        {
            switch (objButton.Name)
            {
                case "BtnUser":
                    ViewManager.ShowUnderPageOn<AppUserView>(AnimatedContentControl);
                    break;
                case "BtnRole":
                    ViewManager.ShowUnderPageOn<RoleView>(AnimatedContentControl);
                    break;
            }
        }
    }
}
