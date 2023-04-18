using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using GenericDoublyLinkedList;

namespace MyStack.Tests
{
    public class StackListTests
    {
        [Test]
        public void BlankConstructor_GiveNoDataToConstructor_Count0()
        {
            //arrange
            //act
            var stack = new StackList<int>();
            //assert
            var expectedCount = 0;
            stack.Count.Should().Be(expectedCount);
        }
        [Test]
        public void ConstructorWithParameter_GiveDataToConstructor_DataIsSet()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var linkedList = new GenericDoublyLinkedList<int>();
            linkedList.AddToHead(node1);
            linkedList.AddToHead(node2);
            linkedList.AddToHead(node3);
            linkedList.AddToHead(node4);
            var storage = new GenericDoublyLinkedList<int>();
            //act
            var stack = new StackList<int>(linkedList);
            //assert
            foreach (var node in stack)
            {
                storage.AddToTail(node);
            }
            
            var expectedCount = 4;
            var expectedCompareResult = 0;
            stack.Count.Should().Be(expectedCount);
            storage.CompareTo(linkedList).Should().Be(expectedCompareResult);
        }
        [Test]
        public void Push_AddDataToStack_TopIsExpectedNodeCount4()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int expectedTopNode = 4;
            var stack = new StackList<int>();
            var storage = new GenericDoublyLinkedList<int>();
            //act
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(expectedTopNode);
            //assert
            foreach (var node in stack)
                storage.AddToTail(node);

