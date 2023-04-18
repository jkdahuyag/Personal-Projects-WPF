namespace GenericStack
{
    public interface IStack<T>
    {
        int Count { get; }

        void Clear();
        bool IsEmpty();
        void Push(T data);
        T Pop();
        T Peek();
    }
}
