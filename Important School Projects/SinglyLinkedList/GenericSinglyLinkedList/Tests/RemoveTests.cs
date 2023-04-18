using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace GenericSinglyLinkedList.Tests
{
    public class RemoveTests
    {
        [Test]
        public void RemoveFromHead_EmptyList_ThrowError()
        {
            //arrange
            var ls = new SinglyLinkedList<Person>();
            //act
            var act = () =>
            {
                ls.RemoveFromHead();
            };
            //assert
            act.Should().Throw<InvalidOperationException>();
        }
        [Test]
        public void RemoveFromHead_List1Node_HeadTailNullCount0()
        {
            //arrange
            var ls = new SinglyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard",19));
            //act
            ls.RemoveFromHead();
            //assert
            int expectedCount = 0;
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void RemoveFromHead_List2Node_HeadTailEqualCount1()
        {
            //arrange
            var ls = new SinglyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            var expectedName = "Bro";
            var expectedAge = 20;
            ls.AddToTail(new Person(expectedName,expectedAge));
            //act
            ls.RemoveFromHead();
            //assert
            int expectedCount = 1;
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Data.Name.Should().Be(expectedName);
            ls.Head.Data.Age.Should().Be(expectedAge);
            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void RemoveFromHead_List3Node_HeadTailNullCount0()
        {
            //arrange
            var ls = new SinglyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            var expectedHeadName = "Bro";
            var expectedHeadAge = 20;
            ls.AddToTail(new Person(expectedHeadName, expectedHeadAge));
            var expectedTailName = "Peacock";
            var expectedTailAge = 7;
            ls.AddToTail(new Person(expectedTailName, expectedTailAge));
            //act
            ls.RemoveFromHead();
            //assert
            int expectedCount = 2;

            ls.Head.Data.Name.Should().Be(expectedHeadName);
            ls.Head.Data.Age.Should().Be(expectedHeadAge);
            ls.Tail.Data.Name.Should().Be(expectedTailName);
            ls.Tail.Data.Age.Should().Be(expectedTailAge);

            ls.Head.Next.Should().Be(ls.Tail);
            ls.Tail.Next.Should().BeNull();
            ls.Count.Should().Be(expectedCount);

        }


        [Test]
        public void RemoveFromTail_EmptyList_ThrowError()
        {
            //arrange
            var ls = new SinglyLinkedList<Person>();
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
            var ls = new SinglyLinkedList<Person>();
            ls.AddToTail(new Person("Jerard", 19));
            //act
            ls.RemoveFromTail();
            //assert
            int expectedCount = 0;
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Should().BeNull();
            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void RemoveFromTail_List2Node_HeadTailEqualCount1()
        {
            //arrange
            var ls = new SinglyLinkedList<Person>();
            ls.AddToHead(new Person("Jerard", 19));
            var expectedName = "Bro";
            var expectedAge = 20;
            ls.AddToHead(new Person(expectedName, expectedAge));
            //act
            ls.RemoveFromTail();
            //assert
            int expectedCount = 1;
            ls.Head.Should().Be(ls.Tail);
            ls.Head.Data.Name.Should().Be(expectedName);
            ls.Head.Data.Age.Should().Be(expectedAge);
            ls.Count.Should().Be(expectedCount);
        }
        [Test]
        public void RemoveFromTail_List3Node_HeadTailNullCount0()
        {
            //arrange
            var ls = new SinglyLinkedList<Person>();
            
            var expectedHeadName = "Bro";
            var expectedHeadAge = 20;
            ls.AddToHead(new Person(expectedHeadName, expectedHeadAge));
            var expectedTailName = "Peacock";
            var expectedTailAge = 7;
            ls.AddToTail(new Person(expectedTailName, expectedTailAge));
            ls.AddToTail(new Person("Jerard", 19));
            //act
            ls.RemoveFromTail();
            //assert
            int expectedCount = 2;
            
            ls.Head.Data.Name.Should().Be(expectedHeadName);
            ls.Head.Data.Age.Should().Be(expectedHeadAge);
            ls.Tail.Data.Name.Should().Be(expectedTailName);
            ls.Tail.Data.Age.Should().Be(expectedTailAge);

            ls.Head.Next.Should().Be(ls.Tail);
            ls.Tail.Next.Should().BeNull();
            ls.Count.Should().Be(expectedCount);

        }
    }
}
