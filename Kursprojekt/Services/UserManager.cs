using System.Security.Cryptography;
using System.Text;
using Kursprojekt.Datenbank.DBServices;
using Kursprojekt.Datenbank.DTOs;
using Kursprojekt.Datenbank.Models;

namespace Kursprojekt.Services;

public class UserManager
{
    private static void HashUserPassword(ref AppUser appUser)
    {
        string strPW = appUser.Password;
        using var sha256 = SHA256.Create();
        var PWByte = Encoding.Default.GetBytes(strPW);
        var PWByteSHA = sha256.ComputeHash(PWByte);
        appUser.Password = Convert.ToBase64String(PWByteSHA);
    }

    public static async Task<DBResponse> RegisterUserAsync(AppUser appUser)
    {
        var user = await DBUnit.User.GetFirstOrDefaultAsync(filter: f => f.Email == appUser.Email);
        if (user != null)
        {
            return new DBResponse() { Message = $"{user.Email} ist bereits vorhanden!" };
        }
        
        HashUserPassword(ref appUser);
        appUser.Role = null;
        return await DBUnit.User.AddAsync((appUser));
    }

    public static async Task<DBResponse> LoginUserAsync(AppUser appUser)
    {
        var DBuser = await DBUnit.User.GetFirstOrDefaultAsync(filter: f => f.Email == appUser.Email, includeProperties: nameof(Role));
        if (DBuser == null)
        {
            return new DBResponse() { Message = $"{DBuser.Email} ist nicht vorhanden!" };
        }

        HashUserPassword(ref appUser);

        if (DBuser.Password != appUser.Password)
        {
            return new DBResponse() { Message = "Das Passwort ist falsch!" };
        }

        return new DBResponse() { Success = true, Data = DBuser};
    }
}