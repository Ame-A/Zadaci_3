using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Zadaci_3
{
    class Program
    {
        static void Main()
        {
            do
            {
                Meni();
                char unos_korisnika = Console.ReadKey().KeyChar;
                if (Imenik.Osobe.Count == 0 && unos_korisnika != '1')
                {
                    if (unos_korisnika == '0')
                    {
                        Environment.Exit(0);
                    }
                    Console.WriteLine("\nTrenutno nemate ni jedan kontakt.");
                    Main();
                }

                switch (unos_korisnika)
                {
                    case '1':
                        Dodaj();
                        break;
                    case '2':
                        Prikaz();
                        break;
                    case '3':
                        Brisanje();
                        break;
                    case '4':
                        Izmena();
                        break;
                    default:
                        Console.WriteLine("\nGreska u unosu!");
                        break;
                }

            } while (true);
        }

        static void Meni()
        {
            Console.Write("\n1) Unos novog kontakta \n2) Prikaz kontakata \n3) Brisanje kontakata \n4) Izmena kontakta \n0) Izlaz \nUnos > ");
        }

        static void Dodaj()
        {
            string[] Imeiprezime = { };
            while (Imeiprezime.Length != 3 || Imenik.Provera(Imeiprezime[2], out _) == true)
            {
                Console.WriteLine("\nIme\tPrezime\t  Broj");
                Imeiprezime = Console.ReadLine().Split(' ');
                if (Imeiprezime.Length != 3)
                {
                    Console.WriteLine("Unos treba da bude u formatu \"Ime Prezime Broj\" bez dodatnog space na kraju broja!");
                }
                else if (Imenik.Provera(Imeiprezime[2], out Imenik IPB) == true)
                {
                    Console.WriteLine($"Greska vec postojeci broj! > {IPB.ime} {IPB.prezime} {IPB.broj} <");
                }
            }
            new Imenik(Imeiprezime[0], Imeiprezime[1], ProveraBroja(Imeiprezime[2]));
        }

        static void Prikaz()
        {
            Console.WriteLine("\n\tIme\tPrezime\t  Broj");
            for (int i = 0; i < Imenik.Osobe.Count; i++)
            {
                Console.WriteLine($"{i + 1}.\t{Imenik.Osobe[i].ime}\t{Imenik.Osobe[i].prezime}\t  {Imenik.Osobe[i].broj}");
            } 
        }

        static void Brisanje()
        {
            Prikaz();
            Console.Write("\nOdaberite kontakt koji zelite da obrisete > ");
            proveraUnosa(out int obrisati);
            Imenik.Osobe.RemoveAt(obrisati - 1);
        }
        static void Izmena() 
        {
            Prikaz();
            Console.Write("Odaberite kontakt koji zelite da izmenite > ");
            proveraUnosa(out int promeniti);           
            string promena = "";
            char unos = ' ';
            while (promena != "0")
            {
                Console.WriteLine($"{Imenik.Osobe[promeniti - 1].ime}  {Imenik.Osobe[promeniti - 1].prezime} {Imenik.Osobe[promeniti - 1].broj}");
                Console.Write("Izmena imena 1, izmena prezimena 2, izmena broja 3, izlaz 0 > ");
                unos = Console.ReadKey().KeyChar;
                switch (unos)
                {
                    case '1':
                        Console.Write("\nNovo ime > ");
                        string novo_ime = Console.ReadLine();
                        Imenik.Osobe[promeniti - 1].ime = novo_ime;
                        break;
                    case '2':
                        Console.Write("\nNovo prezime > ");
                        string novo_prezime = Console.ReadLine();
                        Imenik.Osobe[promeniti - 1].prezime = novo_prezime;
                        break;
                    case '3':
                        Console.Write("\nNovi broj > ");
                        string novi_broj = Console.ReadLine();
                        if (Imenik.Provera(novi_broj, out _)  ==  true)
                        {
                            Console.WriteLine("Vec postojeci broj!");
                        }
                        else
                        {
                            Imenik.Osobe[promeniti - 1].broj = ProveraBroja(novi_broj);
                        }

                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("\nGreska u unosu !");
                        break;
                }    
            }
        }

        static void proveraUnosa(out int unos) //Metoda proverava da korisnik nije uneo pogresne vrednosti puput stringa, 
        {                                      //i proverava velicinu broja da nije jednaka nuli ili da broj nije veci od ponudjenih opcija.
            bool provera = int.TryParse(Console.ReadLine(), out unos);
            
            while (provera == false || unos > Imenik.Osobe.Count || unos <= 0)
            {
                if (provera == false)
                {
                    Console.Write("\nGreska! Treba da unesete broj! > ");
                    provera = int.TryParse(Console.ReadLine(), out unos);
                }
                else if (unos > Imenik.Osobe.Count || unos <= 0)
                {
                    Console.Write("\nGreska ne postojeci kontakt! > ");
                    provera = int.TryParse(Console.ReadLine(), out unos);
                }

            }
        }

        static string ProveraBroja(string unos) //Ova metoda proverava da li je korisnik uneo u polju koje je namenjeno za broj
        {                                      //numericku vrednost. 
            bool check = Regex.IsMatch(unos, @"^[0-9]+$");
            while (check == false)
            {
                Console.Write("\nU ovom polju samo idu numericke vrednosit! > ");
                unos = Console.ReadLine();
                check = Regex.IsMatch(unos, @"^[0-9]+$");

            }
            return unos;
        }

        class Imenik
        {
            static public List<Imenik> Osobe = new List<Imenik>();
            public string ime, prezime, broj;

            public Imenik(string i, string p, string b)
            {
                ime = i;
                prezime = p;
                broj = b;
                Osobe.Add(this);
            }
            public Imenik() { Osobe.Add(this); }
            static public bool Provera(string broj, out Imenik br)
            {
                foreach (Imenik i in Osobe)
                {
                    if (i.broj == broj)
                    {
                        br = i;
                        return true;
                    }
                }
                br = null;
                return false;

            }

        }
    }
}
