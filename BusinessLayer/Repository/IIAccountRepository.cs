using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Repository
{
    public interface IIAccountRepository 
    {
        public Account GetAccountByCode(int accountId);
        public int AddAccount(Account accountDTO);
        public bool EditAccount(Account accountDTO);
        public List<TransactionDTO> GetTransactions(int accountCode);
        public bool Delete(int code);

    }
}
