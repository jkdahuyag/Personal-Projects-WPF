using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace Dahuyag_Assessment_1.Tests
{
    public class InsertTests
    {
        [Test]
        public void AddToHead_EmptyList_HeadEqualsTail()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<int>();
            int data = 99;
            //act
            ls.AddToHead(data);
            //assert
            int expectedCount = 1;
            ls.Head.Data.Should().Be(data);
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Next.Should().BeNull();
            ls.Head.Prev.Should().BeNull();

            ls.Count.Should().Be(expectedCount);
        }

        [Test]
        public void AddToHead_Given1Node_HeadNotEqualToTailCount2()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<int>();
            int initialNodeData = 99;
            ls.AddToHead(initialNodeData);
            //act
            int newData = 1;
            ls.AddToHead(newData);
            //assert
            int expectedCount = 2;

            ls.Head.Data.Should().Be(newData);
            ls.Tail.Data.Should().Be(initialNodeData);

            ls.Head.Should().NotBe(ls.Tail);

            ls.Head.Next.Should().Be(ls.Tail);
            ls.Head.Prev.Should().BeNull();

            ls.Tail.Prev.Should().Be(ls.Head);
            ls.Tail.Next.Should().BeNull();

            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void AddToTail_EmptyList_HeadEqualsTail()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<int>();
            int data = 99;
            //act
            ls.AddToTail(data);
            //assert
            int expectedCount = 1;
            ls.Head.Data.Should().Be(data);
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Next.Should().BeNull();
            ls.Head.Prev.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }

        [Test]
        public void AddToTail_Given1Node_HeadNotEqualToTailCount2()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<int>();
            int initialNodeData = 99;
            ls.AddToHead(initialNodeData);
            //act
            int newData = 1;
            ls.AddToTail(newData);
            //assert
            int expectedCount = 2;

            ls.Head.Data.Should().Be(initialNodeData);
            ls.Tail.Data.Should().Be(newData);

            ls.Head.Should().NotBe(ls.Tail);

            ls.Head.Next.Should().Be(ls.Tail);
            ls.Head.Prev.Should().BeNull();

            ls.Tail.Prev.Should().Be(ls.Head);
            ls.Tail.Next.Should().BeNull();

            ls.Count.Should().Be(expectedCount);
        }

    }
}
