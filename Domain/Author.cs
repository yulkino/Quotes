namespace Domain;

public sealed class Author
{
    public Author(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Quotes = new List<Quote>();
    }

    public Guid Id { get; init; }
    public string Name { get; set; }

    public IEnumerable<Quote> Quotes { get; init; }
}
