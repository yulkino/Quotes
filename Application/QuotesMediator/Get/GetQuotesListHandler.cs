using Application.Repositories;
using Domain;

namespace Application.QuotesMediator.Get;

internal class GetQuotesListHandler : QuoteBaseHandler<GetQuotesListQuery, OperationResult<IEnumerable<Quote>>>
{
    public GetQuotesListHandler(IQuoteRepository quoteRepository) 
        : base(quoteRepository) { }

    public override async Task<OperationResult<IEnumerable<Quote>>> Handle(GetQuotesListQuery request, CancellationToken cancellationToken)
    {
        var author = await _quoteRepository.FindAuthorIncludeQuotesAsync(request.AuthorId);
        if (author is null)
            return new(Status.DoesNotExist, null);

        var quotes = author.Quotes;
        return new(Status.Success, quotes);
    }
}
