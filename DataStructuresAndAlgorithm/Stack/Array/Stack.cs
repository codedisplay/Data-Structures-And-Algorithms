namespace DataStructuresAndAlgorithm.Stack.Array
{
    public class Stack<T> : System.Collections.Generic.IEnumerable<T>
    {
        // Array of items contained in the Stack.
        // Initialized to 0 length, will grow as needed during Push
        private T[] _items = new T[0];

        // Count of current items in the Stack
        private int _size;

        public void Push(T item)
        {
            // Grow array size if no array slot is available
            if (_size == _items.Length)
            {
                int tempSize = _size == 0 ? 4 : _size * 2;

                T[] tempArray = new T[tempSize];

                _items.CopyTo(tempArray, 0);
                //_items = newArray;//not needed?
            }

            _items[_size] = item;
            _size++;
        }

        public T Pop()
        {
            if (_items.Length == 0)
                throw new System.Exception();

            _size--;

            return _items[_size];
        }

        public T Peek()
        {
            if (_items.Length == 0)
                throw new System.Exception();

            return _items[_size - 1];
        }

        public int Count
        {
            get
            {
                return _size; // or _items.Length; 
            }
        }

        public void Clear()
        {
            // Setting to '0' doesn't clear the array
            // For integers that's fine
            // If the items have disposable or finalize  
            // then leaving them keep the references
            _size = 0;
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                yield return _items[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
