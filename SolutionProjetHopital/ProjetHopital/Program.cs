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
            DAOPatient daoPatient = new DAOPatient();
            MenuSecretaire();
        }
        static void MenuSecretaire()
        {
            while (true)
            {
            
                Console.WriteLine("----- Interface secrétaire -----");
                Console.WriteLine("1. Rajouter a la file d’attente un patient");
                Console.WriteLine("2. Afficher la file d’attente");
                Console.WriteLine("3. Afficher le prochain patient de la file(sans le retirer)");
                Console.WriteLine("4. Sortir de ce menu et revenir au menu principal");

                int saisieMenu = Convert.ToInt32(Console.ReadLine());
                if (saisieMenu == 4)
                {
                    break;
                }

                switch (saisieMenu)
                {
                    case 1:
                        Console.WriteLine("Entrez le numéro du patient");
                        int saisieId = Convert.ToInt32(Console.ReadLine());
                        Patient patient = new DAOPatient().SelectById(saisieId);
                        if (patient == null) //Si true : Création d'un nouveau patient
                        {
                            Console.WriteLine("Aucun enregistrement trouvé. Création d'un nouveau patient.");
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
                            patient = new Patient(saisieNom, saisiePrenom, saisieAge, saisieAdresse, saisieTel);
                            DAOPatient.CreatePatient(patient);
                        }
                        //Affectation dans liste d'attente
                        Hopital.Instance.AjouterPatient(patient);
                        break;
                    case 2:
                        //Afficher liste d'attente
                        Console.WriteLine("File d'attente");
                        foreach (var p in Hopital.Instance.FileAttente)
                        {
                            Console.WriteLine($" {p.Id} {p.Nom} {p.Prenom} {p.Age} {p.Adresse} {p.Telephone}");
                        }
                        Console.WriteLine("Nombre de patients en file d'attente : " + Hopital.Instance.FileAttente.Count);
                        break;
                    case 3:
                        //Afficher prochain patient de la liste
                        if (Hopital.Instance.FileAttente.Count > 0)
                        {
                            Patient prochainPatient = Hopital.Instance.ProchainPatient();
                            Console.WriteLine(prochainPatient.ToString());
                        }
                        else
                        {
                            Console.WriteLine("La file d'attente est vide.");
                        }
                        break;
                }
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
