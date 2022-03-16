using Domain;

namespace Application.Repositories;

public interface IQuoteRepository
{
    Task<Author?> FindAuthorIncludeQuotesAsync(Guid authorId);
    void DeleteQuoteAsync(Quote quote);
    Task AddQuoteAsync(Quote quote);
    Task SaveQuoteChangesAsync();
}
