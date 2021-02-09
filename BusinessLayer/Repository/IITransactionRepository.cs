using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Repository
{
    public interface IITransactionRepository
    {
        public int AddTranstion(TransactionDTO transaction);
        public bool EditTranstion(TransactionDTO transaction);
        public TransactionDTO GetTransaction(int id);
        public bool Delete(int id);

    }
}
