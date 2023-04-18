using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListCore
{
    public interface ILinkedList<T>
    {
        void AddToTail(T data);
        void AddToHead(T data);
        void RemoveFromTail();
        void RemoveFromHead();

        int Count { get; }
    }
}
