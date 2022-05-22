namespace Banking
{
    class AccountDatabaseManager
    {
        private DatabaseHandle dbHandle;

        public AccountDatabaseManager(DatabaseHandle dbHandle)
        {
            this.dbHandle = dbHandle;
        }

        public bool Add(AccountData data)
        {
            if(Exists(data))
            {
                Console.WriteLine("Error: Account exists already.");
                return false;
            }
            var addCommand = dbHandle.GetConnection().CreateCommand();
            addCommand.CommandText =
          @"INSERT INTO accounts
            VALUES ($id, $name, $balance, $password, $hash)";
            addCommand.Parameters.AddWithValue("$id", data.accountId);
            addCommand.Parameters.AddWithValue("$name", data.name);
            addCommand.Parameters.AddWithValue("$balance", data.balance);
            addCommand.Parameters.AddWithValue("$password", data.password);
            addCommand.Parameters.AddWithValue("$hash", data.hash);
            addCommand.ExecuteNonQuery();
            if(Exists(data))
            {
                Console.WriteLine("Account {0} created successfully!", data.name);
                return true;
            }
            else
            {
                Console.WriteLine("Account couldn't be created.");
                return false;
            }
        }

        public bool Exists(AccountData data)
        {
            return this.Exists(data.name) || this.Exists(data.accountId);
        }

        public bool Exists(String name)
        {
            var checkNameCommand = dbHandle.GetConnection().CreateCommand();
            checkNameCommand.CommandText =
          @"SELECT EXISTS(SELECT * FROM accounts WHERE name=$name)";
            checkNameCommand.Parameters.AddWithValue("$name", name);
            Int64? result = (Int64?)checkNameCommand.ExecuteScalar();
            if(result == 1)
            {
                return true;
            }
            return false;
        }

        public bool Exists(uint id)
        {
            var checkIdCommand = dbHandle.GetConnection().CreateCommand();
            checkIdCommand.CommandText =
          @"SELECT EXISTS(SELECT * FROM accounts WHERE accountId=$id)";
            checkIdCommand.Parameters.AddWithValue("$id", id);
            Int64? result = (Int64?)checkIdCommand.ExecuteScalar();
            if(result == 1)
            {
                return true;
            }
            return false;
        }

        public bool Remove(uint id)
        {
            if(!Exists(id))
            {
                Console.WriteLine("Error: Cannot remove non-existent account");
                return false;
            }
            var removeCommand = dbHandle.GetConnection().CreateCommand();
            removeCommand.CommandText =
          @"DELETE FROM accounts
            WHERE accountId = $id";
            removeCommand.Parameters.AddWithValue("$id", id);
            removeCommand.ExecuteNonQuery();
            if(!Exists(id))
            {
                Console.WriteLine("Account {0} terminated successfully.", id);
                return true;
            }
            else
            {
                Console.WriteLine("Couldn't terminate account {0}.", id);
                return false;
            }
        }

        public uint GetAmount()
        {
            var getAmountCommand = dbHandle.GetConnection().CreateCommand();
            getAmountCommand.CommandText =
          @"SELECT COUNT(*) FROM accounts";
            Int64? result = (Int64?)getAmountCommand.ExecuteScalar();
            if(result == null)
            {
                return 0;
            }
            return (uint)result;
        }
    }
}