using Application.Repositories;
using Domain;

namespace Application.QuotesMediator.Update;

internal class UpdateQuoteHandler : QuoteBaseHandler<UpdateQuoteCommand, OperationResult<Quote>>
{
    public UpdateQuoteHandler(IQuoteRepository quoteRepository) 
        : base(quoteRepository) { }

    public override async Task<OperationResult<Quote>> Handle(UpdateQuoteCommand request, CancellationToken cancellationToken)
    {
        var (authorId, id, text) = request;
        var author = await _quoteRepository.FindAuthorIncludeQuotesAsync(authorId);
        if (author is null)
            return new(Status.DoesNotExist, null);

        var quote = author.Quotes.SingleOrDefault(q => q.Id == id);
        if (quote is null)
            return new(Status.DoesNotExist, null);

        quote.Text = text;
        await _quoteRepository.SaveQuoteChangesAsync();
        return new(Status.Success, quote);
    }
}
