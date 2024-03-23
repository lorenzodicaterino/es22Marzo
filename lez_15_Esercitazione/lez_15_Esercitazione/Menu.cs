using lez_15_Esercitazione.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lez_15_Esercitazione
{
    internal class Menu
    {
        private static Menu Istanza;

        public static Menu getIstanza()
        {
            if (Istanza == null)
            {
                Istanza = new Menu();
            }
            return Istanza;
        }

        bool continua = true;
        public void Interfaccia()
        {
            while (continua)
            {

                using (var ctx = new Esercitazione22MarzoContext())
                {

                    Console.WriteLine("\n1. IMPORTA CSV\n2. INSERISCI\n3. LEGGI\n4. AGGIORNA\n5. ELIMINA\n6. ESPORTA CSV\n7. ESCI\n");


                    Console.Write("Inserire quale menu aprire: ");
                    String sceltaIniziale = Console.ReadLine();

                    switch (sceltaIniziale)
                    {
                        case "1":
                            Console.Write("\nHAI SCELTO IMPORTA CSV\n");
                            #region IMPORTACSV
                            #endregion
                            break;
                        case "2":
                            Console.Write("\nHAI SCELTO INSERISCI\n");
                            #region INSERISCI
                            Istanza.Inserisci();
                            #endregion
                            break;
                        case "3":
                            Console.Write("\nHAI SCELTO LEGGI\n");
                            #region LEGGI

                            Istanza.Leggi();

                            #endregion
                            break;

                        case "4":
                            Console.Write("\nHAI SCELTO AGGIORNA\n");
                            #region AGGIORNA

                            Istanza.Aggiorna();

                            #endregion
                            break;

                        case "5":
                            Console.Write("\nHAI SCELTO ELIMINA\n");
                            #region ELIMINA

                            Istanza.Elimina();

                            #endregion
                            break;

                        case "6":
                            Console.Write("\nHAI SCELTO ESPORTA CSV\n");
                            #region ESPORTACSV

                            Istanza.EsportaCSV();

                            #endregion
                            break;

                        case "7":
                            return;
                            break;

                        default:
                            Console.Write("\nNON HAI SCELTO UN CAZZO\n");
                            break;
                    }
                }
            }
        }
        public void Inserisci()
        {
            using (var ctx = new Esercitazione22MarzoContext())
            {
                ICollection<Evento> eventi = new List<Evento>();

                bool continuaInserisci = true;

                while (continuaInserisci)
                {
                    Console.Write("SCEGLI IN QUALE TABELLA INSERIRE (evento/partecipante/risorsa/indietro): ");
                    string sceltaInserisci = Console.ReadLine().ToLower();
                    switch (sceltaInserisci)
                    {
                        case "evento":
                            Console.Write("\nHAI SCELTO EVENTO\n");
                            #region INSERISCI EVENTO
                            bool continuaInserisciEvento = true;

                            while (continuaInserisciEvento)
                            {
                                Console.Write("INSERISCI NOME EVENTO: ");
                                string nomeEvento = Console.ReadLine();
                                Console.Write("INSERISCI DESCRIZIONE EVENTO: ");
                                string descrizioneEvento = Console.ReadLine();
                                Console.Write("INSERISCI DATA EVENTO (dd/mm/yyyy): ");
                                DateTime dataEvento = Convert.ToDateTime(Console.ReadLine());
                                Console.Write("INSERISCI LUOGO EVENTO: ");
                                string luogoEvento = Console.ReadLine();
                                Console.Write("INSERISCI CAPACITA' MASSIMA EVENTO: ");
                                int capacitaEvento = Convert.ToInt32(Console.ReadLine());

                                ICollection<Partecipante> partecipanti = new List<Partecipante>();


                                Console.Write("VUOI INSERIRE DEI PARTECIPANTI ALL'EVENTO? (y/n) : ");

                                ICollection<Partecipante> p = ctx.Partecipantes.Where(p => p.Deleted == null).ToList();
                                string sceltaPartecipanteInEventi = Console.ReadLine();

                                if (sceltaPartecipanteInEventi is not null && sceltaPartecipanteInEventi.Equals("y") && p.Count <= 0)
                                {
                                    Console.WriteLine("PER AGGIUNGERE PARTECIPANTI ALL'EVENTO E' NECESSARIO CREARE DEI PARTECIPANTI");
                                    Evento ev = new Evento
                                    {
                                        NomeEvento = nomeEvento,
                                        DescrizioneEvento = descrizioneEvento,
                                        DataEvento = dataEvento,
                                        LuogoEvento = luogoEvento,
                                        CapacitaMassima = capacitaEvento,
                                        PartecipanteRifs = partecipanti
                                    };

                                    ctx.Eventos.Add(ev);
                                    ctx.SaveChanges();
                                }
                                else if (sceltaPartecipanteInEventi.ToLower().Equals("y"))
                                {
                                    List<int> idPartecipantiInEventi = new List<int>();
                                    bool inserisciInEvento = true;
                                    while (inserisciInEvento)
                                    {
                                        Console.Write("INSERISCI ID PARTECIPANTE DA AGGIUNGERE: ");
                                        int idDaAggiungere = Convert.ToInt32(Console.ReadLine());
                                        idPartecipantiInEventi.Add(idDaAggiungere);

                                        Console.Write("VUOI INSERIRE ALTRI PARTECIPANTI? (y/n) :");
                                        if (Console.ReadLine().ToLower().Equals("n"))
                                            inserisciInEvento = false;
                                    }

                                    foreach (int i in idPartecipantiInEventi)
                                        partecipanti.Add(ctx.Partecipantes.First(p => p.PartecipanteId == i));

                                    Evento ev = new Evento
                                    {
                                        NomeEvento = nomeEvento,
                                        DescrizioneEvento = descrizioneEvento,
                                        DataEvento = dataEvento,
                                        LuogoEvento = luogoEvento,
                                        CapacitaMassima = capacitaEvento,
                                        PartecipanteRifs = partecipanti
                                    };

                                    ctx.Eventos.Add(ev);
                                    ctx.SaveChanges();
                                }

                                else
                                {
                                    Evento ev = new Evento
                                    {
                                        NomeEvento = nomeEvento,
                                        DescrizioneEvento = descrizioneEvento,
                                        DataEvento = dataEvento,
                                        LuogoEvento = luogoEvento,
                                        CapacitaMassima = capacitaEvento,
                                        PartecipanteRifs = partecipanti
                                    };

                                    ctx.Eventos.Add(ev);
                                    ctx.SaveChanges();
                                }

                                Console.Write("VUOI INSERIRE ALTRI EVENTI? (y/n) : ");

                                if (Console.ReadLine().ToLower().Equals("n"))
                                    continuaInserisciEvento = false;
                            }

                            #endregion
                            break;

                        case "partecipante":
                            Console.Write("\nHAI SCELTO PARTECIPANTE\n");
                            #region INSERISCI PARTECIPANTE
                            bool continuaInserisciPartecipante = true;

                            while (continuaInserisciPartecipante)
                            {
                                Console.Write("INSERISCI NOME PARTECIPANTE: ");
                                string nominativo = Console.ReadLine();
                                Console.Write("INSERISCI TELEFONO PARTECIPANTE: ");
                                string telefono = Console.ReadLine();
                                Console.Write("INSERISCI EMAIL PARTECIPANTE: ");
                                string email = Console.ReadLine();

                                Console.Write("VUOI INSERIRE QUESTO PARTECIPANTE  IN QUALCHE EVENTO? (y/n) : ");

                                ICollection<Evento> p = ctx.Eventos.Where(e => e.Deleted == null).ToList();
                                string sceltaEventiInPartecipanti = Console.ReadLine();

                                if (sceltaEventiInPartecipanti is not null && sceltaEventiInPartecipanti.Equals("y") && p.Count <= 0)
                                {
                                    Console.WriteLine("PER INSERIRE UN PARTECIPANTE IN UN EVENTO E' NECESSARIO CREARE PRIMA UN EVENTO. RIPROVA.");

                                    Partecipante pa = new Partecipante
                                    {
                                        Nominativo = nominativo,
                                        Telefono = telefono,
                                        Email = email,
                                        EventoRifs = eventi
                                    };
                                }
                                else if (sceltaEventiInPartecipanti.ToLower().Equals("y"))
                                {
                                    List<int> idEventiInPartecipanti = new List<int>();
                                    bool inserisciInPartecipante = true;
                                    while (inserisciInPartecipante)
                                    {
                                        Console.Write("INSERISCI ID EVENTO IN CUI AGGIUNGERE IL PARTECIPANTE: ");
                                        int idDaAggiungere = Convert.ToInt32(Console.ReadLine());
                                        idEventiInPartecipanti.Add(idDaAggiungere);

                                        Console.Write("VUOI INSERIRE ALTRI PARTECIPANTI? (y/n) :");
                                        if (Console.ReadLine().ToLower().Equals("n"))
                                            inserisciInPartecipante = false;
                                    }

                                    foreach (int i in idEventiInPartecipanti)
                                        eventi.Add(ctx.Eventos.First(e => e.EventoId == i));

                                    Partecipante pa = new Partecipante
                                    {
                                        Nominativo = nominativo,
                                        Telefono = telefono,
                                        Email = email,
                                        EventoRifs = eventi
                                    };

                                    ctx.Partecipantes.Add(pa);
                                    ctx.SaveChanges();
                                }
                                else
                                {
                                    Partecipante pa = new Partecipante
                                    {
                                        Nominativo = nominativo,
                                        Telefono = telefono,
                                        Email = email
                                    };
                                    ctx.Partecipantes.Add(pa);
                                    ctx.SaveChanges();
                                }



                                Console.Write("VUOI INSERIRE ALTRI PARTECIPANTI? (y/n) : ");

                                if (Console.ReadLine().ToLower().Equals("n"))
                                    continuaInserisciPartecipante = false;
                            }
                            #endregion
                            break;

                        case "risorsa":

                            Console.Write("\nHAI SCELTO RISORSA\n");
                            #region INSERISCI RISORSA
                            if (sceltaInserisci.ToLower().Equals("risorsa"))
                            {
                                eventi = ctx.Eventos.Where(e => e.Deleted == null).ToList();

                                if (eventi.Count > 0)
                                {
                                    bool continuaInserisciRisorsa = true;

                                    while (continuaInserisciRisorsa)
                                    {
                                        Console.Write("INSERISCI QUANTITA' RISORSA: ");
                                        int quantita = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("INSERISCI COSTO RISORSA: ");
                                        decimal costo = Convert.ToDecimal(Console.ReadLine());
                                        Console.Write("INSERISCI FORNITORE RISORSA: ");
                                        string fornitore = Console.ReadLine();
                                        Console.Write("INSERISCI TIPO RISORSA (cibo, bevanda, attrezzatura): ");
                                        string tipo = Console.ReadLine();
                                        Console.Write("INSERISCI ID EVENTO DI RIFERIMENTO: ");
                                        int eventorif = Convert.ToInt32(Console.ReadLine());


                                        Risorse ri = new Risorse
                                        {
                                            Quantita = quantita,
                                            Costo = costo,
                                            Fornitore = fornitore,
                                            Tipo = tipo,
                                            EventoRif = eventorif
                                        };

                                        ctx.Risorses.Add(ri);
                                        ctx.SaveChanges();

                                        Console.Write("VUOI INSERIRE ALTRE RISORSE? (y/n) : ");

                                        if (Console.ReadLine().ToLower().Equals("n"))
                                            continuaInserisciRisorsa = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("PER INSERIRE UN CAMPO DENTRO LA TABELLA RISORSE E' NECESSARIO INSERIRE PRIMA UN CAMPO NELLA TABELLA EVENTI. RIPROVA. ");
                                }
                            }

                            #endregion
                            break;

                        case "indietro":
                            continuaInserisci = false;
                            break;

                        default:
                            Console.Write("\nNON HAI SCELTO UN CAZZO\n");
                            break;
                    };
                }
            }
        }

        public void Leggi()
        {
            using (var ctx = new Esercitazione22MarzoContext())
            {
                bool continuaInserisci = true;

                while (continuaInserisci)
                {
                    Console.Write("SCEGLI QUALE TABELLA VISUALIZZARE (evento/partecipante/risorsa/indietro): ");
                    string sceltaInserisci = Console.ReadLine().ToLower();
                    switch (sceltaInserisci)
                    {
                        case "evento":
                            Console.Write("\nHAI SCELTO EVENTO\n");
                            #region STAMPA EVENTO
                            ICollection<Evento> eventi = new List<Evento>();
                            eventi = ctx.Eventos.Where(e => e.Deleted == null).ToList();
                            Console.Write("VUOI VISUALIZZARE UN SOLO ELEMENTO O TUTTI GLI ELEMENTI? (1/+): ");
                            string sceltaVisualizzaEvento = Console.ReadLine();


                            if (eventi.Count > 0 && sceltaVisualizzaEvento.Equals("1"))
                            {
                                bool continuaVisualizzaEvento = true;

                                while (continuaVisualizzaEvento)
                                {
                                    Console.Write("INSERISCI L'ID DELL'EVENTO DA VISUALIZZARE: ");
                                    int inputVisualizzaEvento = Convert.ToInt32(Console.ReadLine());
                                    if (inputVisualizzaEvento != 0 && inputVisualizzaEvento > 0)
                                    {
                                        eventi = ctx.Eventos.Where(e => e.EventoId == inputVisualizzaEvento && e.Deleted == null).ToList();
                                    }

                                    foreach (Evento e in eventi)
                                        Console.WriteLine(e.ToString());

                                    Console.Write("VUOI VISUALIZZARE TUTTI I PARTECIPANTI A QUESTO EVENTO? (y/n) : ");
                                    if (Console.ReadLine().ToLower().Equals("y"))
                                    {

                                        Evento partecipantiAllEvento = ctx.Eventos.Include(p => p.PartecipanteRifs).Single(e => e.EventoId == inputVisualizzaEvento);
                                        foreach (Partecipante p in partecipantiAllEvento.PartecipanteRifs)
                                            Console.WriteLine(p.ToString());
                                    }

                                    Console.Write("VUOI VISUALIZZARE ALTRI ELEMENTI? (y/n) : ");
                                    string sceltaContinuaVisualizzaEvento = Console.ReadLine();

                                    if (sceltaContinuaVisualizzaEvento.ToLower().Equals("n"))
                                        continuaVisualizzaEvento = false;
                                }
                            }
                            else if (eventi.Count > 0 && sceltaVisualizzaEvento.Equals("+"))
                            {
                                foreach (Evento e in eventi)
                                    Console.WriteLine(e.ToString());
                            }
                            else
                            {
                                Console.WriteLine("INPUT SBAGLIATO OPPURE LA TABELLA NON E' STATA ANCORA RIEMPITA");
                            }


                            #endregion
                            break;

                        case "partecipante":
                            Console.Write("\nHAI SCELTO PARTECIPANTE\n");
                            #region STAMPA PARTECIPANTE
                            ICollection<Partecipante> partecipanti = new List<Partecipante>();
                            partecipanti = ctx.Partecipantes.Where(p => p.Deleted == null).ToList();
                            Console.Write("VUOI VISUALIZZARE UN SOLO ELEMENTO O TUTTI GLI ELEMENTI? (1/+): ");
                            string sceltaVisualizzaPartecipante = Console.ReadLine();

                            if (partecipanti.Count > 0 && sceltaVisualizzaPartecipante.Equals("1"))
                            {
                                bool continuaVisualizzaPartecipante = true;

                                while (continuaVisualizzaPartecipante)
                                {
                                    Console.Write("INSERISCI L'ID DELL'EVENTO DA VISUALIZZARE: ");
                                    int inputVisualizzaPartecipant = Convert.ToInt32(Console.ReadLine());
                                    if (inputVisualizzaPartecipant != 0 && inputVisualizzaPartecipant > 0)
                                    {
                                        partecipanti = ctx.Partecipantes.Where(p => p.PartecipanteId == inputVisualizzaPartecipant && p.Deleted == null).ToList();
                                    }

                                    foreach (Partecipante p in partecipanti)
                                        Console.WriteLine(p.ToString());

                                    Console.Write("VUOI VISUALIZZARE TUTTI GLI EVENTI A CUI PARTECIPA? (y/n) : ");
                                    if (Console.ReadLine().ToLower().Equals("y"))
                                    {

                                        Partecipante eventoPartecipante = ctx.Partecipantes.Include(p => p.EventoRifs).Single(e => e.PartecipanteId == inputVisualizzaPartecipant);
                                        foreach (Evento e in eventoPartecipante.EventoRifs)
                                            Console.WriteLine(e.ToString());
                                    }

                                    Console.Write("VUOI VISUALIZZARE ALTRI ELEMENTI? (y/n) : ");
                                    string sceltaContinuaVisualizzaPartecipante = Console.ReadLine();

                                    if (sceltaContinuaVisualizzaPartecipante.ToLower().Equals("n"))
                                        continuaVisualizzaPartecipante = false;
                                }
                            }
                            else if (partecipanti.Count > 0 && sceltaVisualizzaPartecipante.Equals("+"))
                            {
                                partecipanti = ctx.Partecipantes.Where(p => p.Deleted == null).ToList();

                                foreach (Partecipante p in partecipanti)
                                    Console.WriteLine(p.ToString());
                            }
                            else
                            {
                                Console.WriteLine("INPUT SBAGLIATO OPPURE LA TABELLA NON E' STATA ANCORA RIEMPITA");
                            }
                            #endregion
                            break;

                        case "risorsa":

                            Console.Write("\nHAI SCELTO RISORSA\n");
                            #region STAMPA RISORSA
                            ICollection<Risorse> risorse = new List<Risorse>();
                            risorse = ctx.Risorses.Where(r => r.Deleted == null).ToList();
                            Console.Write("VUOI VISUALIZZARE UN SOLO ELEMENTO O TUTTI GLI ELEMENTI? (1/+): ");
                            string sceltaVisualizzaRisorse = Console.ReadLine();


                            if (risorse.Count > 0 && sceltaVisualizzaRisorse.Equals("1"))
                            {
                                bool continuaVisualizzaRisorse = true;

                                while (continuaVisualizzaRisorse)
                                {
                                    Console.Write("INSERISCI L'ID DELLA RISORSA DA VISUALIZZARE: ");
                                    int inputVisualizzaRisorse = Convert.ToInt32(Console.ReadLine());
                                    if (inputVisualizzaRisorse != 0 && inputVisualizzaRisorse > 0)
                                    {
                                        risorse = ctx.Risorses.Where(r => r.RisorseId == inputVisualizzaRisorse && r.Deleted == null).ToList();
                                    }

                                    int? eventoRIF = 0;

                                    foreach (Risorse r in risorse)
                                    {
                                        Console.WriteLine(r.ToString());
                                        eventoRIF = r.EventoRif;
                                    }

                                    Console.Write("VUOI VISUALIZZARE L'EVENTO AL QUALE E' COLLEGATO? (y/n) : ");
                                    string sceltaVisualizzaRisorseEvento = Console.ReadLine();
                                    if (sceltaVisualizzaRisorseEvento.ToLower().Equals("y"))
                                    {
                                        eventi = ctx.Eventos.Where(e => e.EventoId == eventoRIF && e.Deleted == null).ToList();

                                        foreach (Evento e in eventi)
                                            Console.WriteLine(e.ToString());

                                    }

                                    Console.Write("VUOI VISUALIZZARE ALTRI ELEMENTI? (y/n) : ");
                                    string sceltaContinuaVisualizzaRisorse = Console.ReadLine();

                                    if (sceltaContinuaVisualizzaRisorse.ToLower().Equals("n"))
                                        continuaVisualizzaRisorse = false;
                                }
                            }
                            else if (risorse.Count > 0 && sceltaVisualizzaRisorse.Equals("+"))
                            {
                                risorse = ctx.Risorses.Where(r => r.Deleted == null).ToList();

                                foreach (Risorse r in risorse)
                                    Console.WriteLine(r.ToString());
                            }
                            else
                            {
                                Console.WriteLine("INPUT SBAGLIATO OPPURE LA TABELLA NON E' STATA ANCORA RIEMPITA");
                            }
                            #endregion
                            break;

                        case "indietro":
                            continuaInserisci = false;
                            break;

                        default:
                            Console.Write("\nNON HAI SCELTO UN CAZZO\n");
                            break;
                    };
                }
            }
        }

        //TODO

        public void Aggiorna()
        {
            Console.Write("SCEGLI QUALE TABELLA AGGIORNARE (evento/partecipante/risorsa/indietro) : ");
            string sceltaAggiorna = Console.ReadLine();

            bool continuaAggiorna = true;

            while (continuaAggiorna)
            {
                switch (sceltaAggiorna)
                {
                    case "evento":
                        Console.Write("\nHAI SCELTO EVENTO\n");
                        #region AGGIORNA EVENTO
                        using (var ctx = new Esercitazione22MarzoContext())
                        {
                            ICollection<Evento> eventi = ctx.Eventos.Where(e => e.Deleted == null).ToList();

                            if (eventi.Count > 0)
                            {
                                bool continuaAggiornaEvento = true;

                                while (continuaAggiornaEvento)
                                {
                                    Console.Write("INSERISCI ID DELL'EVENTO DA AGGIONARE: ");
                                    int idDaAggiornareEvento = Convert.ToInt32(Console.ReadLine());
                                    Evento eventoAggiorna = ctx.Eventos.Single(e => e.EventoId == idDaAggiornareEvento);
                                    Console.WriteLine("L'ELEMENTO ATTUALMENTE CONTIENE : " + eventoAggiorna.ToString());

                                    Console.Write("INSERISCI NOME EVENTO AGGIORNATO: ");
                                    string nomeEvento = Console.ReadLine();
                                    Console.Write("INSERISCI DESCRIZIONE EVENTO AGGIORNATO: ");
                                    string descrizioneEvento = Console.ReadLine();
                                    Console.Write("INSERISCI DATA EVENTO AGGIORNATA (dd/mm/yyyy): ");
                                    DateTime dataEvento = Convert.ToDateTime(Console.ReadLine());
                                    Console.Write("INSERISCI LUOGO EVENTO AGGIORNATO: ");
                                    string? luogoEvento = Console.ReadLine();
                                    Console.Write("INSERISCI CAPACITA' MASSIMA EVENTO AGGIORNATA: ");
                                    int capacitaMassima = Convert.ToInt32(Console.ReadLine());

                                    Console.Write("VUOI MODIFICARE I PARTECIPANTI ALL'EVENTO (y/n) : ");
                                    if (Console.ReadLine().ToLower().Equals("y"))
                                    {

                                        Console.WriteLine("ATTUALMENTE PARTECIPANO ALL'EVENTO QUESTE PERSONE: ");
                                        Evento partecipantiAllEvento = ctx.Eventos.Include(p => p.PartecipanteRifs).Single(e => e.EventoId == idDaAggiornareEvento);
                                        foreach (Partecipante p in partecipantiAllEvento.PartecipanteRifs)
                                            Console.WriteLine(p.ToString());

                                        List<int> idPartecipantiInEventi = new List<int>();
                                        bool inserisciInEvento = true;
                                        while (inserisciInEvento)
                                        {
                                            Console.Write("INSERISCI ID PARTECIPANTE DA AGGIORNARE: ");
                                            int idDaAggiungere = Convert.ToInt32(Console.ReadLine());
                                            idPartecipantiInEventi.Add(idDaAggiungere);

                                            Console.Write("VUOI INSERIRE ALTRI PARTECIPANTI? (y/n) :");
                                            if (Console.ReadLine().ToLower().Equals("n"))
                                                inserisciInEvento = false;
                                        }

                                        ICollection<Partecipante> partecipanti = new List<Partecipante>();

                                        foreach (int i in idPartecipantiInEventi)
                                            partecipanti.Add(ctx.Partecipantes.First(p => p.PartecipanteId == i));


                                        eventoAggiorna.NomeEvento = nomeEvento;
                                        eventoAggiorna.DescrizioneEvento = descrizioneEvento;
                                        eventoAggiorna.DataEvento = dataEvento;
                                        eventoAggiorna.LuogoEvento = luogoEvento;
                                        eventoAggiorna.CapacitaMassima = capacitaMassima;
                                        eventoAggiorna.PartecipanteRifs = partecipanti;


                                        ctx.SaveChanges();
                                    }

                                    else
                                    {
                                        eventoAggiorna.NomeEvento = nomeEvento;
                                        eventoAggiorna.DescrizioneEvento = descrizioneEvento;
                                        eventoAggiorna.DataEvento = dataEvento;
                                        eventoAggiorna.LuogoEvento = luogoEvento;
                                        eventoAggiorna.CapacitaMassima = capacitaMassima;

                                        ctx.SaveChanges();
                                    }
                                }
                            }

                            else
                            {
                                Console.WriteLine("HAI BISOGNO DI POPOLARE LA TABELLA PRIMA DI AGGIORNARE DEGLI OGGETTI");
                            }
                        }
                }
            }

                        #endregion
                        break;

                                case "partecipante":
                            Console.Write("\nHAI SCELTO PARTECIPANTE\n");
                            #region AGGIORNA PARTECIPANTE
                            using (var ctx = new Esercitazione22MarzoContext())
                            {
                                ICollection<Partecipante> partecipanti = ctx.Partecipantes.Where(e => e.Deleted == null).ToList();

                                if (partecipanti.Count > 0)
                                {
                                    bool continuaAggiornaPartecipante = true;

                                    while (continuaAggiornaPartecipante)
                                    {
                                        Console.Write("INSERISCI ID DEL PARTECIPANTE DA AGGIONARE: ");

                                        Partecipante partecipanteAggiorna = ctx.Partecipantes.Single(p => p.PartecipanteId == Convert.ToInt32(Console.ReadLine()));
                                        Console.WriteLine("L'ELEMENTO ATTUALMENTE CONTIENE : " + partecipanteAggiorna.ToString());

                                        Console.Write("INSERISCI NOMINATIVO PARTECIPANTE AGGIORNATO: ");
                                        partecipanteAggiorna.Nominativo = Console.ReadLine();
                                        Console.Write("INSERISCI TELEFONO PARTECIPANTE AGGIORNATO: ");
                                        partecipanteAggiorna.Telefono = Console.ReadLine();
                                        Console.Write("INSERISCI EMAIL PARTECIPANTE AGGIORNATO: ");
                                        partecipanteAggiorna.Email = Console.ReadLine();

                                        ctx.SaveChanges();

                                        Console.Write("VUOI AGGIORNARE UN ALTRO PARTECIPANTE (y/n) : ");
                                        if (Console.ReadLine().ToLower().Equals("n"))
                                            continuaAggiornaPartecipante = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("HAI BISOGNO DI POPOLARE LA TABELLA PRIMA DI AGGIORNARE DEGLI OGGETTI");
                                }
                            }
                            #endregion
                            break;

                        case "risorsa":

                            Console.Write("\nHAI SCELTO RISORSA\n");
                            #region AGGIORNA RISORSA
                            using (var ctx = new Esercitazione22MarzoContext())
                            {
                                ICollection<Risorse> risorse = ctx.Risorses.Where(e => e.Deleted == null).ToList();

                                if (risorse.Count > 0)
                                {
                                    bool continuaAggiornaRisorsa = true;

                                    while (continuaAggiornaRisorsa)
                                    {
                                        Console.Write("INSERISCI ID DELLA RISORSA DA AGGIONARE: ");

                                        Risorse risorsaAggiorna = ctx.Risorses.Single(r => r.RisorseId == Convert.ToInt32(Console.ReadLine()));
                                        Console.WriteLine("L'ELEMENTO ATTUALMENTE CONTIENE : " + risorsaAggiorna.ToString());

                                        Console.Write("INSERISCI QUANTITA RISORSA AGGIORNATO: ");
                                        risorsaAggiorna.Quantita = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("INSERISCI COSTO RISORSA AGGIORNATO: ");
                                        risorsaAggiorna.Costo = Convert.ToDecimal(Console.ReadLine());
                                        Console.Write("INSERISCI FORNITORE RISORSA AGGIORNATO: ");
                                        risorsaAggiorna.Fornitore = Console.ReadLine();
                                        Console.Write("INSERISCI TIPO RISORSA AGGIORNATO: ");
                                        risorsaAggiorna.Tipo = Console.ReadLine();
                                        Console.Write("INSERISCI EVENTO DI RIFERIMENTO DELLA RISORSA AGGIORNATO: ");
                                        risorsaAggiorna.EventoRif = Convert.ToInt32(Console.ReadLine());

                                        ctx.SaveChanges();

                                        Console.Write("VUOI AGGIORNARE UN ALTRA RISORSA? (y/n) : ");
                                        if (Console.ReadLine().ToLower().Equals("n"))
                                            continuaAggiornaRisorsa = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("HAI BISOGNO DI POPOLARE LA TABELLA PRIMA DI AGGIORNARE DEGLI OGGETTI");
                                }
                            }

                            #endregion
                            break;

                        case "indietro":
                            continuaAggiorna = false;
                            break;

                        default:
                            Console.Write("\nNON HAI SCELTO UN CAZZO\n");
                            break;
            
        }
    }
}













public void Elimina()
{

}

public void EsportaCSV()
{

}

    }



