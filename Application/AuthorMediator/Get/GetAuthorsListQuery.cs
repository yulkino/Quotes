using Domain;
using MediatR;

namespace Application.AuthorMediator.Get;

public record GetAuthorsListQuery : IRequest<OperationResult<List<Author>>>;
