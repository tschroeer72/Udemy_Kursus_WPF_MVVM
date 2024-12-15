using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kursprojekt.Datenbank.DBServices;
using Kursprojekt.Datenbank.Models;
using Kursprojekt.DTOs;
using Kursprojekt.Services;

namespace Kursprojekt.ViewModel;

public partial class BuchungViewModel :BaseViewModel
{
    public delegate void DelSchowBarChartDataHandlerType(List<BarChartDto> BarChartDtoLst);
    public DelSchowBarChartDataHandlerType? DelSchowBarChartData;

    public ObservableCollection<Buchung> Buchungen { get; } = new();
    public ObservableCollection<Buchung> BuchungenBackup { get; } = new();
    public ObservableCollection<Haus> VerkaufteHaeuser { get; } = new();
    public ObservableCollection<Haus> VerkaufteHaeuserBackup { get; } = new();
    public IMapper Mapper { get; }

    public BuchungViewModel(IMapper mapper)
    {
        Titel = "Buchung";
        Mapper = mapper;
    }

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
            if (IsPageBusy) return;

            IsPageBusy = true;
            Message = "";

            var BuchungLst = await DBUnit.Buchung.GetAllAsync(includeProperties: nameof(Haus));
            var HausLst = await DBUnit.Haus.GetAllAsync(filter: h => h.IstVerkauft == true);
            if (!BoolAlleBuchungen)
            {
                BuchungLst = BuchungLst?.Where(fb => fb.BesichtigungDatum >= vonDateFilter && fb.BesichtigungDatum <= BisDateFilter).ToList();
                HausLst = HausLst?.Where(fh => fh.VerkaufDatum >= vonDateFilter && fh.VerkaufDatum <= BisDateFilter).ToList();
            }

            Buchungen.Clear();
            BuchungenBackup.Clear();

            VerkaufteHaeuser.Clear();
            VerkaufteHaeuserBackup.Clear();

            if (BuchungLst != null)
                foreach (var buchung in BuchungLst)
                {
                    Buchungen.Add(buchung);
                    BuchungenBackup.Add(buchung);
                }

