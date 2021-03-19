using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.POCO;
using WebApi.Models.DTO;

namespace WebApi.Database.Mapper
{
    public class Mapper
    {
        public static Person Map(PersonDTO personDTO)
        {
            Person person = new Person();

            person.PersonID = personDTO.PersonID;
            person.FirstName = personDTO.FirstName;
            person.LastName = personDTO.LastName;
            person.City = personDTO.City;
            person.Address = personDTO.Address;

            return person;
        }

        public static PersonDTO Map(Person person)
        {
            PersonDTO personDTO = new PersonDTO();

            personDTO.PersonID = person.PersonID;
            personDTO.FirstName = person.FirstName;
            personDTO.LastName = person.LastName;
            personDTO.City = person.City;
            personDTO.Address = person.Address;

            return personDTO;
        }

        public static IQueryable<PersonDTO> Map(IQueryable<Person> people)
        {
            List<PersonDTO> list = new List<PersonDTO>(); 
            foreach (var person in people)
            {
                list.Add(Map(person));
            }
            
            return list.AsQueryable();
        }


        public static IQueryable<Person> Map(IQueryable<PersonDTO> people)
        {
            List<Person> list = new List<Person>();
            foreach (var person in people)
            {
                list.Add(Map(person));
            }

            return list.AsQueryable();
        }

    }
}
