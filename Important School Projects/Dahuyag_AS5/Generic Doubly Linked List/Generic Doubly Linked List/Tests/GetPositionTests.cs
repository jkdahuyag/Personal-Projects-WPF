using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace GenericDoublyLinkedList.Tests
{
    public class GetPositionTests
    {
        [Test]
        public void GetPosition_NonExistentData_ReturnNegative1()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            var person1 = new Person("Jerard", 20);
            var person2 = new Person("Will", 12);
            var person3 = new Person("Yoru", 22);
            var personNotInList = new Person("Yoru", 20);
            int expectedReturn = -1;
            ls.AddToHead(person1);
            ls.AddToHead(person2);
            ls.AddToHead(person3);
            //act
            int serchedNodePosition = ls.GetPosition(personNotInList);
            //assert
            serchedNodePosition.Should().Be(expectedReturn);
        }
        [Test]
        public void GetPosition_Given1NodeExistingData_ReturnIndex()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            var person1 = new Person("Jerard", 20);
            ls.AddToHead(person1);
            int expectedReturn = 0;
            //act
            int serchedNodePosition = ls.GetPosition(person1);
            //assert
            serchedNodePosition.Should().Be(expectedReturn);
        }
        [Test]
        public void GetPosition_Given2NodeExistingData_ReturnIndex()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            var person2 = new Person("Will", 12);
            var person1 = new Person("Jerard", 20);
            ls.AddToHead(person1);
            ls.AddToHead(person2);
            int expectedReturn = 1;
            //act
            int serchedNodePosition = ls.GetPosition(person1);
            //assert
            serchedNodePosition.Should().Be(expectedReturn);
        }
        [Test]
        public void GetPosition_Given3NodeExistingData_ReturnIndex()
        {
            //arrange
            var ls = new GenericDoublyLinkedList<Person>();
            var person2 = new Person("Will", 12);
            var person3 = new Person("Yoru", 22);
            var person1 = new Person("Jerard", 20);
            ls.AddToHead(person1);
            ls.AddToHead(person2);
            ls.AddToTail(person3);
            int expectedReturn = 1;
            //act
            int serchedNodePosition = ls.GetPosition(person1);
            //assert
            serchedNodePosition.Should().Be(expectedReturn);
        }
    }
}
