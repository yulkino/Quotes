using Domain;
using MediatR;

namespace Application.QuotesMediator.Get;

public record GetQuotesListQuery(Guid AuthorId) : IRequest<OperationResult<IEnumerable<Quote>>>;
