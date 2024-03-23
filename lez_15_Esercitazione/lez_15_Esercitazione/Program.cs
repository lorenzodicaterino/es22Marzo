using lez_15_Esercitazione.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace lez_15_Esercitazione
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Insert

            ////using (var ctx = new Esercitazione22MarzoContext())
            ////{

            ////    Evento ev = new Evento()
            ////    {
            ////        NomeEvento = "vasco",
            ////        DescrizioneEvento = "vasco",
            ////        DataEvento = new DateTime(2012, 12, 20),
            ////        LuogoEvento = "Roma",
            ////        CapacitaMassima = 1000
            ////    };

            ////    //ctx.Eventos.Add(ev);
            ////    //ctx.SaveChanges();

            ////    ICollection<Evento> evv = ctx.Eventos.Where(s=>s.EventoId != 0).ToList();

            ////    List<Evento> eventiPerId = new List<Evento>()
            ////    {

            ////    };

            ////    Partecipante par = new Partecipante()
            ////    {
            ////        Nominativo = "Mario Pace",
            ////        Telefono = "123456",
            ////        Email = "mar@pac.com",
            ////        EventoRifs = evv
            ////    };

            ////    ctx.Partecipantes.Add(par);
            ////    //ctx.Partecipantes.Add(par);
            ////    ctx.SaveChanges();
            ////}

            ////using (var ctx = new Esercitazione22MarzoContext())
            ////{
            ////    Risorse ris = new Risorse()
            ////    {
            ////        Quantita = 12,
            ////        Costo = 12,
            ////        Fornitore = "Giovanni Pace",
            ////        Tipo = "attrezzatura",
            ////        EventoRifNavigation = ctx.Eventos.First()
            ////    };

            ////    //ctx.Risorses.Add(ris);
            ////    //ctx.SaveChanges();


            ////}


            #endregion

            //using (var ctx = new Esercitazione22MarzoContext())
            //{
            //    ICollection<Evento> eventiCSV = ctx.Eventos.Where(e => e.EventoId != 0).ToList();
            //    ICollection<Partecipante> partecipanteCSV = ctx.Partecipantes.Where(e => e.PartecipanteId != 0).ToList();
            //    ICollection<Risorse> risorseCSV = ctx.Risorses.Where(e => e.RisorseId != 0).ToList();
            //    string? appoggio = "";

            //    foreach (Evento v in eventiCSV)
            //        appoggio += v.EsportaCSV();
            //    appoggio += "\n";
            //    foreach (Partecipante p in partecipanteCSV)
            //        appoggio += p.EsportaCSV();
            //    appoggio += "\n";
            //    foreach (Risorse r in risorseCSV)
            //        appoggio += r.EsportaCSV();
            //    appoggio += "\n";

            //    using (StreamWriter sw = new StreamWriter("C:\\Users\\utente\\Desktop\\eventi.txt"))
            //    {
            //        sw.WriteLine(appoggio);
            //    }
            //}

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("DDDDDDDDDDDDD      IIIIIIIIII     OOOOOOOOO             CCCCCCCCCCCCC\r\nD::::::::::::DDD   I::::::::I   OO:::::::::OO        CCC::::::::::::C\r\nD:::::::::::::::DD I::::::::I OO:::::::::::::OO    CC:::::::::::::::C\r\nDDD:::::DDDDD:::::DII::::::IIO:::::::OOO:::::::O  C:::::CCCCCCCC::::C\r\n  D:::::D    D:::::D I::::I  O::::::O   O::::::O C:::::C       CCCCCC\r\n  D:::::D     D:::::DI::::I  O:::::O     O:::::OC:::::C              \r\n  D:::::D     D:::::DI::::I  O:::::O     O:::::OC:::::C              \r\n  D:::::D     D:::::DI::::I  O:::::O     O:::::OC:::::C              \r\n  D:::::D     D:::::DI::::I  O:::::O     O:::::OC:::::C              \r\n  D:::::D     D:::::DI::::I  O:::::O     O:::::OC:::::C              \r\n  D:::::D     D:::::DI::::I  O:::::O     O:::::OC:::::C              \r\n  D:::::D    D:::::D I::::I  O::::::O   O::::::O C:::::C       CCCCCC\r\nDDD:::::DDDDD:::::DII::::::IIO:::::::OOO:::::::O  C:::::CCCCCCCC::::C\r\nD:::::::::::::::DD I::::::::I OO:::::::::::::OO    CC:::::::::::::::C\r\nD::::::::::::DDD   I::::::::I   OO:::::::::OO        CCC::::::::::::C\r\nDDDDDDDDDDDDD      IIIIIIIIII     OOOOOOOOO             CCCCCCCCCCCCC\r\n                                                                     \r\n                                                                     \r\n                                                                     \r\n                                                                     \r\n                                                                     \r\n                                                                     \r\n                                                                     \r\n                                                                               \r\n                                                                               \r\n               AAA               NNNNNNNN        NNNNNNNNEEEEEEEEEEEEEEEEEEEEEE\r\n              A:::A              N:::::::N       N::::::NE::::::::::::::::::::E\r\n             A:::::A             N::::::::N      N::::::NE::::::::::::::::::::E\r\n            A:::::::A            N:::::::::N     N::::::NEE::::::EEEEEEEEE::::E\r\n           A:::::::::A           N::::::::::N    N::::::N  E:::::E       EEEEEE\r\n          A:::::A:::::A          N:::::::::::N   N::::::N  E:::::E             \r\n         A:::::A A:::::A         N:::::::N::::N  N::::::N  E::::::EEEEEEEEEE   \r\n        A:::::A   A:::::A        N::::::N N::::N N::::::N  E:::::::::::::::E   \r\n       A:::::A     A:::::A       N::::::N  N::::N:::::::N  E:::::::::::::::E   \r\n      A:::::AAAAAAAAA:::::A      N::::::N   N:::::::::::N  E::::::EEEEEEEEEE   \r\n     A:::::::::::::::::::::A     N::::::N    N::::::::::N  E:::::E             \r\n    A:::::AAAAAAAAAAAAA:::::A    N::::::N     N:::::::::N  E:::::E       EEEEEE\r\n   A:::::A             A:::::A   N::::::N      N::::::::NEE::::::EEEEEEEE:::::E\r\n  A:::::A               A:::::A  N::::::N       N:::::::NE::::::::::::::::::::E\r\n A:::::A                 A:::::A N::::::N        N::::::NE::::::::::::::::::::E\r\nAAAAAAA                   AAAAAAANNNNNNNN         NNNNNNNEEEEEEEEEEEEEEEEEEEEEE");


            Menu.getIstanza().Interfaccia();
        }
    }
}
