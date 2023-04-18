using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace GenericDoublyLinkedList.Tests
{
    public class SwapTests
    {
        [Test]
        public void Swap_EmptyList_ThrowError()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            //act
            var act = () =>
            {
                ls.Swap();
            };
            //assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Swap_Given2Nodes_SwapHeadAndTail()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            string initialHeadName = "Jerard";
            int initialHeadAge = 20;
            string initialTailName = "Moore";
            int initialTailAge = 80;
            ls.AddToHead(new Person(initialHeadName,initialHeadAge));
            ls.AddToTail(new Person(initialTailName,initialTailAge));
            //act
            ls.Swap();
            //assert
            ls.Head.Next.Should().Be(ls.Tail);
            ls.Head.Prev.Should().BeNull();
            ls.Tail.Next.Should().BeNull();
            ls.Tail.Prev.Should().Be(ls.Head);
            ls.Head.Data.Name.Should().Be(initialTailName);
            ls.Head.Data.Age.Should().Be(initialTailAge);
            ls.Tail.Data.Name.Should().Be(initialHeadName);
            ls.Tail.Data.Age.Should().Be(initialHeadAge);
        }
        [Test]
        public void Swap_Given3Nodes_SwapHeadAndTail()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            string initialHeadName = "Jerard";
            int initialHeadAge = 20;
            string initialTailName = "Moore";
            int initialTailAge = 80;
            string middleName = "Middle";
            int middleAge = 22;
            ls.AddToHead(new Person(initialHeadName, initialHeadAge));
            ls.AddToTail(new Person(middleName, middleAge));
            ls.AddToTail(new Person(initialTailName, initialTailAge));
            //act
            ls.Swap();
            //assert
            ls.Head.Next.Data.Name.Should().Be(middleName);
            ls.Head.Next.Data.Age.Should().Be(middleAge);
            ls.Head.Prev.Should().BeNull();
            ls.Tail.Next.Should().BeNull();
            ls.Tail.Prev.Data.Name.Should().Be(middleName);
            ls.Tail.Prev.Data.Age.Should().Be(middleAge);
            ls.Head.Data.Name.Should().Be(initialTailName);
            ls.Head.Data.Age.Should().Be(initialTailAge);
            ls.Tail.Data.Name.Should().Be(initialHeadName);
            ls.Tail.Data.Age.Should().Be(initialHeadAge);

            int test = 007;
            Console.WriteLine(test);
        }
    }
}
