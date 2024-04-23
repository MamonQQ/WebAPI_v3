using System.ComponentModel.DataAnnotations;

namespace WebAPI_v3.Models
{
    public class Book_Author
    {
        [Key]
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
        public Authors Author { get; set; }
    }
}
