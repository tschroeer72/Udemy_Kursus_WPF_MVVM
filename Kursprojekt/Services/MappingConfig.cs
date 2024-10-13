using AutoMapper;
using Kursprojekt.Datenbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Services;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Role, Role>();
        CreateMap<AppUser, AppUser>();
    }
}