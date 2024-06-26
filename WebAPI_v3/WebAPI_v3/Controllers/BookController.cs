﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;
using WebAPI_v3.CustomActionFilter;
using WebAPI_v3.Data;
using WebAPI_v3.Models;
using WebAPI_v3.Models.DTO;
using WebAPI_v3.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI_v3.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class BookController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookController> _logger;
        public BookController(AppDbContext dbContext, IBookRepository bookRepository,
ILogger<BookController> logger)
        {
            _dbContext = dbContext;
            _bookRepository = _bookRepository;
            _logger = logger;

        }
        [HttpGet("get-all-books")]
        [Authorize(Roles ="Read")]
        public IActionResult GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,[FromQuery] string? sortBy, [FromQuery] bool isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            _logger.LogInformation("GetAll Book Action method was invoked");
            _logger.LogWarning("This is a warning log");
            _logger.LogError("This is a error log");
            // su dung reposity pattern  
            var allBooks = _bookRepository.GetAllBooks(filterOn, filterQuery, sortBy,isAscending, pageNumber, pageSize);
            _logger.LogInformation($"Finished GetAllBook request with data { JsonSerializer.Serialize(allBooks)}");
            return Ok(allBooks);
            /*
            //var allBooksDomain = _dbContext.Books.ToList();
            var allBooksDomain = _dbContext.Books;
            var allBooksDTO = allBooksDomain.Select(Books => new BookWithAuthorAndPublisherDTO(){
                
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
            return Ok(allBooksDTO);
            */
        }
        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var bookWithIdDTO = _bookRepository.GetBookById(id);
            return Ok(bookWithIdDTO);
            /*
            //get book domain model from db
            var bookWithDomain = _dbContext.Books.Where(n => n.Id == id);
            if (bookWithDomain == null)
            {
                return NotFound();
            }
            //map domain model to DTO
            var bookWithIdDTO = bookWithDomain.Select(Books => new BookWithAuthorAndPublisherDTO
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
            });
            return Ok(bookWithIdDTO);
            */
        }
        [HttpPost("AddBook")]
        public IActionResult Addbook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
            return Ok(bookAdd);
            /*
            //map DTO to domain model
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
                PublishersID = addBookRequestDTO.PublisherID,
            };
            //use domain model to add to create book
            _dbContext.Books.Add(bookDomainModel);
            _dbContext.SaveChanges();
            foreach (var id in addBookRequestDTO.AuthorIDs)
            {
                var bookAuthor = new Book_Author()
                {
                    BookID = bookDomainModel.Id,
                    AuthorID = id
                };
                _dbContext.Book_Authors.Add(bookAuthor);
                _dbContext.SaveChanges();
            }

            return Ok();
            */
        }
            
        //update book by id
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById([FromRoute] int id, [FromBody] AddBookRequestDTO BookDTO)
        {
            var updateBook = _bookRepository.UpdateBookById(id, BookDTO);
            return Ok(updateBook);
            /*
            //get book domain model from db
            var bookDomain = _dbContext.Books.Where(n => n.Id == id).FirstOrDefault();
            if (bookDomain == null)
            {
                return NotFound();
            }
            //map DTO to domain model
            bookDomain.Title = BookDTO.Title;
            bookDomain.Description = BookDTO.Description;
            bookDomain.isRead = BookDTO.isRead;
            bookDomain.DateRead = BookDTO.DateRead;
            bookDomain.Rate = BookDTO.Rate;
            bookDomain.Genre = BookDTO.Genre;
            bookDomain.CoverUrl = BookDTO.CoverUrl;
            bookDomain.DateAdded = BookDTO.DateAdded;
            bookDomain.PublishersID = BookDTO.PublisherID;
            _dbContext.SaveChanges();
            //delete all book authors
            var authorsDomain = _dbContext.Book_Authors.Where(n => n.BookID == id).ToList();
            if (authorsDomain != null)
            {
                _dbContext.Book_Authors.RemoveRange(authorsDomain);
                _dbContext.SaveChanges();
            }

            //add new book authors
            foreach (var authorId in BookDTO.AuthorIDs)
            {
                var bookAuthor = new Book_Author()
                {
                    BookID = bookDomain.Id,
                    AuthorID = authorId
                };
                _dbContext.Book_Authors.Add(bookAuthor);
                _dbContext.SaveChanges();
            }
            return Ok(BookDTO);
            */
        }
        //delete book by id
        [HttpDelete("delete-book-by-id/{id}")]
        [Authorize(Roles ="Write")]
        public IActionResult DeleteBookById([FromRoute] int id)
        {
            var deleteBook = _bookRepository.DeleteBookById(id);
            return Ok(deleteBook);
            /*
            //get book domain model from db
            var bookDomain = _dbContext.Books.Where(n => n.Id == id).FirstOrDefault();
            if (bookDomain != null)
            {
                //delete book
                _dbContext.Books.Remove(bookDomain);
                _dbContext.SaveChanges();
            }

            return Ok();
            */
        }

        [HttpPost("and-book")]
        [ValidateModel]
        [Authorize(Roles = "Write")]
        public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            //validate request 
            if (!ValidateAddBook(addBookRequestDTO))
            {
                return BadRequest(ModelState);
            }
            // before add data 
            if (ModelState.IsValid)
            {
                var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
                return Ok(bookAdd);
            }
            return BadRequest(ModelState);

        }
        
        #region Private methods 
        private bool ValidateAddBook(AddBookRequestDTO addBookRequestDTO)
        {
            if (addBookRequestDTO == null)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO), $"Please add book data"); 
                return false;
            }
            // kiem tra Description NotNull 
            if (string.IsNullOrEmpty(addBookRequestDTO.Description))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Description),$"{nameof(addBookRequestDTO.Description)} cannot be null");
            }
            // kiem tra rating (0,5) 
            if (addBookRequestDTO.Rate < 0 || addBookRequestDTO.Rate > 5)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Rate),$"{nameof(addBookRequestDTO.Rate)} cannot be less than 0 and more than 5");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion

       
    }

}

