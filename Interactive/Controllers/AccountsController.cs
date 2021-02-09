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
    public class AccountsController : Controller
    {
        // GET: AccountsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountsController/Details/5
        public ActionResult Details(int id)
        {
            var accountRepo = new AccountRepository();
            var accountDTO = accountRepo.GetAccountByCode(id);

            return View(accountDTO);
        }

        // GET: AccountsController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateForAccount(int personCode)
        {
            var personRepo = new PersonRepository();
            personRepo.GetPersonByPersonCode(personCode);
            var account = new Account();
            account.PersonCode = personCode;
            var accountRepo = new AccountRepository();
            
            return View("CreateForAccount", account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var accountRepo = new AccountRepository();
                var accountDTO = new Account();

                accountDTO.AccountNumber = collection["AccountNumber"];
                accountDTO.OutstandingBalance = Convert.ToInt32(collection["OutstandingBalance"]);
                accountDTO.PersonCode = Convert.ToInt32(collection["PersonCode"]);
                var code = accountRepo.AddAccount(accountDTO);

                return RedirectToAction(nameof(Details), new { id = code });
            }
            catch
            {
                return View();
            }
        }

        // POST: AccountsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForAccount(IFormCollection collection)
        {
            try
            {
                var accountRepo = new AccountRepository();
                var accountDTO = new Account();

                accountDTO.AccountNumber = collection["AccountNumber"];
                accountDTO.OutstandingBalance = Convert.ToInt32(collection["OutstandingBalance"]);
                accountDTO.PersonCode = Convert.ToInt32(collection["PersonCode"]);
                var code = accountRepo.AddAccount(accountDTO);

                return RedirectToAction(nameof(Details), "Persons", new { id = code });
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountsController/Edit/5
        public ActionResult Edit(int id)
        {
            var accountRepo = new AccountRepository();
            var accpuntDTO = accountRepo.GetAccountByCode(id);

            return View(accpuntDTO);
        }

        // POST: AccountsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var accountRepo = new AccountRepository();
                var accountDTO = new Account();
                accountRepo.GetAccountByCode(id);
                accountDTO.AccountNumber = collection["AccountNumber"];
                accountDTO.OutstandingBalance = Convert.ToInt32(collection["OutstandingBalance"]);
                accountDTO.PersonCode = Convert.ToInt32(collection["PersonCode"]);
                accountRepo.EditAccount(accountDTO);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountsController/Delete/5
        public ActionResult Delete(int id)
        {
            var accountRepo = new AccountRepository();
            var accoutDTO = accountRepo.GetAccountByCode(id);

            return View(accoutDTO);
        }

        // POST: AccountsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var accountRepo = new AccountRepository();
                accountRepo.GetAccountByCode(id);
                accountRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
