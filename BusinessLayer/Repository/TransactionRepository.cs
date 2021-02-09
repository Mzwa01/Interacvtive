using AutoMapper;
using BusinessLayer.DTO;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Repository
{
    public class TransactionRepository : IITransactionRepository
    {
        public int AddTranstion(TransactionDTO transactionDTO)
        {
            var _context = new InteractiveDBContext();
            var transaction = new Transaction();
            using (_context)
            {
                transaction.Code = transactionDTO.Code;
                transaction.AccountCode = transactionDTO.AccountCode;
                transaction.Amount = transactionDTO.Amount;
                transaction.CaptureDate = transactionDTO.CaptureDate;
                transaction.Description = transactionDTO.Description;

                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }

            return transaction.AccountCode;
        }

        public bool EditTranstion(TransactionDTO transactionDTO)
        {
            var _context = new InteractiveDBContext();
            var transaction = new Transaction();
            using (_context)
            {
                transaction.Code = transactionDTO.Code;
                transaction.AccountCode = transactionDTO.AccountCode;
                transaction.Amount = transactionDTO.Amount;
                transaction.CaptureDate = transactionDTO.CaptureDate;
                transaction.Description = transactionDTO.Description;

                _context.Transactions.Attach(transaction);
                _context.Entry(transaction).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return true;
        }

        public TransactionDTO GetTransaction(int id)
        {
            var _context = new InteractiveDBContext();
            var transactionDTO = new TransactionDTO();
            using (_context)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<DataLayer.Models.Account, DTO.Account>();
                    cfg.CreateMap<DataLayer.Models.Transaction, DTO.TransactionDTO>();
                });
                var transaction = _context.Transactions.Where(x => x.Code == id).FirstOrDefault();
                IMapper iMapper = config.CreateMapper();
                var destination = iMapper.Map<DataLayer.Models.Transaction, TransactionDTO>(transaction);
                transactionDTO = destination;
            }

            return transactionDTO;
        }

        public bool Delete(int code)
        {

            var _context = new InteractiveDBContext();
            using (_context)
            {
                var transaction = _context.Transactions.Where(x => x.Code == code).FirstOrDefault();
                _context.Transactions.Remove(transaction);
                return true;
            }
        }
    }
}
