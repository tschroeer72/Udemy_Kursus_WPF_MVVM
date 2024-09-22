using Kursprojekt.Datenbank.Models;

namespace Kursprojekt.Services;

public class CurrentUser
{
    public static AppUser AppUser { get; set; } = new();
}