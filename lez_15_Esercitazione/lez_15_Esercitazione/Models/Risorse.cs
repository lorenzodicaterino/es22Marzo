using System;
using System.Collections.Generic;

namespace lez_15_Esercitazione.Models;

public partial class Risorse
{
    public int RisorseId { get; set; }

    public int Quantita { get; set; }

    public decimal Costo { get; set; }

    public string Fornitore { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int? EventoRif { get; set; }

    public DateTime? Deleted { get; set; }

    public virtual Evento? EventoRifNavigation { get; set; }

    public string EsportaCSV()
    {
        return $"{RisorseId};{Quantita};{Costo};{Fornitore};{Tipo};{EventoRif};";
    }

    public override string ToString()
    {
        return $"{RisorseId} {Quantita} {Costo} {Fornitore} {Tipo} {EventoRif}";
    }
}
