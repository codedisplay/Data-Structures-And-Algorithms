namespace DataStructuresAndAlgorithm.Queue.LinkedList
{
    public class Queue<T> : System.Collections.Generic.IEnumerable<T>
    {
        System.Collections.Generic.LinkedList<T> _list =
            new System.Collections.Generic.LinkedList<T>();

        public void Enqueue(T value)
        {
            _list.AddLast(value);
        }

        public T Dequeue()
        {
            if (_list.Count == 0)
                throw new System.Exception();

            T value = _list.Last.Value;

            _list.RemoveFirst();

            return value;
        }

        public T Peek()
        {
            if (_list.Count == 0)
                throw new System.Exception();

            return _list.First.Value;
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public void Clear()
        {
            _list.Clear();
        }

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
