namespace DataStructuresAndAlgorithm.BinaryTree
{
    public class BinaryTree<T> : System.Collections.Generic.IEnumerable<T>
        where T : System.IComparable<T>
    {
        private BinaryTreeNode<T> _head;
        private int _count;

        public void Add(T value)
        {
            if (_head == null)
            {
                _head = new BinaryTreeNode<T>(value);
            }
            else
            {
                AddTo(_head, value);
            }

            _count++;
        }

        public void AddTo(BinaryTreeNode<T> node, T value)
        {
            // value is less than current node value
            if (node.Value.CompareTo(value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
        }

        public bool Contains(T value)
        {
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        public bool Remove(T value)
        {
            // 3 cases:
            // a) Removed node has no right child
            //    -- Left child replaces removed 
            // b) Removed node has no left child  
            //    -- Right child replaces removed
            // c) Removed node's right child has left child
            //    -- Right child 's left most node replaces removed
            BinaryTreeNode<T> parent;
            BinaryTreeNode<T> toBeRemovedNode = FindWithParent(value, out parent);


            if (toBeRemovedNode == null)
            {
                return false;
            }

            // wrong implementation
            //if (toBeRemovedNode.Right == null)
            //{
            //    parent = toBeRemovedNode.Left;
            //}
            //else if (toBeRemovedNode.Right != null &&
            //    toBeRemovedNode.Right.Left == null)
            //{
            //    toBeRemovedNode = toBeRemovedNode.Right;
            //}
            //else if (toBeRemovedNode.Right != null &&
            //   toBeRemovedNode.Right.Left != null)
            //{
            //    toBeRemovedNode = toBeRemovedNode.Right.Left;
            //}

            // a)
            if (toBeRemovedNode.Right == null)
            {
                if (parent == null)
                {
                    _head = toBeRemovedNode.Left;
                }
                else
                {
                    if (parent.Value.CompareTo(toBeRemovedNode.Value) > 0)
                    {
                        // in case of parent > current value
                        // assign the current left child to left child of parent
                        parent.Left = toBeRemovedNode.Left;
                    }
                    else
                    {
                        parent.Right = toBeRemovedNode.Left;
                    }
                }
            }
            // b)
            else if (toBeRemovedNode.Right != null &&
                    toBeRemovedNode.Right.Left == null)
            {
                if (parent == null)
                {
                    _head = toBeRemovedNode.Right;
                }
                else
                {
                    if (parent.Value.CompareTo(toBeRemovedNode.Value) > 0)
                    {
                        // in case of parent > current value
                        // assign the current left child to left child of parent
                        parent.Left = toBeRemovedNode.Right;
                    }
                    else
                    {
                        parent.Right = toBeRemovedNode.Right;
                    }
                }
            }
            // c)
            // replace current with current's right child's left most child
            else
            {
                BinaryTreeNode<T> leftMostParent = toBeRemovedNode.Right;
                BinaryTreeNode<T> leftMost = toBeRemovedNode.Right.Left;

                while (leftMost.Left != null)
                {
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }

                leftMostParent.Left = leftMost.Right;

                leftMost.Left = toBeRemovedNode.Left;
                leftMost.Right = toBeRemovedNode.Right;

                if (parent == null)
                {
                    _head = leftMost;
                }
                else
                {
                    if (parent.Value.CompareTo(toBeRemovedNode.Value) > 0)
                    {
                        // in case of parent > current value
                        // assign the current left child to left child of parent
                        parent.Left = leftMost;
                    }
                    else
                    {
                        parent.Right = leftMost;
                    }
                }
            }

            _count--;
            return true;
        }

        public BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            BinaryTreeNode<T> temp = _head;

            while (temp != null)
            {
                // value is less than current node value
                if (temp.Value.CompareTo(value) < 0)
                {
                    parent = temp;
                    temp = temp.Left;
                }
                else if (temp.Value.CompareTo(value) > 0)
                {
                    parent = temp;
                    temp = temp.Right;
                }
                else// temp.value == value 
                {
                    break;
                }
            }

            return FindWithParent(value, out parent);
        }

        // Recursive approach is not preferrable in production quality tree
        #region Recursive Approach

        public void PreOrderTraversal(System.Action<T> action)
        {
            PreOrderTraversal(action, _head);
        }

        private void PreOrderTraversal(System.Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }

        public void PostOrderTraversal(System.Action<T> action)
        {
            PostOrderTraversal(action, _head);
        }

        private void PostOrderTraversal(System.Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }

        public void InOrderTraversal(System.Action<T> action)
        {
            InOrderTraversal(action, _head);
        }

        private void InOrderTraversal(System.Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(action, node.Left);
                action(node.Value);
                InOrderTraversal(action, node.Right);
            }
        }

        //private void PerformSomething(T value)
        //{
        //    //PerformSomething here
        //}

        #endregion

        #region Non Recursive Approach

            // TODO: create missing traversal methods and move to different class?

        public System.Collections.Generic.IEnumerator<T> InOrderTraversal()
        {
            if (_head != null)
            {
                System.Collections.Generic.Stack<BinaryTreeNode<T>> stack = 
                    new System.Collections.Generic.Stack<BinaryTreeNode<T>>();

                BinaryTreeNode<T> current = _head;

                bool goLeftNext = true;

                stack.Push(current);

                while (current != null)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current.Left);
                            current = current.Left;
                        }   
                    }

                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = false;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        #endregion

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
           return this.GetEnumerator();
        }
    }
}
