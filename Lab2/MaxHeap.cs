using System;
using System.Reflection;

namespace Lab2
{
	public class MaxHeap<T> where T: IComparable<T>
    {
        private T[] array;
        private const int initialSize = 8;

        public int Count { get; private set; }

        public int Capacity => array.Length;

        public bool IsEmpty => Count == 0;


        public MaxHeap(T[] initialArray = null)
        {
            array = new T[initialSize];

            if (initialArray == null)
            {
                return;
            }

            foreach (var item in initialArray)
            {
                Add(item);
            }

        }

        /// <summary>
        /// Returns the max item but does NOT remove it.
        /// Time complexity: O(1)
        /// </summary>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            return array[0];
        }

        // TODO
        /// <summary>
        /// Adds given item to the heap.
        /// Time complexity: O(log(n))
        /// </summary>
        public void Add(T item)
        {
            int nextEmptyIndex = Count;

            array[nextEmptyIndex] = item;

            TrickleUp(nextEmptyIndex);

            Count++;

            // resize if full
            if (Count == Capacity)
            {
                DoubleArrayCapacity();
            }

        }

        public T Extract()
        {
            return ExtractMax();
        }

        // TODO
        /// <summary>
        /// Removes and returns the max item in the max-heap.
        /// Time complexity: O( N )
        /// </summary>
        public T ExtractMax()
        {
            if (IsEmpty)
            {
                throw new Exception("Empty Heap");
            }

            T max = array[0];

            // swap root (first) and last element
            Swap(0, Count - 1);

            // "remove" last
            Count--;

            // trickle down from root (first)
            TrickleDown(0);

            return max;

        }

        // TODO
        /// <summary>
        /// Removes and returns the min item in the max-heap.
        /// Time complexity: O( log(n) )
        /// </summary>
        public T ExtractMin()
        {
            // linear search
            var min = array[0];
            foreach (var item in array)
            {
                if (item.CompareTo(min) < 0)
                {
                    min = item;
                    break;
                }
            }
            // remove min
            Remove(min);
            return min;
        }

        // TODO
        /// <summary>
        /// Returns true if the heap contains the given value; otherwise false.
        /// Time complexity: O( N )
        /// </summary>
        public bool Contains(T value)
        {
            // linear search

            foreach (var item in array)
            {
                if (item.CompareTo(value) == 0)
                {
                    return true;
                }
            }

            return false;

        }

        // TODO
        /// <summary>
        /// Updates the first element with the given value from the heap.
        /// Time complexity: O( log(n) )
        /// </summary>
        public void Update(T oldValue, T newValue)
        {
            if (Contains(oldValue))
            {
                for (int i = 0; i < Count; i++)
                {
                    if (array[i].CompareTo(oldValue) == 0)
                    {
                        array[i] = newValue;

                        if (newValue.CompareTo(oldValue) > 0)
                        {
                            TrickleUp(i);
                        }
                        if (newValue.CompareTo(oldValue) < 0)
                        {
                            TrickleDown(i);
                        }
                        return;
                    }
                }
            }
            else
            {
                throw new Exception("Value doesn't exist in array.");
            }         
        }

        // TODO
        /// <summary>
        /// Removes the first element with the given value from the heap.
        /// Time complexity: O( log(n) )
        /// </summary>
        public void Remove(T value)
        {
            if (Contains(value))
            {
                int index = 0;
                for(int i = 0; i < Count; i++)
                {
                    if (array[i].CompareTo(value) == 0)
                    {
                        index= i;
                        break;
                    }
                }
                Swap(index, Count - 1);
                Count--;
                TrickleDown(index);
            }
            else
            {
                throw new Exception("Value doesn't exist in array.");
            }
        }

        // TODO
        // Time Complexity: O( log(n) )
        private void TrickleUp(int index)
        {
            if (index == 0)
            {
                return;
            }
            if (array[index].CompareTo(array[Parent(index)]) > 0)
            {
                Swap(index, Parent(index));
                TrickleUp(Parent(index));
            }

        }

        // TODO
        // Time Complexity: O( log(n) )
        private void TrickleDown(int index)
        {
            if (LeftChild(index) == Count - 1 && array[index].CompareTo(array[LeftChild(index)]) < 0)
            {
                Swap(index, LeftChild(index));
                return;
            }
            if (RightChild(index) == Count - 1 && array[index].CompareTo(array[RightChild(index)]) < 0)
            {
                Swap(index, RightChild(index));
                return;
            }
            if (LeftChild(index) >  Count || RightChild(index) > Count)
            {
                return;
            }
            if (array[LeftChild(index)].CompareTo(array[RightChild(index)]) > 0 && array[index].CompareTo(array[LeftChild(index)]) < 0)
            {
                Swap(index, LeftChild(index));
                TrickleDown(LeftChild(index));
            }
            if (array[index].CompareTo(array[RightChild(index)]) < 0)
            {
                Swap(index, RightChild(index));
                TrickleDown(RightChild(index));
            }

        }

        // TODO
        /// <summary>
        /// Gives the position of a node's parent, the node's position in the heap.
        /// </summary>
        private static int Parent(int position)
        {
            return (position - 1) / 2;
        }

        // TODO
        /// <summary>
        /// Returns the position of a node's left child, given the node's position.
        /// </summary>
        private static int LeftChild(int position)
        {
            return position * 2 + 1;
        }

        // TODO
        /// <summary>
        /// Returns the position of a node's right child, given the node's position.
        /// </summary>
        private static int RightChild(int position)
        {
            return position * 2 + 2;
        }

        private void Swap(int index1, int index2)
        {
            var temp = array[index1];

            array[index1] = array[index2];
            array[index2] = temp;
        }

        private void DoubleArrayCapacity()
        {
            Array.Resize(ref array, array.Length * 2);
        }


    }
}

