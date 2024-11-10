using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Datenbank.Models;

public class Haus
{
    [Key]
    public int ID { get; set; }

    [Required]
    public byte[] Bild { get; set; }

    [Required]
    public string AuftragID { get; set; }

    [Required]
    public int ZimmerAnzahl { get; set; }

    [Required]
    public double Preis { get; set; }

    [Required]
    public string Anschrift { get; set; }

    public bool IstReserviert { get; set; } = false;

    public bool IstVerkauft { get; set; } = false;

    public DateTime VerkaufDatum { get; set; }
}