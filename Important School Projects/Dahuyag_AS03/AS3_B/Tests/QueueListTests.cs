using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Assessment_1;
using FluentAssertions;
using NUnit.Framework;

namespace AS3_B.TestsReal
{
    public class QueueListTests
    {
        [Test]
        public void BlankConstructor_GiveNoDataToConstructor_Count0()
        {
            //arrange
            //act
            var queue = new QueueList<int>();
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
            var linkedList = new GenericDoublyLinkedList<int>();
            linkedList.AddToHead(node1);
            linkedList.AddToHead(node2);
            linkedList.AddToHead(node3);
            linkedList.AddToHead(node4);
            var storage = new GenericDoublyLinkedList<int>();

            //act
            var queue = new QueueList<int>(linkedList);
            //assert
            foreach (var node in queue)
                storage.AddToTail(node);

            var expectedCount = 4;
            var expectedCompareResult = 0;
            queue.Count.Should().Be(expectedCount);
            storage.CompareTo(linkedList).Should().Be(expectedCompareResult);
        }
        [Test]
        public void Enqueue_AddDataToQueue_TopIsExpectedNodeCount4()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int expectedTopNode = 4;
            var queue = new QueueList<int>();
            var storage = new GenericDoublyLinkedList<int>();

            //act
            queue.Enqueue(expectedTopNode);
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            //assert
            foreach (var node in queue)
                storage.AddToTail(node);

            var expectedCount = 4;
            storage.Head.Data.Should().Be(expectedTopNode);
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
            var queue = new QueueList<int>();
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
            var queue = new QueueList<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            //act
            var check = queue.IsEmpty();
            //assert
            var expectedCount = 4;
            check.Should().BeFalse();
            queue.Count.Should().Be(expectedCount);
        }
        [Test]
        public void IsEmpty_EmptyQueue_ReturnTrue()
        {
            //arrange
            var Queue = new QueueList<int>();
            //act
            var check = Queue.IsEmpty();
            //assert
            var expectedCount = 0;
            check.Should().BeTrue();
            Queue.Count.Should().Be(expectedCount);
        }
        [Test]
        public void Dequeue_DequeueFirstElement_ReturnHeadDataCount3()
        {
            //arrange
            int node1 = 1;
            int node2 = 2;
            int node3 = 3;
            int expectedNode = 4;
            var queue = new QueueList<int>();
            queue.Enqueue(expectedNode);
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            //act
            var DequeuedNode = queue.Dequeue();
            //assert
            var expectedCount = 3;
            DequeuedNode.Should().Be(expectedNode);
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
            var queue = new QueueList<int>();
            queue.Enqueue(expectedNode);
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            //act
            var PeekedNode = queue.Peek();
            //assert
            var expectedCount = 4;
            PeekedNode.Should().Be(expectedNode);
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
            var queue = new QueueList<int>();
            var queue2 = new QueueList<int>();
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
            var queue = new QueueList<int>();
            var queue2 = new QueueList<int>();
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
            var queue = new QueueList<int>();
            var queue2 = new QueueList<int>();
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
            var queue = new QueueList<int>();
            var queue2 = new QueueList<int>();
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
            var queue = new QueueList<int>();
            var expectedQueue2Data = new QueueList<int>();
            var queue2 = new QueueList<int>();
            var expectedQueueData = new QueueList<int>();
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
            var expectedBoolResult = queue.HasSameContents(expectedQueueData);
            var expectedBoolResult2 = queue2.HasSameContents(expectedQueue2Data);
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
            var queue = new QueueList<int>();
            var expectedFlippedQueue = new QueueList<int>();
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
            var expectedBoolResult = queue.HasSameContents(expectedFlippedQueue);
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
            var queue = new QueueList<int>();
            var expectedReturnQueue = new QueueList<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            queue.Enqueue(node5);
            expectedReturnQueue.Enqueue(node1);
            expectedReturnQueue.Enqueue(node2);
            expectedReturnQueue.Enqueue(node3);
            expectedReturnQueue.Enqueue(node4);
            expectedReturnQueue.Enqueue(node5);
            //act
            var newQueue = queue.CloneLeftToRight();
            //check if returned queue is independent (not a reference)
            queue.Enqueue(node3);
            //assert
            var expectedBoolResult = newQueue.HasSameContents(expectedReturnQueue);


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
            var queue = new QueueList<int>();
            var expectedReturnQueue = new QueueList<int>();
            queue.Enqueue(node1);
            queue.Enqueue(node2);
            queue.Enqueue(node3);
            queue.Enqueue(node4);
            queue.Enqueue(node5);
            var storage = new GenericDoublyLinkedList<int>();
            foreach (var node in queue)
                storage.AddToTail(node);
            var expectedUnchangedQueueData = new QueueList<int>(storage);
            expectedReturnQueue.Enqueue(node5);
            expectedReturnQueue.Enqueue(node4);
            expectedReturnQueue.Enqueue(node3);
            expectedReturnQueue.Enqueue(node2);
            expectedReturnQueue.Enqueue(node1);
            //act
            var newqueue = queue.CloneRightToLeft();
            //assert
            var expectedBoolResult = newqueue.HasSameContents(expectedReturnQueue);
            var expectedBoolResult2 = queue.HasSameContents(expectedUnchangedQueueData);
            expectedBoolResult.Should().BeTrue();
            expectedBoolResult2.Should().BeTrue();
        }

    }

}

