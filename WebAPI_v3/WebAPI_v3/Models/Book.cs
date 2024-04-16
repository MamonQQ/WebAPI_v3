using System.ComponentModel.DataAnnotations;

namespace WebAPI_v3.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }   
        public string Descriotion { get; set; }
        public bool IsRead { get; set; }
        public int Rate { get; set; }
        public int Genre { get; set; }
        public string CoverUrl { get; set; }
        public DateTime? DateAdded { get; set; }
        
    }
}
