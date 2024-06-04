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


        static void MedecinMenu(DAOPatient daoPatient, DAOVisite daoVisite, int salleNumero)
        {
            while (true)
            {
                Console.WriteLine(" ------------- Interface Medecin --------------");
                Console.WriteLine(" 1 : Rendre la salle disponible");
                Console.WriteLine(" 2 : Afficher la file d'attente");
                Console.WriteLine(" 3 : afficher info patient");
                Console.WriteLine(" 4 : Sauvegarder les visites en base");
                Console.WriteLine(" 5 : Quitter");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 5)
                {
                    break;
                }

                Salle salle = Hopital.Instance.Salles.Find(s => s.Numero == salleNumero);

                switch (choice)
                {
                    case 1:
                        salle.LibererSalle();
                        if (Hopital.Instance.FileAttente.Count > 0)
                        {
                            Patient prochainPatient = Hopital.Instance.ProchainPatient();
                            salle.AssignerPatient(prochainPatient);
                            Hopital.Instance.RetirerPatient();
                            Console.WriteLine($"Patient {prochainPatient.Nom} {prochainPatient.Prenom} est maintenant dans la salle {salleNumero}");
                        }
                        else
                        {
                            Console.WriteLine("Pas de patients en attente.");
                        }
                        break;

                    case 2:

                        break;

                    case 3:
                        Console.WriteLine("Info patient");

                        break;

                    case 4:
                        Console.WriteLine("Sauvegarde des visites...");

                        break;
                }
            }
        }
    }
}
