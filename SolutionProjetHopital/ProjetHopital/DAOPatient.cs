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
        public const string connectionString = @"Data Source = DESKTOP-QRR8BDC\SQLEXPRESS; Initial Catalog = hopital-ajc ; Integrated Security = True";

        public static void CreatePatient(Patient patient)
        {
            string sql = "insert into patients VALUES(@Nom, @Prenom, @Age, @Adresse, @Telephone)";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;

            command.Parameters.AddWithValue("@Nom", patient.Nom);
            command.Parameters.AddWithValue("@Prenom", patient.Prenom);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Adresse", patient.Adresse);
            command.Parameters.AddWithValue("@Telephone", patient.Telephone);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        //public static Patient SelectById(int id)
        //{

        //    SqlConnection connection = new SqlConnection(connectionString);
        //    {
        //        Patient p = null;
        //        SqlCommand command = new SqlCommand("SELECT * FROM patients WHERE id = @id", connection);
        //        command.Parameters.AddWithValue("@id", id);
        //        connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();
        //        {
        //            if (reader.Read())
        //            {
        //                p = new Patient(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5));
        //            }
        //        }
        //        return p;
        //    }
        //}
        public Patient SelectById(int id)
        {
            try
            {
                Patient p = null;
                string sql = "select * from patients where id=" + id;

                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    p = new Patient(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5));
                }
                connection.Close();


                return p;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
