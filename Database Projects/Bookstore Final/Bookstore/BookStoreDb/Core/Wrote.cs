using System.ComponentModel.DataAnnotations;

namespace BookStoreDb.Core;

public class Wrote
{
    [Key]
    public int BookAuthorId { get; set; }
    public float RoyaltyRate { get; set; }

    // Links
    public int AuthorId { get; set; }
    public Author AuthorLink { get; set; }

    public int BookId { get; set; }
    public Book BookLink { get; set; }
}