using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedListCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace Dahuyag_Assessment_1
{
    public class GenericDoublyLinkedList<T> : ILinkedList<T>
    {
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }
        public int Count { get; private set; }

        public void AddToTail(T data)
        {
            var tmp = new Node<T>(data);
            if(Head==null) Head = Tail = tmp;
            else
            {
                Tail.Next = tmp;
                tmp.Prev = Tail;
                Tail = tmp;
            }
            tmp = null;
            Count++;
        }
        public void AddToHead(T data)
        {
            var tmp = new Node<T>(data);
            if (Head == null) Head = Tail = tmp;
            else
            {
                tmp.Next = Head;
                Head.Prev = tmp;
                Head = tmp;
            }
            tmp = null;
            Count++;
        }

        public T RemoveFromTail()
        {
            if (Head == null) throw new InvalidOperationException("List is empty");
            
            var dataReturn = Tail.Data;
            
            if (Head == Tail) Head = Tail = null;
            else
            {
                Tail = Tail.Prev;
                Tail.Next.Prev = null;
                Tail.Next = null;
            }
            Count--;
            return dataReturn;
        }
        public T RemoveFromHead()
        {
            if (Head == null) throw new InvalidOperationException("List is empty");
            
            var dataReturn = Head.Data;

            if (Head == Tail) Head = Tail = null;
            else
            {
                Head = Head.Next;
                Head.Prev.Next = null;
                Head.Prev = null;
            }
            Count--;
            return dataReturn;
        }

        public void Remove(int position)
        {
            if (Head == null) throw new InvalidOperationException("List is empty");
            if (position < 0 || position >= Count) throw new InvalidOperationException("Position is out of bounds");
            
            var tmp = Head;

            for (int i = 0; i < position; i++)
            {
                tmp = tmp.Next;
            }

            if(tmp == Head) RemoveFromHead();
            else if(tmp == Tail) RemoveFromTail();
            else
            {
                if (tmp.Next != null) tmp.Next.Prev = tmp.Prev;
                if (tmp.Prev != null) tmp.Prev.Next = tmp.Next;

                tmp.Next = null;
                tmp.Prev = null;
                tmp = null;
                Count--;
            }
        }

        public Node<T> Search(T data, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            var tmp = Head;
            while (tmp != null)
            {
                if (comparer.Compare(data, tmp.Data) == 0) return tmp;
                tmp = tmp.Next;
            }

            throw new InvalidOperationException("Data does not exist");
        }

        public int GetPosition(T data, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            var tmp = Head;
            int position = 0;
            while (comparer.Compare(data, tmp.Data) != 0)
            {
                if(tmp.Next != null)
                {
                    tmp = tmp.Next;
                    position++;
                }
                else
                {
                    position = -1;
                    break;
                }
            }
            return position;
        }

        public void Swap()
        {
            if (Head == null) throw new InvalidOperationException("List is empty");
            if (Head.Next == Tail)
            {
                Head = Tail;
                Tail = Tail.Prev;
                Head.Next = Tail;
                Head.Prev = null;
                Tail.Prev = Head;
                Tail.Next = null;
            }
            else
            {
                var tmp = Head;
                Head = Tail;
                Tail = tmp;
                Head.Prev.Next = Tail;
                Head.Next = Tail.Next;
                Tail.Next.Prev = Head;
                Tail.Prev = Head.Prev;
                Tail.Next = null;
                Head.Prev = null;
                tmp = null;
            }
        }
    }
}
