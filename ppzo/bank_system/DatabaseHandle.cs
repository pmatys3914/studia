using Microsoft.Data.Sqlite;

namespace Banking
{
    class DatabaseHandle {
        private String dbPath;
        private SqliteConnection? dbConnection;
        public DatabaseHandle(String dbPath = "bank.db")
        {
            this.dbPath = dbPath;
            prepareDatabase();
            if(dbConnection == null)
            {
                throw new NullReferenceException("Failed to initialize database connection!");
            }
        }

        public SqliteConnection? GetConnection()
        {
            return dbConnection;
        }

        private void prepareDatabase()
        {
            if(!File.Exists(dbPath))
            {
                Console.WriteLine("Database file doesn't exist! Creating...");
                createDatabase();
                Console.WriteLine("Database Created!");
            }
            dbConnection = new SqliteConnection("Data Source = " + dbPath);
        }

        private void createDatabase()
        {
            using (var con = new SqliteConnection("Data Source = " + dbPath))
            {
                con.Open();
                var createCommand = con.CreateCommand();
                createCommand.CommandText =
              @"CREATE TABLE accounts (
                    AccountId INTEGER PRIMARY KEY,
                    Name VARCHAR(255) NOT NULL,
                    Balance INTEGER NOT NULL,
                    Password BINARY(255) NOT NULL,
                    Salt BINARY(255) NOT NULL
                )";
                createCommand.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}