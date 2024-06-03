using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    public class Patient
    {
        private int id;
        private string nom;
        private string prenom;
        private int age;
        private string adresse;
        private string telephone;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public int Age { get => age; set => age = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Telephone { get => telephone; set => telephone = value; }

        public Patient(int id, string nom, string prenom, int age, string adresse, string telephone)
        {
            this.Id = id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Age = age;
            this.Adresse = adresse;
            this.Telephone = telephone;
        }
    }
}
