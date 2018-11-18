namespace DataStructuresAndAlgorithm.LinkedList.SinglyLinkedList
{
    public class LinkedList<T> : System.Collections.Generic.IEnumerable<T>
    {
        public int Count { get; private set; }
        public LinkedListNode<T> Head { get; private set; }
        public LinkedListNode<T> Tail { get; private set; }

        public LinkedList()
        {
            //Head = Tail = null;//not needed
        }

        public void AddFirst(T value)
        {
            AddFirst(new LinkedListNode<T>(value));
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            LinkedListNode<T> temp = Head;
            Head = node;
            Head.Next = temp;

            Count++;

            if (Count == 1) //or Tail == null
                Tail = Head;
        }

        public void AddLast(T value)
        {
            AddLast(new LinkedListNode<T>(value));
        }

        // TODO: handle the code for the case when node passed is 
        // already existing(duplicate) in the LinkedList
        public void AddLast(LinkedListNode<T> node)
        {
            if (Count == 0)// or Head == null
                Head = node;
            else
                Tail.Next = node;

            Tail = node;

            Count++;
        }

        public void RemoveFirst()
        {
            if (Count == 0)
                return;// or throw exception

            Head = Head.Next;

            Count--;

            if (Count == 0)
                Tail = null;
        }

        public void RemoveLast()
        {
            if (Count == 0)
                return;

            if (Count == 1)
            {
                Head = Tail = null;
            }
            else
            {
                LinkedListNode<T> temp = Head;

                while (temp.Next != Tail)
                {
                    temp.Next = null;
                }

                Tail = temp;
            }
            Count--;
        }

        #region ICollection

        public void Add(T item)
        {
            AddFirst(item);
        }

        public bool Remove(T item)
        {
            LinkedListNode<T> currentTemp = Head;
            LinkedListNode<T> previousTemp = null;

            // TODO: Refactor below logic
            while (currentTemp != null)
            {
                if (currentTemp.Value.Equals(item))
                {
                    if (Count == 1)
                    {
                        //Head = currentValue.Next;
                        //Tail = null;
                        RemoveFirst();
                    }
                    else if (previousTemp == null)
                    {
                        //Head = currentTemp.Next;
                        RemoveFirst();
                    }
                    else
                    {
                        previousTemp.Next = currentTemp.Next;

                        if (currentTemp.Next == null)
                            Tail = previousTemp;

                        Count--;
                    }

                    return true;
                }

                previousTemp = currentTemp;
                currentTemp = currentTemp.Next;
            }

            return false;
        }

        public bool Contains(T item)
        {
            LinkedListNode<T> temp = Head;

            while (temp != null)
            {
                if (temp.Value.Equals(item))
                {
                    return true;
                }

                temp = temp.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            LinkedListNode<T> temp = Head;

            while (temp != null)
            {
                array[arrayIndex++] = temp.Value;
                temp = temp.Next;
            }
        }

        public void Clear()
        {
            // Very simple to implement in Garbage Collected environment
            Head = null;
            Tail = null;
            Count = 0;
        }

        #endregion

        #region System.Collections.Generic.IEnumerable<T> implementation

        public System.Collections.Generic.IEnumerator<T>
            GetEnumerator()
        {
            LinkedListNode<T> temp = Head;

            while (temp != null)
            {
                yield return temp.Value;
                temp = temp.Next;
            }
        }

        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.Generic.IEnumerable<T>)this).GetEnumerator();
        }

        #endregion
    }
}
