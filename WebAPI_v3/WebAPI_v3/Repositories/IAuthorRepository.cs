using WebAPI_v3.Models;
using WebAPI_v3.Models.DTO;

namespace WebAPI_v3.Repositories
{
    public interface IAuthorRepository
    {
        List<AuthorDTO> GellAllAuthors();
        AuthorNoIdDTO GetAuthorById(int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
        AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO);
        AuthorDTO? DeleteAuthorById(int id);
    }

}
