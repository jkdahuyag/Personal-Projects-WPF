using GenericDoublyLinkedList;
using System.Collections;

namespace MyStack
{
    public class StackList<T> : IStack<T>
    {
        private GenericDoublyLinkedList<T> _storage = new GenericDoublyLinkedList<T>();

        public StackList() { }
        public StackList(GenericDoublyLinkedList<T> internalData)
        {
            _storage = internalData;
        }

        public int Count => _storage.Count;

        public void Clear()
        {
            _storage = new GenericDoublyLinkedList<T>();
        }

        public T Pop()
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
        public void Push(T data)
        {
            _storage.AddToHead(data);
        }

        public bool IsEmpty() => _storage.Count == 0;

        public bool HasSameContents(IStack<T> other)
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

        public bool HasEquivalentContents(IStack<T> other)
        {
            var otherStorage = new GenericDoublyLinkedList<T>();
            foreach (var node in other)
                otherStorage.AddToTail(node);
            
            var uniqueElements = _storage.GroupBy(c => c);
            var otherListUniqueElements = other.GroupBy(c => c);

            var mostUniqueCounts = uniqueElements.Count() >= otherListUniqueElements.Count()
                ? uniqueElements 
                : otherListUniqueElements;
           
            var stackWithLessUniqueCount = uniqueElements.Count() >= otherListUniqueElements.Count()
                ? otherStorage 
                : _storage;
            
            foreach (var result in mostUniqueCounts)
            {
                try
                {
                    stackWithLessUniqueCount.Search(result.Key);
                }
                catch(InvalidOperationException)
                {
                    return false;
                }

            }

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
            var newList = new GenericDoublyLinkedList<T>();
            var nodeCount = Count;
            for (int i = 0; i < nodeCount; i++)
            {
                newList.AddToHead(Pop());
            }
            _storage = newList;
        }

        public IStack<T> CloneBottomToTop()
        {
            var newStack = new StackList<T>();
            var tmp = _storage.Tail;
            while (tmp!=null)
            {
                newStack.Push(tmp.Data);
                tmp = tmp.Prev;
            }
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
