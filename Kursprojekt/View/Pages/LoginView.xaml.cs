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
    /// Interaktionslogik für LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginViewModel LoginViewModel { get; }

        public LoginView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            LoginViewModel = loginViewModel;
            DataContext = LoginViewModel;
            LoginViewModel.InitBaseViewModelDelegateAndEvents();
        }


        private void BtnBeenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
