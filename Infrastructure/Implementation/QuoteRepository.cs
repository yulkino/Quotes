using Application.Repositories;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation;

public class QuoteRepository : IQuoteRepository
{
    private readonly ApplicationDbContext _context;

    public QuoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task AddQuoteAsync(Quote quote)
        => _context.Quotes.AddAsync(quote).AsTask();

    public void DeleteQuoteAsync(Quote quote)
        => _context.Quotes.Remove(quote);

    public Task<Author?> FindAuthorIncludeQuotesAsync(Guid authorId)
        => _context.Authors
            .Include(a => a.Quotes)
            .SingleOrDefaultAsync(a => a.Id == authorId);

    public Task SaveQuoteChangesAsync()
        => _context.SaveChangesAsync();
}
