using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebAPI_v3.Models;

namespace WebAPI_v3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextoptions) : base(dbContextoptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book_Author>()
				.HasOne(b => b.Book)
				.WithMany(ba => ba.Book_Authors)
				.HasForeignKey(bi => bi.BookID);
            modelBuilder.Entity<Book_Author>().HasOne(b => b.Author).WithMany(ba => ba.Book_Authors).HasForeignKey(bi => bi.AuthorID);
		}
        public DbSet<Book> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Book_Author> Book_Authors { get; set; }
        public DbSet<Publishers> Publishers { get; set; }

        
    }
}
