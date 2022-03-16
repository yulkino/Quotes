using Domain;
using MediatR;

namespace Application.QuotesMediator.Add;

public record AddQuoteCommand(Guid AuthorId, string Text) : IRequest<OperationResult<Quote>>;
