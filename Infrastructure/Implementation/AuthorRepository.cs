using Application.Repositories;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task AddAuthorAsync(Author author)
        => _context.Authors.AddAsync(author).AsTask();

    public void DeleteAuthor(Author author)
        => _context.Authors.Remove(author);

    public ValueTask<Author?> GetAuthorByIdAsync(Guid id)
        => _context.Authors.FindAsync(id);

    public Task<Author?> GetAuthorByNameAsync(string name)
        => _context.Authors.FirstOrDefaultAsync(a => a.Name == name);

    public Task<List<Author>> GetAuthorsAsync()
        => _context.Authors.ToListAsync();

    public Task SaveAuthorChangesAsync()
        => _context.SaveChangesAsync();
}
