using MediatR;

namespace Application.QuotesMediator.Delete;

public record DeleteQuoteCommand(Guid AuthorId, Guid Id) : IRequest<Status>;
