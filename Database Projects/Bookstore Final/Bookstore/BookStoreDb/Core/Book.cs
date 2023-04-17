using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDb.Core;

public class Book
{
    [Key]
    public int BookId { get; set; }
    public string Isbn { get; set; }

    [MaxLength(100)]
    public string Title { get; set; }
    public float Price { get; set; }

    public DateTime DatePublished { get; set; }
    public int NumPages { get; set; }
    public int DaysOld { get; set; }

    // Links

    // has one publisher
    public int PublisherId { get; set; }
    public Publisher PublisherLink { get; set; }

    // has many wrotes
    public ICollection<Wrote> Wrotes { get; set; }
}