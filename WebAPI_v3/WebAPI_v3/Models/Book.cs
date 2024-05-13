using System.ComponentModel.DataAnnotations;

namespace WebAPI_v3.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }


        public int PublishersID { get; set; }
        public Publishers Publishers {  set; get; }
        public List<Book_Author> Book_Authors { get; set; }

    }

}
