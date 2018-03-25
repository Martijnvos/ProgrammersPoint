using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Interfaces;

namespace ProgrammersPoint.Models.MSSQLContext
{
    public class CategorieMSSQLContext : ICategorieContext
    {
        public async Task<List<Categorie>> GetAll()
        {
            List<Categorie> categorieLijst = new List<Categorie>();
            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Categorie", connectie);
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (sqlDataReader.Read())
                        {
                            categorieLijst.Add(CreëerCategorieVanReader(sqlDataReader));
                        }
                    }

                }
                catch (SqlException exception)
                {
                    //TODO handle error
                    string error = exception.ToString();
                    return null;
                }
            }

            return categorieLijst;
        }

        public Categorie GetByNaam(string naam)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery = "SELECT * FROM Categorie WHERE naam = @Naam";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter naamParamater = new SqlParameter("Naam", SqlDbType.NVarChar)
                    {
                        Value = naam
                    };

                    sqlCommand.Parameters.Add(naamParamater);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreëerCategorieVanReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void Insert(Categorie categorie)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                    "INSERT INTO Categorie (categorie, naam, omschrijving) VALUES (@Categorie, @Naam, @Omschrijving)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, connectie))
                {
                    cmd.Parameters.Add(new SqlParameter("@Categorie", categorie.CategorieWaarde));
                    cmd.Parameters.Add(new SqlParameter("@Naam", categorie.Naam));
                    cmd.Parameters.Add(new SqlParameter("@Omschrijving", categorie.Omschrijving));

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException exp)
                    {
                        throw exp;
                    }
                }
            }
        }

        public void Update(Categorie categorie)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                    "UPDATE Categorie SET omschrijving = @Omschrijving WHERE naam = @Naam";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    List<SqlParameter> sqlParameterList = new List<SqlParameter>
                    {
                        new SqlParameter("Omschrijving", SqlDbType.NVarChar) { Value = categorie.Omschrijving },
                        new SqlParameter("Naam", SqlDbType.NVarChar) { Value = categorie.Naam }
                    };
                    sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException exp)
                    {
                        throw exp;
                    }
                }
            }
        }

        public void Delete(Categorie categorie)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                    "DELETE FROM Categorie WHERE categorie = @Categorie";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter gebruikerIdParameter = new SqlParameter("Categorie", SqlDbType.Int)
                    {
                        Value = categorie.CategorieWaarde
                    };

                    sqlCommand.Parameters.Add(gebruikerIdParameter);

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException exp)
                    {
                        throw exp;
                    }
                }
            }
        }

        /// <summary>
        /// Hulpfunctie om een categorie-instantie van een DataReader te construeren.
        /// Verwacht dat read() al aangeroepen is.
        /// </summary>
        /// <param name="reader">De DataReader om uit te lezen.</param>
        /// <returns>Een nieuwe categorie-instantie gebaseerd op de uitgelezen data</returns>
        private Categorie CreëerCategorieVanReader(SqlDataReader reader)
        {
            return new Categorie(
                (Categorieën) reader["categorie"],
                Convert.ToString(reader["naam"]),
                Convert.ToString(reader["omschrijving"]));
        }
    }
}
