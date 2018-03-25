using System.Data.SqlClient;

namespace ProgrammersPoint.Models
{
    public class Database
    {
        private static readonly string connectieString = "PUT YOUR CONNECTION STRING HERE";

        /// <summary>
        /// Creeërt een nieuw databaseobject en opent de connectie. De aanroeper
        /// is verantwoordelijk voor het sluiten van de connectie.
        /// </summary>
        public static SqlConnection Connectie
        {
            get
            {
                SqlConnection connectie = new SqlConnection(connectieString);
                connectie.Open();
                return connectie;
            }
        }

    }
}
