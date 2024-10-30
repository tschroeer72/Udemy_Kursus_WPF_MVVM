using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Kursprojekt.Datenbank.DTOs;
using Kursprojekt.Datenbank.Models;
using Kursprojekt.Services;
using Kursprojekt.Validators;

namespace Kursprojekt.ViewModel;

public partial class LoginViewModel:BaseViewModel
{
    private UserLoginValidator UserLoginValidator { get; }
    private MainViewModel MainViewModel { get; }

    public LoginViewModel(UserLoginValidator userLoginValidator, MainViewModel mainViewModel)
    {
        UserLoginValidator = userLoginValidator;
        MainViewModel = mainViewModel;
    }

    [ObservableProperty] 
    AppUser user = new();

    [RelayCommand]
    async Task LoginUser()
    {

        //Test des InfoWindows und des Flyouts
        //var erg1 = DelShowInformationWindow?.Invoke("Hallo 1");
        //var erg2 = DelShowConfirmationWindow?.Invoke("Hallo 2");
        //var erg3 = DelShowInputWindow?.Invoke("Hallo 3");
        //DelShowMainInfoFlyout?.Invoke("Hallo 4");
        //DelShowMainInfoFlyout?.Invoke("Hallo 5", true);

        // ------------

        try
        {
            if (IsPageBusy) return;

            IsPageBusy = true;
            Message = string.Empty;

            var validationResponse = UserLoginValidator.Validate(User);
            if (!validationResponse.IsValid)
            {
                Message = validationResponse.Errors[0].ToString();
                return;
            }

            DBResponse response = await UserManager.LoginUserAsync(User);
            if (!response.Success)
            {
                Message = response.Message;
                return;
            }

            DelGoBackOrGotoHome?.Invoke();
            User = new();
            
        }
        finally
        {
            IsPageBusy = false;
        }
    }
    
    [RelayCommand]
    void GoBack()
    {            
        DelGoBackOrGotoHome?.Invoke();
    }
}
