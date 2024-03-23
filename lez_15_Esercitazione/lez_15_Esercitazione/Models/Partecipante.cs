using System;
using System.Collections.Generic;

namespace lez_15_Esercitazione.Models;

public partial class Partecipante
{
    public int PartecipanteId { get; set; }

    public string Nominativo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? Deleted { get; set; }

    public virtual ICollection<Evento> EventoRifs { get; set; } = new List<Evento>();

    public string EsportaCSV()
    {
        if (Email is not null)
            return $"{PartecipanteId};{Nominativo};{Telefono};{Email};";
        else
            return $"{PartecipanteId};{Nominativo};{Telefono};";
    }

    public override string ToString()
    {
        return $"{PartecipanteId} {Nominativo} {Telefono} {Email}";
    }
}
