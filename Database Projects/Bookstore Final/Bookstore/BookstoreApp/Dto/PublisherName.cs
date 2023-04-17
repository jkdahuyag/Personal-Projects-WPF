using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Dto
{
    public class PublisherName
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }

        public PublisherName(int publisherId, string name)
        {
            PublisherId = publisherId;
            Name = name;
        }
    }
}
