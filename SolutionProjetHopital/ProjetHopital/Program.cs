using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Program
    {
        static void Main(string[] args)
        {
            //secrétaire
            MenuSecretaire();
        }
        static void MenuSecretaire()
        {
            Console.WriteLine("----- Interface secrétaire -----");
            Console.WriteLine("1. Rajouter a la file d’attente un patient");
            Console.WriteLine("2. Afficher la file d’attente");
            Console.WriteLine("3. Afficher le prochain patient de la file(sans le retirer)");
            Console.WriteLine("4. Sortir de ce menu et revenir au menu principal");

            int saisieMenu = Convert.ToInt32(Console.ReadLine());
            switch (saisieMenu)
            {
                case 1:
                    Console.WriteLine("Entrez le numéro du patient");
                    int saisieId = Convert.ToInt32(Console.ReadLine());
                    Patient patient = null;
                    patient = DAOPatient.SelectById(saisieId);
                    if (patient == null)
                    {
                        Console.WriteLine("Entrez le nom du patient");
                        string saisieNom = Console.ReadLine();
                        Console.WriteLine("Entrez le prenom du patient");
                        string saisiePrenom = Console.ReadLine();
                        Console.WriteLine("Entrez l'age du patient");
                        int saisieAge = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Entrez l'adresse du patient");
                        string saisieAdresse = Console.ReadLine();
                        Console.WriteLine("Entrez le telephone du patient");
                        string saisieTel = Console.ReadLine();
                        DAOPatient.CreatePatient(new Patient(saisieNom, saisiePrenom, saisieAge, saisieAdresse, saisieTel));
                    }
                    //Affectation dans liste d'attente
                    break;
                case 2:
                    //Afficher liste d'attente
                    break;
                case 3:
                    //Afficher prochain patient de la liste
                    break;
                case 4:
                    //Quitter le menu
                    break;
            }

        }
    }
}
