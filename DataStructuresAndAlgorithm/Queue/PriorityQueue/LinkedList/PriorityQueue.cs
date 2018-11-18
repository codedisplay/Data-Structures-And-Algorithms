namespace DataStructuresAndAlgorithm.Queue.PriorityQueue.LinkedList
{
    public class PriorityQueue<T> : System.Collections.Generic.IEnumerable<T>
        where T : System.IComparable<T>
    {
        System.Collections.Generic.LinkedList<T> _list =
            new System.Collections.Generic.LinkedList<T>();

        public void Enqueue(T value)
        {
            if (_list.Count == 0)
                _list.AddLast(value);
            else
            {
                System.Collections.Generic.LinkedListNode<T> temp = 
                    _list.First;

                while (temp != null && temp.Value.CompareTo(value) > 0)
                {
                    temp = temp.Next;
                }

                if (temp == null)
                {
                    _list.AddLast(value);
                }
                else
                {
                    _list.AddBefore(temp, value);
                }
            }
        }

        //TODO: implement Dequeue and other methods below
        //public T Dequeue()
        //{

        //}


        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
            //return ((System.Collections.Generic.IEnumerable<T>)this).GetEnumerator();
        }
    }
}
