using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Assessment_1;

namespace GenericStack
{
    public class Stack<T> : IStack<T>
    {
        private GenericDoublyLinkedList<T> _internalData = new GenericDoublyLinkedList<T>();

        public int Count => _internalData.Count;

        public void Clear()
        {
            _internalData = new GenericDoublyLinkedList<T>();
        }

        public T Pop()
        {
            var removedData = _internalData.RemoveFromHead();
            return removedData;
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException();
            var fromHead = _internalData.Head.Data;
            return fromHead;
        }

        public void Push(T data)
        {
            _internalData.AddToHead(data);
        }

        public bool IsEmpty() => _internalData.Count == 0;
    }
}
