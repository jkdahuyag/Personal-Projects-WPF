﻿namespace BookStoreDb.Core;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    // Links
    public ICollection<Wrote> Wrotes { get; set; }
}