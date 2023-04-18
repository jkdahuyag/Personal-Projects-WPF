using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GenericDoublyLinkedList;
using NUnit.Framework;

namespace Dahuyag_AS5.Tests
{
    public class BinaryTreeTests
    {
        [TestCase(100,
            new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 },
            new[] { 5, 0, 24, -43, 1, 21, 52, -69, -1, 15, 23, 202, 100 },
            new[] { -69, -43, -1, 0, 1, 5, 15, 21, 23, 24, 52, 100, 202 })]
        public void Insert_GiveOneData_ReturnExpectedBreadthFirst(
            int dataToInsert,
            int[] existingData,
            int[] expectedBreadthFirstData,
            int[] expectedInorderData)
        {
            //arrange
            var bt = new BinaryTree<int>(existingData);


            //act
            bt.Insert(dataToInsert);

            //assert
            var BreadthFirstResult = bt.BreadthFirst();
            var inOrderResult = bt.InOrder();

            expectedBreadthFirstData.Should().Equal(BreadthFirstResult);
            expectedInorderData.Should().Equal(inOrderResult);
        }
        [TestCase(new[] { 4, 59, -78, 72, 28, 99, -43, 23, 79, 92, 88, 18 }, new[] { 4, -78, 59, -43, 28, 72, 23, 99, 18, 79, 92, 88 })]
        public void Insert_GivenData_ReturnExpectedBreadthFirst(int[] data, int[] expected)
        {
            //arrange
            var bt = new BinaryTree<int>();
            bt.Insert(data);

            //act
            var result = bt.BreadthFirst();

            //assert
            result.Should().Equal(expected);
        }
        [Test]
        public void Insert_GivenPersonData_returnExpectedBreadthFirstTraversal()
        {
            //arrange
            var bt = new BinaryTree<Person>();

            //act
            var result = bt.BreadthFirst();

            //aasert
            var expected = new Queue<Person>();
            expected.Enqueue(new Person("Nicole", 24));
            expected.Enqueue(new Person("Dirk", 18));
            expected.Enqueue(new Person("Sean", 20));
            expected.Enqueue(new Person("Dirk", 19));
        }
        [TestCase(new[] { 4, 59, -78, 72, 28, 99, -43, 23, 79, 92, 88, 18 }, new[] { 4, -78, -43, 59, 28, 23, 18, 72, 99, 79, 92, 88 })]
        public void Insert_GivenData_ReturnExpectedPreOrder(int[] data, int[] expected)
        {
            //arrange
            var bt = new BinaryTree<int>();
            bt.Insert(data);

            //act
            var result = bt.PreOrder();

            //assert
            result.Should().Equal(expected);
        }

