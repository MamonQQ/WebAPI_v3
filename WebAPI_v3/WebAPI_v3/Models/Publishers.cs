using System.ComponentModel.DataAnnotations;

namespace WebAPI_v3.Models
{
    public class Publishers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
