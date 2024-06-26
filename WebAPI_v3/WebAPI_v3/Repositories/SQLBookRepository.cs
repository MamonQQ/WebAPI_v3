﻿using Microsoft.EntityFrameworkCore;
using WebAPI_v3.Models.DTO;
using WebAPI_v3.Data;
using WebAPI_v3.Repositories;
using WebAPI_v3.Models;
using System.Globalization;
namespace WebAPI_v3.Repositories
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLBookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<BookWithAuthorAndPublisherDTO> GetAllBooks()
        {
            var allBooks = _dbContext.Books.Select(Books => new BookWithAuthorAndPublisherDTO()
            {
                Id = Books.Id,
                Title = Books.Title,
                Description = Books.Description,
                IsRead = Books.isRead,
                DateRead = Books.isRead ? Books.DateRead.Value : null,
                Rate = Books.isRead ? Books.Rate.Value : null,
                Genre = Books.Genre,
                CoverUrl = Books.CoverUrl,
                PublishersName = Books.Publishers.Name,
                AuthorNames = Books.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).ToList();
            return allBooks;
        }
        public BookWithAuthorAndPublisherDTO GetBookById(int id)
        {
            var bookWithDomain = _dbContext.Books.Where(n => n.Id == id);
            //Map Domain Model to DTOs
            var bookWithIdDTO = bookWithDomain.Select(book => new
           BookWithAuthorAndPublisherDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.isRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublishersName = book.Publishers.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();
            return bookWithIdDTO;
        }
        public AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO)
        {
            //map DTO to Domain Model
            var bookDomainModel = new Book
            {
                Title = addBookRequestDTO.Title,
                Description = addBookRequestDTO.Description,
                isRead = addBookRequestDTO.isRead,
                DateRead = addBookRequestDTO.DateRead,
                Rate = addBookRequestDTO.Rate,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateAdded = addBookRequestDTO.DateAdded,
                PublishersID = addBookRequestDTO.PublisherID
            };
            //Use Domain Model to add Book 
            _dbContext.Books.Add(bookDomainModel);
            _dbContext.SaveChanges();
            foreach (var id in addBookRequestDTO.AuthorIDs)
            {
                var _book_author = new Book_Author()
                {
                    BookID = bookDomainModel.Id,
                    AuthorID = id
                };
                _dbContext.Book_Authors.Add(_book_author);
                _dbContext.SaveChanges();
            }
            return addBookRequestDTO;
        }
        public AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDomain != null)
            {
                bookDomain.Title = bookDTO.Title;
                bookDomain.Description = bookDTO.Description;
                bookDomain.isRead = bookDTO.isRead;
                bookDomain.DateRead = bookDTO.DateRead;
                bookDomain.Rate = bookDTO.Rate;
                bookDomain.Genre = bookDTO.Genre;
                bookDomain.CoverUrl = bookDTO.CoverUrl;
                bookDomain.DateAdded = bookDTO.DateAdded;
                bookDomain.PublishersID = bookDTO.PublisherID;
                _dbContext.SaveChanges();
            }
            var authorDomain = _dbContext.Book_Authors.Where(a => a.BookID == id).ToList();
            if (authorDomain != null)
            {
                _dbContext.Book_Authors.RemoveRange(authorDomain);
                _dbContext.SaveChanges();
            }
            foreach (var authorid in bookDTO.AuthorIDs)
            {
                var _book_author = new Book_Author()
                {
                    BookID = id,
                    AuthorID = authorid
                };
                _dbContext.Book_Authors.Add(_book_author);
                _dbContext.SaveChanges();
            }
            return bookDTO;
        }
        public Book? DeleteBookById(int id)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDomain != null)
            {
                _dbContext.Books.Remove(bookDomain);
                _dbContext.SaveChanges();
            }
            return bookDomain;
        }
        public List<BookWithAuthorAndPublisherDTO> GetAllBooks(string? filterOn = null, string?
 filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int
pageSize = 1000)
        {
            var allBooks = _dbContext.Books.Select(Books => new
BookWithAuthorAndPublisherDTO()
            {
                Id = Books.Id,
                Title = Books.Title,
                Description = Books.Description,
                IsRead = Books.isRead,
                DateRead = Books.isRead ? Books.DateRead.Value : null,
                Rate = Books.isRead ? Books.Rate.Value : null,
                Genre = Books.Genre,
                CoverUrl = Books.CoverUrl,
                PublishersName = Books.Publishers.Name,
                AuthorNames = Books.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).AsQueryable();
            //filtering 
            if (string.IsNullOrWhiteSpace(filterOn) == false &&
string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    allBooks = allBooks.Where(x => x.Title.Contains(filterQuery));
                }
            }

            //sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    allBooks = isAscending ? allBooks.OrderBy(x => x.Title) :
allBooks.OrderByDescending(x => x.Title);
                }
            }
            //pagination 
            var skipResults = (pageNumber - 1) * pageSize;
            return allBooks.Skip(skipResults).Take(pageSize).ToList();
        }
        }
}