        [TestCase(new[] { 3061, -7708, -8667,-352,7901,-9645, 7114, 420, -222, 0
        }, new[] { -9645, -8667, 0, -222, 420, -352, -7708, 7114, 7901, 3061 })]
        public void Insert_GivenData_ReturnExpectedPostOrder(int[] data, int[] expected)
        {
            //arrange
            var bt = new BinaryTree<int>();
            bt.Insert(data);

            //act
            var result = bt.PostOrder();

            //assert
            result.Should().Equal(expected);
        }
        [TestCase(new[] {3061, -7708, -8667,-352,7901,-9645, 7114, 420, -222, 0
        }, new[] { -9645, -8667, -7708, -352, -222, 0, 420, 3061, 7114, 7901 })]
        public void Insert_GivenData_ReturnExpectedInOrder(int[] data, int[] expected)
        {
            //arrange
            var bt = new BinaryTree<int>();
            bt.Insert(data);

            //act
            var result = bt.InOrder();

            //assert
            result.Should().Equal(expected);
        }
        [TestCase(
            new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 },
            new[] { 5, 0, 24, -43, 1, 21, 52, -69, -1, 15, 23, 202 },
            new[] { -69, -43, -1, 0, 1, 5, 15, 21, 23, 24, 52, 202 })]
        public void GetChildren_PickExistingNode_ReturnExpectedChildCount(
            int[] existingData,
            int[] expectedBreadthFirstData,
            int[] expectedInOrderData)
        {
            //arrange
            var bt = new BinaryTree<int>(existingData);
            var node1 = bt.Root.Left;
            var node2 = bt.Root.Right.Right;
            var node3 = bt.Root.Left.Right;

            //act
            var childrenCount1 = bt.GetChildrenCount(node1);
            var childrenCount2 = bt.GetChildrenCount(node2);
            var childrenCount3 = bt.GetChildrenCount(node3);

            //assert
            var BreadthFirstResult = bt.BreadthFirst();
            var inOrderResult = bt.InOrder();
            var expectedChildrenCount1 = 2;
            var expectedChildrenCount2 = 1;
            var expectedChildrenCount3 = 0;
            expectedBreadthFirstData.Should().Equal(BreadthFirstResult);
            expectedInOrderData.Should().Equal(inOrderResult);
            childrenCount1.Should().Be(expectedChildrenCount1);
            childrenCount2.Should().Be(expectedChildrenCount2);
            childrenCount3.Should().Be(expectedChildrenCount3);
        }
        [TestCase(
            new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 },
            new[] { 5, 0, 24, -43, 1, 21, 52, -69, -1, 15, 23, 202 },
            new[] { -69, -43, -1, 0, 1, 5, 15, 21, 23, 24, 52, 202 })]
        public void GetParent_PickExistingNode_ReturnExpectedParent(
            int[] existingData,
            int[] expectedBreadthFirstData,
            int[] expectedInOrderData)
        {
            //arrange
            var bt = new BinaryTree<int>(existingData);
            var node1 = bt.Root.Left;
            var node2 = bt.Root.Right.Right;
            var node3 = bt.Root.Left.Right;

            //act
            var parent1 = bt.GetParent(node1);
            var parent2 = bt.GetParent(node2);
            var parent3 = bt.GetParent(node3);

            //assert
            var BreadthFirstResult = bt.BreadthFirst();
            var inOrderResult = bt.InOrder();

            expectedBreadthFirstData.Should().Equal(BreadthFirstResult);
            expectedInOrderData.Should().Equal(inOrderResult);
            parent1.Should().Be(bt.Root);
            parent2.Should().Be(bt.Root.Right);
            parent3.Should().Be(bt.Root.Left);
        }
        [TestCase(new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 })]
        public void Search_PickExistingData_ReturnExpectedNode(int[] existingData)
        {
            //arrange
            var bt = new BinaryTree<int>(existingData);
            var intToSearch = 52;


            //act
            var result = bt.Search(intToSearch);

            //assert
            var expectedResult = bt.Root.Right.Right;
            result.Should().Be(expectedResult);
        }
        [TestCase(new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 })]
        public void Search_PickExistingNode_ReturnExpectedNode(int[] existingData)
        {
            //arrange
            var bt = new BinaryTree<int>(existingData);
            var nodeToSearch = new BinaryTreeNode<int>(52);


            //act
            var result = bt.Search(nodeToSearch);

            //assert
            var expectedResult = bt.Root.Right.Right;
            result.Should().Be(expectedResult);
        }

        [TestCase(new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 })]
        public void Level_PickExistingNode_ReturnExpectedLevel(int[] existingData)
        {
            //arrange
            var bt = new BinaryTree<int>(existingData);
            var nodeToSearch = new BinaryTreeNode<int>(52);

            //act
            var result = bt.Level(nodeToSearch);

            //assert
            var expectedLevel = 3;
            result.Should().Be(expectedLevel);
        }

        [Test]
        public void GetLeaves_StartFromRoot_ReturnCorrectNumberOfLeaves()
        {
            //arrange
            int[] existingData = new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 };
            var bt = new BinaryTree<int>(existingData);

            //act
            var result = bt.GetLeaves();

            //assert
            var expectedLeaves = 6;
            result.Should().Be(expectedLeaves);
        }
        [Test]
        public void GetLeaves_StartFromGivenNode_ReturnCorrectNumberOfLeaves()
        {
            //arrange
            int[] existingData = new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 };
            var bt = new BinaryTree<int>(existingData);
            var nodeStart = bt.Root.Left;
            //act
            var result = bt.GetLeaves(nodeStart);

            //assert
            var expectedLeaves = 3;
            result.Should().Be(expectedLeaves);
        }
        [Test]
        public void GetNonLeaves_StartFromRoot_ReturnCorrectNumberOfNonLeaves()
        {
            //arrange
            int[] existingData = new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 };
            var bt = new BinaryTree<int>(existingData);

            //act
            var result = bt.GetNonLeaves();

            //assert
            var expectedNonLeaves = 6;
            result.Should().Be(expectedNonLeaves);
        }
        [Test]
        public void GetNonLeaves_StartFromGivenNode_ReturnCorrectNumberOfNonLeaves()
        {
            //arrange
            int[] existingData = new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 };
            var bt = new BinaryTree<int>(existingData);
            var nodeStart = bt.Root.Left;
            //act
            var result = bt.GetNonLeaves(nodeStart);

            //assert
            var expectedNonLeaves = 2;
            result.Should().Be(expectedNonLeaves);
        }
    }
}
