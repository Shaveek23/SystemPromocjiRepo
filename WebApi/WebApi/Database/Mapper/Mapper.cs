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
        //Mapowanie dla komentarzy
        public static CommentDTO Map(Comment comment)
        {
            CommentDTO commentDTO = new CommentDTO();
           
            commentDTO.UserID = comment.UserID;
            commentDTO.PostID = comment.PostID;
            commentDTO.DateTime = comment.DateTime;
            commentDTO.Content = comment.Content;
            return commentDTO;

        }
        public static Comment Map(CommentDTO commentDTO)
        {
            Comment comment = new Comment();
         
            comment.UserID = commentDTO.UserID;
            comment.PostID = commentDTO.PostID;
            comment.DateTime = commentDTO.DateTime;
            comment.Content = commentDTO.Content;
            return comment;

        }
        public static CommentDTOOutput MapOutput(Comment comment)
        {
            CommentDTOOutput commentDTO = new CommentDTOOutput();
            commentDTO.CommentID = comment.CommentID;
            commentDTO.UserID = comment.UserID;
            commentDTO.PostID = comment.PostID;
            commentDTO.DateTime = comment.DateTime;
            commentDTO.Content = comment.Content;
            return commentDTO;

        }
        public static Comment Map(CommentDTOOutput commentDTO)
        {
            Comment comment = new Comment();
            comment.CommentID = commentDTO.CommentID;
            comment.UserID = commentDTO.UserID;
            comment.PostID = commentDTO.PostID;
            comment.DateTime = commentDTO.DateTime;
            comment.Content = commentDTO.Content;
            return comment;

        }
        public static IQueryable<CommentDTO> Map(IQueryable<Comment> comments)
        {
            List<CommentDTO> list = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                list.Add(Map(comment));
            }

            return list.AsQueryable();
        }


        public static IQueryable<Comment> Map(IQueryable<CommentDTO> comments)
        {
            List<Comment> list = new List<Comment>();
            foreach (var comment in comments)
            {
                list.Add(Map(comment));
            }

            return list.AsQueryable();
        }
        public static IQueryable<CommentDTOOutput> MapOutput(IQueryable<Comment> comments)
        {
            List<CommentDTOOutput> list = new List<CommentDTOOutput>();
            foreach (var comment in comments)
            {
                list.Add(MapOutput(comment));
            }

            return list.AsQueryable();
        }


        public static IQueryable<Comment> Map(IQueryable<CommentDTOOutput> comments)
        {
            List<Comment> list = new List<Comment>();
            foreach (var comment in comments)
            {
                list.Add(Map(comment));
            }

            return list.AsQueryable();
        }

    }

}

