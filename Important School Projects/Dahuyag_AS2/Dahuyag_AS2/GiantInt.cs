using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Assessment_1;
using LinkedListCore;

namespace Dahuyag_AS2
{
    public class GiantInt : IComparable<GiantInt>
    {
        private ILinkedList<int> _list = new GenericDoublyLinkedList<int>();

        private char[] _alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public ILinkedList<Occurances> numberOccurances { get; } = new GenericDoublyLinkedList<Occurances>();

        public GiantInt(string numberString)
        {
            List<string> splits = new List<string>();
            bool negative = false;

            if (numberString.Contains('-') && numberString.IndexOf('-',1) != -1) throw new InvalidOperationException("Invalid Input");
            if (numberString.Contains('-') && numberString.IndexOf('-') == 0)
            {
                negative = true;
                numberString = numberString.Remove(0, 1);
            }

            if (numberString.Contains(','))
            {
                splits.AddRange(numberString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
            else
            {
                while (numberString.Length % 3 != 0)
                {
                    numberString = numberString.Insert(0, "0");
                }
                for (int i = 0; i < numberString.Length / 3; i++)
                {
                    splits.Add(numberString.Substring(i*3,3));
                }
            }

            for (var index = 0; index < splits.Count; index++)
            {
                var number = splits[index];
                if (negative && index == 0) _list.AddToTail(Convert.ToInt32(number.Trim()) * -1);
                else
                {
                    if (number.Trim().Length > 3) throw new InvalidOperationException();
                    _list.AddToTail(Convert.ToInt32(number.Trim()));
                }
            }

            while (_list.Head.Data == 0 && _list.Count > 1)
            {
                _list.RemoveFromHead();
            }
        }
        public GiantInt(string[] numbers)
        {
            bool negative = false;
            
            if (numbers[0].Contains('-') && numbers[0].IndexOf('-') == 0)
            {
                negative = true;
                numbers[0] = numbers[0].Remove(0, 1);
            }

            for (var index = 0; index < numbers.Length; index++)
            {
                var number = numbers[index];
                if (number.Contains('-') && index != 0) 
                    throw new InvalidOperationException("Invalid Input");
                if (negative && index == 0) _list.AddToTail(Convert.ToInt32(number.Trim()) * -1);
                else
                {
                    if (Convert.ToInt32(number.Trim()) > 999 || Convert.ToInt32(number.Trim()) < 0)
                        throw new InvalidOperationException("Out of range");
                    _list.AddToTail(Convert.ToInt32(number.Trim()));
                }
            }
            while (_list.Head.Data == 0 && _list.Count > 1)
            {
                _list.RemoveFromHead();
            }
        }

        public int CompareTo(GiantInt other)
        {
            var comparingNode = _list.Head;
            var otherNode = other._list.Head;
            if (_list.Count == other._list.Count)
            {
                while (comparingNode != null)
                {
                    if (comparingNode.Data == otherNode.Data)
                    {
                        comparingNode = comparingNode.Next;
                        otherNode = otherNode.Next;
                    }
                    else if (comparingNode.Data > otherNode.Data) return 1;
                    else return -1;
                }
                return 0;
            }
            if (_list.Count < other._list.Count) return -1;
            return 1;
        }

        public GiantInt Add(GiantInt ItemToAdd)
        {
            var comparingNode = _list.Tail;
            var otherNode = ItemToAdd._list.Tail;
            int result;
            string stringResult = "";
            var carryOver = 0;

            while (comparingNode != null || otherNode != null)
            {
                if (comparingNode == null) result = otherNode.Data + carryOver;
                else if (otherNode == null) result = comparingNode.Data + carryOver;
                else result = comparingNode.Data + otherNode.Data + carryOver;
                if (result > 999)
                {
                    carryOver = 1;
                    if (comparingNode.Prev != null || otherNode.Prev != null)
                    {
                        stringResult = stringResult.Insert(0, $"{result-1000:000}");
                    }
                    else
                    {
                        stringResult = stringResult.Insert(0, $"{result:000}");
                    }
                }
                else
                {
                    stringResult = stringResult.Insert(0, $"{result:000}");
                }
                if (comparingNode != null) comparingNode = comparingNode.Prev;
                if (otherNode != null) otherNode = otherNode.Prev;
            }
            return new GiantInt(stringResult);
        }

        public GiantInt Subtract(GiantInt ItemToAdd)
        {
            var comparingNode = _list.Tail;
            var otherNode = ItemToAdd._list.Tail;
            /*var resultInt = new GiantInt()*/
            int result;
            string stringResult = "";
            var carryOver = 0;
            while (comparingNode != null || otherNode != null)
            {
                if (comparingNode == null) result = otherNode.Data - carryOver;
                else if (otherNode == null) result = comparingNode.Data - carryOver;
                else result = comparingNode.Data - otherNode.Data - carryOver;
                if (result < 0)
                {
                    carryOver = 1;
                    if (comparingNode.Prev != null || otherNode.Prev != null)
                    {
                        stringResult = stringResult.Insert(0, $"{result + 1000:000}");
                    }
                    else
                    {
                        stringResult = stringResult.Insert(0, $"{result:000}");
                    }
                }
                else
                {
                    stringResult = stringResult.Insert(0, $"{result:000}");
                }
                if (comparingNode != null) comparingNode = comparingNode.Prev;
                if (otherNode != null) otherNode = otherNode.Prev;
            }
            return new GiantInt(stringResult);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (_list.Count == 0) sb.Append($"{0F:F3}");
            else if (_list.Count == 1) sb.Append($"{_list.Head.Data:F3}");
            else
            {
                sb.Append($"{_list.Head.Data}.");
                sb.Append($"{_list.Head.Next.Data:000}");

                switch (_list.Count)
                {
                    case 2:
                        sb.Append('k');
                        break;
                    case 3:
                        sb.Append('M');
                        break;
                    case 4:
                        sb.Append('G');
                        break;
                    case 5:
                        sb.Append('T');
                        break;
                    default:
                        sb.Append(_alpha[_list.Count - 5]);
                        sb.Append(_alpha[_list.Count - 5]);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
