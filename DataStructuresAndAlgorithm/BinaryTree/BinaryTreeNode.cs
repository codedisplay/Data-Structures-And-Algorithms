namespace DataStructuresAndAlgorithm.BinaryTree
{
    public class BinaryTreeNode<TNode> :System.IComparable<TNode>
        where TNode: System.IComparable<TNode>
    {
        public BinaryTreeNode(TNode value)
        {
            this.Value = value;
        }
        public TNode Value { get; private set; }
        
        public BinaryTreeNode<TNode> Left { get; set; }
        public BinaryTreeNode<TNode> Right { get; set; }

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
    }
}