            if (HausLst != null)
                foreach (var haus in HausLst)
                {
                    VerkaufteHaeuser.Add(haus);
                    VerkaufteHaeuserBackup.Add(haus);
                }
        }
        finally
        {
            IsPageBusy = false;
            VerkaufFilterText = "";
            BuchungFilterText = "";
            ShowBarChartData();
        }
    }

    [RelayCommand]
    async Task ReSetInitData()
    {
        await LoadAndSetData();
    }

    [RelayCommand]
    void ResetForNew()
    {
        Message = "";
        Buchung = new();
        Haus = new();
    }

    [ObservableProperty]
    Buchung buchung = new();

    [ObservableProperty]
    Haus haus = new();

    [ObservableProperty]
    DateTime vonDateFilter = new();

    [ObservableProperty]
    DateTime bisDateFilter = new();

    [ObservableProperty]
    bool boolAlleBuchungen = true;

    [ObservableProperty]
    string verkaufFilterText = "";

    [ObservableProperty]
    string buchungFilterText = "";

    [ObservableProperty]
    double totalUmsatz = 0;

    [RelayCommand]
    void ShowBarChartData()
    {
        if (IsPageBusy) return;
        try
        {
            IsPageBusy = true;
            List<BarChartDto> lstBarChart = new();
            TotalUmsatz = 0;

            foreach (var haus in VerkaufteHaeuser)
            {
                BarChartDto barChartDto = new()
                {
                    AuftragID = haus.AuftragID,
                    HausPreis = haus.Preis
                };

                lstBarChart.Add(barChartDto);
                TotalUmsatz += haus.Preis;
            }

            DelSchowBarChartData?.Invoke(lstBarChart);
        }
        finally
        {
            IsPageBusy = false;
        }

    }

    [RelayCommand]
    async Task BuchungHausVerkaufen()
    {
        try
        {
            if (IsPageBusy) return;
            Message = "";

            if (Buchung == null)
            {
                Message = $"Wählen Sie ein Buchung aus!";
                return;
            }

            //Buchung suchen
            IsPageBusy = true;
            var buchung = await DBUnit.Buchung.GetFirstOrDefaultAsync(filter: b => b.ID == Buchung.ID, includeProperties: nameof(Haus));
            if (buchung == null)
            {
                Message = $"Die Buchung wurde schon storniert. Bitte aktualisieren Sie die Daten";
                return;
            }
            //Buchung Anleger(Bearbeiter) prüfen
            IsPageBusy = false;
            if (buchung.Bearbeiter != UserManager.LoginUserInfos.LoginUser.Email)
            {
                var erg = DelShowConfirmationWindow?.Invoke($"Die Buchung wurde von {buchung.Bearbeiter} angelegt! {Environment.NewLine}" + $"Wollen Sie sich ummelden?");
                if (erg != true) return;

                DelShowLoginView?.Invoke(false);
                return;
            }

            //Buchen stornieren
            var bolJaNein = DelShowConfirmationWindow?.Invoke($"Wollen Sie das Haus Verkaufen?") ?? false;
            if (bolJaNein)
            {
                IsPageBusy = true;
                var resp = await DBUnit.Buchung.DeleteByIDAsync(buchung.ID);
                if (!resp.Success)
                {
                    Message = $"Die Buchung kann nicht storniert werden.";
                    return;
                }

                //Update Haus IsFrei Property
                buchung.Haus.IstVerkauft = true;
                buchung.Haus.VerkaufDatum = DateTime.Now;
                var respUpdate = await DBUnit.Haus.UpdateAsync(buchung.Haus);
                if (!respUpdate.Success)
                {
                    Message = respUpdate.Message;
                }

                DelShowMainInfoFlyout?.Invoke($"Die Buchung wurde zum Verkauf geändert.");
            }
        }
        finally
        {
            IsPageBusy = false;
            ResetForNew();
            await LoadAndSetData();
        }
    }

    [RelayCommand]
    async Task BuchungStornieren()
    {
        try
        {
            if (IsPageBusy) return;
            Message = "";

            if (Buchung == null)
            {
                Message = $"Wählen Sie ein Buchung aus!";
                return;
            }

            //Wurde das Haus gebucht?
            if (!Buchung.Haus.IstReserviert)
            {
                Message = $"Das Haus wurde nicht reserviert!";
                return;
            }

            //Haus aus der Buchung bekommen für ein Update später
            Haus hausBuchung = Mapper.Map<Haus>(Buchung.Haus);

            //Buchung suchen
            IsPageBusy = true;
            var resp = await DBUnit.Buchung.GetFirstOrDefaultAsync(filter: b => b.ID == Buchung.ID);
            if (resp is null)
            {
                Message = $"Die Buchung wurde schon storniert. Bitte aktualisieren Sie die Daten";
                return;
            }

            //Buchung Anleger(Bearbeiter) prüfen
            IsPageBusy = false;
            if (Buchung.Bearbeiter != UserManager.LoginUserInfos.LoginUser.Email)
            {
                var erg = DelShowConfirmationWindow?.Invoke($"Die Buchung wurde von {Buchung.Bearbeiter} angelegt! {Environment.NewLine}" + $"Wollen Sie sich ummelden?");
                if (erg != true) return;

                DelShowLoginView?.Invoke(false);
                return;
            }

            //Buchen stornieren (Löschen)
            var bolJaNein = DelShowConfirmationWindow?.Invoke($"Wollen Sie die Buchung stornieren?") ?? false;
            if (bolJaNein)
            {
                IsPageBusy = true;
                var respStorno = await DBUnit.Buchung.DeleteByIDAsync(Buchung.ID);
                if (!respStorno.Success)
                {
                    Message = respStorno.Message;
                    return;
                }

                //Update Haus IsFrei Property
                hausBuchung.IstReserviert = false;
                var respUpdate = await DBUnit.Haus.UpdateAsync(hausBuchung);
                if (!respUpdate.Success)
                {
                    Message = respUpdate.Message;
                }

                DelShowMainInfoFlyout?.Invoke($"Die Buchung wurde storniert.");
            }
        }
        finally
        {
            IsPageBusy = false;
            await LoadAndSetData();
        }
    }


    [RelayCommand]
    async Task VerkaufFiltern()
    {
        try
        {
            if (IsPageBusy) return;
            IsPageBusy = true;
            List<Haus> lstVerkaufteHaeuser = new();
            if (!string.IsNullOrEmpty(VerkaufFilterText))
            {
                var hLst = await Task.Run(() => VerkaufteHaeuser.Where(h => h.AuftragID.Contains(VerkaufFilterText)));

                lstVerkaufteHaeuser.AddRange(hLst);
            }

            VerkaufteHaeuser.Clear();

            if (lstVerkaufteHaeuser.Count > 0)
                foreach (var haus in lstVerkaufteHaeuser)
                    VerkaufteHaeuser.Add(haus);
            else
                foreach (var haus in VerkaufteHaeuserBackup)
                    VerkaufteHaeuser.Add(haus);
        }
        finally
        {
            IsPageBusy = false;
        }
    }

    async partial void OnBuchungFilterTextChanged(string value)
    {
        try
        {
            if (IsPageBusy) return;
            IsPageBusy = true;
            List<Buchung> lstBuchung = new();
            if (!string.IsNullOrEmpty(BuchungFilterText))
            {
                var hLst = await Task.Run(() => Buchungen.Where(b => b.Haus.AuftragID.Contains(BuchungFilterText)));

                lstBuchung.AddRange(hLst);
            }

            Buchungen.Clear();

            if (lstBuchung.Count > 0)
                foreach (var buchung in lstBuchung)
                    Buchungen.Add(buchung);
            else
                foreach (var buchung in BuchungenBackup)
                    Buchungen.Add(buchung);
        }
        finally
        {
            IsPageBusy = false;
        }
    }
}
