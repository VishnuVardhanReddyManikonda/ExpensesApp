using Expenses.API.Dtos;
using Expenses.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Expenses.API.Data.Services
{
    public interface ITransactionService
    {
        List<Transaction> GetAll();

        Transaction? GetById(int id);

        Transaction Add(PostTransactionDto transaction);

        Transaction? Update(int id ,PutTransactionDto transaction);

        void Delete(int id);
    }

    public class TransactionService(AppDbContext context) : ITransactionService

    {
        public Transaction Add(PostTransactionDto transaction)
        {
            var newTransaction = new Transaction
            {
                Type = transaction.Type,
                Amount = transaction.Amount,
                Category = transaction.Category,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = DateTime.Now
            };

            context.Transactions.Add(newTransaction);
            context.SaveChanges();
            return newTransaction;
        }

        public void Delete(int id)
        {
            var transaction = context.Transactions.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                context.Transactions.Remove(transaction);
                context.SaveChanges();
            }
            
        }

        public List<Transaction> GetAll()
        {
            var allTransactions = context.Transactions.ToList();
            return allTransactions;
        }

        public Transaction? GetById(int id)
        {
            var transaction = context.Transactions.FirstOrDefault(t => t.Id == id);
            
            return transaction;
        }

        public Transaction? Update(int id,PutTransactionDto transaction)
        {
            var transactionToUpdate = context.Transactions.FirstOrDefault(t => t.Id == id);
            if (transactionToUpdate != null)
            {
                transactionToUpdate.Type = transaction.Type ?? transactionToUpdate.Type;
                transactionToUpdate.Amount = transaction.Amount ?? transactionToUpdate.Amount;
                transactionToUpdate.Category = transaction.Category ?? transactionToUpdate.Category;
                transactionToUpdate.UpdatedAt = DateTime.Now;

                context.Transactions.Update(transactionToUpdate);
                context.SaveChanges();
            }
            return transactionToUpdate;


        }
    }
}