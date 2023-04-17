using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreDb.Core;

namespace BookstoreApp.Dto
{
    public class AuthorName
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

        public AuthorName(int authorId, string name)
        {
            AuthorId = authorId;
            Name = name;
        }
    }

    public class AuthorDetails
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public List<Price> RoyaltyRates { get; set; } = new();

        public string RoyaltyInString { get; set; }

        public AuthorDetails(Author author)
        {
            AuthorId = author.AuthorId;
            Name = author.Name;
            Address = author.Address;
            RoyaltyRates.Clear();

            foreach (var wrote in author.Wrotes)
            {
                RoyaltyRates.Add(new Price(wrote.RoyaltyRate));
            }

            if (RoyaltyRates.Count != 0)
            {
                RoyaltyInString = RoyaltyRates[0].Data;
            }
            else
            {
                RoyaltyInString = "50.00";
            }
        }
    }
}
