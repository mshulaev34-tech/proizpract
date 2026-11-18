using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Helpers
{
    /// <summary>
    /// Static Array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyStack<T>
    {
        private T[] _items;
        private int _count;

        public MyStack(int capacity = 4)
        {
            _items = new T[capacity];
            _count = 0;
        }

        /// <summary>
        /// Adds an item to the top of the stack.
        /// </summary>
        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }
            _items[_count++] = item;
        }

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
            return _items[--_count];
        }

        /// <summary>
        /// Returns the item at the top of the stack without removing it.
        /// </summary>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
            return _items[_count - 1];
        }

        /// <summary>
        /// Gets the number of elements in the stack.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gets a value indicating whether the stack is empty.
        /// </summary>
        public bool IsEmpty => _count == 0;
    }
    /// <summary>
    /// Dynamic Array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyQueue<T>
    {
        private T[] _items;
        private int _head;
        private int _tail;
        private int _count;

        public MyQueue(int capacity = 4)
        {
            _items = new T[capacity];
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        /// <summary>
        /// Adds an item to the end of the queue.
        /// </summary>
        public void Enqueue(T item)
        {
            if (_count == _items.Length)
            {
                T[] newArray = new T[_items.Length * 2];
                if (_count > 0)
                {
                    if (_head < _tail)
                    {
                        Array.Copy(_items, _head, newArray, 0, _count);
                    }
                    else
                    {
                        Array.Copy(_items, _head, newArray, 0, _items.Length - _head);
                        Array.Copy(_items, 0, newArray, _items.Length - _head, _tail);
                    }
                }
                _items = newArray;
                _head = 0;
                _tail = _count;
            }
            _items[_tail] = item;
            _tail = (_tail + 1) % _items.Length;
            _count++;
        }

        /// <summary>
        /// Removes and returns the item at the beginning of the queue.
        /// </summary>
        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Queue is empty.");
            }
            T item = _items[_head];
            _head = (_head + 1) % _items.Length;
            _count--;
            return item;
        }

        /// <summary>
        /// Returns the item at the beginning of the queue without removing it.
        /// </summary>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Queue is empty.");
            }
            return _items[_head];
        }

        /// <summary>
        /// Gets the number of elements in the queue.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gets a value indicating whether the queue is empty.
        /// </summary>
        public bool IsEmpty => _count == 0;
    }
    public static class GenericUtils
    {
        /// <summary>
        /// Swaps the values of two variables.
        /// </summary>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Returns the greater of two values based on IComparable<T>.
        /// </summary>
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }
}
