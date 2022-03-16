using Domain;
using MediatR;

namespace Application.AuthorMediator.Add;

public record AddAuthorCommand(string Name) : IRequest<OperationResult<Author>>;
