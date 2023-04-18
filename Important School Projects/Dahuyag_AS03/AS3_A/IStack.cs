using Dahuyag_Assessment_1;

namespace AS3_A
{
    public interface IStack<T> : IEnumerable<T>
    {
        int Count { get; }

        void Clear();
        bool IsEmpty();
        void Push(T data);
        T Pop();
        T Peek();
        bool HasSameContents(IStack<T> other);
        bool HasEquivalentContents(IStack<T> other);
        void Swap(IStack<T> other);
        void Flip();
        IStack<T> CloneBottomToTop();
        IStack<T> CloneTopToBottom();
    }
}
