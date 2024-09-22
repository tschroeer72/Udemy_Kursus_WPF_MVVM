using Kursprojekt.Datenbank.Models;

namespace Kursprojekt.DTOs;

public class LoginUserInfos
{
    public AppUser LoginUser { get; set; } = new();
    public bool IsAdmin { get; set; }
    public bool IsUser { get; set; }
    public bool IsNurLesenUser { get; set; }
}