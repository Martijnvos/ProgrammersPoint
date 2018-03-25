using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Interfaces;
using ProgrammersPoint.ViewModels;

namespace ProgrammersPoint.Models.MSSQLContext
{
    public class GebruikerMSSQLContext : IGebruikerContext
    {
        public List<Gebruiker> GetAll()
        {
            List<Gebruiker> gebruikersLijst = new List<Gebruiker>();
            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Gebruiker", connectie);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            gebruikersLijst.Add(CreëerGebruikerVanReader(sqlDataReader));
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

            return gebruikersLijst;
        }

        public Gebruiker GetById(int id)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery = "SELECT * FROM Gebruiker WHERE gebruikerId = @gebruikerId";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter gebruikerIdParamater = new SqlParameter("gebruikerId", SqlDbType.Int)
                    {
                        Value = id
                    };

                    sqlCommand.Parameters.Add(gebruikerIdParamater);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreëerGebruikerVanReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async Task<Gebruiker> GetByNaam(string naam)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery = "SELECT * FROM Gebruiker WHERE gebruikersnaam = @gebruikersnaam";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter gebruikersnaamParamater = new SqlParameter("gebruikersnaam", SqlDbType.NVarChar)
                    {
                        Value = naam
                    };

                    sqlCommand.Parameters.Add(gebruikersnaamParamater);

                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return CreëerGebruikerVanReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async Task<bool> Insert(Gebruiker gebruiker)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                using (SqlCommand cmd = new SqlCommand("GebruikerInsertProcedure", connectie))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Gebruikersnaam", gebruiker.Gebruikersnaam));
                    cmd.Parameters.Add(new SqlParameter("@Wachtwoord", gebruiker.Wachtwoord));
                    cmd.Parameters.Add(new SqlParameter("@Emailadres", gebruiker.Emailadres));

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        return true;
                    }
                    catch (SqlException exp)
                    {
                        throw exp;
                    }
                    catch (SqlTypeException exp)
                    {
                        throw exp;
                    }
                }
            }
        }

        public void Update(Gebruiker gebruiker)
        {
            using (SqlConnection connectie = Database.Connectie)
            {

                string sqlQuery =
                "UPDATE Gebruiker SET gebruikersnaam = @Gebruikersnaam, functie = @Functie WHERE gebruikerId = @gebruikerId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    List<SqlParameter> sqlParameterList = new List<SqlParameter>
                            {
                            new SqlParameter("Gebruikersnaam", SqlDbType.NVarChar) { Value = gebruiker.Gebruikersnaam },
                            new SqlParameter("Functie", SqlDbType.Int) { Value = gebruiker.Functie },
                            new SqlParameter("gebruikerId", SqlDbType.Int) { Value = gebruiker.GebruikerId }
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

        public void Delete(Gebruiker gebruiker)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                "DELETE FROM Gebruiker WHERE gebruikerId = @gebruikerId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter gebruikerIdParameter = new SqlParameter("gebruikerId", SqlDbType.Int)
                    {
                        Value = gebruiker.GebruikerId
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

        public Gebruiker GetByNaamNonAsyncTest(string naam)
        {
            throw new NotImplementedException();
        }

        public bool InsertNonAsyncTest(Gebruiker gebruiker)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hulpfunctie om een gebruikersinstantie van een DataReader te construeren.
        /// Verwacht dat read() al aangeroepen is.
        /// </summary>
        /// <param name="reader">De DataReader om uit te lezen.</param>
        /// <returns>Een nieuwe gebruikerinstantie gebaseerd op de uitgelezen data</returns>
        private Gebruiker CreëerGebruikerVanReader(SqlDataReader reader)
        {
            return new Gebruiker(
                Convert.ToInt32(reader["gebruikerId"]),
                Convert.ToInt32(reader["postcount"]),
                Convert.ToString(reader["gebruikersnaam"]),
                Convert.ToString(reader["wachtwoord"]),
                Convert.ToString(reader["badge"]),
                Convert.ToString(reader["badgetekst"]),
                Convert.ToInt32(reader["karma"]),
                Convert.ToString(reader["emailadres"]),
                (Functie) reader["functie"]);
        }
    }
}
