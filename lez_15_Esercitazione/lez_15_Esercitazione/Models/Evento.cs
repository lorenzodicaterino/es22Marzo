using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

namespace lez_15_Esercitazione.Models;

public partial class Evento
{
    public int EventoId { get; set; }

    public string NomeEvento { get; set; } = null!;

    public string? DescrizioneEvento { get; set; }

    public DateTime DataEvento { get; set; }

    public string LuogoEvento { get; set; } = null!;

    public int CapacitaMassima { get; set; }

    public DateTime? Deleted { get; set; }

    public virtual ICollection<Risorse> Risorses { get; set; } = new List<Risorse>();

    public virtual ICollection<Partecipante> PartecipanteRifs { get; set; } = new List<Partecipante>();

    //public Evento(string? nome, string? descrizione, DateTime data, string? luogo,int capa)
    //{
    //    NomeEvento = nome;
    //    DescrizioneEvento = descrizione;
    //    DataEvento = data;
    //    LuogoEvento = luogo;
    //    CapacitaMassima = capa;
    //}


    public override string ToString()
    {
        return $"{EventoId} {NomeEvento} {DescrizioneEvento} {DataEvento.ToString("dd/mm/yyyy")} {LuogoEvento} {CapacitaMassima}";
    }

    public string EsportaCSV()
    {
        if (DescrizioneEvento is not null)
            return $"{EventoId};{NomeEvento};{DescrizioneEvento};{DataEvento.ToString("dd/mm/yyyy")};{LuogoEvento};{CapacitaMassima}";
        else
            return $"{EventoId};{NomeEvento};{DataEvento.ToString("dd/mm/yyyy")};{LuogoEvento};{CapacitaMassima}";

    }
}
