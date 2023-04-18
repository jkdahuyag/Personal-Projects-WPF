namespace Dahuyag_AS5
{
    public class BinaryTreeNode<T>
    {
        public T Data { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public int Instances { get; private set; }

        public BinaryTreeNode(T data)
        {
            Instances = 1;
            Data = data;
        }

        public void AddInstance()
        {
            Instances++;
        }

    }

    public enum DeleteOptions
    {
        DeleteByMerging, DeleteByCopying
    }
}