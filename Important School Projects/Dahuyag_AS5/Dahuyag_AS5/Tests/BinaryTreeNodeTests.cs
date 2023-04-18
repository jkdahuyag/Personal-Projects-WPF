using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Dahuyag_AS5.Tests
{
    public class BinaryTreeNodeTests
    {
        [Test]
        public void Constructor_SupplyData_PropertyEqualToData()
        {
            //arrange
            //act
            const int expected = 5;
            var node = new BinaryTreeNode<int>(expected);
            //assert
            var expectedNodeInstance = 1;
            node.Data.Should().Be(expected);
            node.Right.Should().BeNull();
            node.Left.Should().BeNull();
            node.Instances.Should().Be(expectedNodeInstance);
        }
        [TestCase(new[] { 5, 5, 5, 5, 5, 5, 5, 24, 24, 24, 0, 0 },
            new[] { 5, 24, 0, 52, -43, -1, 202, -69, 21, 1, 23, 15 },
            new[] { 5, 0, 24, -43, 1, 21, 52, -69, -1, 15, 23, 202 },
            new[] { -69, -43, -1, 0, 1, 5, 15, 21, 23, 24, 52, 202 })]
        public void Instances_GiveRepeatingData_ReturnExpectedInstancesUnchangedTraversals(
            int[] dataToInsert,
            int[] existingData,
            int[] expectedBreadthFirstData,
            int[] expectedInOrderData)
        {
            //arrange
            var bt = new BinaryTree<int>(existingData);


            //act
            bt.Insert(dataToInsert);

            //assert
            var expectedInstance5 = 8;
            var expectedInstance24 = 4;
            var expectedInstance0 = 3;
            var breadthFirstResult = bt.BreadthFirst();
            var inOrderResult = bt.InOrder();

            bt.Root.Instances.Should().Be(expectedInstance5);
            bt.Root.Left.Instances.Should().Be(expectedInstance0);
            bt.Root.Right.Instances.Should().Be(expectedInstance24);
            expectedBreadthFirstData.Should().Equal(breadthFirstResult);
            expectedInOrderData.Should().Equal(inOrderResult);
        }
    }
}
