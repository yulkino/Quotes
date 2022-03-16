using Application.Repositories;
using Domain;

namespace Application.AuthorMediator.Add;

internal class AddAuthorHandler : AuthorBaseHandler<AddAuthorCommand, OperationResult<Author>>
{
    public AddAuthorHandler(IAuthorRepository authorRepository) 
        : base(authorRepository) { }

    public override async Task<OperationResult<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var name = request.Name;
        if (name is "")
            return new(Status.BadInput, null);
        var existingAuthor = await _authorRepository.GetAuthorByNameAsync(name);
        if (existingAuthor is not null)
            return new(Status.KeyIsOccupied, null);

        var author = new Author(name);
        await _authorRepository.AddAuthorAsync(author);
        await _authorRepository.SaveAuthorChangesAsync();
        return new(Status.SuccessCreated, author);
    }
}
