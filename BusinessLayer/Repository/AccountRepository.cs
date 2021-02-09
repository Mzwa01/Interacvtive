using AutoMapper;
using BusinessLayer.DTO;
using DataLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Repository
{
    public class AccountRepository : IIAccountRepository

    {
        public int AddAccount(DTO.Account accountDTO )
        {
            var _context = new InteractiveDBContext();
            var account = new DataLayer.Models.Account();
            using (_context)
            {
                account.AccountNumber = accountDTO.AccountNumber;
                account.Code = accountDTO.Code;
                account.OutstandingBalance = accountDTO.OutstandingBalance;
                account.PersonCode = accountDTO.PersonCode;
                account.PersonCodeNavigation = accountDTO.PersonCodeNavigation;
              //  account.Transactions = accountDTO.Transactions;

                _context.Accounts.Add(account);

                _context.SaveChanges();

            }

            return account.PersonCode;
        }

        public bool EditAccount(DTO.Account accountDTO )
        {
            var _context = new InteractiveDBContext();
            var account = new DataLayer.Models.Account();
            using (_context)
            {
                account.AccountNumber = accountDTO.AccountNumber;
                account.Code = accountDTO.Code;
                account.OutstandingBalance = accountDTO.OutstandingBalance;
                account.PersonCode = accountDTO.PersonCode;
                account.PersonCodeNavigation = accountDTO.PersonCodeNavigation;
             //   account.Transactions = accountDTO.Transactions;

                _context.Accounts.Add(account);

                _context.SaveChanges();

            }

            return true;
        }

        public DTO.Account GetAccountByCode(int accountCode)
        {
            var _context = new InteractiveDBContext();
            var accountDTO = new DTO.Account();
            var account = _context.Accounts.Where(x => x.Code == accountCode).FirstOrDefault();

            var param = new SqlParameter("@accountCode", accountCode);
            var transactions = _context.Set<DataLayer.Models.Transaction>().FromSqlRaw("dbo.GetByTransactionByAccountCode @accountCode", param).ToList();


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DataLayer.Models.Account, DTO.Account>();
                cfg.CreateMap<DataLayer.Models.Transaction,DTO.TransactionDTO> ();
            });
            IMapper iMapper = config.CreateMapper();
            var transactionsDTO = new List<DTO.TransactionDTO>();

            foreach (var item in transactions)
            {

                var destination = iMapper.Map<DataLayer.Models.Transaction, TransactionDTO>(item);
                transactionsDTO.Add(destination);
            }

            accountDTO.Code = account.Code;
            accountDTO.AccountNumber = account.AccountNumber;
            accountDTO.OutstandingBalance = account.OutstandingBalance;
            accountDTO.PersonCode = account.PersonCode;
            accountDTO.PersonCodeNavigation = account.PersonCodeNavigation;
            accountDTO.Transactions = transactionsDTO;

            return accountDTO;

        }

        public List<TransactionDTO> GetTransactions(int accountCode)
        {
            var _context = new InteractiveDBContext();
            var transactionList = new List<TransactionDTO>();
            using (_context)
            {

                object[] sqlParams = {
                    new SqlParameter("@personCode", accountCode),
                };

                var accounts = _context.Set<TransactionDTO>().FromSqlRaw("dbo.GetByAccountsByPersonCode", sqlParams);
            }
            return transactionList;
        }

        public bool Delete(int code)
        {

            var _context = new InteractiveDBContext();
            using (_context)
            {
                var person = _context.Accounts.Where(x => x.Code == code).FirstOrDefault();
                _context.Accounts.Remove(person);
                return true;
            }
        }
    }
}
