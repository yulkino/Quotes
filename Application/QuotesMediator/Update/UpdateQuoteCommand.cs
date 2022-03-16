using Domain;
using MediatR;

namespace Application.QuotesMediator.Update;

public record UpdateQuoteCommand(Guid AuthorId, Guid Id, string Text) : IRequest<OperationResult<Quote>>;
