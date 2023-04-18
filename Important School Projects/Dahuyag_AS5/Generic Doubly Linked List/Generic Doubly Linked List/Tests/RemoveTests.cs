using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using NUnit.Framework;

namespace GenericDoublyLinkedList.Tests
{
    public class RemoveTests
    {
        #region RemoveFromHeadTests

        [Test]
        public void RemoveFromHead_EmptyList_ThrowError()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            //act
            var act = () =>
            {
                ls.RemoveFromHead();
            };
            //assert
            act.Should().Throw<InvalidOperationException>();
        }
        [Test]
        public void RemoveFromHead_List1Node_CorrectDataHeadTailNullCount0()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            //act
            var expectedData = ls.Head.Data;
            var removedData = ls.RemoveFromHead();
            //assert
            int expectedCount = 0;
            expectedData.Should().Be(removedData);
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void RemoveFromHead_List2Node_CorrectDataHeadTailEqualCount1()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            var expectedName = "Bro";
            var expectedAge = 20;
            ls.AddToTail(new Person(expectedName, expectedAge));
            //act
            var expectedData = ls.Head.Data;
            var removedData = ls.RemoveFromHead();
            //assert
            int expectedCount = 1;
            expectedData.Should().Be(removedData);
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Data.Name.Should().Be(expectedName);
            ls.Head.Data.Age.Should().Be(expectedAge);

            ls.Head.Next.Should().BeNull();
            ls.Head.Prev.Should().BeNull();

            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void RemoveFromHead_List3Node_CorrectDataHeadTailNullCount2()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            var expectedHeadName = "Bro";
            var expectedHeadAge = 20;
            ls.AddToTail(new Person(expectedHeadName, expectedHeadAge));
            var expectedTailName = "Peacock";
            var expectedTailAge = 7;
            ls.AddToTail(new Person(expectedTailName, expectedTailAge));
            //act
            var expectedData = ls.Head.Data;
            var removedData = ls.RemoveFromHead();
            //assert
            int expectedCount = 2;
            expectedData.Should().Be(removedData);
            ls.Head.Data.Name.Should().Be(expectedHeadName);
            ls.Head.Data.Age.Should().Be(expectedHeadAge);
            ls.Tail.Data.Name.Should().Be(expectedTailName);
            ls.Tail.Data.Age.Should().Be(expectedTailAge);

            ls.Head.Next.Should().Be(ls.Tail);
            ls.Tail.Next.Should().BeNull();
            ls.Head.Prev.Should().BeNull();
            ls.Tail.Prev.Should().Be(ls.Head);

