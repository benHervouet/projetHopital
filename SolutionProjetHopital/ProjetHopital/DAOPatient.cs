using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetHopital
{
    public class DAOPatient
    {
        public const string connectionString = @"Data Source = DESKTOP-37D0GD8\SQLEXPRESS; Initial Catalog = bdd_hopital ; Integrated Security = True";
        private db database;

        public DAOPatient(db database)
        {
            this.database = database;
        }

        public void CreatePatient(Patient patient)
        {
            string sql = "insert into patient VALUES(@Nom, @Prenom, @Age, @Adresse, @Telephone)";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;

            command.Parameters.AddWithValue("@Nom", patient.Nom);
            command.Parameters.AddWithValue("@Prenom", patient.Prenom);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Adresse", patient.Adresse);
            command.Parameters.AddWithValue("@Tel", patient.Telephone);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
}
