using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Medecin
    {
        private string nom;
        private int salle;

        public string Nom { get => nom; set => nom = value; }
        public int Salle { get => salle; set => salle = value; }

        public Medecin(string nom, int salle)
        {
            this.Nom = nom;
            this.Salle = salle;
        }
    }
}
