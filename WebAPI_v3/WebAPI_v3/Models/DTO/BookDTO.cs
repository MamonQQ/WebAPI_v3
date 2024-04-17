namespace WebAPI_v3.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public int Genre { get; set; }
        public string CoverUrl { get; set; }
        public string? DateAdded { get; set; }
        public string PublishersName { get; set; }
        public List<string> AuthorNames { get; set; }
    }
}
