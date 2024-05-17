using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_v3.Models
{
    public class Image
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }

    }
}
