using System;
using ProductApi.Data.Entity;

namespace ProductApi.Data.Service
{
	public interface ITransactionService
	{
        public Task<List<TransactionEntity>> GetTransactions();
        public Task<TransactionEntity> GetTransactionByIdAsync(string transactionId);
        public Task AddTransaction(TransactionEntity transactionEntity);
        public Task UpdateTransaction(TransactionEntity transactionEntity);
        public Task DeleteTransaction(string transactionId);
    }
}

