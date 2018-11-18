namespace DataStructuresAndAlgorithm.Stack.LinkedList
{
    public class Stack<T> : System.Collections.Generic.IEnumerable<T>
    {
        private System.Collections.Generic.LinkedList<T> _list =
            new System.Collections.Generic.LinkedList<T>();

        public void Push(T value)
        {
            _list.AddFirst(value);
        }

        public T Pop()
        {
            if (_list.Count == 0)
                throw new System.Exception();

            T temp = _list.First.Value;

            _list.RemoveFirst();

            return temp;
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
