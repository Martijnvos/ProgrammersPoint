using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ProgrammersPoint.Interfaces;

namespace ProgrammersPoint.Models.MSSQLContext
{
    public class ReviewWaarderingMSSQLContext : IReviewWaarderingContext
    {
        public async Task<List<ReviewWaardering>> GetAll()
        {
            List<ReviewWaardering> reviewWaarderingLijst = new List<ReviewWaardering>();

            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ReviewWaardering", connectie);
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (sqlDataReader.Read())
                        {
                            reviewWaarderingLijst.Add(CreëerReviewWaarderingVanReader(sqlDataReader));
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

            return reviewWaarderingLijst;
        }

        public ReviewWaardering GetReviewWaarderingById(int id)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery = "SELECT * FROM ReviewWaardering WHERE reviewWaarderingId = @reviewWaarderingId";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter reviewWaarderingIdParameter = new SqlParameter("reviewWaarderingId", SqlDbType.Int)
                    {
                        Value = id
                    };

                    sqlCommand.Parameters.Add(reviewWaarderingIdParameter);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreëerReviewWaarderingVanReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void InsertOrUpdateReviewWaardering(ReviewWaardering reviewWaardering)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                using (SqlCommand sqlCommand = new SqlCommand("ReviewWaarderingInsertOrUpdate", connectie))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    List<SqlParameter> sqlParameterList = new List<SqlParameter>
                    {
                        new SqlParameter("gebruikerId", SqlDbType.Int) { Value = reviewWaardering.GebruikerId },
                        new SqlParameter("reviewId", SqlDbType.Int) { Value = reviewWaardering.ReviewId },
                        new SqlParameter("upvote", SqlDbType.Bit) { Value = reviewWaardering.Upvote },
                        new SqlParameter("downvote", SqlDbType.Bit) { Value = reviewWaardering.Downvote },
                        new SqlParameter("report", SqlDbType.Bit) { Value = reviewWaardering.Report }
                    };
                    sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

                    try
                    {
                        int rowsAangepast = sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException exp)
                    {
                        throw exp;
                    }
                }
            }
        }

        public void DeleteReviewWaardering(ReviewWaardering reviewWaardering)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                    "DELETE FROM ReviewWaardering WHERE reviewWaarderingId = @reviewWaarderingId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter reviewWaarderingIdParameter = new SqlParameter("reviewWaarderingId", SqlDbType.Int)
                    {
                        Value = reviewWaardering.ReviewId
                    };

                    sqlCommand.Parameters.Add(reviewWaarderingIdParameter);

                    int rowsAangepast = sqlCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Hulpfunctie om een reviewwaarderinginstantie van een DataReader te construeren.
        /// Verwacht dat read() al aangeroepen is.
        /// </summary>
        /// <param name="reader">De DataReader om uit te lezen.</param>
        /// <returns>Een nieuwe reviewwaarderinginstantie gebaseerd op de uitgelezen data</returns>
        private ReviewWaardering CreëerReviewWaarderingVanReader(SqlDataReader reader)
        {
            return new ReviewWaardering(
                Convert.ToInt32(reader["gebruikerId"]),
                Convert.ToInt32(reader["reviewId"]),
                Convert.ToBoolean(reader["upvote"]),
                Convert.ToBoolean(reader["downvote"]),
                Convert.ToBoolean(reader["report"]));
        }
    }
}
