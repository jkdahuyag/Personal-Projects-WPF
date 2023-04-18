using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace SinglyLinkedList.Tests
{
    public class LinkedListTest
    {
        [Test]
        public void Constructor_NoGivenData_HeadAndTailShouldBeNull()
        {
            //arrange
            //act
            var singlyLinkedList = new SinglyLinkedList();
            //assert
            singlyLinkedList.Tail.Should().BeNull();
            singlyLinkedList.Head.Should().BeNull();
            singlyLinkedList.Count.Should().Be(0);
        }
        [Test]
        public void AddToHead_EmptyList_HeadAndTailShouldBeEqualToNode()
        {
            //arrange
            var singlyLinkedList = new SinglyLinkedList();
            string data = "hello";

            const int FINALCOUNT = 1;
            //act
            singlyLinkedList.AddToHead(data);
            //assert
            singlyLinkedList.Tail.Data.Should().Be(data);
            singlyLinkedList.Head.Data.Should().Be(data);
            singlyLinkedList.Head.Next.Should().BeNull();
            singlyLinkedList.Count.Should().Be(FINALCOUNT);
        }
        [Test]
        public void AddToTail_EmptyList_HeadAndTailShouldBeEqualToNode()
        {
            //arrange
            var singlyLinkedList = new SinglyLinkedList();
            string data = "hello";
            const int FINALCOUNT = 1;

            //act
            singlyLinkedList.AddToTail(data);
            //assert
            singlyLinkedList.Tail.Data.Should().Be(data);
            singlyLinkedList.Head.Data.Should().Be(data);
            singlyLinkedList.Head.Next.Should().BeNull();
            singlyLinkedList.Count.Should().Be(FINALCOUNT);
        }
        [Test]
        public void AddToTail_OneInitialNode_TailEqualsNewNode()
        {
            //arrange
            var singlyLinkedList = new SinglyLinkedList();
            string data = "hello";
            string data2 = "hi";
            const int FINALCOUNT = 2;
            singlyLinkedList.AddToTail(data);
            //act
            singlyLinkedList.AddToTail(data2);
            //assert
            singlyLinkedList.Tail.Data.Should().Be(data2);
            singlyLinkedList.Head.Data.Should().Be(data);
            singlyLinkedList.Head.Next.Data.Should().Be(data2);
            singlyLinkedList.Count.Should().Be(FINALCOUNT);
        }
        [Test]
        public void AddToHead_OneInitialNode_HeadEqualsNewNode()
        {
            //arrange
            var singlyLinkedList = new SinglyLinkedList();
            string data = "hello";
            string data2 = "hi";
            const int FINALCOUNT = 2;
            singlyLinkedList.AddToHead(data2);
            //act
            singlyLinkedList.AddToHead(data);
            //assert
            singlyLinkedList.Tail.Data.Should().Be(data2);
            singlyLinkedList.Head.Data.Should().Be(data);
            singlyLinkedList.Head.Next.Data.Should().Be(data2);
            singlyLinkedList.Count.Should().Be(FINALCOUNT);
        }
        [TestCase (2)]
        [TestCase (5)]
        [TestCase (4)]
        public void AddToHead_MoreThanOneInitialNode_HeadEqualsNewNode(int initialNodes)
        {
            //arrange
            var singlyLinkedList = new SinglyLinkedList();
            string baseData = "hello";
            int FINALCOUNT = initialNodes + 1;
            for (int i = 1; i < initialNodes; i++)
            {
                singlyLinkedList.AddToHead(baseData + i);
            }
        }
    }
}
