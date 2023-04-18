using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimplyLinkedList
{
    public class Node
    {
        public string Data { get; set; }
        public Node Next { get; set; }

        public Node(string data)
        {
            Data = data;
        }
    }

    public class SinglyLinkedList
    {
        public Node Head { get; private set; }
        public Node Tail { get; private set; }
        public int Count { get; private set; }

        // Insertion Methods
        public void AddToTail(string data)
        {
            var newNode = new Node(data);

            // empty list
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
            }
            // list contains one or more nodes
            else
            {
                Tail.Next = newNode;
                Tail = newNode;
            }

            Count++;
        }

        public void AddToHead(string data)
        {
            var tmp = new Node(data);

            // empty list
            if (Head == null)
            {
                Head = tmp;
                Tail = tmp;
            }
            // list contains one or more nodes
            else
                tmp.Next = Head;

            Head = tmp;
            tmp = null;

            Count++;
        }

        public void RemoveFromHead()
        {
            var tmp = Head;
            if (Head == null)
            {
                Console.WriteLine("List Contains no Nodes");
            }
            else if (Head == Tail) // list contains one node
            {
                Head = null;
                Tail = null;

                Count--;
            }
            // list contains two or more nodes
            else
            {
                tmp.Next = Head;
                tmp.Next = null;
                tmp = null;
                Count--;
            }
        }

        public void RemoveFromTail()
        {
            var tmp = Head;


            if (Head == null)
            {
                Console.WriteLine("List Contains no Nodes");
            }
            else if (Head == Tail) // list contains one node
            {
                Head = null;
                Tail = null;
                Count--;
            }
            // list contains two or more nodes
            else
            {
                for (int i = 0; i < Count - 1; i++)
                {
                    if (tmp.Next != Tail) tmp = tmp.Next;
                    else break;
                }

                Tail = tmp;
                tmp.Next = null;
                tmp = null;
                Count--;
            }
        }

        public Node SearchNode(string keyword)
        {
            var tmp = Head;
            for (int i = 0; i < Count - 1; i++)
            {
                if (tmp.Data != keyword) tmp = tmp.Next;
                else return tmp;
            }

            return null;
        }

        public bool SearchNodeBool(string keyword)
        {
            var tmp = Head;
            for (int i = 0; i < Count - 1; i++)
            {
                if (tmp.Data != keyword) tmp = tmp.Next;
                else return true;
            }

            return false;
        }

        public int IndexOf(string keyword)
        {
            var tmp = Head;
            for (int i = 0; i < Count - 1; i++)
            {
                if (tmp.Data != keyword) tmp = tmp.Next;
                else return i;
            }

            throw new Exception("Node does not exist");
        }
    }
}

