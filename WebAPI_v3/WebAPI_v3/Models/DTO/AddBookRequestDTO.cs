using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPI_v3.Repositories;

namespace WebAPI_v3.Models.DTO
{
    public class AddBookRequestDTO
    {
        [Required]
        [MinLength(10)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool isRead { get; set; }
        public DateTime? DateRead { get; set; }
        [Range(0,5,ErrorMessage = "From 0 to 5")]
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public int PublisherID { get; set; }
        public List<int> AuthorIDs { get; set; }


    }
    

}
