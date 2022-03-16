namespace Domain;

public sealed class Quote
{
    public Quote(Author author, string text)
    {
        Id = Guid.NewGuid();
        Author = author;
        Text = text;
    }

    private Quote()
    {

    }

    public Guid Id { get; init; }
    public Author Author { get; init; }
    public string Text { get; set; }
}
