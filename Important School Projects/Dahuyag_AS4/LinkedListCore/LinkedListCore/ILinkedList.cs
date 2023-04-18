using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Assessment_1;

namespace LinkedListCore
{
    public interface ILinkedList<T>
    {
        Node<T> Head { get; }
        Node<T> Tail { get; }
        void AddToTail(T data);
        void AddToHead(T data);
        T RemoveFromTail();
        T RemoveFromHead();

        int Count { get; }
    }
}
