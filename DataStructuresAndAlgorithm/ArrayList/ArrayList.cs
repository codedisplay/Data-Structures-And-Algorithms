namespace DataStructuresAndAlgorithm.ArrayList
{
    public class ArrayList
    {
        private object[] _items = new object[0];

        // Count of current items in the Stack
        private int _size;

        public int Add(object value)
        {
            if (_items.Length == _size)
            {
                int newSize = _size == 0 ? 4 : _size * 2;

                object[] newArray = new object[newSize];
                _items.CopyTo(newArray, 0);
            }

            _items[_size] = value;
            return _size - 1;
        }

        public void Insert(int index, object value)
        {
            // CONFIRM: Is this needed?
            if (index < _items.Length || index < 0)
                throw new System.Exception();

            if (_items[index] != null)//CONFIRM: Is this needed?
            {
                if (_items.Length == _size)
                {
                    int newSize = _size == 0 ? 4 : _size * 2;

                    object[] newArray = new object[newSize];
                    _items.CopyTo(newArray, 0);
                }

                if (index < _size)
                {
                    object[] tempArray = _items;

                    for (int i = 0; i < index; i++)
                    {
                        tempArray[i] = _items[i];
                    }

                    for (int i = index; i < _items.Length; i++)
                    {
                        tempArray[i + 1] = _items[i];
                    }
                }
            }

            _items[index] = value;
            _size++;
        }

        public void Remove(object value)
        {
            int index = IndexOf(value);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index < _items.Length)
                throw new System.Exception();
       
            _items[index] = null;
            _size--;
        }

        public object GetItemAt(int index)
        {
            return _items[index];
        }

        public int IndexOf(object value)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        // Using Property and Indexer
        public object this[int index]
        {
            // now instead of arrayList.GetItemAt(0), arrayList[0] can be used
            get { return _items[index]; }
            // now arrayList[0]= value can be used to set value
            set { _items[index] = value; }
        }
    }
}
