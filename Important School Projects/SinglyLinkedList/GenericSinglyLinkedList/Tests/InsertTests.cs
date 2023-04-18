using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace GenericSinglyLinkedList.Tests
{
    public class InsertTests
    {
        [Test]
        public void AddToHead_EmptyList_HeadEqualsTail()
        {
            //arrange
            var ls = new SinglyLinkedList<int>();
            int data = 99;
            //act
            ls.AddToHead(data);
            //assert
            int expectedCount = 1;
            ls.Head.Data.Should().Be(data); 
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Next.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }

        [Test]
        public void AddToHead_Given1Node_HeadNotEqualToTailCount2()
        {
            //arrange
            var ls = new SinglyLinkedList<int>();
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
            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void AddToTail_EmptyList_HeadEqualsTail()
        {
            //arrange
            var ls = new SinglyLinkedList<int>();
            int data = 99;
            //act
            ls.AddToTail(data);
            //assert
            int expectedCount = 1;
            ls.Head.Data.Should().Be(data);
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Next.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }

        [Test]
        public void AddToTail_Given1Node_HeadNotEqualToTailCount2()
        {
            //arrange
            var ls = new SinglyLinkedList<int>();
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
            ls.Tail.Next.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }

    }
}
