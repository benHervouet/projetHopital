using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetHopital
{
    class DAOVisite
    {
        public const string connectionString = @"Data Source = DESKTOP-QRR8BDC\SQLEXPRESS; Initial Catalog = hopital-ajc ; Integrated Security = True";

        public void InsertList(List<Visite> visites)
        {
            foreach(Visite visite in visites)
            {
                Create(visite);
            }
        }

        public void Create(Visite visite)
        {
            string sql = "insert into visites VALUES (@idpatient, @date, @medecin, [num-salle], @tarif )";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add("idpatient", SqlDbType.Int).Value = visite.Patientid;
            command.Parameters.Add("date", SqlDbType.DateTime).Value = visite.DateVisite;
            command.Parameters.Add("medecin", SqlDbType.VarChar).Value = visite.NomMedecin;
            command.Parameters.Add("@numsalle", SqlDbType.Int).Value = visite.NumeroSalle;
            command.Parameters.Add("tarif", SqlDbType.Decimal).Value = visite.Tarif;

            command.CommandText = command.CommandText.Replace("[num-salle]", "@numsalle");

            connection.Open();
            // execution de la requete
            command.ExecuteNonQuery();

            connection.Close();
        }

    }
}