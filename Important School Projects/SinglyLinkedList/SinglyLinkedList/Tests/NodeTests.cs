using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace SinglyLinkedList.Tests
{
    public class NodeTests
    {
        [Test]
        public void Constructor_GivenData_EqualData()
        {
            //arrange
            //act
            string data = "hello";
            var node = new Node(data);
            //assert
            node.Data.Should().Be(data);
            node.Next.Should().BeNull();
        }
    }
}
