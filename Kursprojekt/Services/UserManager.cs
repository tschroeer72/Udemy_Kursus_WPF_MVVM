using System.Security.Cryptography;
using System.Text;
using Kursprojekt.Datenbank.DBServices;
using Kursprojekt.Datenbank.DTOs;
using Kursprojekt.Datenbank.Models;
using Kursprojekt.DTOs;

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
            return new DBResponse() { Message = $"{DBuser?.Email} ist nicht vorhanden!" };
        }

        HashUserPassword(ref appUser);

        if (DBuser.Password != appUser.Password)
        {
            return new DBResponse() { Message = "Das Passwort ist falsch!" };
        }

        CurrentUser.AppUser = DBuser;
        return new DBResponse() { Success = true, Data = DBuser};
    }

    public static async Task<List<AppUser>> GetALLUserAsync()
    {
        List<AppUser> lstUsers = new();
        try
        {
            var lstErg = await DBUnit.User.GetAllAsync(includeProperties: nameof(Role));
            if (lstErg != null)
            {
                lstUsers.AddRange(lstErg);
            }

            return lstUsers;
        }
        catch (Exception)
        {
            return lstUsers;
        }
    }

    public static async Task<DBResponse> DeleteUserByIDAsync(int ID)
    {
        return await DBUnit.User.DeleteByIDAsync(ID);
    }

    public static async Task<AppUser?> GetUserByIDAsync(int ID)
    {
        return await DBUnit.User.GetByIDAsync(ID);
    }

    public static async Task<DBResponse> UpdateUserAsync(AppUser appUser, bool passwordAlso = false)
    {
        //Wird nur bei Update Passwort benutzt
        if (passwordAlso)
        {
            HashUserPassword(ref appUser);
        }

        //Sonst versucht er Role hinzufügen
        appUser.Role = null;
        return await DBUnit.User.UpdateAsync(appUser);
    }

    public static bool IsUserInRole(AppUser appUser, RoleType roleType)
    {
        if (appUser.Role == null) return false;
        
        //WENN User ist Admin DANN hat er auch die Rollen User und NurLesen
        if (appUser.Role.RoleName == RoleType.Admin.ToString())
        {
            return true;
        }

        return appUser.Role.RoleName.ToString() == roleType.ToString();

    }

    public static LoginUserInfos GetLoginUserInfos()
    {
        LoginUserInfos loginUserInfos = new();

        loginUserInfos.LoginUser = CurrentUser.AppUser;
        loginUserInfos.IsAdmin = IsUserInRole(CurrentUser.AppUser, RoleType.Admin);
        loginUserInfos.IsUser = IsUserInRole(CurrentUser.AppUser, RoleType.User);
        loginUserInfos.IsNurLesenUser = IsUserInRole(CurrentUser.AppUser, RoleType.NurLesen);

        return loginUserInfos;
    }
}