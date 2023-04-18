using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace MyQueue.TestsReal
{
    public class QueueArrayTests
    {
        [Test]
        public void BlankConstructor_GiveNoDataToConstructor_Count0()
        {
            //arrange
            //act
            var queue = new QueueArray<int>();
            //assert
            var expectedCount = 0;
            queue.Count.Should().Be(expectedCount);
        }
        [Test]
        public void ConstructorWithParameter_GiveDataToConstructor_DataIsSet()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int[] newArray = { };
            newArray = newArray.Prepend(node1).ToArray();
            newArray = newArray.Prepend(node2).ToArray();
            newArray = newArray.Prepend(node3).ToArray();
            newArray = newArray.Prepend(node4).ToArray();
            int[] storage = { };
            //act
            var queue = new QueueArray<int>(newArray);
            //assert
            storage = queue.Aggregate(storage, (current, node) => current.Append(node).ToArray());
            var expectedCount = 4;
            queue.Count.Should().Be(expectedCount);
            storage.SequenceEqual(newArray).Should().BeTrue();
        }
        [Test]
        public void Enqueue_AddDataToQueue_TopIsExpectedNodeCount4()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int expectedTopNode = 4;
            var queue = new QueueArray<int>();
            int[] storage = { };
            //act
            queue.Enqueue(expectedTopNode);
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            //assert
            storage = queue.Aggregate(storage, (current, node) => current.Append(node).ToArray());
            var expectedCount = 4;
            storage[0].Should().Be(expectedTopNode);
            queue.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Clear_ClearAllData_Count0()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var queue = new QueueArray<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            //act
            queue.Clear();
            //assert
            var expectedCount = 0;
            queue.Count.Should().Be(expectedCount);
        }
        [Test]
        public void IsEmpty_NonEmptyQueue_ReturnFalse()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var queue = new QueueArray<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            //act
            var check = queue.IsEmpty();
            //assert
            var expectedCount = 4;
            var expectedResult = false;
            check.Should().Be(expectedResult);
            queue.Count.Should().Be(expectedCount);
        }
        [Test]
        public void IsEmpty_EmptyQueue_ReturnTrue()
        {
            //arrange
            var queue = new QueueArray<int>();
            //act
            var check = queue.IsEmpty();
            //assert
            var expectedCount = 0;
            var expectedResult = true;
            check.Should().Be(expectedResult);
            queue.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Dequeue_DequeueTopElement_ReturnHeadDataCount3()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int expectedNode = 4;
            var queue = new QueueArray<int>();
            queue.Enqueue(expectedNode);
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            //act
            var DequeuepedNode = queue.Dequeue();
            //assert
            var expectedCount = 3;
            DequeuepedNode.Should().Be(expectedNode);
            queue.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Peek_PeekAtTopNode_ReturnHeadDataCount4()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int expectedNode = 4;
            var queue = new QueueArray<int>();
            queue.Enqueue(expectedNode);
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            //act
            var DequeuepedNode = queue.Peek();
            //assert
            var expectedCount = 4;
            DequeuepedNode.Should().Be(expectedNode);
            queue.Count.Should().Be(expectedCount);
        }
        [Test]
        public void HasSameContents_QueuesWithSameContent_ReturnTrue()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var queue = new QueueArray<int>();
            var queue2 = new QueueArray<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            queue2.Enqueue(node1);
            queue2.Enqueue(node2);
            queue2.Enqueue(node3);
            queue2.Enqueue(node4);
            //act
            var expectedBoolResult = queue.HasSameContents(queue2);
            var expectedBoolResult2 = queue2.HasSameContents(queue);

            //assert
            expectedBoolResult.Should().BeTrue();
            expectedBoolResult2.Should().BeTrue();
        }
        [Test]
        public void HasSameContents_QueuesWithDifferentContent_ReturnFalse()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var queue = new QueueArray<int>();
            var queue2 = new QueueArray<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            queue2.Enqueue(node1);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node3);
            queue2.Enqueue(node4);
            //act
            var expectedBoolResult = queue.HasSameContents(queue2);
            var expectedBoolResult2 = queue2.HasSameContents(queue);
            //assert
            expectedBoolResult.Should().BeFalse();
            expectedBoolResult2.Should().BeFalse();
        }
        [Test]
        public void HasEquivalentContents_QueuesWithEquivalentContent_ReturnTrue()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            var queue = new QueueArray<int>();
            var queue2 = new QueueArray<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node1);
            queue.Enqueue(node1);
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node3);
            queue.Enqueue(node3);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            queue2.Enqueue(node1);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node3);
            queue2.Enqueue(node4);
            queue2.Enqueue(node4);
            queue2.Enqueue(node4);
            queue2.Enqueue(node4);
            //act
            var expectedBoolResult = queue.HasEquivalentContents(queue2);
            var expectedBoolResult2 = queue2.HasEquivalentContents(queue);
            //assert
            expectedBoolResult.Should().BeTrue();
            expectedBoolResult2.Should().BeTrue();
        }
        [Test]
        public void HasEquivalentContents_QueuesWithNonEquivalentContent_ReturnFalse()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int parasiteNode = 123;
            var queue = new QueueArray<int>();
            var queue2 = new QueueArray<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node1);
            queue.Enqueue(node1);
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node3);
            queue.Enqueue(node3);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            queue.Enqueue(parasiteNode);
            queue2.Enqueue(node1);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node2);
            queue2.Enqueue(node3);
            queue2.Enqueue(node4);
            queue2.Enqueue(node4);
            queue2.Enqueue(node4);
            //act
            var expectedBoolResult = queue.HasEquivalentContents(queue2);
            var expectedBoolResult2 = queue2.HasEquivalentContents(queue);
            //assert
            expectedBoolResult.Should().BeFalse();
            expectedBoolResult2.Should().BeFalse();
        }
        [Test]
        public void Swap_SwapTwoQueues_CorrectSwapDataRetained()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int node5 = 5;
            var queue = new QueueArray<int>();
            var expectedQueue2Data = new QueueArray<int>();
            var queue2 = new QueueArray<int>();
            var expectedQueueData = new QueueArray<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            queue.Enqueue(node5);
            expectedQueue2Data.Enqueue(node1);
            expectedQueue2Data.Enqueue(node2);
            expectedQueue2Data.Enqueue(node3);
            expectedQueue2Data.Enqueue(node4);
            expectedQueue2Data.Enqueue(node5);
            queue2.Enqueue(node1);
            queue2.Enqueue(node4);
            queue2.Enqueue(node4);
            queue2.Enqueue(node2);
            queue2.Enqueue(node4);
            queue2.Enqueue(node4);
            queue2.Enqueue(node5);
            expectedQueueData.Enqueue(node1);
            expectedQueueData.Enqueue(node4);
            expectedQueueData.Enqueue(node4);
            expectedQueueData.Enqueue(node2);
            expectedQueueData.Enqueue(node4);
            expectedQueueData.Enqueue(node4);
            expectedQueueData.Enqueue(node5);
            //act
            queue.Swap(queue2);
            //assert
            var expectedBoolResult = queue.SequenceEqual(expectedQueueData);
            var expectedBoolResult2 = queue2.SequenceEqual(expectedQueue2Data);
            expectedBoolResult.Should().BeTrue();
            expectedBoolResult2.Should().BeTrue();
        }
        [Test]
        public void Flip_FlipQueueContents_CorrectFlippedList()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int node5 = 5;
            var queue = new QueueArray<int>();
            var expectedFlippedQueue = new QueueArray<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            queue.Enqueue(node5);
            expectedFlippedQueue.Enqueue(node5);
            expectedFlippedQueue.Enqueue(node4);
            expectedFlippedQueue.Enqueue(node3);
            expectedFlippedQueue.Enqueue(node2);
            expectedFlippedQueue.Enqueue(node1);
            //act
            queue.Flip();
            //assert
            var expectedBoolResult = queue.SequenceEqual(expectedFlippedQueue);
            expectedBoolResult.Should().BeTrue();
        }
        [Test]
        public void CloneLeftToRight_CopyContentsToANewQueue_CorrectIndependentCopy()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int node5 = 5;
            var Queue = new QueueArray<int>();
            var expectedReturnQueue = new QueueArray<int>();
            Queue.Enqueue(node1);
            Queue.Enqueue(node2);
            Queue.Enqueue(node3);
            Queue.Enqueue(node4);
            Queue.Enqueue(node5);
            expectedReturnQueue.Enqueue(node1);
            expectedReturnQueue.Enqueue(node2);
            expectedReturnQueue.Enqueue(node3);
            expectedReturnQueue.Enqueue(node4);
            expectedReturnQueue.Enqueue(node5);
            //act
            var newQueue = Queue.CloneLeftToRight();
            //check if returned Queue is independent (not a reference)
            Queue.Enqueue(node3);
            //assert
            var expectedBoolResult = newQueue.SequenceEqual(expectedReturnQueue);

            expectedBoolResult.Should().BeTrue();
        }
        [Test]
        public void CloneRightToLeft_CopyFlippedContentsToANewQueue_CorrectIndependentCopyUnchangedOriginal()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int node4 = 4;
            int node5 = 5;
            var Queue = new QueueArray<int>();
            var expectedReturnQueue = new QueueArray<int>();
            Queue.Enqueue(node1);
            Queue.Enqueue(node2);
            Queue.Enqueue(node3);
            Queue.Enqueue(node4);
            Queue.Enqueue(node5);
            int[] storage = { };
            storage = Queue.Aggregate(storage, (current, node) => current.Append(node).ToArray());
            var expectedUnchangedQueueData = new QueueArray<int>(storage);
            expectedReturnQueue.Enqueue(node5);
            expectedReturnQueue.Enqueue(node4);
            expectedReturnQueue.Enqueue(node3);
            expectedReturnQueue.Enqueue(node2);
            expectedReturnQueue.Enqueue(node1);
            //act
            var newQueue = Queue.CloneRightToLeft();
            //assert
            var expectedBoolResult = newQueue.HasSameContents(expectedReturnQueue);
            var expectedBoolResult2 = Queue.HasSameContents(expectedUnchangedQueueData);
            expectedBoolResult.Should().BeTrue();
            expectedBoolResult2.Should().BeTrue();
        }
    }

}

