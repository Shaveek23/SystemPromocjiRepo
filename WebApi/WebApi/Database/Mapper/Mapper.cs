﻿using System;
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
            if (userDTO == null)
                return null;
            return new User
            {
                UserID = userDTO.id ?? 0,
                UserName = userDTO.userName,
                UserEmail = userDTO.userEmail,
                Timestamp = userDTO.timestamp.Value,
                IsVerified = userDTO.isVerified.Value,
                IsAdmin = userDTO.isAdmin.Value,
                IsEnterprenuer = userDTO.isEnterprenuer.Value,
                Active = userDTO.isActive.Value
            };
        }
        public static UserDTO Map(User user)
        {
            if (user == null)
                return null;
            return new UserDTO
            {
                id = user.UserID,
                userName = user.UserName,
                userEmail = user.UserEmail,
                timestamp = user.Timestamp,
                isVerified = user.IsVerified,
                isAdmin = user.IsAdmin,
                isEnterprenuer = user.IsEnterprenuer,
                isActive = user.Active
            };
        }
        public static PostLike Map(PostLikeDTO postLikeDTO)
        {
            return new PostLike
            {
                PostID = postLikeDTO.PostID,
                PostLikeID = postLikeDTO.PostLikeID,
                UserID = postLikeDTO.UserID
            };

        }
        public static PostLikeDTO Map(PostLike postLike)
        {
            return new PostLikeDTO
            {
                PostID = postLike.PostID,
                PostLikeID = postLike.PostLikeID,
                UserID = postLike.UserID
            };

        }
        public static CommentLike Map(CommentLikeDTO CommentLikeDTO)
        {
            return new CommentLike
            {
                CommentID = CommentLikeDTO.CommentID,
                CommentLikeID = CommentLikeDTO.CommentLikeID,
                UserID = CommentLikeDTO.UserID
            };

        }
        public static CommentLikeDTO Map(CommentLike CommentLike)
        {
            return new CommentLikeDTO
            {
                CommentID = CommentLike.CommentID,
                CommentLikeID = CommentLike.CommentLikeID,
                UserID = CommentLike.UserID
            };

        }

        public static IQueryable<PostLike> Map(IQueryable<PostLikeDTO> likesDTO)
        {
            List<PostLike> postlikes = new List<PostLike>();
            foreach (var like in likesDTO)
                postlikes.Add(Map(like));

            return postlikes.AsQueryable();
        }
        public static IQueryable<PostLikeDTO> Map(IQueryable<PostLike> likes)
        {
            List<PostLikeDTO> postlikes = new List<PostLikeDTO>();
            foreach (var like in likes)
                postlikes.Add(Map(like));

            return postlikes.AsQueryable();
        }
        public static IQueryable<CommentLike> Map(IQueryable<CommentLikeDTO> likesDTO)
        {
            List<CommentLike> Commentlikes = new List<CommentLike>();
            foreach (var like in likesDTO)
                Commentlikes.Add(Map(like));

            return Commentlikes.AsQueryable();
        }
        public static IQueryable<CommentLikeDTO> Map(IQueryable<CommentLike> likes)
        {
            List<CommentLikeDTO> Commentlikes = new List<CommentLikeDTO>();
            foreach (var like in likes)
                Commentlikes.Add(Map(like));

            return Commentlikes.AsQueryable();
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
        public static Category Map(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null) return null;
            return new Category { CategoryID = categoryDTO.id.Value, Name = categoryDTO.name };
        }
        public static CategoryDTO Map(Category category)
        {
            if (category == null) return null;
            return new CategoryDTO { id = category.CategoryID, name = category.Name };
        }
        public static IQueryable<CategoryDTO> Map(IQueryable<Category> categories)
        {
            List<CategoryDTO> list = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                list.Add(Map(category));
            }

            return list.AsQueryable();
        }


        public static IQueryable<Category> Map(IQueryable<CategoryDTO> categories)
        {
            List<Category> list = new List<Category>();
            foreach (var category in categories)
            {
                list.Add(Map(category));
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

