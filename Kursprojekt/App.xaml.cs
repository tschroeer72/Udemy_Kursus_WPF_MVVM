﻿using System.Reflection;
using Kursprojekt.View.Pages;
using Kursprojekt.View.Services;
using Kursprojekt.View.Windows;
using Kursprojekt.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using FluentValidation;
using Kursprojekt.Validators;
using Kursprojekt.Services;
using Kursprojekt.Datenbank.DBServices;
using System.Windows.Markup;
using System.Globalization;

namespace Kursprojekt;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _ServiceProvider;
    
    public App()
    {
        ServiceCollection ServiceCollection = new();
        ConfigureServices(ServiceCollection);
        _ServiceProvider = ServiceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        //AutoMapper 
        //services.AddAutoMapper(typeof(MappingConfig));

        ////Validator 
        //services.AddSingleton<UserLoginValidator>();
        //services.AddSingleton<UserModelValidator>();
        //services.AddSingleton<HausAddAndUpdateValidator>();

        //----------------

        //AutoMapper && Validator  
        Type VMAssembyRefTyp = typeof(MappingConfig);
        services.AddAutoMapper(VMAssembyRefTyp);
        services.AddValidatorsFromAssemblyContaining(VMAssembyRefTyp);

        //----------------

        //Assembly Ass = Assembly.LoadFrom("Kursprojekt");
        //services.AddValidatorsFromAssembly(Ass);

        //--------------------
        //Views und ViewModels
        services.AddSingleton<MainView>();
        services.AddSingleton<MainViewModel>();

        services.AddSingleton<HomeView>();
        services.AddSingleton<HomeViewModel>();

        services.AddSingleton<AdminView>();
        services.AddSingleton<AdminViewModel>();

        services.AddSingleton<RoleView>();
        services.AddSingleton<RoleViewModel>();

        services.AddSingleton<UserView>();
        services.AddSingleton<UserViewModel>();

        services.AddSingleton<VerwaltungView>();
        services.AddSingleton<VerwaltungViewModel>();

        services.AddSingleton<BuchungView>();
        services.AddSingleton<BuchungViewModel>();

        services.AddSingleton<HausView>();
        services.AddSingleton<HausViewModel>();

        services.AddSingleton<LoginView>();
        services.AddSingleton<LoginViewModel>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        //MainView mainView = new();
        //mainView.Show();

        var mainView = _ServiceProvider.GetService<MainView>();
        if(mainView == null)
        {
            MessageBox.Show("Problem im ServiceProvider");
            Current.Shutdown();
        }

        ViewManager.InitViewManager(mainView!, _ServiceProvider);
        //await DBUnit.User.GetAllAsync();
        mainView!.Show();

        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.Name)));

        base.OnStartup(e);
    }
}
