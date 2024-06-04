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
