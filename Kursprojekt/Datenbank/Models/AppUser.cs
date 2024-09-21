using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Datenbank.Models;

#nullable disable
public class AppUser
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    
    public int RoleID { get; set; }

    [ForeignKey(nameof(RoleID))]
    public Role Role { get; set; }
}
