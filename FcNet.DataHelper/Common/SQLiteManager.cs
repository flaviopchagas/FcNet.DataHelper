using System.Data.SQLite;

namespace FcNet.DataHelper.Common
{
    public class SQLiteManager
    {
        private SQLiteConnection conn;
        private string connString;
        private string fileName;

        public SQLiteManager(string connectionString)
        {
            connString = connectionString;
        }

        public void CreateTable()
        {
            string table = @"CREATE TABLE IF NOT EXISTS [Persons] (
                          [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [name] NVARCHAR(2048) NOT NULL)";

            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = table;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
