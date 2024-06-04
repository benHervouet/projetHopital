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
        public const string connectionString = @"Data Source = DESKTOP-7L8P9AQ\SQLEXPRESS; Initial Catalog = ajc ; Integrated Security = True";

        public static void CreatePatient(Patient patient)
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

        public static Patient SelectById(int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Patient p = null;
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM patient WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        p = new Patient(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5));
                    }
                }
                return p;
            }
        }

    }
}
