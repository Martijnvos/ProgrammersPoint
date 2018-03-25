using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using ProgrammersPoint.Enums;
using ProgrammersPoint.Interfaces;

namespace ProgrammersPoint.Models.MSSQLContext
{
    public class ScheldwoordMSSQLContext : IScheldwoordContext
    {
        public async Task<List<Scheldwoord>> GetAll()
        {
            List<Scheldwoord> scheldwoordLijst = new List<Scheldwoord>();
            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Scheldwoord", connectie);
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (sqlDataReader.Read())
                        {
                            scheldwoordLijst.Add(CreëerScheldwoordVanReader(sqlDataReader));
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

            return scheldwoordLijst;
        }

        public async Task<Scheldwoord> GetScheldwoordById(int id)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery = "SELECT * FROM Scheldwoord WHERE scheldwoordId = @scheldwoordId";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter scheldwoordIdParamater = new SqlParameter("scheldwoordId", SqlDbType.Int)
                    {
                        Value = id
                    };

                    sqlCommand.Parameters.Add(scheldwoordIdParamater);

                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return CreëerScheldwoordVanReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async Task<Scheldwoord> GetScheldwoordByNaam(string naam)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery = "SELECT * FROM Scheldwoord WHERE woord = @scheldwoord";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter scheldwoordParamater = new SqlParameter("scheldwoord", SqlDbType.NVarChar)
                    {
                        Value = naam
                    };

                    sqlCommand.Parameters.Add(scheldwoordParamater);

                    using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return CreëerScheldwoordVanReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async void InsertScheldwoord(Scheldwoord scheldwoord)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string query = "INSERT INTO Scheldwoord (woord) VALUES (@Scheldwoord)";

                using (SqlCommand cmd = new SqlCommand(query, connectie))
                {
                    cmd.Parameters.Add(new SqlParameter("@Scheldwoord", scheldwoord.Woord));

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException exp)
                    {
                        throw exp;
                    }
                }
            }
        }

        public void UpdateScheldwoord(Scheldwoord scheldwoord)
        {
            using (SqlConnection connectie = Database.Connectie)
            {

                string sqlQuery =
                    "UPDATE Scheldwoord SET woord = @Scheldwoord WHERE scheldwoordId = @ScheldwoordId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    List<SqlParameter> sqlParameterList = new List<SqlParameter>
                    {
                        new SqlParameter("Scheldwoord", SqlDbType.NVarChar) { Value = scheldwoord.Woord },
                        new SqlParameter("ScheldwoordId", SqlDbType.Int) { Value = scheldwoord.ScheldwoordId }
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

        public void DeleteScheldwoord(string scheldwoord)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                    "DELETE FROM Scheldwoord WHERE woord = @Scheldwoord";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter scheldwoordParameter = new SqlParameter("Scheldwoord", SqlDbType.NVarChar)
                    {
                        Value = scheldwoord
                    };

                    sqlCommand.Parameters.Add(scheldwoordParameter);

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
        /// Hulpfunctie om een scheldwoordinstantie van een DataReader te construeren.
        /// Verwacht dat read() al aangeroepen is.
        /// </summary>
        /// <param name="reader">De DataReader om uit te lezen.</param>
        /// <returns>Een nieuwe scheldwoordinstantie gebaseerd op de uitgelezen data</returns>
        private Scheldwoord CreëerScheldwoordVanReader(SqlDataReader reader)
        {
            return new Scheldwoord(
                Convert.ToInt32(reader["scheldwoordId"]),
                Convert.ToString(reader["woord"]));
        }
    }
}
