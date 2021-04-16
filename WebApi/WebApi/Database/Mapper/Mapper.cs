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

            person.PersonID = personDTO.PersonID.Value;
            person.FirstName = personDTO.FirstName;
            person.LastName = personDTO.LastName;
            person.City = personDTO.City;
            person.Address = personDTO.Address;

            return person;
        }

        public static PersonDTO Map(Person person)
        {
            if (person == null)
                return null;

            PersonDTO personDTO = new PersonDTO();

            personDTO.PersonID = person.PersonID;
            personDTO.FirstName = person.FirstName;
            personDTO.LastName = person.LastName;
            personDTO.City = person.City;
            personDTO.Address = person.Address;

            return personDTO;
        }
        public static User Map(UserDTO userDTO)
        {
            return new User
            {
                UserID = userDTO.UserID,
                UserName = userDTO.UserName,
                UserEmail = userDTO.UserEmail,
                Timestamp = userDTO.Timestamp,
                IsVerified = userDTO.IsVerified,
                IsAdmin = userDTO.IsAdmin,
                IsEnterprenuer = userDTO.IsEnterprenuer,
                Active = userDTO.Active
            };
        }
        public static UserDTO Map(User user)
        {

            return new UserDTO
            {
                UserID = user.UserID,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                Timestamp = user.Timestamp,
                IsVerified = user.IsVerified,
                IsAdmin = user.IsAdmin,
                IsEnterprenuer = user.IsEnterprenuer,
                Active = user.Active
            };
        }
        public static IQueryable<UserDTO> Map(IQueryable<User> people)
        {
            if (people == null)
                return null;
            List<UserDTO> list = new List<UserDTO>();
            foreach (var person in people)
            {
                list.Add(Map(person));
            }

            return list.AsQueryable();
        }


        public static IQueryable<User> Map(IQueryable<UserDTO> people)
        {
            if (people == null)
                return null;
            List<User> list = new List<User>();
            foreach (var person in people)
            {
                list.Add(Map(person));
            }

            return list.AsQueryable();
        }


        public static IQueryable<PersonDTO> Map(IQueryable<Person> people)
        {
            if (people == null)
                return null;
            List<PersonDTO> list = new List<PersonDTO>();
            foreach (var person in people)
            {
                list.Add(Map(person));
            }

            return list.AsQueryable();
        }


        public static IQueryable<Person> Map(IQueryable<PersonDTO> people)
        {
            if (people == null)
                return null;
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

            comment.UserID = commentDTO.UserID.Value;
            comment.PostID = commentDTO.PostID.Value;
            comment.DateTime = commentDTO.DateTime.Value;
            comment.Content = commentDTO.Content;
            return comment;

        }
        public static CommentDTOOutput MapOutput(Comment comment)
        {
            if (comment == null)
                return null;

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
            comment.CommentID = commentDTO.CommentID.Value;
            comment.UserID = commentDTO.UserID.Value;
            comment.PostID = commentDTO.PostID.Value;
            comment.DateTime = commentDTO.DateTime.Value;
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
            if (comments == null)
                return null;

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

        public static PostDTO Map(Post post)
        {
            if (post == null)
                return null;

            return new PostDTO
            {
                authorID = post.UserID,
                category = post.CategoryID,
                content = post.Content,
                datetime = post.Date,
                id = post.PostID,
                isPromoted = post.IsPromoted,
                title = post.Title

            };
        }
    }

}

