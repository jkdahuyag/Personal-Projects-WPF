using MyQueue;

namespace Dahuyag_AS5;

public class BinaryTree<T>
{
    public BinaryTreeNode<T> Root { get; set; }
    /// <summary>
    /// Returns the number of nodes in the tree
    /// </summary>
    public int Count { get; private set; }


    public BinaryTree()
    {
        Root = null;
    }
    /// <summary>
    /// Create a binary tree from a list of data
    /// </summary>
    /// <param name="listOfData"></param>
    /// <param name="comparer"></param>
    public BinaryTree(IEnumerable<T> listOfData, IComparer<T> comparer = null)
    {
        if (listOfData == null) throw new InvalidOperationException("Cannot pass null data");
        if (comparer == null) comparer = Comparer<T>.Default;
        Insert(listOfData, comparer);
    }

    public void Insert(T data, IComparer<T> comparer = null)
    {
        if (comparer == null) comparer = Comparer<T>.Default;
        InsertHelper(Root, data, comparer);
    }
    public void Insert(IEnumerable<T> data, IComparer<T> comparer = null)
    {
        if (comparer == null) comparer = Comparer<T>.Default;
        foreach (var node in data)
        {
            Insert(node, comparer);
        }
    }
    //Recursive adding of nodes
    void InsertHelper(BinaryTreeNode<T> x, T y, IComparer<T> comparer)
    {
        if (Root == null)
        {
            Root = new BinaryTreeNode<T>(y);
            Count++;
        }
        else if (comparer.Compare(x.Data, y) == 1)
        {
            if (x.Left == null)
            {
                x.Left = new BinaryTreeNode<T>(y);
                Count++;
            }
            else
                InsertHelper(x.Left, y, comparer);
        }
        else if (comparer.Compare(x.Data, y) == -1)
        {
            if (x.Right == null)
            {
                x.Right = new BinaryTreeNode<T>(y);
                Count++;
            }
            else
                InsertHelper(x.Right, y, comparer);
        }
        else //logic that updates instances
        {
            x.AddInstance();
        }
    }
    public int GetChildrenCount(BinaryTreeNode<T> node)
    {
        if (node == null) throw new InvalidOperationException("The node does not exist");
        int count = 0;
        if (node.Left != null) count++;
        if (node.Right != null) count++;
        return count;
    }

    public BinaryTreeNode<T> GetParent(BinaryTreeNode<T> node, IComparer<T> comparer = null)
    {
        if (comparer == null) comparer = Comparer<T>.Default;
        if (Root == null) throw new InvalidOperationException("Empty Tree");

        BinaryTreeNode<T> prev = null;
        var curr = Root;
        while (curr != null)
        {
            int result = comparer.Compare(node.Data, curr.Data);

            //the current node has been found
            //thus, the parent that was set before
            //should also be found
            if (result == 0) return prev;
            prev = curr;
            if (result < 0) curr = curr.Left;
            if (result > 0) curr = curr.Right;
        }

        throw new InvalidOperationException("The element does not exist");
    }
    /// <summary>
    /// Searches for a node in the tree with the same data as the given node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="comparer"></param>
    /// <param name="startingNode"></param>
    /// <returns>The node with the same date. Throws error if data is not found</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public BinaryTreeNode<T> Search(BinaryTreeNode<T> node, IComparer<T> comparer = null, BinaryTreeNode<T> startingNode = null)
    {
        if (comparer == null) comparer = Comparer<T>.Default;
        if (startingNode == null) startingNode = Root;
        var tmp = startingNode;
        while (tmp != null)
        {
            if (comparer.Compare(tmp.Data, node.Data) == 0) return tmp;
            if (comparer.Compare(tmp.Data, node.Data) == 1)
                tmp = tmp.Left;
            else
                tmp = tmp.Right;
        }
        throw new InvalidOperationException("Node does not exist");
    }
    public BinaryTreeNode<T> Search(T data, IComparer<T> comparer = null)
    {
        if (comparer == null) comparer = Comparer<T>.Default;
        if (Root == null) throw new InvalidOperationException("Empty Tree");

        var curr = Root;
        while (curr != null)
        {
            int result = comparer.Compare(data, curr.Data);
            switch (result)
            {
                case 0:
                    return curr;
                case < 0:
                    curr = curr.Left;
                    break;
                case > 0:
                    curr = curr.Right;
                    break;
            }
        }
        throw new InvalidOperationException("The element does not exist");
    }
    /// <summary>
    /// Gets the level of a specified node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="comparer"></param>
    /// <returns>int level of the node</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int Level(BinaryTreeNode<T> node, IComparer<T> comparer = null)
    {
        if (comparer == null) comparer = Comparer<T>.Default;
        var tmp = Root;
        var level = 1;
        while (tmp != null)
        {
            if (comparer.Compare(tmp.Data, node.Data) == 0) return level;
            if (comparer.Compare(tmp.Data, node.Data) == 1)
                tmp = tmp.Left;
            else
                tmp = tmp.Right;
            level++;
        }
        throw new InvalidOperationException("Node does not exist");
    }
    /// <summary>
    /// Gets the number of nodes that has no children
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public int GetLeaves(BinaryTreeNode<T> node = null)
    {
        var queue = new QueueList<BinaryTreeNode<T>>();
        if (node == null) node = Root;
        var tmp = node;
        var leavesCount = 0;
        if (tmp != null)
        {
            queue.Enqueue(tmp);
            while (queue.Count != 0)
            {
                tmp = queue.Dequeue();
                if (GetChildrenCount(tmp) == 0) leavesCount++;
                if (tmp.Left != null)
                {
                    queue.Enqueue(tmp.Left);
                }
                if (tmp.Right != null)
                {
                    queue.Enqueue(tmp.Right);
                }
            }
        }

        return leavesCount;
    }
    /// <summary>
    /// Gets the number of nodes that has at least one children
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public int GetNonLeaves(BinaryTreeNode<T> node = null)
    {
        var queue = new QueueList<BinaryTreeNode<T>>();
        if (node == null) node = Root;
        var tmp = node;
        var nonLeavesCount = 0;
        if (tmp != null)
        {
            queue.Enqueue(tmp);
            while (queue.Count != 0)
            {
                tmp = queue.Dequeue();
                if (GetChildrenCount(tmp) != 0) nonLeavesCount++;
                if (tmp.Left != null)
                {
                    queue.Enqueue(tmp.Left);
                }
                if (tmp.Right != null)
                {
                    queue.Enqueue(tmp.Right);
                }
            }
        }

        return nonLeavesCount;
    }

