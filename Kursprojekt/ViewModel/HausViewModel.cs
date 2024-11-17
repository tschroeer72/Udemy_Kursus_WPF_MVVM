using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kursprojekt.Datenbank.DBServices;
using Kursprojekt.Datenbank.Models;
using Kursprojekt.Validators;

namespace Kursprojekt.ViewModel;

public partial class HausViewModel : BaseViewModel
{
    public delegate string OnShowFileDialogHandlerType();
    public OnShowFileDialogHandlerType? DelShowFileDialog { get; set; }

    public ObservableCollection<Haus> Haeuser { get; set; } = new();
    public ObservableCollection<Haus> HaeuserBackup { get; set; } = new();
    public IMapper Mapper { get; }
    public HausAddAndUpdateValidator HausValidator { get; }

    public HausViewModel(IMapper mapper, HausAddAndUpdateValidator hausValidator)
    {
        Titel = "Häuser";
        Mapper = mapper;
        HausValidator = hausValidator;
    }

    [ObservableProperty]
    Haus haus = new();

    [ObservableProperty]
    string hausFilterText = "";

    [RelayCommand]
    public async Task GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            await LoadAndSetData();
            IsViewModelLoaded = true;
        }
    }

    private async Task LoadAndSetData()
    {
        try
        {
            //if (IsPageNotBusy)
            //{
                IsPageBusy = true;
                Message = "";

                Haeuser.Clear();
                HaeuserBackup.Clear();
                var hausLst = await DBUnit.Haus.GetAllAsync();
                if (hausLst != null)
                {
                    foreach (var myHaus in hausLst)
                    {
                        Haeuser.Add(myHaus);
                        HaeuserBackup.Add(myHaus);
                    }
                }
            //}
        }
        finally
        {
            IsPageBusy = false;
            HausFilterText = "";
            ReSetForAddNew();
        }
    }

    [RelayCommand]
    async Task ReSetInitData()
    {
        await LoadAndSetData();
    }

    [RelayCommand]
    void ReSetForAddNew()
    {
        Message = "";
        Haus = new();
    }

    [RelayCommand]
    async Task Add()
    {
        try
        {
            IsPageBusy = true;
            Message = "";

            if (Haus.ID > 0)
            {
                Message = "Bei einem neuen Haus bitte auf 'ALLES NEU' klicken!";
                return;
            }

            var valResult = HausValidator.Validate(Haus);
            if (!valResult.IsValid)
            {
                Message = valResult.Errors[0].ToString();
                return;
            }

            var resp = await DBUnit.Haus.AddAsync(Haus);
            if (!resp.Success)
            {
                Message = resp.Message;
                return;
            }

            DelShowMainInfoFlyout?.Invoke($"Das Haus wurde hinzugefügt.");
            await LoadAndSetData();
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
            Message = "";

            if (Haus.ID <= 0)
            {
                Message = "Bitte wählen Sie zuerst ein Haus aus!";
                return;
            }

            var valResult = HausValidator.Validate(Haus);
            if (!valResult.IsValid)
            {
                Message = valResult.Errors[0].ToString();
                return;
            }

            var bolYes = DelShowConfirmationWindow?.Invoke($"Wollen Sie die Daten ändern?") ?? false;
            if (bolYes)
            {
                IsPageBusy = true;
                var resp = await DBUnit.Haus.UpdateAsync(Haus);
                if (!resp.Success)
                {
                    Message = resp.Message;
                    return;
                }

                DelShowMainInfoFlyout?.Invoke($"Die Daten wurden geändert.");
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
            Message = "";
            if (!(Haus.ID > 0))
            {
                Message = "Bitte wählen Sie zuerst ein Haus aus!";
                return;
            }

            var bolYes = DelShowConfirmationWindow?.Invoke($"Wollen Sie das Haus löschen?") ?? false;
            if (bolYes)
            {
                IsPageBusy = true;
                var resp = await DBUnit.Haus.DeleteByIDAsync(Haus.ID);
                if (!resp.Success)
                {
                    Message = resp.Message;
                    return;
                }

                DelShowMainInfoFlyout?.Invoke($"Das Haus wurde gelöscht.");
                await LoadAndSetData();
            }
        }
        finally
        {
            IsPageBusy = false;
        }
    }


    [RelayCommand]
    void SetSelectetHaus(Haus iSelHaus)
    {
        Haus = Mapper.Map<Haus>(iSelHaus);
    }

    [RelayCommand]
    void ShowFileDialog()
    {
        var filename = DelShowFileDialog?.Invoke();

        if (filename == null || string.IsNullOrEmpty(filename)) return;

        //if (Haus == null) { Haus = new(); }
        Haus.Bild = File.ReadAllBytes(filename);
        OnPropertyChanged(nameof(Haus));
    }

    async partial void OnHausFilterTextChanged(string value)
    {
        try
        {
            IsPageBusy = true;
            List<Haus> lstHausFilter = new();

            if (!string.IsNullOrEmpty(value))
            {
                var hausLst = await Task.Run(() => HaeuserBackup.Where(h => h.AuftragID.Contains(value)));
                lstHausFilter.AddRange(hausLst);
            }

            Haeuser.Clear();
            if (lstHausFilter.Count > 0)
            {
                foreach (var haus in lstHausFilter)
                {
                    Haeuser.Add(haus);
                }
            }
            else
            {
                foreach (var haus in HaeuserBackup)
                {
                    Haeuser.Add(haus);
                }
            }
        }
        finally
        {
            IsPageBusy = false;
        }
    }
}
