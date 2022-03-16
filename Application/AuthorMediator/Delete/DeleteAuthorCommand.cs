using MediatR;

namespace Application.AuthorMediator.Delete;

public record DeleteAuthorCommand(Guid Id) : IRequest<Status>;