using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ProgrammersPoint.Interfaces;

namespace ProgrammersPoint.Models.MSSQLContext
{
    public class ReactieMSSQLContext : IReactieContext
    {
        public List<Reactie> GetAll()
        {
            List<Reactie> reactieLijst = new List<Reactie>();
            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Reactie", connectie);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            reactieLijst.Add(CreëerReactieVanReader(sqlDataReader));
                        }
                    }

                }
                catch (SqlException exception)
                {
                    //TODO throw error
                    string error = exception.ToString();
                    return null;
                }
            }

            return reactieLijst;
        }

        public async Task<List<Reactie>> GetAllByReview(Review review)
        {
            List<Reactie> reactieLijst = new List<Reactie>();
            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Reactie WHERE reviewId = @ReviewId", connectie);

                    List<SqlParameter> sqlParameterList = new List<SqlParameter>
                    {
                        new SqlParameter("ReviewID", SqlDbType.Int) { Value = review.ReviewId }
                    };
                    sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (sqlDataReader.Read())
                        {
                            reactieLijst.Add(CreëerReactieVanReader(sqlDataReader));
                        }
                    }

                }
                catch (SqlException exception)
                {
                    //TODO throw error
                    string error = exception.ToString();
                    return null;
                }
            }

            return reactieLijst;
        }

        public Reactie GetById(int id)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    string sqlQuery = "SELECT * FROM Reactie WHERE reactieId = @Id";
                    using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@Id", id));

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return CreëerReactieVanReader(reader);
                            }
                        }
                    }
                }
                catch (SqlException exception)
                {
                    //TODO throw error
                    string error = exception.ToString();
                    return null;
                }
            }

            return null;
        }

        public void Insert(Reactie reactie)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery;

                if (reactie.ReactieOpReactieId != null)
                {
                    sqlQuery =
                        "INSERT INTO Reactie (reactieOpReactieId, gebruikerId, reviewId, inhoud) " +
                        "VALUES (@ReactieOpReactieId, @GebruikerId, @ReviewId, @Inhoud)";
                }
                else
                {
                    sqlQuery =
                        "INSERT INTO Reactie (gebruikerId, reviewId, inhoud) " +
                        "VALUES (@GebruikerId, @ReviewId, @Inhoud)";
                }
                

                using (SqlCommand cmd = new SqlCommand(sqlQuery, connectie))
                {
                    if (reactie.ReactieOpReactieId != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@ReactieOpReactieId", reactie.ReactieOpReactieId));
                    }
                    cmd.Parameters.Add(new SqlParameter("@GebruikerId", reactie.GebruikerId));
                    cmd.Parameters.Add(new SqlParameter("@ReviewId", reactie.ReviewId));
                    cmd.Parameters.Add(new SqlParameter("@Inhoud", reactie.Inhoud));

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

        public void Update(Reactie reactie)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                    "UPDATE Reactie SET inhoud = @Inhoud WHERE reactieId = @ReactieId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("Inhoud", reactie.Inhoud));
                    sqlCommand.Parameters.Add(new SqlParameter("ReactieId", reactie.ReactieId));

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

        public void Delete(Reactie reactie)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                    "DELETE FROM Reactie WHERE reactieId = @ReactieId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("ReactieId", reactie.ReactieId));

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
        /// Hulpfunctie om een reacite-instantie van een DataReader te construeren.
        /// Verwacht dat read() al aangeroepen is.
        /// </summary>
        /// <param name="reader">De DataReader om uit te lezen.</param>
        /// <returns>Een nieuwe reactie-instantie gebaseerd op de uitgelezen data</returns>
        private Reactie CreëerReactieVanReader(SqlDataReader reader)
        {
            return new Reactie(
                Convert.ToInt32(reader["reactieId"]),
                (reader["reactieOpReactieId"] == DBNull.Value) ? (int?) null : Convert.ToInt32(reader["reactieOpReactieId"]),
                Convert.ToInt32(reader["gebruikerId"]),
                Convert.ToInt32(reader["reviewId"]),
                Convert.ToString(reader["inhoud"]),
                Convert.ToInt32(reader["aantal reports"]),
                Convert.ToDateTime(reader["datum"]));
        }
    }
}
