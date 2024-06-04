﻿using System;
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
            string connectionString = @"Data Source = DESKTOP-37D0GD8\SQLEXPRESS; Initial Catalog = bdd_hopital ; Integrated Security = True";
            db database = new db(connectionString);

            DAOPatient daoPatient = new DAOPatient(database);
            DAOVisite daoVisite = new DAOVisite(database);
            DAOAuthentification daoAuthentification = new DAOAuthentification(database);

            Console.WriteLine("Bienvenue à l'hôpital!");

            while (true)
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Quitter");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 2)
                {
                    break;
                }

                Console.WriteLine("Entrez votre nom d'utilisateur:");
                string username = Console.ReadLine();
                Console.WriteLine("Entrez votre mot de passe:");
                string password = Console.ReadLine();

                var loginResult = daoAuthentification.Login(username, password);
                if (loginResult == null)
                {
                    Console.WriteLine("Identifiants incorrects");
                    continue;
                }

                string role = loginResult.Value.Role;
                int? salle = loginResult.Value.Salle;

                if (role == "secretaire")
                {
                    
                }
                else if (role == "medecin")
                {
                    MedecinMenu(daoPatient, daoVisite, salle.Value);
                }
            }
        }

        static void MedecinMenu(DAOPatient daoPatient, DAOVisite daoVisite, int salleNumero)
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
                            Visite visite = new Visite(prochainPatient.Id, salle.MedecinAssocie.Nom, 23, DateTime.Now, salleNumero);
                            visites.Add(visite);
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
                            daoVisite.Create(visite);
                        }
                        visites.Clear();
                        Console.WriteLine("Les visites sont sauvegardées.");
                        break;
                }

            }
        }
    }
}
