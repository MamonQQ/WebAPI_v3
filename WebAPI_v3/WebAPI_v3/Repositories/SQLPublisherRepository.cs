using Microsoft.EntityFrameworkCore;
using WebAPI_v3.Data;
using WebAPI_v3.Models;
using WebAPI_v3.Models.DTO;
using WebAPI_v3.Repositories;
namespace WebAPI_simple.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<PublisherDTO> GetAllPublishers()
        {
            //Get Data From Database -Domain Model
            var allPublishersDomain = _dbContext.Publishers.ToList();
            //Map domain models to DTOs
            var allPublisherDTO = new List<PublisherDTO>();
            foreach (var publisherDomain in allPublishersDomain)
            {
                allPublisherDTO.Add(new PublisherDTO()
                {
                    Id = publisherDomain.ID,
                    Name = publisherDomain.Name
                });
            }
            return allPublisherDTO;
        }
        public PublisherNoIdDTO GetPublisherById(int id)
        {
            // get book Domain model from Db
            var publisherWithIdDomain = _dbContext.Publishers.FirstOrDefault(x => x.ID ==
           id);
            if (publisherWithIdDomain != null)
            { //Map Domain Model to DTOs
                var publisherNoIdDTO = new PublisherNoIdDTO
                {
                    Name = publisherWithIdDomain.Name,
                };
                return publisherNoIdDTO;
            }
            return null;
        }
        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO
        addPublisherRequestDTO)
        {
            var publisherDomainModel = new Publishers
            {
                Name = addPublisherRequestDTO.Name,
            };
            //Use Domain Model to create Book
            _dbContext.Publishers.Add(publisherDomainModel);
            _dbContext.SaveChanges();
            return addPublisherRequestDTO;
        }
        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO
       publisherNoIdDTO)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.ID == id);
            if (publisherDomain != null)
            {
                publisherDomain.Name = publisherNoIdDTO.Name;
                _dbContext.SaveChanges();
            }
            return null;
        }
        public Publishers? DeletePublisherById(int id)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.ID == id);
            if (publisherDomain != null)
            {
                _dbContext.Publishers.Remove(publisherDomain);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}