using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Assessment_1;

namespace AS3_B
{
    public class QueueArray<T> : IQueue<T>
    {
        private T[] _storage = Array.Empty<T>();

        public QueueArray() { }
        public QueueArray(T[] internalData)
        {
            _storage = internalData;
        }

        public int Count => _storage.Length;

        public void Clear()
        {
            _storage = new T[] { };
        }

        public T Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException();
            var removedData = _storage.First();
            _storage = _storage[Range.StartAt(1)];
            return removedData;
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException();
            var fromHead = _storage.First();
            return fromHead;
        }
        public void Enqueue(T item)
        {
            _storage = _storage.Append(item).ToArray();
        }

        public bool IsEmpty() => _storage.Length == 0;

        public bool HasSameContents(IQueue<T> other)
        {
            if (Count != other.Count) return false;
            if (Count == 0 && other.Count == 0) return true;
            var indexer = 0;
            foreach (var data in other)
            {
                if (!_storage[indexer].Equals(data)) return false;
                indexer++;
            }
            return true;
        }

        public bool HasEquivalentContents(IQueue<T> other)
        {
            var unrepeatedArray = _storage.Distinct().ToArray();
            var unrepeatedOtherArray = other.Distinct().ToArray();

            var stackWithLessCount = unrepeatedArray.Length < unrepeatedOtherArray.Length
                ? unrepeatedArray
                : unrepeatedOtherArray;
            var stackWithMoreCount = unrepeatedArray.Length < unrepeatedOtherArray.Length
                ? unrepeatedOtherArray
                : unrepeatedArray;
            for (int i = 0; i < stackWithMoreCount.Length; i++)
                if (!(stackWithLessCount.Contains(stackWithMoreCount[i]))) return false;
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
            Array.Reverse(_storage);
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
            var newQueue = new QueueArray<T>();
            for (int i = Count - 1; i >= 0; i--)
                newQueue.Enqueue(_storage[i]);
            return newQueue;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_storage).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
