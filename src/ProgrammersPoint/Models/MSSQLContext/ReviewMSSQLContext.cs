using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ProgrammersPoint.Interfaces;

namespace ProgrammersPoint.Models.MSSQLContext
{
    public class ReviewMSSQLContext : IReviewContext
    {
        public async Task<List<Review>> GetAll()
        {
            List<Review> reviewLijst = new List<Review>();

            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Review", connectie);
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (sqlDataReader.Read())
                        {
                            reviewLijst.Add(CreëerReviewVanReader(sqlDataReader));
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

            return reviewLijst;
        }

        public Review GetReviewById(int id)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery = "SELECT * FROM Review WHERE reviewId = @reviewId";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter reviewIdParameter = new SqlParameter("reviewId", SqlDbType.Int)
                    {
                        Value = id
                    };

                    sqlCommand.Parameters.Add(reviewIdParameter);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreëerReviewVanReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<Review>> GetListByPostId(int id)
        {
            List<Review> reviewLijst = new List<Review>();

            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Review WHERE postId = @postId", connectie);

                    SqlParameter postIdParameter = new SqlParameter("postId", SqlDbType.Int)
                    {
                        Value = id
                    };

                    sqlCommand.Parameters.Add(postIdParameter);

                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (sqlDataReader.Read())
                        {
                            reviewLijst.Add(CreëerReviewVanReader(sqlDataReader));
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

            return reviewLijst;
        }

        public void InsertReview(Review review)
        {
            string query = "INSERT INTO Review (postId, gebruikerId, " +
                           "reviewtekst, titel) " +
                           "VALUES (@postId, @gebruikerId, @reviewtekst, @titel)";

            using (SqlConnection connectie = Database.Connectie)
            {
                using (SqlCommand cmd = new SqlCommand(query, connectie))
                {
                    cmd.Parameters.Add(new SqlParameter("@postId", review.PostId));
                    cmd.Parameters.Add(new SqlParameter("@gebruikerId", review.GebruikerId));
                    cmd.Parameters.Add(new SqlParameter("@reviewtekst", review.Reviewtekst));
                    cmd.Parameters.Add(new SqlParameter("@titel", review.Titel));

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException exp)
                    {
                        throw exp;
                    }
                }
            }
        }

        public void Update(Review review)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                    "UPDATE Review SET " +
                    "reviewtekst = @reviewtekst," +
                    "titel = @titel " +
                    "WHERE reviewId = @reviewId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    List<SqlParameter> sqlParameterList = new List<SqlParameter>
                    {
                        new SqlParameter("reviewtekst", SqlDbType.NVarChar) { Value = review.Reviewtekst },
                        new SqlParameter("titel", SqlDbType.NVarChar) { Value = review.Titel },
                        new SqlParameter("reviewId", SqlDbType.Int) { Value = review.ReviewId }
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

        public void Delete(Review review)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                    "DELETE FROM Review WHERE reviewId = @reviewId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter reviewIdParameter = new SqlParameter("reviewId", SqlDbType.Int)
                    {
                        Value = review.ReviewId
                    };

                    sqlCommand.Parameters.Add(reviewIdParameter);

                    int rowsAangepast = sqlCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Hulpfunctie om een reviewinstantie van een DataReader te construeren.
        /// Verwacht dat read() al aangeroepen is.
        /// </summary>
        /// <param name="reader">De DataReader om uit te lezen.</param>
        /// <returns>Een nieuwe reviewinstantie gebaseerd op de uitgelezen data</returns>
        private Review CreëerReviewVanReader(SqlDataReader reader)
        {
            return new Review(
                Convert.ToInt32(reader["reviewId"]),
                Convert.ToInt32(reader["postId"]),
                Convert.ToInt32(reader["gebruikerId"]),
                Convert.ToString(reader["reviewtekst"]),
                Convert.ToString(reader["titel"]));
        }
    }
}
