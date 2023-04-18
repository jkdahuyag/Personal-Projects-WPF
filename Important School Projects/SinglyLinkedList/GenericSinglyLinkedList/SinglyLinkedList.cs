using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedListCore;

namespace GenericSinglyLinkedList
{
    public class SinglyLinkedList<T> : ILinkedList<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        public int Count { get; set; }

        public void AddToTail(T data)
        {
            var newNode = new Node<T>(data);
            if (Head == null) Head = Tail = newNode;
            else if (Head == Tail)
            {
                Head.Next = newNode;
                Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                Tail = newNode;
            }

            Count++;

        }

        public void AddToHead(T data)
        {
            var newNode = new Node<T>(data);
            if (Head == null) Head = Tail = newNode;
            else
            {
                var tmp = Head;
                Head = newNode;
                Head.Next = tmp;
                tmp = null;
            }
            

            Head = newNode;
            newNode = null;
            Count++;
        }

        public void RemoveFromHead()
        {
            if (Head == null) throw new InvalidOperationException("list is empty");
            if (Head == Tail) Head = Tail = null;
            else
            {
                var tmp = Head;
                Head = Head.Next;
                tmp.Next = null;
                tmp = null;
            }
            Count--;
        }

        public void RemoveFromTail()
        {
            if (Head == null) throw new InvalidOperationException("list is empty");

            var tmp = Head;

            if (Head == Tail) Head = Tail = null;
            else
            {
                while (tmp.Next != Tail)
                {
                    tmp = tmp.Next;
                }
                Tail = tmp;
                Tail.Next = null;
                tmp = null;
            }

            Count--;
        }
    }

}
