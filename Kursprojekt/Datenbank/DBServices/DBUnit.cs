using Kursprojekt.Datenbank.Models;

namespace Kursprojekt.Datenbank.DBServices;

public class DBUnit
{
    public static DBUnitBase<AppUser> User
    {
        get { return new DBUnitBase<AppUser>(); }
    }

    public static DBUnitBase<Role> Role
    {
        get { return new DBUnitBase<Role>(); }
    }
}