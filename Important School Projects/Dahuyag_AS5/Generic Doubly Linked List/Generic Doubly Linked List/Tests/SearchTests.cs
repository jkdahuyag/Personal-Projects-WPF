using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace GenericDoublyLinkedList.Tests
{
    public class SearchTests
    {
        [Test]
        public void Search_NonExistingData_ThrowError()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            var person1 = new Person("Jerard", 20);
            var person2 = new Person("Will", 12);
            var person3 = new Person("Yoru", 22);
            var personNotInList = new Person("Yoru", 20);
            ls.AddToHead(person1);
            ls.AddToHead(person2);
            ls.AddToHead(person3);
            //act
            var act = () =>
            {
                var searchedNode = ls.Search(personNotInList);
            };
            //assert
            act.Should().Throw<InvalidOperationException>();
        }
        [Test]
        public void Search_Given1NodeExistingData_ReturnNode()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            string expectedName = "Jerard";
            int expectedAge = 19;
            var person1 = new Person(expectedName, expectedAge);
            ls.AddToHead(person1);
            //act
            var serchedNode = ls.Search(person1);
            //assert
            serchedNode.Data.Name.Should().Be(expectedName);
            serchedNode.Data.Age.Should().Be(expectedAge);
        }
        [Test]
        public void Search_Given2NodeExistingData_ReturnNode()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            string expectedName = "Jerard";
            int expectedAge = 19;
            var person2 = new Person("Will", 12);
            var person1 = new Person(expectedName, expectedAge);
            ls.AddToHead(person1);
            ls.AddToHead(person2);
            //act
            var serchedNode = ls.Search(person1);
            //assert
            serchedNode.Data.Name.Should().Be(expectedName);
            serchedNode.Data.Age.Should().Be(expectedAge);
        }
        [Test]
        public void Search_Given3NodeExistingData_ReturnNode()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            string expectedName = "Jerard";
            int expectedAge = 19;
            var person2 = new Person("Will", 12);
            var person3 = new Person("Yoru", 22);
            var person1 = new Person(expectedName, expectedAge);
            ls.AddToHead(person1);
            ls.AddToHead(person2);
            ls.AddToHead(person3);
            //act
            var serchedNode = ls.Search(person1);
            //assert
            serchedNode.Data.Name.Should().Be(expectedName);
            serchedNode.Data.Age.Should().Be(expectedAge);
        }
    }
}
