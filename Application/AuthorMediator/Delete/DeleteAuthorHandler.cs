using Application.Repositories;

namespace Application.AuthorMediator.Delete;

internal class DeleteAuthorHandler : AuthorBaseHandler<DeleteAuthorCommand, Status>
{
    public DeleteAuthorHandler(IAuthorRepository authorRepository) 
        : base(authorRepository) { }

    public override async Task<Status> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorByIdAsync(request.Id);
        if (author is null)
            return Status.DoesNotExist;

        _authorRepository.DeleteAuthor(author);
        await _authorRepository.SaveAuthorChangesAsync();
        return Status.OkWithoutContent;
    }
}
