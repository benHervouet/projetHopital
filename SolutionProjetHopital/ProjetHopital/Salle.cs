using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Salle
    {
        private int numero;
        private Medecin medecinAssocie;
        private Patient patientActuel;

        public int Numero { get => numero; set => numero = value; }
        internal Medecin MedecinAssocie { get => medecinAssocie; set => medecinAssocie = value; }
        internal Patient PatientActuel { get => patientActuel; set => patientActuel = value; }

        public Salle(int numero, Medecin medecinAssocie, Patient patientActuel)
        {
            this.Numero = numero;
            this.MedecinAssocie = medecinAssocie;
            this.PatientActuel = patientActuel;
        }

        public Salle(int numero)
        {
            Numero = numero;
        }

        public void AssignerMedecin(Medecin medecin)
        {
            MedecinAssocie = medecin;
        }

        public void AssignerPatient(Patient patient)
        {
            PatientActuel = patient;
        }

        public void LibererSalle()
        {
            PatientActuel = null;
        }
    }
}
