﻿using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using uBlogger.Domain.Entities;
using uBlogger.Infrastructure.Database;

namespace uBlogger.Infrastructure.Accounts.DbCommands
{
    public class SaveAccountCommand : SqlCommand
    {
        private readonly Account account;

        public SaveAccountCommand(Account account)
        {
            this.account = account;
            sql = "INSERT INTO public.\"Accounts\"(\"Id\", \"UserName\", \"Email\", \"Hash\") VALUES (@Id, @UserName, @Email, @Hash);";
        }

        public override async Task ExecuteAsync(IDbConnection connection)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await connection.ExecuteAsync(sql, account, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new TransactionFailedException(GetType(), ex);
                }
            }
        }
    }
}