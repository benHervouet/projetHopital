using System;
using System.Collections.Generic;

namespace ProjetHopital
{
    class Program
    {
        static void Main(string[] args)
        {
            //Redirection Utilisateur

            DAOAuthentification daoAuth = new DAOAuthentification();

            Console.WriteLine("Veuillez vous identifier:");
            while (true)
            {
                Console.WriteLine("Login");
                string login = Console.ReadLine();

                Console.WriteLine("Password");
                string password = Console.ReadLine();

                var user = daoAuth.Login(login, password);

                if (user.HasValue)
                {
                    var (role, salle, nom) = user.Value;
                    Console.WriteLine($"Interface {role}");

                    if (role == "secretaire")
                    {
                        MenuSecretaire();
                    }

                    else if (role == "medecin")
                    {
                        MedecinMenu(salle.Value, nom);
                    }
                }

                else
                {
                    Console.WriteLine("Login ou mdp inncorect");
                }
            }
        }
        static void MenuSecretaire()
        {
            while (true)
            {
            
                Console.WriteLine("----- Interface secrétaire -----");
                Console.WriteLine("0. Sortir de ce menu et revenir au menu principal");
                Console.WriteLine("1. Rajouter a la file d’attente un patient");
                Console.WriteLine("2. Afficher la file d’attente");
                Console.WriteLine("3. Afficher le prochain patient de la file(sans le retirer)");
                Console.WriteLine("4. Modifier une fiche patient");
                Console.WriteLine("5. Consulter l'historique des visites d'un patient");

                int saisieMenu = Convert.ToInt32(Console.ReadLine());
                if (saisieMenu == 0)
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
                    case 4:
                        Console.WriteLine("Entrez le numéro du patient à modifier");
                        int saisieIdc4 = Convert.ToInt32(Console.ReadLine());
                        Patient patientC4 = new DAOPatient().SelectById(saisieIdc4);
                        if (patientC4 != null) //Si true : Modification du patient
                        {
                            Console.WriteLine($"Patient {patientC4.Id} trouvé. Modification des informations");

                            // Nom
                            Console.WriteLine("Le nom du patient est : " + patientC4.Nom);
                            Console.Write("Modifiez le nom du patient ou appuyez sur 'entrée' pour conserver l'actuel : ");
                            string response = Console.ReadLine();
                            patientC4.Nom = string.IsNullOrEmpty(response) ? patientC4.Nom : response;

                            // Prénom
                            Console.WriteLine("Le prénom du patient est : " + patientC4.Prenom);
                            Console.Write("Modifiez le prénom du patient ou appuyez sur 'entrée' pour conserver l'actuel : ");
                            string prenomResponse = Console.ReadLine();
                            patientC4.Prenom = string.IsNullOrEmpty(prenomResponse) ? patientC4.Prenom : prenomResponse;

                            // Âge
                            Console.WriteLine("L'âge du patient est : " + patientC4.Age);
                            Console.Write("Modifiez l'âge du patient ou appuyez sur 'entrée' pour conserver l'actuel : ");
                            string ageResponse = Console.ReadLine();
                            int newAge = 0;

                            if (!string.IsNullOrEmpty(ageResponse) && int.TryParse(ageResponse, out newAge))
                            {
                                patientC4.Age = newAge;
                            }

                            // Adresse
                            Console.WriteLine("L'adresse du patient est : " + patientC4.Adresse);
                            Console.Write("Modifiez l'adresse du patient ou appuyez sur 'entrée' pour conserver l'actuel : ");
                            string adresseResponse = Console.ReadLine();
                            patientC4.Adresse = string.IsNullOrEmpty(adresseResponse) ? patientC4.Adresse : adresseResponse;

                            // Téléphone
                            Console.WriteLine("Le numéro de téléphone du patient est : " + patientC4.Telephone);
                            Console.Write("Modifiez le numéro de téléphone du patient ou appuyez sur 'entrée' pour conserver l'actuel : ");
                            string telResponse = Console.ReadLine();
                            patientC4.Telephone = string.IsNullOrEmpty(telResponse) ? patientC4.Telephone : telResponse;


                            DAOPatient.Update(patientC4);
                        }
                        else
                        {
                            Console.WriteLine("Patient non-trouvé");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Entrez le numéro du patient dont vous voulez consulter les visites");
                        int saisieIdc5 = Convert.ToInt32(Console.ReadLine());

                        foreach (Visite v in new DAOVisite().SelectAllByIdPatient(saisieIdc5))
                            Console.WriteLine(v.ToString());
                        break;
                }
            }
        }

        static void MedecinMenu(int salleNumero, string nom)
        {
            List<Visite> visites = new List<Visite>();
            while (true)
            {
                Console.WriteLine(" ------------- Interface Medecin --------------");
                Console.WriteLine(" 1 : Rendre la salle disponible");
                Console.WriteLine(" 2 : Afficher la file d'attente");
                Console.WriteLine(" 3 : Sauvegarder les visites en base");
                Console.WriteLine(" 4 : Quitter");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 4)
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
                            Console.WriteLine($"Patient {prochainPatient.Id} {prochainPatient.Nom} {prochainPatient.Prenom} {prochainPatient.Age} {prochainPatient.Adresse} {prochainPatient.Telephone} est maintenant dans la salle {salleNumero}");

                            // Ajouter une nouvelle visite à la liste des visites

                            Visite visite = new Visite(prochainPatient.Id, nom, 23, DateTime.Now, salleNumero);
                            visites.Add(visite);
                            if (visites.Count==5)
                            {
                                foreach (var uneVisite in visites)
                                {
                                    new DAOVisite().Create(uneVisite);
                                }
                                visites.Clear();
                                Console.WriteLine("Les visites sont sauvegardées.");

                            }
                        }
                        else
                        {
                            Console.WriteLine("Pas de patients en attente.");
                        }
                        break;

                    case 2:
                        Console.WriteLine("File d'attente");
                        foreach (var p in Hopital.Instance.FileAttente)
                        {
                            Console.WriteLine($" {p.Id} {p.Nom} {p.Prenom} {p.Age} {p.Adresse} {p.Telephone}");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Sauvegarde des visites.");
                        foreach (var visite in visites)
                        {
                            new DAOVisite().Create(visite);
                        }
                        visites.Clear();
                        Console.WriteLine("Les visites sont sauvegardées.");
                        break;
                }

            }
        }
    }
}
