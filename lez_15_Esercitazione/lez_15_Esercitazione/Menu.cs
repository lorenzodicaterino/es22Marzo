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

        private Menu()
        {

        }

        bool continua = true;
        public void Interfaccia()
        {
            while (continua)
            {

                using (var ctx = new Esercitazione22MarzoContext())
                {

                    Console.WriteLine("\n1. IMPORTA CSV\n2. INSERISCI\n3. LEGGI\n4. AGGIORNA\n5. ELIMINA\n6. ESPORTA CSV\n7. ESCI\n");


                    Console.Write("INSERIRE NUMERO MENU' : ");
                    String sceltaIniziale = Console.ReadLine();

                    switch (sceltaIniziale)
                    {
                        case "1":
                            Console.Write("\nHAI SCELTO IMPORTA CSV\n");
                            #region IMPORTACSV

                            Console.WriteLine("QUANDO SCOPRO COME SE FA CHO METTO");

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
                            Console.Write("\nINPUT NON VALIDO. RIPROVA.\n");
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
                                    Console.WriteLine("AL MOMENTO QUESTI SONO I PARTECIPANTI INSERIBBILI : ");
                                    Istanza.StampaPartecipanti();

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
                                    Console.WriteLine("AL MOMENTO QUESTI SONO GLI EVENTI INSERIBILI : ");
                                    Istanza.StampaEventi();

                                    List<int> idEventiInPartecipanti = new List<int>();
                                    bool inserisciInPartecipante = true;
                                    while (inserisciInPartecipante)
                                    {
                                        Console.Write("INSERISCI ID EVENTO IN CUI AGGIUNGERE IL PARTECIPANTE: ");
                                        int idDaAggiungere = Convert.ToInt32(Console.ReadLine());
                                        idEventiInPartecipanti.Add(idDaAggiungere);

                                        Console.Write("VUOI INSERIRE ALTRI EVENTI? (y/n) :");
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

                                        Console.WriteLine("QUESTI GLI EVENTI AL QUALE POTER ASSOCIARE LA RISORSA :  ");
                                        Istanza.StampaEventi();


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
                            Console.Write("\nINPUT NON VALIDO. RIPROVA.\n");
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
                            else if(eventi.Count <=0 )
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
                                    Console.Write("INSERISCI L'ID DEL PARTECIPANTE DA VISUALIZZARE: ");
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
                            Console.Write("\nINPUT NON VALIDO. RIPROVA.\n");
                            break;
                    };
                }
            }
        }

        public void Aggiorna()
        {
            Console.Write("SCEGLI QUALE TABELLA AGGIORNARE (evento/partecipante/risorsa/indietro) : ");
            string sceltaAggiorna = Console.ReadLine();

            bool continuaAggiorna = true;

            using (var ctx = new Esercitazione22MarzoContext())
            {
                while (continuaAggiorna)
                {
                    switch (sceltaAggiorna)
                    {
                        case "evento":
                            #region Aggiorna Evento
                            bool continuaAggiornaEvento = true;
                            while (continuaAggiornaEvento)
                            {
                                ICollection<Evento> eventi = ctx.Eventos.Where(e => e.Deleted == null).ToList();

                                if (eventi.Count > 0)
                                {

                                    Console.WriteLine("AL MOMENTO FANNO PARTE DELLA TABELLA EVENTO QUESTI ELEMENTI: ");
                                    Istanza.StampaEventi();

                                    Console.Write("INSERISCI ID DELL'EVENTO DA AGGIONARE: ");
                                    int idDaAggiornareEvento = Convert.ToInt32(Console.ReadLine());
                                    Evento eventoAggiorna = ctx.Eventos.Single(e => e.EventoId == idDaAggiornareEvento);
                                    Console.WriteLine("L'ELEMENTO ATTUALMENTE CONTIENE : " + eventoAggiorna.ToString());

                                    Console.Write("INSERISCI NOME EVENTO AGGIORNATO: ");
                                    string nomeEvento = Console.ReadLine();
                                    //if (nomeEvento is null)
                                    //    nomeEvento = eventoAggiorna.NomeEvento;
                                    Console.Write("INSERISCI DESCRIZIONE EVENTO AGGIORNATO: ");
                                    string descrizioneEvento = Console.ReadLine();
                                    //if (descrizioneEvento is null)
                                    //    descrizioneEvento = eventoAggiorna.DescrizioneEvento;
                                    Console.Write("INSERISCI DATA EVENTO AGGIORNATA (dd/mm/yyyy): ");
                                    DateTime dataEvento = Convert.ToDateTime(Console.ReadLine());
                                    Console.Write("INSERISCI LUOGO EVENTO AGGIORNATO: ");
                                    string? luogoEvento = Console.ReadLine();
                                    //if (luogoEvento is null)
                                    //    luogoEvento = eventoAggiorna.LuogoEvento;
                                    Console.Write("INSERISCI CAPACITA' MASSIMA EVENTO AGGIORNATA: ");
                                    int capacitaMassima = Convert.ToInt32(Console.ReadLine());
                                    //if (capacitaMassima == 0)
                                    //    capacitaMassima = eventoAggiorna.CapacitaMassima;

                                    Console.Write("VUOI MODIFICARE I PARTECIPANTI ALL'EVENTO (y/n) : ");
                                    if (Console.ReadLine().ToLower().Equals("y"))
                                    {
                                        Console.WriteLine("ATTUALMENTE PARTECIPANO ALL'EVENTO QUESTE PERSONE: ");
                                        Evento partecipantiAllEvento = ctx.Eventos.Include(p => p.PartecipanteRifs).Single(e => e.EventoId == idDaAggiornareEvento);
                                        foreach (Partecipante p in partecipantiAllEvento.PartecipanteRifs)
                                            Console.WriteLine(p.ToString());

                                        List<int> idPartecipantiInEventi = new List<int>();
                                        bool inserisciInEvento = true;
                                        Console.Write("LISTA PARTECIPANTI SVUOTATA.");
                                        while (inserisciInEvento)
                                        {
                                            Console.Write("INSERISCI ID DEI PARTECIPANTI DA AGGIUNGERE: ");
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

                                    Console.Write("VUOI MODIFICARE ALTRI EVENTI? (y/n) : ");
                                    if (Console.ReadLine().ToLower().Equals("n"))
                                    {
                                        continuaAggiornaEvento = false;
                                        continuaAggiorna = false;
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("HAI BISOGNO DI POPOLARE LA TABELLA PRIMA DI AGGIORNARE DEGLI OGGETTI");
                                    continuaAggiornaEvento = false;
                                    continuaAggiorna = false;
                                    break;
                                }
                            }
                            #endregion
                            break;
                        case "partecipante":
                            #region Aggiorna Partecipante
                            bool continuaAggiornaPartecipante = true;
                            while (continuaAggiornaPartecipante)
                            {
                                ICollection<Partecipante> partecipanti = ctx.Partecipantes.Where(p => p.Deleted == null).ToList();

                                if (partecipanti.Count > 0)
                                {

                                    Console.WriteLine("AL MOMENTO FANNO PARTE DELLA TABELLA PARTECIPANTI QUESTI ELEMENTI: ");
                                    Istanza.StampaPartecipanti();

                                    Console.Write("INSERISCI ID DEL PARTECIPANTE DA AGGIONARE: ");
                                    int idDaAggiornarePartecipante = Convert.ToInt32(Console.ReadLine());
                                    Partecipante partecipanteAggiorna = ctx.Partecipantes.Single(p => p.PartecipanteId == idDaAggiornarePartecipante);
                                    Console.WriteLine("L'ELEMENTO ATTUALMENTE CONTIENE : " + partecipanteAggiorna.ToString());

                                    Console.Write("INSERISCI NOMINATIVO PARTECIPANTE AGGIORNATO: ");
                                    string nomePartecipante = Console.ReadLine();
                                    Console.Write("INSERISCI TELEFONO PARTECIPANTE AGGIORNATO: ");
                                    string telefonoPartecipante = Console.ReadLine();
                                    Console.Write("INSERISCI EMAIL PARTECIPANTE AGGIORNATO: ");
                                    string? emailPartecipante = Console.ReadLine();

                                    Console.Write("VUOI MODIFICARE GLI EVENTI A CUI PARTECIPA? (y/n) : ");
                                    if (Console.ReadLine().ToLower().Equals("y"))
                                    {
                                        Console.WriteLine("ATTUALMENTE PARTECIPA A QUESTI EVENTI: ");
                                        Partecipante eventiDelPartecipante = ctx.Partecipantes.Include(e => e.EventoRifs).Single(p => p.PartecipanteId == idDaAggiornarePartecipante);
                                        foreach (Evento e in eventiDelPartecipante.EventoRifs)
                                            Console.WriteLine(e.ToString());

                                        List<int> idEventiDelPartecipante = new List<int>();
                                        bool inserisciInPartecipante = true;

                                        Console.Write("LISTA EVENTI SVUOTATA.");
                                        while (inserisciInPartecipante)
                                        {
                                            Console.Write("INSERISCI ID DEGLI EVENTI DA AGGIUNGERE: ");
                                            int idDaAggiungere = Convert.ToInt32(Console.ReadLine());
                                            idEventiDelPartecipante.Add(idDaAggiungere);

                                            Console.Write("VUOI INSERIRE ALTRI EVENTI? (y/n) :");
                                            if (Console.ReadLine().ToLower().Equals("n"))
                                                inserisciInPartecipante = false;

                                            ICollection<Evento> eventi = new List<Evento>();

                                            foreach (int i in idEventiDelPartecipante)
                                                eventi.Add(ctx.Eventos.First(e => e.EventoId == i));


                                            partecipanteAggiorna.Nominativo = nomePartecipante;
                                            partecipanteAggiorna.Telefono = telefonoPartecipante;
                                            partecipanteAggiorna.Email = emailPartecipante;
                                            partecipanteAggiorna.EventoRifs = eventi;

                                            ctx.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        partecipanteAggiorna.Nominativo = nomePartecipante;
                                        partecipanteAggiorna.Telefono = telefonoPartecipante;
                                        partecipanteAggiorna.Email = emailPartecipante;

                                        ctx.SaveChanges();
                                    }

                                    Console.Write("VUOI MODIFICARE ALTRI PARTECIPANTI? (y/n) : ");
                                    if (Console.ReadLine().ToLower().Equals("n"))
                                    {
                                        continuaAggiornaPartecipante = false;
                                        continuaAggiorna = false;
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("HAI BISOGNO DI POPOLARE LA TABELLA PRIMA DI AGGIORNARE DEGLI OGGETTI");
                                    continuaAggiornaPartecipante = false;
                                    continuaAggiorna = false;
                                    break;
                                }
                            }
                            #endregion
                            break;
                        case "risorsa":
                            bool continuaAggiornaRisorsa = true;
                            while (continuaAggiornaRisorsa)
                            {
                                ICollection<Risorse> risorse = ctx.Risorses.Where(r => r.Deleted == null).ToList();

                                if (risorse.Count > 0)
                                {

                                    Console.WriteLine("AL MOMENTO FANNO PARTE DELLA TABELLA RISORSE QUESTI ELEMENTI: ");
                                    Istanza.StampaRisorse();

                                    Console.Write("INSERISCI ID DELLA RISORSA DA AGGIONARE: ");
                                    int idDaAggiornareRisorsa = Convert.ToInt32(Console.ReadLine());
                                    Risorse risorsaDaAggiornare = ctx.Risorses.Single(r => r.RisorseId == idDaAggiornareRisorsa);
                                    Console.WriteLine("L'ELEMENTO ATTUALMENTE CONTIENE : " + risorsaDaAggiornare.ToString());

                                    Console.Write("INSERISCI QUANTITA' RISORSA AGGIORNATA: ");
                                    risorsaDaAggiornare.Quantita = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("INSERISCI COSTO RISORSA AGGIORNATA: ");
                                    risorsaDaAggiornare.Costo = Convert.ToDecimal(Console.ReadLine());
                                    Console.Write("INSERISCI FORNITORE RISORSA AGGIORNATA: ");
                                    risorsaDaAggiornare.Fornitore = Console.ReadLine();
                                    Console.Write("INSERISCI TIPO RISORSA AGGIORNATA: ");
                                    risorsaDaAggiornare.Tipo = Console.ReadLine();
                                    Console.Write("INSERISCI EVENTO DI RIFERIMENTO DELLA RISORSA AGGIORNATA: ");
                                    risorsaDaAggiornare.EventoRif = Convert.ToInt32(Console.ReadLine());

                                    ctx.SaveChanges();


                                    Console.Write("VUOI MODIFICARE ALTRE RISORSE ? (y/n) : ");
                                    if (Console.ReadLine().ToLower().Equals("n"))
                                    {
                                        continuaAggiornaRisorsa = false;
                                        continuaAggiorna = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("HAI BISOGNO DI POPOLARE LA TABELLA PRIMA DI AGGIORNARE DEGLI OGGETTI");
                                    continuaAggiornaRisorsa = false;
                                    continuaAggiorna = false;
                                    break;
                                }
                            }
                            break;

                        case "indietro":


                            break;
                        default:
                            Console.Write("\nINPUT NON VALIDO. RIPROVA.\n");
                            continuaAggiorna = false;
                            break;
                    }
                }
            }
        }

        public void Elimina()
        {
            using (var ctx = new Esercitazione22MarzoContext())
            {
                bool continuaElimina = true;
                Console.Write("DA QUALE TABELLA SI DESIDERA ELIMINARE? (evento/partecipante/risorsa/indietro) : ");
                string sceltaElimina = Console.ReadLine();

                while (continuaElimina)
                {
                    switch (sceltaElimina)
                    {
                        case "evento":
                            #region Elimina Evento
                            bool continuaAggiornaEvento = true;
                            while (continuaAggiornaEvento)
                            {
                                Console.WriteLine("HAI SCELTO EVENTO");

                                ICollection<Evento> eventi = new List<Evento>();
                                eventi = ctx.Eventos.Where(e => e.Deleted == null).ToList();

                                if (eventi.Count <= 0)
                                {
                                    Console.WriteLine("PRIMA DI POTER ELIMINARE UN CAMPO DEVI POPOLARE LA TABELLA. RIPROVA");
                                    continuaAggiornaEvento = false;
                                    continuaElimina = false;
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("AL MOMENTO LA TABELLA CONTIENE :");
                                    Istanza.StampaEventi();

                                    Console.Write("INSERISCI ID CAMPO DA ELIMINARE : ");
                                    Evento eventoDaEliminare = ctx.Eventos.Single(e=>e.EventoId == Convert.ToInt32(Console.ReadLine()) && e.Deleted==null);
                                    if (eventoDaEliminare is not null)
                                    {
                                        eventoDaEliminare.Deleted = DateTime.Now;
                                        ctx.SaveChanges();
                                    }
                                    else
                                        Console.WriteLine("ELEMENTO NON TROVATO");
                                }

                                Console.Write("VUOI ELIMINARE ALTRI EVENTI? (y/n) : ");
                                if(Console.ReadLine().ToLower().Equals("n"))
                                {
                                    continuaAggiornaEvento = false;
                                    continuaElimina = false;
                                }
                            }
                            #endregion
                            break;
                        case "partecipante":
                            bool continuaAggiornaPartecipante = true;
                            #region Elimina Partecipante
                            while (continuaAggiornaPartecipante)
                            {
                                Console.WriteLine("HAI SCELTO PARTECIPANTE");

                                ICollection<Partecipante> partecipante = new List<Partecipante>();
                                partecipante = ctx.Partecipantes.Where(p => p.Deleted == null).ToList();

                                if (partecipante.Count <= 0)
                                {
                                    Console.WriteLine("PRIMA DI POTER ELIMINARE UN CAMPO DEVI POPOLARE LA TABELLA. RIPROVA");
                                    continuaAggiornaPartecipante = false;
                                    continuaElimina = false;
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("AL MOMENTO LA TABELLA CONTIENE :");
                                    Istanza.StampaPartecipanti();

                                    Console.Write("INSERISCI ID CAMPO DA ELIMINARE : ");
                                    Partecipante partecipanteDaEliminare= ctx.Partecipantes.Single(p => p.PartecipanteId== Convert.ToInt32(Console.ReadLine()) && p.Deleted == null);
                                    if (partecipanteDaEliminare is not null)
                                    {
                                        partecipanteDaEliminare.Deleted = DateTime.Now;
                                        ctx.SaveChanges();
                                    }
                                    else
                                        Console.WriteLine("ELEMENTO NON TROVATO");
                                }

                                Console.Write("VUOI ELIMINARE ALTRI PARTECIPANTI? (y/n) : ");
                                if (Console.ReadLine().ToLower().Equals("n"))
                                {
                                    continuaAggiornaPartecipante = false;
                                    continuaElimina = false;
                                }
                            }
                            #endregion
                            break;
                        case "risorsa":
                            #region Elimina Risorsa
                            bool continuaAggiornaRisorsa = true;
                            while (continuaAggiornaRisorsa)
                            {
                                Console.WriteLine("HAI SCELTO RISORSA");

                                ICollection<Risorse> risorse= new List<Risorse>();
                                risorse = ctx.Risorses.Where(r => r.Deleted == null).ToList();

                                if (risorse.Count <= 0)
                                {
                                    Console.WriteLine("PRIMA DI POTER ELIMINARE UN CAMPO DEVI POPOLARE LA TABELLA. RIPROVA");
                                    continuaAggiornaRisorsa = false;
                                    continuaElimina = false;
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("AL MOMENTO LA TABELLA CONTIENE :");
                                    Istanza.StampaRisorse();

                                    Console.Write("INSERISCI ID CAMPO DA ELIMINARE : ");
                                    Risorse risorsaDaEliminare= ctx.Risorses.Single(r => r.RisorseId== Convert.ToInt32(Console.ReadLine()) && r.Deleted == null);
                                    if (risorsaDaEliminare is not null)
                                    {
                                        risorsaDaEliminare.Deleted = DateTime.Now;
                                        ctx.SaveChanges();
                                    }
                                    else
                                        Console.WriteLine("ELEMENTO NON TROVATO");
                                }

                                Console.Write("VUOI ELIMINARE ALTRE RISORSE? (y/n) : ");
                                if (Console.ReadLine().ToLower().Equals("n"))
                                {
                                    continuaAggiornaRisorsa = false;
                                    continuaElimina = false;
                                }
                            }
                            #endregion
                            break;


                        case "indietro":
                            continuaElimina = false;
                            return;
                            break;
                        default:
                            Console.WriteLine("\nINPUT NON VALIDO. RIPROVA.\n");
                            continuaElimina = false;
                            return;
                            break;

                    }
                }
            }
        }

        public void EsportaCSV()
        {
            Console.WriteLine("UN FILE DI NOME EVENTI  DI FORMATO TXT VERRA' CREATO SUL DESKTOP \n CONTERRA' AL SUO INTERNO TUTTI I RECORD CONTENUTI NELLE TABELLE. ");

            using (var ctx = new Esercitazione22MarzoContext())
            {
                ICollection<Evento> eventiCSV = ctx.Eventos.Where(e => e.Deleted != null).ToList();
                ICollection<Partecipante> partecipanteCSV = ctx.Partecipantes.Where(p => p.Deleted != null).ToList();
                ICollection<Risorse> risorseCSV = ctx.Risorses.Where(r => r.Deleted != null).ToList();
                string? appoggio = "";

                foreach (Evento v in eventiCSV)
                    appoggio += v.EsportaCSV();
                appoggio += "\n";
                foreach (Partecipante p in partecipanteCSV)
                    appoggio += p.EsportaCSV();
                appoggio += "\n";
                foreach (Risorse r in risorseCSV)
                    appoggio += r.EsportaCSV();
                appoggio += "\n";

                using (StreamWriter sw = new StreamWriter("C:\\Users\\utente\\Desktop\\eventi.txt"))
                {
                    sw.WriteLine(appoggio);
                }
            }
        }

        #region GET ALL
        private void StampaEventi()
        {
            using (var ctx = new Esercitazione22MarzoContext())
            {
                ICollection<Evento> x = new List<Evento>();
                x = ctx.Eventos.Where(e => e.Deleted == null).ToList();
                foreach (Evento e in x)
                    Console.WriteLine(e.ToString());
            }
        }

        private void StampaPartecipanti()
        {
            using (var ctx = new Esercitazione22MarzoContext())
            {
                ICollection<Partecipante> x = new List<Partecipante>();
                x = ctx.Partecipantes.Where(p => p.Deleted == null).ToList();
                foreach (Partecipante p in x)
                    Console.WriteLine(p.ToString());
            }
        }
        private void StampaRisorse()
        {
            using (var ctx = new Esercitazione22MarzoContext())
            {
                ICollection<Risorse> x = new List<Risorse>();
                x = ctx.Risorses.Where(r => r.Deleted == null).ToList();
                foreach (Risorse r in x)
                    Console.WriteLine(r.ToString());
            }
        }
        #endregion
    }
}