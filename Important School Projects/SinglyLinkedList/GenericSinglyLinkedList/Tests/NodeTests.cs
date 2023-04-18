using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace GenericSinglyLinkedList.Tests
{
    public class NodeTests
    {
        [TestCase("99")]
        [TestCase("12312")]
        [TestCase("9643639")]
        [TestCase("959")]
        [TestCase("2323")]
        public void ConstructNode_GiveStringData_CreateNodeSameDataNullNext(string data)
        {
            //arrange
            //act
            var node = new Node<string>(data);
            //assert
            node.Data.Should().Be(data);
            node.Next.Should().BeNull();
        }

        [TestCase(99)]
        [TestCase(12312)]
        [TestCase(9643639)]
        [TestCase(959)]
        [TestCase(2323)]
        public void ConstructNode_GiveIntData_CreateNodeSameDataNullNext(int data)
        {
            //arrange
            //act
            var node = new Node<int>(data);
            //assert
            node.Data.Should().Be(data);
            node.Next.Should().BeNull();
        }
        [TestCase("XD", 99)]
        [TestCase("XdsgsdD", 1)]
        [TestCase("wa", 123)]
        public void ConstructNode_GivePersonData_CreateNodeSameDataNullNext(string name, int age)
        {
            //arrange
            //act
            var person = new Person(name, age);
            var node = new Node<Person>(person);
            //assert
            node.Data.Name.Should().Be(name);
            node.Data.Age.Should().Be(age);
            node.Next.Should().BeNull();
        }

        
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
