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
    public class PostMSSQLContext : IPostContext
    {
        public List<Post> GetNieuwePosts(int dateTimeVerschil)
        {
            List<Post> postLijst = new List<Post>();

            using (SqlConnection connectie = Database.Connectie)
            {
                using (SqlCommand command = new SqlCommand("PostGetNieuwePostsProcedure", connectie))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@DateTimeVerschil", dateTimeVerschil));

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    postLijst.Add(CreëerPostVanReader(reader));
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
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

            return postLijst;
        }

        public List<Post> GetInteressantePosts(int gebruikerId)
        {
            List<Post> postLijst = new List<Post>();

            using (SqlConnection connectie = Database.Connectie)
            {
                using (SqlCommand cmd = new SqlCommand("GetInteressantePostsProcedure", connectie))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@GebruikerId", gebruikerId));

                    try
                    {
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                postLijst.Add(CreëerPostVanReader(sqlDataReader));
                            }
                        }
                    }
                    catch (SqlException exp)
                    {
                        throw exp;
                    }
                }
            }

            return postLijst;
        }

        public async Task<List<Post>> GetAll()
        {
            List<Post> postLijst = new List<Post>();

            using (SqlConnection connectie = Database.Connectie)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Post", connectie);
                    using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (sqlDataReader.Read())
                        {
                            postLijst.Add(CreëerPostVanReader(sqlDataReader));
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

            return postLijst;
        }

        public Post GetById(int id)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery = "SELECT * FROM Post WHERE postId = @postId";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter postIdParamater = new SqlParameter("postId", SqlDbType.Int)
                    {
                        Value = id
                    };

                    sqlCommand.Parameters.Add(postIdParamater);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreëerPostVanReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public Post GetByNaam(string naam)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery = "SELECT * FROM Post WHERE naam = @Postnaam";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter postNaamParameter = new SqlParameter("postNaam", SqlDbType.NVarChar)
                    {
                        Value = naam
                    };

                    sqlCommand.Parameters.Add(postNaamParameter);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreëerPostVanReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void RecordCategoryVisit(Categorieën categorie, Gebruiker huidigeGebruiker)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                using (SqlCommand cmd = new SqlCommand("RecordCategoryVisitProcedure", connectie))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Categorie", categorie + 1));
                    cmd.Parameters.Add(new SqlParameter("@GebruikerId", huidigeGebruiker.GebruikerId));

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

        public void Insert(Post post)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                using (SqlCommand cmd = new SqlCommand("PostInsertProcedure", connectie))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Categorie", post.Categorie + 1));
                    cmd.Parameters.Add(new SqlParameter("@BeschrijvingTaal", post.BeschrijvingTaal));
                    cmd.Parameters.Add(new SqlParameter("@MoeilijkheidsgraadOnderwerp", post.MoeilijkheidsgraadOnderwerp));
                    cmd.Parameters.Add(new SqlParameter("@TaalVersie", post.TaalVersie));
                    cmd.Parameters.Add(new SqlParameter("@Naam", post.Naam));

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

        public void Update(Post post)
        {
            using (SqlConnection connectie = Database.Connectie)
            {

                string sqlQuery =
                "UPDATE Post SET categorie = @categorie," +
                "[beschrijving taal] = @beschrijving_taal," +
                "postversie = @postversie," +
                "taalversie = @taalversie," +
                "[moeilijkheidsgraad onderwerp] = @moeilijkheidsgraad_onderwerp," +
                "naam = @naam " +
                "WHERE postId = @postId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    List<SqlParameter> sqlParameterList = new List<SqlParameter>
                            {
                            new SqlParameter("categorie", SqlDbType.Int) { Value = post.Categorie + 1 },
                            new SqlParameter("beschrijving_taal", SqlDbType.NVarChar) { Value = post.BeschrijvingTaal },
                            new SqlParameter("postversie", SqlDbType.Int) { Value = post.PostVersie },
                            new SqlParameter("taalversie", SqlDbType.NVarChar) { Value = post.TaalVersie },
                            new SqlParameter("moeilijkheidsgraad_onderwerp", SqlDbType.TinyInt) { Value = post.MoeilijkheidsgraadOnderwerp },
                            new SqlParameter("naam", SqlDbType.NVarChar) { Value = post.Naam },
                            new SqlParameter("postId", SqlDbType.Int) { Value = post.PostId }
                            };
                    sqlCommand.Parameters.AddRange(sqlParameterList.ToArray());

                    try
                    {
                        int rowsAangepast = sqlCommand.ExecuteNonQuery();
                    } catch (SqlException exp)
                    {
                        throw exp;
                    }
                }
            }
        }

        public void Delete(Post post)
        {
            using (SqlConnection connectie = Database.Connectie)
            {
                string sqlQuery =
                "DELETE FROM Post WHERE postId = @postId";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, connectie))
                {
                    SqlParameter postIdParameter = new SqlParameter("postId", SqlDbType.Int)
                    {
                        Value = post.PostId
                    };

                    sqlCommand.Parameters.Add(postIdParameter);

                    int rowsAangepast = sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public List<Post> GetAllNonAsyncTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hulpfunctie om een postinstantie van een DataReader te construeren.
        /// Verwacht dat read() al aangeroepen is.
        /// </summary>
        /// <param name="reader">De DataReader om uit te lezen.</param>
        /// <returns>Een nieuwe postinstantie gebaseerd op de uitgelezen data</returns>
        private Post CreëerPostVanReader(SqlDataReader reader)
        {
            return new Post(
                Convert.ToInt32(reader["postId"]),
                (Categorieën)reader["categorie"] - 1,
                Convert.ToString(reader["naam"]),
                Convert.ToString(reader["beschrijving taal"]),
                Convert.ToInt32(reader["moeilijkheidsgraad onderwerp"]),
                Convert.ToInt32(reader["postversie"]),
                Convert.ToString(reader["taalversie"]),
                Convert.ToDateTime(reader["laatst geüpdatet"]),
                Convert.ToInt32(reader["reports postbeschrijving"]));
        }

    }
}