            ls.Count.Should().Be(expectedCount);

        }

        #endregion

        #region RemoveFromTailTests
        [Test]
        public void RemoveFromTail_EmptyList_ThrowError()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            //act
            var act = () =>
            {
                ls.RemoveFromTail();
            };
            //assert
            act.Should().Throw<InvalidOperationException>();
        }
        [Test]
        public void RemoveFromTail_List1Node_HeadTailNullCount0()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            //act
            var expectedData = ls.Tail.Data;
            var removedData = ls.RemoveFromTail();
            //assert
            int expectedCount = 0;
            expectedData.Should().Be(removedData);
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void RemoveFromTail_List2Node_HeadTailEqualCount1()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            ls.AddToHead(new Person("Jerard", 19));
            var expectedName = "Bro";
            var expectedAge = 20;
            ls.AddToHead(new Person(expectedName, expectedAge));
            //act
            var expectedData = ls.Tail.Data;
            var removedData = ls.RemoveFromTail();
            //assert
            int expectedCount = 1;
            expectedData.Should().Be(removedData);
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Data.Name.Should().Be(expectedName);
            ls.Head.Data.Age.Should().Be(expectedAge);

            ls.Head.Next.Should().BeNull();
            ls.Head.Prev.Should().BeNull();

            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void RemoveFromTail_List3Node_HeadTailNullCount0()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();

            var expectedHeadName = "Bro";
            var expectedHeadAge = 20;
            ls.AddToHead(new Person(expectedHeadName, expectedHeadAge));
            var expectedTailName = "Peacock";
            var expectedTailAge = 7;
            ls.AddToTail(new Person(expectedTailName, expectedTailAge));
            ls.AddToTail(new Person("Jerard", 19));
            //act
            var expectedData = ls.Tail.Data;
            var removedData = ls.RemoveFromTail();
            //assert
            int expectedCount = 2;
            expectedData.Should().Be(removedData);
            ls.Head.Data.Name.Should().Be(expectedHeadName);
            ls.Head.Data.Age.Should().Be(expectedHeadAge);
            ls.Tail.Data.Name.Should().Be(expectedTailName);
            ls.Tail.Data.Age.Should().Be(expectedTailAge);

            ls.Head.Next.Should().Be(ls.Tail);
            ls.Tail.Next.Should().BeNull();

            ls.Head.Prev.Should().BeNull();
            ls.Tail.Prev.Should().Be(ls.Head);

            ls.Count.Should().Be(expectedCount);
        }

        #endregion

        #region RemoveNodeTests

        [Test]
        public void Remove_EmptyList_ThrowEmptyListError()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<string>();
            //act
            var act = () =>
            {
                ls.Remove(0);
            };
            //assert
            act.Should().Throw<InvalidOperationException>();
        }
        [TestCase(33)]
        [TestCase(7)]
        [TestCase(11)]
        [TestCase(-7)]
        public void Remove_OutOfRange_ThrowOutOfBoundsError(int index)
        {
            //arrange
            var ls = new GenericDoublyLinkedList<string>();
            ls.AddToHead("weewoo");
            ls.AddToHead("Air");
            ls.AddToHead("Wobble");
            ls.AddToHead("Play");
            //act
            var act = () =>
            {
                ls.Remove(index);
            };
            //assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Remove_Given1Node_RemovedNodeAndCount0()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<string>();
            ls.AddToHead("Jerard");
            int index = 0;
            //act
            ls.Remove(index);
            //assert
            int expectedCount = 0;
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Remove_Given2Node_RemovedNodeAndCount1()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<string>();
            string expectedData = "Jerard";
            ls.AddToHead("Not Me");
            ls.AddToTail(expectedData);
            int index = 0;
            //act
            ls.Remove(index);
            //assert
            int expectedCount = 1;
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Data.Should().Be(expectedData);
            ls.Head.Next.Should().BeNull();
            ls.Head.Prev.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Remove_Given3Node_RemovedMiddleNodeAndCount2()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            var expectedHeadName = "Bro";
            var expectedHeadAge = 20;
            ls.AddToHead(new Person(expectedHeadName, expectedHeadAge));
            var expectedTailName = "Peacock";
            var expectedTailAge = 7;
            ls.AddToTail(new Person(expectedTailName, expectedTailAge));
            int indexToRemove = 1;
            //act
            ls.Remove(indexToRemove);
            //assert
            int expectedCount = 2;
            ls.Head.Data.Name.Should().Be(expectedHeadName);
            ls.Head.Data.Age.Should().Be(expectedHeadAge);
            ls.Tail.Data.Name.Should().Be(expectedTailName);
            ls.Tail.Data.Age.Should().Be(expectedTailAge);

            ls.Head.Next.Should().Be(ls.Tail);
            ls.Tail.Next.Should().BeNull();
            ls.Head.Prev.Should().BeNull();
            ls.Tail.Prev.Should().Be(ls.Head);

            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Remove_Given3Node_RemovedHeadAndCount2()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            var expectedHeadName = "Bro";
            var expectedHeadAge = 20;
            ls.AddToTail(new Person(expectedHeadName, expectedHeadAge));
            var expectedTailName = "Peacock";
            var expectedTailAge = 7;
            ls.AddToTail(new Person(expectedTailName, expectedTailAge));
            int indexToRemove = 0;
            //act
            ls.Remove(indexToRemove);
            //assert
            int expectedCount = 2;
            ls.Head.Data.Name.Should().Be(expectedHeadName);
            ls.Head.Data.Age.Should().Be(expectedHeadAge);
            ls.Tail.Data.Name.Should().Be(expectedTailName);
            ls.Tail.Data.Age.Should().Be(expectedTailAge);

            ls.Head.Next.Should().Be(ls.Tail);
            ls.Tail.Next.Should().BeNull();
            ls.Head.Prev.Should().BeNull();
            ls.Tail.Prev.Should().Be(ls.Head);

            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Remove_Given3Node_RemovedTailAndCount2()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            var expectedTailName = "Bro";
            var expectedTailAge = 20;
            var expectedHeadName = "Peacock";
            var expectedHeadAge = 7;
            ls.AddToHead(new Person(expectedTailName, expectedTailAge));
            ls.AddToHead(new Person(expectedHeadName, expectedHeadAge));
            int indexToRemove = 2;
            //act
            ls.Remove(indexToRemove);
            //assert
            int expectedCount = 2;
            ls.Head.Data.Name.Should().Be(expectedHeadName);
            ls.Head.Data.Age.Should().Be(expectedHeadAge);
            ls.Tail.Data.Name.Should().Be(expectedTailName);
            ls.Tail.Data.Age.Should().Be(expectedTailAge);

            ls.Head.Next.Should().Be(ls.Tail);
            ls.Tail.Next.Should().BeNull();
            ls.Head.Prev.Should().BeNull();
            ls.Tail.Prev.Should().Be(ls.Head);

            ls.Count.Should().Be(expectedCount);
        }
        [TestCase(3)]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(2)]
        [TestCase(6)]
        [TestCase(7)]
        public void Remove_ValidPosition_RemovedNodeCountCorrectFlow(int index)
        {
            //arrange
            var ls = new GenericDoublyLinkedList<string>();
            ls.AddToHead("weewoo");
            ls.AddToHead("Air");
            ls.AddToHead("Wobble");
            ls.AddToHead("Play");
            ls.AddToHead("weewoo");
            ls.AddToHead("Air");
            ls.AddToHead("Wobble");
            ls.AddToHead("Play");
            int testCount = 1;
            var getTail = () =>
            {
                var tmp = ls.Head;
                while (tmp != ls.Tail)
                {
                    testCount++;
                    tmp = tmp.Next;
                }

                return tmp;
            };
            var getHead = () =>
            {
                var tmp = ls.Tail;
                while (tmp != ls.Head)
                {
                    testCount++;
                    tmp = tmp.Prev;
                }

                return tmp;
            };
            //act
            ls.Remove(index);
            //assert
            var lastNode = getTail();
            testCount = 1;
            var firstNode = getHead();
            int expectedCount = 7;

            lastNode.Should().Be(ls.Tail);
            firstNode.Should().Be(ls.Head);
            testCount.Should().Be(expectedCount);

            ls.Head.Prev.Should().BeNull();
            ls.Tail.Next.Should().BeNull();

            ls.Count.Should().Be(expectedCount);
        }
        #endregion
    }
}
