using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetHopital
{
    class Authentification
    {
        private string login;
        private string password;
        private string nom;
        private int metier;

        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public string Nom { get => nom; set => nom = value; }
        public int Metier { get => metier; set => metier = value; }

        public Authentification(string login, string password, string nom, int metier)
        {
            this.Login = login;
            this.Password = password;
            this.Nom = nom;
            this.Metier = metier;
        }
    }
}
