namespace DataStructuresAndAlgorithm.Queue.Array
{
    public class Queue<T> : System.Collections.Generic.IEnumerable<T>
    {
        private T[] _items = new T[0];

        private int _size;//total items in array;
        private int _head;
        private int _tail=-1;

        public void Enqueue(T item)
        {
            // grow array
            if (_size == _items.Length)
            {
                int tempSize = _size == 0 ? 4 : _size * 2;

                T[] newArray = new T[tempSize];
                //_items.CopyTo(newArray,0);
                //reset _head and _tail to zero here? and sort the array
                if (_size > 0)// multiples of 4
                {
                    //
                    int index = 0;

                    if (_head > _tail)
                    {
                        for (int i = _head; i < _items.Length; i++)
                        {
                            newArray[index] = _items[i];
                            index++;
                        }

                        for (int i = 0; i <= _tail; i++)
                        {
                            newArray[index] = _items[i];
                            index++;
                        }
                    }
                    else//if(_head < _tail) // _head == _tail wont be possible here
                    {
                        for (int i = _head; i <= _tail; i++)
                        {
                            newArray[index] = _items[i];
                            index++;
                        }
                    }
                    
                    _tail = index - 1;
                }
                else
                {
                    _tail = -1;
                }

                _head = 0;

                _items = newArray;
            }

            if (_tail == _items.Length - 1)
            {
                _tail = 0;
            }
            else if (_tail == _head - 1)
            {
                _tail++;
            }

            _items[_tail] = item;

            _size++;
        }

        public T Dequeue()
        {
            if (_items.Length == 0)
                throw new System.Exception();

            T value = _items[_head];

            //_items[_head] = default(T);// not needed?
            if (_head == _items.Length - 1)
            {
                _head = 0;
            }
            else
            {
                _head++;
            }

            _size--;

            return value;
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            if (_size > 0)
            {
                if (_head>_tail)
                {
                    for (int i = _head; i < _items.Length; i++)
                    {
                        yield return _items[i];
                    }

                    for (int i = 0; i <= _tail; i++)
                    {
                        yield return _items[i];
                    }
                }
                else
                {
                    for (int i = _head; i <= _tail; i++)
                    {
                        yield return _items[i];
                    }
                }
            }

            
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
