using Application.Repositories;
using Domain;

namespace Application.AuthorMediator.Update;

internal class UpdateAuthorHandler : AuthorBaseHandler<UpdateAuthorCommand, OperationResult<Author>>
{
    public UpdateAuthorHandler(IAuthorRepository authorRepository) 
        : base(authorRepository) { }

    public override async Task<OperationResult<Author>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var (id, newName) = request;
        var author = await _authorRepository.GetAuthorByIdAsync(id);
        if (author is null)
            return new(Status.DoesNotExist, null);

        var existingAuthor = await _authorRepository.GetAuthorByNameAsync(newName);
        if (existingAuthor is not null)
            return new(Status.KeyIsOccupied, null);

        author.Name = newName;
        await _authorRepository.SaveAuthorChangesAsync();
        return new(Status.Success, author);
    }
}
