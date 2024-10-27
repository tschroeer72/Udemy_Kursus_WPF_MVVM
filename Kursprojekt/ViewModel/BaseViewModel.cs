using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursprojekt.DTOs;
using Kursprojekt.Services;

namespace Kursprojekt.ViewModel;

public partial class BaseViewModel : ObservableRecipient
{
    protected override void OnActivated()
    {
        SetLoginUserInfos();
        base.OnActivated();

        IsActive = false;
    }

    public delegate bool DelShowInformationWindowType(string iMessage);
    public DelShowInformationWindowType? DelShowInformationWindow { get; set; }

    public delegate bool DelShowConfirmationWindowType(string iMessage);
    public DelShowConfirmationWindowType? DelShowConfirmationWindow { get; set; }

    public delegate string DelShowInputWindowType(string iMessage);
    public DelShowInputWindowType? DelShowInputWindow { get; set; }

    public delegate void DelShowMainInfoFlyoutType(string iMessage, bool iWarnung = false);
    public DelShowMainInfoFlyoutType? DelShowMainInfoFlyout { get; set; }


    public delegate void DelGoBackOrGotoHomeType();
    public DelGoBackOrGotoHomeType? DelGoBackOrGotoHome { get; set; }

    public delegate void DelShowLoginViewType(bool bAsStartLogin);
    public DelShowLoginViewType? DelShowLoginView { get; set; }


    [RelayCommand]
    public virtual void GetInitialData()
    {
        SetLoginUserInfos(); // Wird am Ende geändert
    }

    public void SetLoginUserInfos()
    {
        LoginUserInfos = UserManager.GetLoginUserInfos();
    }
    
    [ObservableProperty]
    string titel = "";

    [ObservableProperty]
    string message = string.Empty;

    [ObservableProperty]
    bool isViewModelLoaded = false;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPageNotBusy))]
    bool isPageBusy = false;

    public bool IsPageNotBusy => !IsPageBusy;

    [ObservableProperty] 
    LoginUserInfos loginUserInfos = new();
}
