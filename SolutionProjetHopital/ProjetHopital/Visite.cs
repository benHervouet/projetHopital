using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Visite
    {
        private int id;
        private int patientid;
        private string nomMedecin;
        private decimal tarif;
        private DateTime dateVisite;
        private int numeroSalle;

        public int Id { get => id; set => id = value; }
        public int Patientid { get => patientid; set => patientid = value; }
        public string NomMedecin { get => nomMedecin; set => nomMedecin = value; }
        public decimal Tarif { get => tarif; set => tarif = value; }
        public DateTime DateVisite { get => dateVisite; set => dateVisite = value; }
        public int NumeroSalle { get => numeroSalle; set => numeroSalle = value; }

        public Visite(int id, int patientid, string nomMedecin, decimal tarif, DateTime dateVisite, int numeroSalle)
        {
            this.Id = id;
            this.Patientid = patientid;
            this.NomMedecin = nomMedecin;
            this.Tarif = tarif;
            this.DateVisite = dateVisite;
            this.NumeroSalle = numeroSalle;
        }
    }
}
