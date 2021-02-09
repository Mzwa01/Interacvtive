using BusinessLayer.DTO;
using BusinessLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interactive.Controllers
{
    public class TransactionsController : Controller
    {

        // GET: TransactionsController/Details/5
        public ActionResult Details(int id)
        {
            var  transRepo = new TransactionRepository();
            var transactionDTO = transRepo.GetTransaction(id);
            return View(transactionDTO);
        }

        // GET: TransactionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var transactionDTO = new TransactionDTO();
                transactionDTO.AccountCode = Convert.ToInt32(collection["AccountCode"]);
                transactionDTO.Code = Convert.ToInt32(collection["Code"]);
                transactionDTO.Amount = Convert.ToInt32(collection["Amount"]);
                transactionDTO.CaptureDate =  DateTime.Now;
                transactionDTO.TransactionDate = DateTime.Now;
                transactionDTO.Description = collection["Description"];

                var transactionsRepo = new TransactionRepository();
                transactionsRepo.AddTranstion(transactionDTO);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateForAccount(int accountCode)
        {
            var personRepo = new AccountRepository();
            personRepo.GetAccountByCode(accountCode);
            var transactionDTO = new TransactionDTO();
            transactionDTO.AccountCode = accountCode;
            transactionDTO.CaptureDate = DateTime.Now;
            transactionDTO.TransactionDate = DateTime.Now;

            return View("Create", transactionDTO);
        }

        // GET: TransactionsController/Edit/5
        public ActionResult Edit(int id)
        {
            var transactionsRepo = new TransactionRepository();
            var transactionDTO = transactionsRepo.GetTransaction(id);
            return View(transactionDTO);
        }

        // POST: TransactionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var transactionDTO = new TransactionDTO();
                transactionDTO.AccountCode = Convert.ToInt32(collection["AccountCode"]);
                transactionDTO.Code = Convert.ToInt32(collection["Code"]);
                transactionDTO.Amount = Convert.ToInt32(collection["Amount"]);
                transactionDTO.CaptureDate = Convert.ToDateTime(collection["CaptureDate"]);
                transactionDTO.Description = collection["Description"];

                var transactionsRepo = new TransactionRepository();
                transactionsRepo.EditTranstion(transactionDTO);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionsController/Delete/5
        public ActionResult Delete(int id)
        {
            
            var transactionsRepo = new TransactionRepository();
            var transactionDTO = transactionsRepo.GetTransaction(id);
            
            return View(transactionDTO);
        }

        // POST: TransactionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var transactionDTO = new TransactionDTO();
                transactionDTO.AccountCode = Convert.ToInt32(collection["AccountCode"]);
                transactionDTO.Code = Convert.ToInt32(collection["Code"]);
                transactionDTO.Amount = Convert.ToInt32(collection["Amount"]);
                transactionDTO.CaptureDate = Convert.ToDateTime(collection["CaptureDate"]);
                transactionDTO.Description = collection["Description"];

                var transactionsRepo = new TransactionRepository();
                transactionsRepo.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
