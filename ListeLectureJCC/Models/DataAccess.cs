using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ListeLectureJCC.Models
{
    public class DataAccess
    {

        public static DetailModel ChargerDetailDepuisBDD(int idLivre)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=.\SQLExpress;Database=ListeLecture;Integrated Security=true"))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(
                    "SELECT Titre, Auteur, Note, DateDebutLecture, DateFinLecture FROM Livre WHERE ID = @idLivre", connection);
                command.Parameters.AddWithValue("@idLivre", idLivre);

                SqlDataReader reader = command.ExecuteReader();

                //On avance sur la première ligne
                reader.Read();

                string titre = (string)reader["Titre"];
                string auteur = (string)reader["Auteur"];

                short? note;
                if (reader.IsDBNull(2))
                {
                    note = null;
                }
                else
                {
                    note = (byte)reader["Note"];
                }

                DateTime dateDebutLecture = (DateTime)reader["DateDebutLecture"];

                DateTime? dateFinLecture;
                if (reader.IsDBNull(4))
                {
                    dateFinLecture = null;
                }
                else
                {
                    dateFinLecture = (DateTime)reader["DateFinLecture"];
                }

                DetailModel model = new DetailModel(titre, auteur, dateDebutLecture, dateFinLecture, note);
                return model;
            }
        }
            


    }
}