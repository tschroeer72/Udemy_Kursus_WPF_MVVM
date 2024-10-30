using CommunityToolkit.Mvvm.ComponentModel;
using Kursprojekt.Datenbank.Models;

namespace Kursprojekt.DTOs;

//[INotifyPropertyChanged]
public partial class LoginUserInfos : ObservableObject
{
    [ObservableProperty] AppUser loginUser = new();

    [ObservableProperty] bool isAdmin = false;

    [ObservableProperty] bool isUser = false;

    [ObservableProperty] bool isNurLesenUser = false;
}