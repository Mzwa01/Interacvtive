using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using BusinessLayer.Repository;
using System.Collections;

namespace Interactive.Controllers
{
    public class PersonsController : Controller
    {
        // GET: PersonsController
        public ActionResult Index()
        {
            var personsRepo = new PersonRepository();
            var people = personsRepo.GetPeople();
            return View(people);
        }

        // GET: PersonsController/Details/5
        public ActionResult Details(int id)
        {
            var personsRepo = new PersonRepository();
            var person = personsRepo.GetPersonByPersonCode(id);

            return View(person);
        }

        public ActionResult AccountDetails(int id)
        {
            return RedirectToAction("Details", "Accounts", new { id = id });
        }


        // GET: PersonsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var personsRepo = new PersonRepository();
                var personDTO = new PersonDTO();
                personDTO.Name = collection["Name"];
                personDTO.Surname = collection["Surname"];
                personDTO.IdNumber = collection["IdNumber"];
                var result = personsRepo.AddPerson(personDTO);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonsController/Edit/5
        public ActionResult Edit(int id)
        {
            var personsRepo = new PersonRepository();
            var person = personsRepo.GetPersonByPersonCode(id);

            return View("Edit", person);
        }

        // POST: PersonsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var personsRepo = new PersonRepository();
                
                var personDTO = personsRepo.GetPersonByPersonCode(id);
                personDTO.Name = collection["Name"];
                personDTO.Surname = collection["Surname"];
                personDTO.IdNumber = collection["IdNumber"];
                var people = personsRepo.EditPerson(personDTO);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonsController/Delete/5
        public ActionResult Delete(int id)
        {
            var personsRepo = new PersonRepository();
            var personDTO = personsRepo.GetPersonByPersonCode(id);

            return View(personDTO);
        }

        // POST: PersonsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var personsRepo = new PersonRepository();

                var personDTO = personsRepo.GetPersonByPersonCode(id);
                personDTO.Name = collection["Name"];
                personDTO.Surname = collection["Surname"];
                personDTO.IdNumber = collection["IdNumber"];
                var people = personsRepo.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
