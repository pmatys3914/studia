﻿namespace Banking
{
    class Program
    {
        static void Main(string[] args)
        {
            new BankSystem();
            AccountDatabaseManager man = new AccountDatabaseManager(new DatabaseHandle());

            //test
            AccountData data = new AccountData();
            data.name = "Test Test";
            data.accountId = 1000000;
            data.balance = 0;
            data.password = "a";
            data.hash = "h";

            man.Add(data);
            man.Remove(data.accountId);
        }
    }
}