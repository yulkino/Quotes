using Application.Repositories;
using Domain;

namespace Application.QuotesMediator.Add;

internal class AddQuoteHandler : QuoteBaseHandler<AddQuoteCommand, OperationResult<Quote>>
{
    private readonly IAuthorRepository _authorRepository;

    public AddQuoteHandler(IQuoteRepository quoteRepository, IAuthorRepository authorRepository) 
        : base(quoteRepository)
    {
        _authorRepository = authorRepository;
    }

    public override async Task<OperationResult<Quote>> Handle(AddQuoteCommand request, CancellationToken cancellationToken)
    {
        var (authorId, text) = request;
        var author = await _authorRepository.GetAuthorByIdAsync(authorId);
        if (author is null)
            return new(Status.DoesNotExist, null);

        var quote = new Quote(author, text);
        await _quoteRepository.AddQuoteAsync(quote);
        await _quoteRepository.SaveQuoteChangesAsync();
        return new(Status.SuccessCreated, quote);
    }
}
