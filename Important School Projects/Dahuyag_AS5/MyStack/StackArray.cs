using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStack
{
    public class StackArray<T> : IStack<T>
    {
        private T[] _storage = Array.Empty<T>();

        public StackArray() { }
        public StackArray(T[] internalData)
        {
            _storage = internalData;
        }

        public int Count => _storage.Length;

        public void Clear() 
        {
            _storage = new T[]{};
        }

        public T Pop()
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
        public void Push(T item)
        {
            _storage = _storage.Prepend(item).ToArray();
        }

        public bool IsEmpty() => _storage.Length == 0;

        public bool HasSameContents(IStack<T> other)
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

        public bool HasEquivalentContents(IStack<T> other)
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

        public void Swap(IStack<T> other)
        {
            var newList = CloneTopToBottom();
            var otherList = other.CloneTopToBottom();

            Clear();
            other.Clear();

            foreach (var node in otherList)
                Push(node);
            foreach (var node in newList)
                other.Push(node);
        }

        public void Flip()
        {
            Array.Reverse(_storage);
        }

        public IStack<T> CloneBottomToTop()
        {
            var newStack = new StackArray<T>();
            for (int i = Count-1; i >= 0; i--)
                newStack.Push(_storage[i]);
            return newStack;
        }

        public IStack<T> CloneTopToBottom()
        {
            var newStack = new StackList<T>();
            foreach (var node in _storage)
                newStack.Push(node);
            return newStack;
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
