using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Hopital
    {
        private static Hopital instance = null;
        public Queue<Patient> FileAttente { get; private set; }
        public List<Medecin> Medecins { get; private set; }
        public List<Salle> Salles { get; private set; }

        private Hopital()
        {
            FileAttente = new Queue<Patient>();
            Medecins = new List<Medecin>();
            Salles = new List<Salle> { new Salle(1), new Salle(2) };
        }

        public static Hopital Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Hopital();
                }
                return instance;
            }
        }

        public void AjouterMedecin(Medecin medecin)
        {
            Medecins.Add(medecin);
        }

        public void AjouterPatient(Patient patient)
        {
            FileAttente.Enqueue(patient);
        }

        public Patient ProchainPatient()
        {
            return FileAttente.Peek();
        }

        public void RetirerPatient()
        {
            FileAttente.Dequeue();
        }
    }
}
