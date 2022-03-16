using Domain;

namespace Application.Repositories;

public interface IAuthorRepository
{
    Task<List<Author>> GetAuthorsAsync();
    ValueTask<Author?> GetAuthorByIdAsync(Guid id);
    Task<Author?> GetAuthorByNameAsync(string name);
    void DeleteAuthor(Author author);
    Task AddAuthorAsync(Author author);
    Task SaveAuthorChangesAsync();
}
