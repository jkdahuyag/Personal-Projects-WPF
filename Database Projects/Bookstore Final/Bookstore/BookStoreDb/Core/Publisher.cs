namespace BookStoreDb.Core;

public class Publisher
{
    public int PublisherId { get; set; }
    public string Name { get; set; }

    // Links
    public ICollection<Book> Books { get; set; }
}