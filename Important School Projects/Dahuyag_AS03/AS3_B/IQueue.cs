using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Assessment_1;

namespace AS3_B
{
    public interface IQueue<T> : IEnumerable<T>
    {
        int Count { get; }

        void Clear();
        bool IsEmpty();
        void Enqueue(T data);
        T Dequeue();
        T Peek();
        bool HasSameContents(IQueue<T> other);
        bool HasEquivalentContents(IQueue<T> other);
        void Swap(IQueue<T> other);
        void Flip();
        IQueue<T> CloneLeftToRight();
        IQueue<T> CloneRightToLeft();
    }
}
