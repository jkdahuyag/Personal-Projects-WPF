using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericDoublyLinkedList;

namespace MyQueue
{
    public class QueueList<T> : IQueue<T>
    {
        private GenericDoublyLinkedList<T> _storage = new GenericDoublyLinkedList<T>();

        public QueueList() { }
        public QueueList(GenericDoublyLinkedList<T> internalData)
        {
            _storage = internalData;
        }

        public int Count => _storage.Count;

        public void Clear()
        {
            _storage = new GenericDoublyLinkedList<T>();
        }

        public T Dequeue()
        {
            var removedData = _storage.RemoveFromHead();
            return removedData;
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException();
            var fromHead = _storage.Head.Data;
            return fromHead;
        }
        public void Enqueue(T data)
        {
            _storage.AddToTail(data);
        }

        public bool IsEmpty() => _storage.Count == 0;

        public bool HasSameContents(IQueue<T> other)
        {
            if (Count != other.Count) return false;
            var thisCheckerNode = _storage.Head;

            foreach (var nodeData in other)
            {
                if (Comparer<T>.Default.Compare(thisCheckerNode.Data, nodeData) != 0) return false;
                thisCheckerNode = thisCheckerNode.Next;
            }
            return true;
        }

        public bool HasEquivalentContents(IQueue<T> other)
        {
            var otherStorage = new GenericDoublyLinkedList<T>();
            foreach (var node in other)
                otherStorage.AddToTail(node);

            var uniqueElements = _storage.GroupBy(c => c);
            var otherListUniqueElements = other.GroupBy(c => c);

            var mostUniqueCounts = uniqueElements.Count() >= otherListUniqueElements.Count()
                ? uniqueElements
                : otherListUniqueElements;

            var stackwithLessUniqueCount = uniqueElements.Count() >= otherListUniqueElements.Count()
                ? otherStorage
                : _storage;

            foreach (var result in mostUniqueCounts)
            {
                try
                {
                    stackwithLessUniqueCount.Search(result.Key);
                }
                catch(InvalidOperationException)
                {
                    return false;
                }
            }

            return true;
        }

        public void Swap(IQueue<T> other)
        {
            var newList = CloneLeftToRight();
            var otherList = other.CloneLeftToRight();

            Clear();
            other.Clear();

            foreach (var node in otherList)
                Enqueue(node);
            foreach (var node in newList)
                other.Enqueue(node);
        }

        public void Flip()
        {
            var newList = new GenericDoublyLinkedList<T>();
            var nodeCount = Count;
            for (int i = 0; i < nodeCount; i++)
            {
                newList.AddToHead(this.Dequeue());
            }
            _storage = newList;
        }

        public IQueue<T> CloneLeftToRight()
        {
            var newQueue = new QueueList<T>();
            foreach (var node in _storage)
                newQueue.Enqueue(node);
            return newQueue;
        }

        public IQueue<T> CloneRightToLeft()
        {
            var newQueue = new QueueList<T>();
            var tmp = _storage.Tail;
            while (tmp != null)
            {
                newQueue.Enqueue(tmp.Data);
                tmp = tmp.Prev;
            }
            return newQueue;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_storage == null) yield break;
            var tmp = _storage.Head;
            while (tmp != null)
            {
                yield return tmp.Data;
                tmp = tmp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
