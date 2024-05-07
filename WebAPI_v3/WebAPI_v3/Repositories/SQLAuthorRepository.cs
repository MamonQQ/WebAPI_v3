using WebAPI_v3.Data;
using WebAPI_v3.Models.DTO;
using WebAPI_v3.Models;

namespace WebAPI_v3.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLAuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AuthorDTO> GellAllAuthors()
        {
            //Get Data From Database -Domain Model 
            var allAuthorsDomain = _dbContext.Authors.ToList();
            //Map domain models to DTOs 
            var allAuthorDTO = new List<AuthorDTO>();
            foreach (var authorDomain in allAuthorsDomain)
            {
                allAuthorDTO.Add(new AuthorDTO()
                {
                    Id = authorDomain.AuthorID,
                    FullName = authorDomain.FullName
                });
            }
            //return DTOs 
            return allAuthorDTO;
        }
        public AuthorNoIdDTO GetAuthorById(int id)
        {
            // get book Domain model from Db
            var authorWithIdDomain = _dbContext.Authors.FirstOrDefault(x => x.AuthorID ==
           id);
            if (authorWithIdDomain == null)
            {
                return null;
            }
            //Map Domain Model to DTOs 
            var authorNoIdDTO = new AuthorNoIdDTO
            {
                FullName = authorWithIdDomain.FullName,
            };
            return authorNoIdDTO;
        }
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorDomainModel = new Models.Authors
            {
                FullName = addAuthorRequestDTO.FullName,
            };
            //Use Domain Model to create Author 
            _dbContext.Authors.Add(authorDomainModel);
            _dbContext.SaveChanges();
            return addAuthorRequestDTO;
        }
        public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.AuthorID == id);
            if (authorDomain != null)
            {
                authorDomain.FullName = authorNoIdDTO.FullName;
                _dbContext.SaveChanges();
            }
            return authorNoIdDTO;
        }
        public AuthorDTO? DeleteAuthorById(int id)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.AuthorID == id);
            if (authorDomain != null)
            {
                _dbContext.Authors.Remove(authorDomain);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}

        
