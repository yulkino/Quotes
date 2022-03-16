using Application.Repositories;

namespace Application.QuotesMediator.Delete;

internal class DeleteQuoteHandler : QuoteBaseHandler<DeleteQuoteCommand, Status>
{
    public DeleteQuoteHandler(IQuoteRepository quoteRepository) 
        : base(quoteRepository) { }

    public override async Task<Status> Handle(DeleteQuoteCommand request, CancellationToken cancellationToken)
    {
        var (authorId, id) = request;
        var author = await _quoteRepository.FindAuthorIncludeQuotesAsync(authorId);
        if (author is null)
            return Status.DoesNotExist;

        var quote = author.Quotes.SingleOrDefault(q => q.Id == id);
        if (quote is null)
            return Status.DoesNotExist;

        _quoteRepository.DeleteQuoteAsync(quote);
        await _quoteRepository.SaveQuoteChangesAsync();
        return Status.OkWithoutContent;
    }
}
