
using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Repository
{
    public interface IIPersonRepository
    {
        public List<PersonDTO> GetPeople();
        public PersonDTO GetPersonByIdNumber(string idNumber);
        public PersonDTO GetPersonByPersonCode(int personCode);
        public List<PersonDTO> GetPersonBySurname(string surname);
        public PersonDTO GetPersonByAccount(string account);
        public bool EditPerson(PersonDTO person);
        public List<Account> GetAccounts(int persinId);
        public bool Addacount(Account accountDTO);
        public bool AddPerson(PersonDTO accountDTO);
        public bool Delete(int code);

    }
}
