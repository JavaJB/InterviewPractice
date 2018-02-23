using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sorts
{
    public class MySorts <T> where T : IComparable<T>, IEquatable<T>
    {
        public Type type { get; }
        public Collection<T> collection { get; private set; }
        /// <summary>
        /// Obligatory constructor. ideas for interesting features???
        /// </summary>
        public MySorts()
        {
            type = typeof(T);
            collection = new Collection<T>();
        }

        public MySorts(IEnumerable<T> enumerable)
        {
            collection = new Collection<T>();
            foreach(T item in enumerable)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="recursive"></param>
        public void MySelectionSort(Collection<T> collection, bool recursive)
        {
            if(recursive)
            {
                SelectionSortRecursive(collection, 0);
            }
            else
            {
                SelectionSortIterative(collection, 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="idx"></param>
        private void SelectionSortRecursive(Collection<T> collection, int idx)
        {
            if(idx < collection.Count - 1) // recursive case
            {
                Swap(idx, IndexOfMinimum(collection, idx), collection); // swapping the current element with the minimum element found in the collection
                SelectionSortRecursive(collection, ++idx); // recursive call
            }
            else //base case
            {
                return; //the collection is sorted at this point
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="collection"></param>
        private void Swap(int index1, int index2, Collection<T> collection)
        {
            if (index1 != index2)
            {
                T temp = collection[index2];
                collection[index2] = collection[index1];
                collection[index1] = temp;
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private int IndexOfMinimum(Collection<T> collection, int startIndex)
        {
            int minimumValueIndex = startIndex;
            for(int i = startIndex; i < collection.Count - 1; i++)
            {
                if(collection.ElementAt(i).CompareTo(collection.ElementAt(minimumValueIndex)) < 0) //new item is l.t. current minimum
                {
                    minimumValueIndex = i;
                }
            }
            return minimumValueIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="start"></param>
        private void SelectionSortIterative(Collection<T> collection, int start)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="recursive"></param>
        public void MyInsertionSort(Collection<T> collection, bool recursive)
        {
            if(!recursive)
            {
                InsertionSortIterative(collection);
            }
            else
            {
                InsertionSortRecursive(collection);
            }
        }

        /// <summary>
        /// BEST CASE: O(n)
        /// AVG CASE: O(n^2)
        /// WORST CASE: O(n^2)
        /// </summary>
        /// <param name="collection"></param>
        private void InsertionSortIterative(Collection<T> collection)
        {
            for(int i = 1; i < collection.Count; i++)
            {
                T value = collection.ElementAt(i);
                for (int ii = 0; ii < i; ii++)
                {
                    if(collection.ElementAt(ii).CompareTo(value) > 0)
                    {
                        //copy array between ii+1 to i to shift it right
                        //set value of item at ii to be value
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        private void InsertionSortRecursive(Collection<T> collection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="recursive"></param>
        public Collection<T> MyQuickSort(Collection<T> collection, bool recursive)
        {
            if(recursive)
            {
                return QuickSortRecursive(collection);
            }
            else
            {
                return QuickSortIterative(collection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        private Collection<T> QuickSortRecursive(Collection<T> collection)
        {
            if(collection.Count < 2)
            {
                return collection;
            }
            int pivotIndex = CalculatePivotIndex(collection);
            T pivotValue = collection.ElementAt(pivotIndex);

            Collection<T> leftCollec = new Collection<T>();
            Collection<T> rightCollec = new Collection<T>();
            for(int i = 0; i < collection.Count; i++)
            {
                if(i == pivotIndex)
                {
                    continue;
                }
                T value = collection.ElementAt(i);
                if(value.CompareTo(pivotValue) < 0)
                {
                    leftCollec.Add(value);
                }
                else
                {
                    rightCollec.Add(value);
                }
            }
            //recursive call
            leftCollec = QuickSortRecursive(leftCollec);
            rightCollec = QuickSortRecursive(rightCollec);

            //merge subsets
            Collection<T> sorted = new Collection<T>();
            for(int i = 0; i < leftCollec.Count; i++)
            {
                sorted.Add(leftCollec[i]);
            }
            sorted.Add(pivotValue);
            for(int i = 0; i < rightCollec.Count; i++)
            {
                sorted.Add(rightCollec[i]);
            }
            return sorted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        private Collection<T> QuickSortIterative(Collection<T> collection)
        {
            throw new NotImplementedException();
        }

        private int CalculatePivotIndex(Collection<T> collection)
        {
            //want to calc the average of all items in the list
            //then return the first index of the value that is closest to this value
            //Nice idea and all, but what do you do when its a collection of shapes, or
            //something that isn't readily averageable?
            return collection.Count/2;
        }
        //TODO: add in bool for hybrid call using insertion sort for a user specified index threshold
        public Collection<T> MyMergeSort(Collection<T> collection, bool recursive)
        {
            if(recursive)
            {
                return MergeSortRecursive(collection);
            }
            else
            {
                return MergeSortIterative(collection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private Collection<T> MergeSortRecursive(Collection<T> collection)
        {
            if(collection.Count < 2) //base case
            {
                return collection;
            }
            Collection<T> left = new Collection<T>();
            Collection<T> right = new Collection<T>();
            for (int i = 0; i < collection.Count-1; i++) //split the collection into 2 sub collections
            {
                if(i < (collection.Count-1)/2)
                {
                    left.Add(collection[i]);
                }
                else
                {
                    right.Add(collection[i]);
                }
            }

            left = MergeSortRecursive(left);
            right = MergeSortRecursive(right);

            return MyMerge(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private Collection<T> MergeSortIterative(Collection<T> collection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private Collection<T> MyMerge(Collection<T> left, Collection<T> right)
        {
            Collection<T> merged = new Collection<T>();
            int lidx = 0;
            int ridx = 0;
            while(lidx < left.Count && ridx < right.Count)
            {
                if(left[lidx].CompareTo(right[ridx]) <= 0) //l.e.q.
                {
                    merged.Add(left[lidx++]);
                }
                else
                {
                    merged.Add(right[ridx++]);
                }
            }
            //getting remainders
            while(lidx < left.Count)
            {
                merged.Add(left[lidx]);
            }
            while(ridx < right.Count)
            {
                merged.Add(right[ridx]);
            }
            return merged;
        }
    }
}