            var expectedCount = 4;
            storage.Head.Data.Should().Be(expectedTopNode);
            stack.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Clear_ClearAllData_Count0()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var stack = new StackList<int>();
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node4);
            //act
            stack.Clear();
            //assert
            var expectedCount = 0;
            stack.Count.Should().Be(expectedCount);
        }
        [Test]
        public void IsEmpty_NonEmptyStack_ReturnFalse()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var stack = new StackList<int>();
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node4);
            //act
            var check = stack.IsEmpty();
            //assert
            var expectedCount = 4;
            var expectedResult = false;
            check.Should().Be(expectedResult);
            stack.Count.Should().Be(expectedCount);
        }
        [Test]
        public void IsEmpty_EmptyStack_ReturnTrue()
        {
            //arrange
            var stack = new StackList<int>();
            //act
            var check = stack.IsEmpty();
            //assert
            var expectedCount = 0;
            var expectedResult = true;
            check.Should().Be(expectedResult);
            stack.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Pop_PopTopElement_ReturnHeadDataCount3()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int expectedNode = 4;
            var stack = new StackList<int>();
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(expectedNode);
            //act
            var poppedNode = stack.Pop();
            //assert
            var expectedCount = 3;
            poppedNode.Should().Be(expectedNode);
            stack.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Peek_PeekAtTopNode_ReturnHeadDataCount4()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int expectedNode = 4;
            var stack = new StackList<int>();
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(expectedNode);
            //act
            var poppedNode = stack.Peek();
            //assert
            var expectedCount = 4;
            poppedNode.Should().Be(expectedNode);
            stack.Count.Should().Be(expectedCount);
        }
        [Test]
        public void HasSameContents_StacksWithSameContent_ReturnTrue()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var stack = new StackList<int>();
            var stack2 = new StackList<int>();
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node4);
            stack2.Push(node1);
            stack2.Push(node2);
            stack2.Push(node3);
            stack2.Push(node4);
            //act
            var expectedBoolResult = stack.HasSameContents(stack2);
            var expectedBoolResult2 = stack2.HasSameContents(stack);

            //assert
            expectedBoolResult.Should().BeTrue();
            expectedBoolResult2.Should().BeTrue();
        }
        [Test]
        public void HasSameContents_StacksWithDifferentContent_ReturnFalse()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var stack = new StackList<int>();
            var stack2 = new StackList<int>();
            stack.Push(node1);
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node3);
            stack.Push(node4);
            stack2.Push(node1);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node3);
            stack2.Push(node4);
            //act
            var expectedBoolResult = stack.HasSameContents(stack2);
            var expectedBoolResult2 = stack2.HasSameContents(stack);
            //assert
            expectedBoolResult.Should().BeFalse();
            expectedBoolResult2.Should().BeFalse();
        }
        [Test]
        public void HasEquivalentContents_StacksWithEquivalentContent_ReturnTrue()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var stack = new StackList<int>();
            var stack2 = new StackList<int>();
            stack.Push(node1);
            stack.Push(node1);
            stack.Push(node1);
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node3);
            stack.Push(node3);
            stack.Push(node3);
            stack.Push(node4);
            stack2.Push(node1);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node3);
            stack2.Push(node4);
            stack2.Push(node4);
            stack2.Push(node4);
            stack2.Push(node4);
            //act
            var expectedBoolResult = stack.HasEquivalentContents(stack2);
            var expectedBoolResult2 = stack2.HasEquivalentContents(stack);
            //assert
            expectedBoolResult.Should().BeTrue();
            expectedBoolResult2.Should().BeTrue();
        }
        [Test]
        public void HasEquivalentContents_StacksWithNonEquivalentContent_ReturnFalse()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int parasiteNode = 123;
            var stack = new StackList<int>();
            var stack2 = new StackList<int>();
            stack.Push(node1);
            stack.Push(node1);
            stack.Push(node1);
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node3);
            stack.Push(node3);
            stack.Push(node3);
            stack.Push(node4);
            stack.Push(parasiteNode);
            stack2.Push(node1);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node2);
            stack2.Push(node3);
            stack2.Push(node4);
            stack2.Push(node4);
            stack2.Push(node4);
            //act
            var expectedBoolResult = stack.HasEquivalentContents(stack2);
            var expectedBoolResult2 = stack2.HasEquivalentContents(stack);
            //assert
            expectedBoolResult.Should().BeFalse();
            expectedBoolResult2.Should().BeFalse();
        }
        [Test]
        public void Swap_SwapTwoStacks_CorrectSwapDataRetained()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int node5 = 5;
            var stack = new StackList<int>();
            var expectedFlippedStack2Data = new StackList<int>();
            var stack2 = new StackList<int>();
            var expectedFlippedStackData = new StackList<int>();
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node4);
            stack.Push(node5);
            expectedFlippedStack2Data.Push(node1);
            expectedFlippedStack2Data.Push(node2);
            expectedFlippedStack2Data.Push(node3);
            expectedFlippedStack2Data.Push(node4);
            expectedFlippedStack2Data.Push(node5);
            stack2.Push(node1);
            stack2.Push(node4);
            stack2.Push(node4);
            stack2.Push(node2);
            stack2.Push(node4);
            stack2.Push(node4);
            stack2.Push(node5);
            expectedFlippedStackData.Push(node1);
            expectedFlippedStackData.Push(node4);
            expectedFlippedStackData.Push(node4);
            expectedFlippedStackData.Push(node2);
            expectedFlippedStackData.Push(node4);
            expectedFlippedStackData.Push(node4);
            expectedFlippedStackData.Push(node5);
            //act
            stack.Swap(stack2);
            //assert
            var expectedBoolResult = stack.HasSameContents(expectedFlippedStackData);
            var expectedBoolResult2 = stack2.HasSameContents(expectedFlippedStack2Data);
            expectedBoolResult.Should().BeTrue();
            expectedBoolResult2.Should().BeTrue();
        }
        [Test]
        public void Flip_FlipStackContents_CorrectFlippedList()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int node5 = 5;
            var stack = new StackList<int>();
            var expectedFlippedStack = new StackList<int>();
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node4);
            stack.Push(node5);
            expectedFlippedStack.Push(node5);
            expectedFlippedStack.Push(node4);
            expectedFlippedStack.Push(node3);
            expectedFlippedStack.Push(node2);
            expectedFlippedStack.Push(node1);
            //act
            stack.Flip();
            //assert
            var expectedBoolResult = stack.HasSameContents(expectedFlippedStack);
            expectedBoolResult.Should().BeTrue();
        }
        [Test]
        public void CloneBottomToTop_CopyContentsToANewStack_CorrectIndependentCopy()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int node5 = 5;
            var stack = new StackList<int>();
            var expectedReturnStack = new StackList<int>();
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node4);
            stack.Push(node5);
            expectedReturnStack.Push(node1);
            expectedReturnStack.Push(node2);
            expectedReturnStack.Push(node3);
            expectedReturnStack.Push(node4);
            expectedReturnStack.Push(node5);
            //act
            var newStack = stack.CloneBottomToTop();
            //check if returned stack is independent (not a reference)
            stack.Push(node3);
            //assert
            var expectedBoolResult = newStack.HasSameContents(expectedReturnStack);
            
            expectedBoolResult.Should().BeTrue();
        }
        [Test]
        public void CloneTopToBottom_CopyFlippedContentsToANewStack_CorrectIndependentCopyUnchangedOriginal()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int node5 = 5;
            var stack = new StackList<int>();
            var expectedReturnStack = new StackList<int>();
            stack.Push(node1);
            stack.Push(node2);
            stack.Push(node3);
            stack.Push(node4);
            stack.Push(node5); 
            var storage = new GenericDoublyLinkedList<int>();
            foreach (var node in stack)
                storage.AddToTail(node);

            var expectedUnchangedStackData = new StackList<int>(storage);
            expectedReturnStack.Push(node5);
            expectedReturnStack.Push(node4);
            expectedReturnStack.Push(node3);
            expectedReturnStack.Push(node2);
            expectedReturnStack.Push(node1);
            //act
            var newStack = stack.CloneTopToBottom();
            //assert
            var expectedBoolResult = newStack.HasSameContents(expectedReturnStack);
            var expectedBoolResult2 = stack.HasSameContents(expectedUnchangedStackData);
            expectedBoolResult.Should().BeTrue();
            expectedBoolResult2.Should().BeTrue();
        }

    }
}