    public void Clear()
    {
        Root = null;
    }

    public bool isEmpty()
    {
        return Root == null;
    }
    /// <summary>
    /// Top-Down Left-Right Traversal
    /// </summary>
    /// <param name="startingNode"></param>
    /// <returns>A Queue with the elements in Breadth-First Order</returns>
    public QueueList<T> BreadthFirst(BinaryTreeNode<T> startingNode = null)
    {
        if (startingNode == null) startingNode = Root;
        var result = new QueueList<T>();
        var queue = new QueueList<BinaryTreeNode<T>>();
        var tmp = Root;
        if (tmp != null)
        {
            queue.Enqueue(tmp);
            while (queue.Count != 0)
            {
                tmp = queue.Dequeue();
                result.Enqueue(tmp.Data);
                if (tmp.Left != null)
                {
                    queue.Enqueue(tmp.Left);
                }
                if (tmp.Right != null)
                {
                    queue.Enqueue(tmp.Right);
                }
            }
        }
        return result;
    }
    /// <summary>
    /// Tree Traversal - LVR
    /// </summary>
    /// <param name="startingNode"></param>
    /// <returns>Returns a QueueList with the elements of Tree in ascending order</returns>
    public QueueList<T> InOrder(BinaryTreeNode<T> startingNode = null)
    {
        var result = new QueueList<T>();
        if (startingNode == null) startingNode = Root;

        InOrderHelper(startingNode, result);
        return result;
    }
    protected void InOrderHelper(BinaryTreeNode<T> node, QueueList<T> resultQueue)
    {
        if (node != null)
        {
            InOrderHelper(node.Left, resultQueue);
            resultQueue.Enqueue(Visit(node));
            InOrderHelper(node.Right, resultQueue);
        }
    }
    /// <summary>
    /// Tree Traversal - VLR
    /// </summary>
    /// <param name="startingNode"></param>
    /// <returns>Returns a QueueList with the elements of Tree in pre-order arrangement</returns>
    public QueueList<T> PreOrder(BinaryTreeNode<T> startingNode = null)
    {
        var result = new QueueList<T>();
        if (startingNode == null) startingNode = Root;
        PreOrderHelper(startingNode, result);
        return result;
    }
    protected void PreOrderHelper(BinaryTreeNode<T> node, QueueList<T> resultQueue)
    {
        if (node != null)
        {
            resultQueue.Enqueue(Visit(node));
            PreOrderHelper(node.Left, resultQueue);
            PreOrderHelper(node.Right, resultQueue);
        }
    }
    /// <summary>
    /// Tree Traversal - LRV (Traverses the tree from the roots up)
    /// </summary>
    /// <param name="startingNode"></param>
    /// <returns>Returns a QueueList with the elements of Tree in post-order arrangement</returns>
    public QueueList<T> PostOrder(BinaryTreeNode<T> startingNode = null)
    {
        var result = new QueueList<T>();
        if (startingNode == null) startingNode = Root;
        PostOrderHelper(startingNode, result);
        return result;
    }
    protected void PostOrderHelper(BinaryTreeNode<T> node, QueueList<T> resultQueue)
    {
        if (node != null)
        {
            PostOrderHelper(node.Left, resultQueue);
            PostOrderHelper(node.Right, resultQueue);
            resultQueue.Enqueue(Visit(node));
        }
    }


    protected virtual T Visit(BinaryTreeNode<T> node)
    {
        return node.Data;
    }
}