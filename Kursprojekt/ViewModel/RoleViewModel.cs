using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kursprojekt.Datenbank.DBServices;
using Kursprojekt.Datenbank.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.ViewModel;

public partial class RoleViewModel : BaseViewModel
{
    public ObservableCollection<Role> Rollen { get; set; } = new();
    public IMapper Mapper { get; }

    public RoleViewModel(IMapper mapper)
    {
        Titel = "Rolle";
        Mapper = mapper;
    }

    public async override void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            await LoadAndSetDataAsync();
            IsViewModelLoaded = true;
        }
        base.GetInitialData();
    }

    private async Task LoadAndSetDataAsync()
    {
        bool IstIsPageBusyTrue = IsPageBusy;

        try
        {
            IsPageBusy = true;
            Message = "";
            var roleLst = await DBUnit.Role.GetAllAsync();
            if (Rollen.Count > 0) Rollen.Clear();
            if (roleLst is not null)
            {
                foreach (var role in roleLst)
                {
                    Rollen.Add(role);
                }
            }
            EnumRollen = Enum.GetValues(typeof(RoleType)).Cast<RoleType>();
        }
        finally
        {
            if (!IstIsPageBusyTrue)
            {
                IsPageBusy = false;
            }
        }
    }

    [RelayCommand]
    async Task ReSetInitDataAsync()
    {
        await LoadAndSetDataAsync();
    }

    [RelayCommand]
    void ResetForNew()
    {
        Message = "";
        EnumRolle = null;
        Rolle = new();
    }

    [RelayCommand]
    void SetSelectetRole(Role iSelRolle)
    {
        Rolle = Mapper.Map<Role>(iSelRolle);
    }

    //Add
    [RelayCommand]
    async Task Add()
    {
        if (IsPageNotBusy)
        {
            try
            {
                IsPageBusy = true;
                Message = "";

                if (Rolle.ID > 0)
                {
                    Message = "Bei einer neuen Rolle bitte auf 'ALLES NEU' klicken!";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Rolle.RoleName))
                {
                    Message = "Bitte Rolle auswählen!";
                    return;
                }

                var role = await DBUnit.Role.GetFirstOrDefaultAsync(filter: r => r.RoleName == Rolle.RoleName);
                if (role is not null)
                {
                    Message = $"{Rolle.RoleName} ist schon vorhanden.";
                    return;
                }

                var resp = await DBUnit.Role.AddAsync(Rolle);
                if (!resp.Success)
                {
                    Message = resp.Message;
                    return;
                }

                DelShowMainInfoFlyout?.Invoke($"{Rolle.RoleName} wurde hinzugefügt.");
                await LoadAndSetDataAsync();
                ResetForNew();
            }
            finally
            {
                IsPageBusy = false;
            }
        }
    }
    //Delete
    [RelayCommand]
    async Task Delete()
    {
        if (IsPageNotBusy)
        {
            try
            {
                IsPageBusy = true;
                Message = "";
                if (!(Rolle.ID > 0))
                {
                    Message = "Bitte eine Rolle auswählen!";
                    return;
                }

                var role = await DBUnit.User.GetFirstOrDefaultAsync(filter: u => u.RoleID == Rolle.ID);
                if (role is not null)
                {
                    Message = $"{Rolle.RoleName} wird von einem Nutzer benutzt und kann nicht gelöscht werden.";
                    return;
                }

                IsPageBusy = false;
                var iSJA = DelShowConfirmationWindow?.Invoke($"Soll {Rolle.RoleName} gelöscht werden?") ?? false;
                if (iSJA)
                {
                    IsPageBusy = true;
                    var resp = await DBUnit.Role.DeleteByIDAsync(Rolle.ID);

                    if (!resp.Success)
                    {
                        Message = resp.Message;
                        return;
                    }

                    DelShowMainInfoFlyout?.Invoke($"{Rolle.RoleName} wurde gelöscht.");
                    await LoadAndSetDataAsync();
                    ResetForNew();
                }

            }
            finally
            {
                IsPageBusy = false;
            }
        }
    }
    //Update 
    [RelayCommand]
    async Task Update()
    {
        if (IsPageNotBusy)
        {
            try
            {
                IsPageBusy = true;
                Message = "";
                if (!(Rolle.ID > 0))
                {
                    Message = "Bitte eine Rolle auswählen!";
                    return;
                }

                IsPageBusy = false;
                var iSJA = DelShowConfirmationWindow?.Invoke($"Soll die Daten geändert werden?") ?? false;
                if (iSJA)
                {
                    IsPageBusy = true;
                    var resp = await DBUnit.Role.UpdateAsync(Rolle);
                    if (!resp.Success)
                    {
                        Message = resp.Message;
                        return;
                    }

                    DelShowMainInfoFlyout?.Invoke($"{Rolle.RoleName} Daten wurde geändert.");
                    await LoadAndSetDataAsync();
                    ResetForNew();
                }
            }
            finally
            {
                IsPageBusy = false;
            }
        }
    }

    [ObservableProperty]
    Role rolle = new();


    [ObservableProperty]
    IEnumerable<RoleType>? enumRollen;

    [ObservableProperty]
    RoleType? enumRolle = null;
}
