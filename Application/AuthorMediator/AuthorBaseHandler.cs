using Application.Repositories;
using MediatR;

namespace Application.AuthorMediator;

internal abstract class AuthorBaseHandler<TRequest, TResponse> 
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly IAuthorRepository _authorRepository;

    public AuthorBaseHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
