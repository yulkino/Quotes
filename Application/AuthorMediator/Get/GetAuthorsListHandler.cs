using Application.Repositories;
using Domain;

namespace Application.AuthorMediator.Get;

internal sealed class GetAuthorsListHandler : AuthorBaseHandler<GetAuthorsListQuery, OperationResult<List<Author>>>
{
    public GetAuthorsListHandler(IAuthorRepository authorRepository) 
        : base(authorRepository) { }

    public override async Task<OperationResult<List<Author>>> Handle(GetAuthorsListQuery request, CancellationToken cancellationToken)
        => new(Status.Success, await _authorRepository.GetAuthorsAsync());
}
