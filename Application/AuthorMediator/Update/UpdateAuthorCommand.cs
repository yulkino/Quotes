using Domain;
using MediatR;

namespace Application.AuthorMediator.Update;

public record UpdateAuthorCommand(Guid Id, string NewName) : IRequest<OperationResult<Author>>;
