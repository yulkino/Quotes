using Application.Repositories;
using MediatR;

namespace Application.QuotesMediator;

internal abstract class QuoteBaseHandler<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    protected readonly IQuoteRepository _quoteRepository;

    public QuoteBaseHandler(IQuoteRepository quoteRepository)
    {
        _quoteRepository = quoteRepository;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
