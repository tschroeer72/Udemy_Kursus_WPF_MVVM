using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Datenbank.Models;

public class Buchung
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string Bearbeiter { get; set; }

    [Required]
    public string KundenName { get; set; }

    [Required]
    public string KundenTelefonNummer { get; set; }

    [Required]
    public DateTime BuchungDatum { get; set; }

    [Required]
    public DateTime BesichtigungDatum { get; set; }

    [Required]
    public string BesichtigungZeit { get; set; }

    public int HausID { get; set; }


    [ForeignKey(nameof(HausID))]
    public Haus Haus { get; set; }
}