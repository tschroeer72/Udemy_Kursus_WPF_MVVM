using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Datenbank.Models;

#nullable disable
public class Role
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string RoleName { get; set; } 
    public string Description { get; set; }
}
