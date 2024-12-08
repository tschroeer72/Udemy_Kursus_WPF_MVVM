using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Kursprojekt.Datenbank.Models;
using Kursprojekt.Validators;
using System.Collections.ObjectModel;
using Kursprojekt.Datenbank.DBServices;
using Kursprojekt.Services;

namespace Kursprojekt.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    public event EventHandler? EventScrollToSelectedHaus;
    public ObservableCollection<Haus> Haeuser { get; set; } = new();
    public ObservableCollection<Haus> HaeuserBackUp { get; set; } = new();
    public AddBuchungValidator AddBuchungValidator { get; }


    public HomeViewModel(AddBuchungValidator addBuchungValidator)
    {
        Titel = "Home";
        AddBuchungValidator = addBuchungValidator;
    }

    [ObservableProperty]
    Buchung buchung = new();

    [ObservableProperty]
    Haus haus = new();

    [ObservableProperty]
    bool nurReservierte = false;

    [ObservableProperty]
    bool nurNichtReservierte = false;


    [ObservableProperty]
    string filterText = "";

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
        if (IsPageBusy) return;

        List<Haus>? hausLst = null;
        try
        {
            IsPageBusy = true;
            Message = "";

            if (NurReservierte)
            {
                hausLst = await DBUnit.Haus.GetAllAsync(filter: h => h.IstReserviert == true && h.IstVerkauft == false);
            }
            else if (NurNichtReservierte)
            {
                hausLst = await DBUnit.Haus.GetAllAsync(filter: h => h.IstReserviert == false && h.IstVerkauft == false);
            }
            else
            {
                hausLst = await DBUnit.Haus.GetAllAsync(filter: h => h.IstVerkauft == false);
            }

            Haeuser.Clear();
            HaeuserBackUp.Clear();


            if (hausLst is not null)
            {
                foreach (var haus in hausLst)
                {
                    Haeuser.Add(haus);
                    HaeuserBackUp.Add(haus);
                }

                if (Haeuser.Count > 0)
                {
                    Haus = Haeuser[0];
                    EventScrollToSelectedHaus?.Invoke(0, EventArgs.Empty);
                }
            }
        }
        finally
        {
            IsPageBusy = false;
        }
    }

    [RelayCommand]
    async Task ReSetInitData()
    {
        await LoadAndSetData();
        FilterText = "";
    }

    [RelayCommand]
    void ResetForNew()
    {
        Message = "";
        Buchung = new(); //Für neu
        Haus = new();
        FilterText = "";  // Vielleicht das Haus wurde gefiltet
        NurNichtReservierte = false;
        NurReservierte = false;
    }

    [RelayCommand]
    void SelectPreviousHaus(int iSelIndex)
    {
        if (Haeuser == null || Haeuser.Count == 0) return;

        int indexTo;
        if (iSelIndex > 0) //Nicht das erste Item
        {
            indexTo = iSelIndex - 1;
            Haus = Haeuser[indexTo];
        }
        else //Das erste Item
        {
            indexTo = Haeuser.Count - 1;
            Haus = Haeuser[indexTo];
        }

        EventScrollToSelectedHaus?.Invoke(indexTo, EventArgs.Empty);
    }

    [RelayCommand]
    void SelectNextHaus(int iParam)
    {
        if (Haeuser == null || Haeuser.Count == 0) return;

        int indexTo;
        if (iParam < Haeuser.Count - 1)
        {
            indexTo = iParam + 1;
            Haus = Haeuser[indexTo];
        }
        else
        {
            indexTo = 0;
            Haus = Haeuser[indexTo];
        }
        EventScrollToSelectedHaus?.Invoke(indexTo, EventArgs.Empty);
    }

    [RelayCommand]
    async Task HausReservieren()
    {
        try
        {
            IsPageBusy = true;
            Message = "";


            if (Haus == null)
            {
                Message = $"Wählen Sie ein Haus aus!";
                return;
            }

            if (Haus.IstReserviert)
            {
                Message = $"Das Haus wurde leider schon reserviert";
                return;
            }


            Buchung.HausID = Haus.ID;
            Buchung.Bearbeiter = UserManager.LoginUserInfos.LoginUser.Email;
            Buchung.BuchungDatum = DateTime.Now;



            var valResult = AddBuchungValidator.Validate(Buchung);
            if (!valResult.IsValid)
            {
                Message = valResult.Errors[0].ToString();
                return;
            }

            var resp = await DBUnit.Buchung.AddAsync(Buchung);
            if (!resp.Success)
            {
                Message = resp.Message;
                return;
            }

            DelShowMainInfoFlyout?.Invoke($"Das Haus ist für {Buchung.KundenName} reserviert.");

            //Update Haus
            Haus.IstReserviert = true;
            await DBUnit.Haus.UpdateAsync(Haus);


        }
        finally
        {
            IsPageBusy = false;

            ResetForNew();
            await LoadAndSetData();
        }

    }

    [RelayCommand]
    async Task BuchenStornieren()
    {
        try
        {
            Message = "";

            if (Haus == null)
            {
                Message = $"Wählen Sie ein Haus aus!";
                return;
            }


            //Wurde das Haus gebucht?
            if (!Haus.IstReserviert)
            {
                Message = $"Das Haus wurde nicht reserviert!";
                return;
            }

            //Buchung suchen
            IsPageBusy = true;
            var buchung = await DBUnit.Buchung.GetFirstOrDefaultAsync(filter: b => b.HausID == Haus.ID);
            if (buchung is null)
            {
                Message = $"Die Buchung wurde schon storniert. Bitte aktualisieren Sie die Daten";
                return;
            }


            //Buchung Anleger(Bearbeiter) prüfen
            IsPageBusy = false;
            if (buchung.Bearbeiter != UserManager.LoginUserInfos.LoginUser.Email)
            {
                var erg = DelShowConfirmationWindow?.Invoke($"Die Buchung wurde von {buchung.Bearbeiter} angelegt! {Environment.NewLine}" +
                    $"Wollen Sie sich ummelden?");
                if (erg != true) return;

                DelShowLoginView?.Invoke(false);
                return;
            }

            //Buchen stornieren
            var JaNein = DelShowConfirmationWindow?.Invoke($"Wollen Sie die Buchung stornieren?") ?? false;
            if (JaNein)
            {
                IsPageBusy = true;
                var resp = await DBUnit.Buchung.DeleteByIDAsync(buchung.ID);
                if (!resp.Success)
                {
                    Message = resp.Message;
                    return;
                }


                //Update Haus IsFrei Property
                Haus.IstReserviert = false;
                var Updresp = await DBUnit.Haus.UpdateAsync(Haus);
                if (!Updresp.Success)
                {
                    Message = Updresp.Message;
                }

                DelShowMainInfoFlyout?.Invoke($"Die Buchung wurde storniert.");

            }
        }
        finally
        {
            IsPageBusy = false;

            ResetForNew();
            await LoadAndSetData();
        }

    }

    async partial void OnFilterTextChanged(string value)
    {
        try
        {
            IsPageBusy = true;
            List<Haus> FilterhausLst = new();

            if (!string.IsNullOrEmpty(FilterText))
            {
                var hLst = await Task.Run(() => HaeuserBackUp.Where(h => h.AuftragID.Contains(FilterText)));
                FilterhausLst.AddRange(hLst);
            }

            Haeuser.Clear();
            if (FilterhausLst.Count > 0)
            {
                foreach (var haus in FilterhausLst)
                {
                    Haeuser.Add(haus);
                }
            }
            else
            {
                foreach (var haus in HaeuserBackUp)
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
