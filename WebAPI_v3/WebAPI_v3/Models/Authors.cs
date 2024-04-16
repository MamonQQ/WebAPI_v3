using System.ComponentModel.DataAnnotations;

namespace WebAPI_v3.Models
{
    public class Authors
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
