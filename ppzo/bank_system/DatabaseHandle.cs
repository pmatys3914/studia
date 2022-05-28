using Microsoft.Data.Sqlite;

namespace Banking
{
    class DatabaseHandle {
        private String dbPath;
        private SqliteConnection dbConnection;
        public DatabaseHandle(String dbPath = "bank.db")
        {
            this.dbPath = dbPath;
            Prepare(); 
            this.dbConnection = new SqliteConnection("Data Source = " + dbPath);
            this.dbConnection.Open();
            Console.WriteLine("Connected to the database.");
        }

        ~DatabaseHandle()
        {
            if(dbConnection != null)
            {
                this.dbConnection.Close();
            }
        }

        public SqliteConnection GetConnection()
        {
            return this.dbConnection;
        }

        private void Prepare()
        {
            if(!File.Exists(dbPath))
            {
                Console.WriteLine("Database file doesn't exist! Creating...");
                Create();
                Console.WriteLine("Database Created!");
            }
        }

        private void Create()
        {
            using (var con = new SqliteConnection("Data Source = " + dbPath))
            {
                con.Open();
                var createCommand = con.CreateCommand();
                createCommand.CommandText =
              @"CREATE TABLE accounts (
                    accountId CHAR(16) UNIQUE PRIMARY KEY,
                    name VARCHAR(255) UNIQUE NOT NULL,
                    balance INTEGER NOT NULL,
                    hash BINARY(255) NOT NULL,
                    salt BINARY(255) NOT NULL
                )";
                createCommand.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}