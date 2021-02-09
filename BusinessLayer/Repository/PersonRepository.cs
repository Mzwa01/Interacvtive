using AutoMapper;
using BusinessLayer.DTO;
using DataLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Repository
{
    public class PersonRepository : IIPersonRepository
    {
        

        public bool Addacount(DTO.Account accountDTO)
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
                account.Transactions = (ICollection<Transaction>)accountDTO.Transactions;

                _context.Accounts.Add(account);

                _context.SaveChanges();

            }

            return true;
        }

        public bool EditPerson(PersonDTO personDTO)
        {
            var _context = new InteractiveDBContext();
            var person = new Person();

            using (_context)
            {


                person.Code = personDTO.Code;
                person.IdNumber = personDTO.IdNumber;
                person.Name = personDTO.Name;
                person.Surname = personDTO.Surname;
                person.Accounts = (ICollection<DataLayer.Models.Account>)personDTO.Accounts;

                _context.Persons.Attach(person);
                _context.Entry(person).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return true;
        }

        public List<DTO.Account> GetAccounts(int personCode)
        {
            var _context = new InteractiveDBContext();
            var accountList = new List<DTO.Account>();
            using (_context)
            {

                object[] sqlParams = {
                    new SqlParameter("@personCode", personCode),
                };

                var accounts = _context.Set<Person>().FromSqlRaw("dbo.GetByAccountsByPersonCode", sqlParams);
            }
            return accountList;
        }

        public List<PersonDTO> GetPeople()
        {
            var _context = new InteractiveDBContext();
            var peopleList = new List<PersonDTO>();
            
            using (_context)
            {
                var people = _context.Persons;

                foreach (var item in people)
                {
                    var person = new PersonDTO();
                    person.Code = item.Code;
                    person.IdNumber = item.IdNumber;
                    person.Name = item.Name;
                    person.Surname = item.Surname;
                    person.Accounts = item.Accounts as ICollection<DTO.Account>;

                    peopleList.Add(person);
                }
            }

            return peopleList;
                       
        }

        public PersonDTO GetPersonByAccount(string account)
        {
            var _context = new InteractiveDBContext();
            var personDTO = new PersonDTO();

            using (_context)
            {

                object[] sqlParams = {
                    new SqlParameter("@personCode", account),
                };

                var person = _context.Set<Person>().FromSqlRaw("dbo.GetPersonByAccount", sqlParams);
            }

            return personDTO;
        }

        public PersonDTO GetPersonByIdNumber(string idNumber)
        {
            var _context = new InteractiveDBContext();
            var personDTO = new PersonDTO();

            using (_context)
            {
                var person = _context.Persons.Where(x => x.IdNumber == idNumber).FirstOrDefault();

                personDTO.Code = person.Code;
                personDTO.IdNumber = person.IdNumber;
                personDTO.Name = person.Name;
                personDTO.Surname = person.Surname;
                personDTO.Accounts = (ICollection<DTO.Account>)person.Accounts;
                                    
            }

            return personDTO;

        }

        public List<PersonDTO> GetPersonBySurname(string surname)
        {
            var _context = new InteractiveDBContext();
            var peopleList = new List<PersonDTO>();

            using (_context)
            {
                var people = _context.Persons.Where(x => x.Surname == surname);

                foreach (var item in people)
                {
                    var person = new PersonDTO();
                    person.Code = item.Code;
                    person.IdNumber = item.IdNumber;
                    person.Name = item.Name;
                    person.Surname = item.Surname;
                    person.Accounts = (ICollection<DTO.Account>)item.Accounts;

                    peopleList.Add(person);
                }
            }

            return peopleList;
        }

        public bool AddPerson(PersonDTO personDTO)
        {
            var _context = new InteractiveDBContext();
            var person = new Person();

            using (_context)
            {


              //  person.Code = personDTO.Code;
                person.IdNumber = personDTO.IdNumber;
                person.Name = personDTO.Name;
                person.Surname = personDTO.Surname;
                person.Accounts = (ICollection<DataLayer.Models.Account>)personDTO.Accounts;
                _context.Persons.Add(person);
                _context.SaveChanges();
            }                     
            return true;
        }

        public PersonDTO GetPersonByPersonCode(int personCode)
        {
            var _context = new InteractiveDBContext();
            var personDTO = new PersonDTO();
            var person =_context.Persons.Where(x => x.Code == personCode).FirstOrDefault();
            
            var param = new SqlParameter("@personCode", personCode);
            var accounts = _context.Set<DataLayer.Models.Account>().FromSqlRaw("dbo.GetByAccountsByPersonCode @personCode", param).ToList();
            

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DataLayer.Models.Account,DTO.Account > ();
                cfg.CreateMap<DataLayer.Models.Transaction, DTO.TransactionDTO> ();
            });
            IMapper iMapper = config.CreateMapper();


            var accountsDTO = new List<DTO.Account>();

            foreach (var item in accounts)
            {

                var destination = iMapper.Map<DataLayer.Models.Account, DTO.Account>(item);
                accountsDTO.Add(destination);
            }

            personDTO.Code = person.Code;
            personDTO.IdNumber = person.IdNumber;
            personDTO.Name = person.Name;
            personDTO.Surname = person.Surname;
            personDTO.Accounts = accountsDTO;

            return personDTO;
        }

        public bool Delete(int code)
        {

            var _context = new InteractiveDBContext();
            using (_context)
            {
                var person = _context.Persons.Where(x => x.Code == code).FirstOrDefault();
                _context.Persons.Remove(person);
                return true;
            }
        }

    }
}
