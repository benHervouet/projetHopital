using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetHopital
{
    class DAOAuthentification 
    {
        public const string connectionString = @"Data Source = DESKTOP-37D0GD8\SQLEXPRESS; Initial Catalog = bdd_hopital ; Integrated Security = True";
        private db database;

        public DAOAuthentification(db database)
        {
            this.database = database;
        }

        public (string Role, int? Salle)? Login(string login, string password)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT nom, metier FROM authentification WHERE login = @login AND passowrd = @Password", connection);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string role = reader.GetInt32(1) == 0 ? "secretaire" : "medecin";
                        int? salle = reader.GetInt32(1) == 1 ? 1 : reader.GetInt32(1) == 2 ? 2 : (int?)null;
                        return (role, salle);
                    }
                }

            }
            return null;


        }
    }
}
