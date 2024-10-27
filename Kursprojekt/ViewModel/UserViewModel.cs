using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kursprojekt.Datenbank.DBServices;
using Kursprojekt.Datenbank.Models;
using Kursprojekt.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kursprojekt.Validators;

namespace Kursprojekt.ViewModel;

public partial class UserViewModel: BaseViewModel
{
    public ObservableCollection<AppUser> Users { get; set; } = new();
    public ObservableCollection<Role> Roles { get; set; } = new();

    public IMapper Mapper { get; }
    public UserModelValidator UserModelValidator { get; }

    public UserViewModel(IMapper mapper, UserModelValidator userModelValidator)
    {
        Titel = "User";
        Mapper = mapper;
        UserModelValidator = userModelValidator;
    }

    public override async void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            await LoadAndSetData();
            IsViewModelLoaded = true;
        }
        base.GetInitialData();
    }
    
    private async Task LoadAndSetData()
    {
        bool IstIsPageBusyTrue = IsPageBusy;
        try
        {
            IsPageBusy = true;
            Message = "";

            var userList = await DBUnit.User.GetAllAsync(includeProperties: nameof(Role));
            var roleList = await DBUnit.Role.GetAllAsync();
            Users.Clear();
            Roles.Clear();

            if (userList != null)
            {
                foreach (var user in userList)
                {
                    Users.Add(user);
                }
            }

            if (roleList != null)
            {
                foreach (var role in roleList)
                {
                    Roles.Add(role);
                }
            }

            ResetForNew();
        }
        finally
        {
            if (!IstIsPageBusyTrue)
            {
                IsPageBusy = false;
            }
        }
    }
    
    #region Eigenschaften
    [ObservableProperty]
    AppUser user = new();

    [ObservableProperty]
    Role role = new();

    [ObservableProperty]
    string messageDeleteUpdate = "";

    [ObservableProperty]
    string messageAdd = "";

    [ObservableProperty]
    string neuesPasswort = "";
    #endregion

    #region RelayCommand

    [RelayCommand]
    async Task ReSetInitData()
    {
        await LoadAndSetData();
    }

    [RelayCommand]
    void ResetForNew()
    {
        Message = "";
        User = new();
        Role = new();
    }

    [RelayCommand]
    void SetSelectetUser(AppUser iUser)
    {
        User = Mapper.Map<AppUser>(iUser);
    }

    //Add
    [RelayCommand]
    async Task Add()
    {
        try
        {
            MessageAdd = "";
            if (IsPageNotBusy)
            {
                if (User.ID > 0)
                {
                    MessageAdd = "Bei einem neuen User bitte auf 'ALLES NEU' klicken!";
                    return;
                }

                var valResult = await UserModelValidator.ValidateAsync(User);
                if (!valResult.IsValid)
                {
                    MessageAdd = valResult.Errors[0].ToString();
                    return;
                }

                var resp = await UserManager.RegisterUserAsync(User);
                if (!resp.Success)
                {
                    MessageAdd = resp.Message;
                    return;
                }

                DelShowMainInfoFlyout?.Invoke($"{User.Email} wurde hinzugefügt.");
                await LoadAndSetData();
            }
        }
        finally
        {
            IsPageBusy = false;
        }
    }

    [RelayCommand]
    async Task Delete()
    {
        try
        {
            if (IsPageNotBusy)
            {
                MessageDeleteUpdate = "";
                if (User.ID == 0)
                {
                    MessageDeleteUpdate = "Bitte den Nutzer auswählen";
                    return;
                }

                var IstJa = DelShowConfirmationWindow?.Invoke($"Wollen Sie {User.Email} löschen?") ?? false;
                if (IstJa)
                {
                    IsPageBusy = true;
                    var resp = await UserManager.DeleteUserByIDAsync(User.ID);
                    if (!resp.Success)
                    {
                        MessageDeleteUpdate = resp.Message;
                        return;
                    }

                    DelShowMainInfoFlyout?.Invoke($"{User.Email} wurde gelöscht.");
                    await LoadAndSetData();
                }
            }
        }
        finally
        {
            IsPageBusy = false;
        }

    }

    [RelayCommand]
    async Task Update()
    {
        try
        {
            if (IsPageNotBusy)
            {
                MessageDeleteUpdate = "";
                if (User.ID == 0)
                {
                    MessageDeleteUpdate = "Bitte den Nutzer auswählen";
                    return;
                }

                var valResult = UserModelValidator.Validate(User);
                if (!valResult.IsValid)
                {
                    MessageDeleteUpdate = valResult.Errors[0].ToString();
                    return;
                }

                var IstJa = DelShowConfirmationWindow?.Invoke($"Wollen Sie {User.Email} Daten ändern?") ?? false;
                if (IstJa)
                {
                    IsPageBusy = true;
                    var resp = await UserManager.UpdateUserAsync(User);
                    if (!resp.Success)
                    {
                        MessageDeleteUpdate = resp.Message;
                        return;
                    }

                    DelShowMainInfoFlyout?.Invoke($"Der Nutzer Daten wurden geändert.");
                    await LoadAndSetData();
                }
            }
        }
        finally
        {
            IsPageBusy = false;
        }

    }

    [RelayCommand]
    async Task UpdatePassword()
    {
        try
        {
            Message = "";
            if (User.ID == 0)
            {
                Message = "Bitte den Nutzer auswählen";
                return;
            }

            if (string.IsNullOrWhiteSpace(NeuesPasswort))
            {
                Message = "Geben Sie das neue Passwort ein.";
                return;
            }

            //Nur um zu prüfen
            IsPageBusy = true;
            var user = await DBUnit.User.GetByIDAsync(User.ID);
            if (user == null)
            {
                Message = $"{User.Email} wurde nicht gefunden.";
                return;
            }
            //<---

            IsPageBusy = false;
            User.Password = NeuesPasswort;
            var IstJA = DelShowConfirmationWindow?.Invoke($"Wollen Sie {User.Email} Passwort ändern?") ?? false;
            if (IstJA)
            {
                IsPageBusy = true;
                var resp = await UserManager.UpdateUserAsync(User, true);
                if (!resp.Success)
                {
                    Message = resp.Message;
                    return;
                }
                DelShowMainInfoFlyout?.Invoke($"Das Passwort wurden geändert.");
                await LoadAndSetData();
            }
        }
        finally
        {
            IsPageBusy = false;
        }
    }
    
    #endregion
}
