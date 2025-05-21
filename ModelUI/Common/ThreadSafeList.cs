using System;
using System.Collections.Generic;

public class ThreadSafeList<T>
{
    // The underlying list that will hold the data
    private readonly List<T> list = new List<T>();

    // Lock object to synchronize access
    private readonly object lockObject = new object();

    // Add an item to the list
    public void Add(T item)
    {
        lock (lockObject)
        {
            list.Add(item);
        }
    }

    // Remove an item from the list at a specific index
    public bool RemoveAt(int index)
    {
        lock (lockObject)
        {
            if (index >= 0 && index < list.Count)
            {
                list.RemoveAt(index);
                return true;
            }
            return false;
        }
    }

    // Get an item at a specific index
    public T Get(int index)
    {
        lock (lockObject)
        {
            if (index >= 0 && index < list.Count)
            {
                return list[index];
            }
            throw new ArgumentOutOfRangeException("Index is out of range");
        }
    }

    // Get the count of items in the list
    public int Count()
    {
        lock (lockObject)
        {
            return list.Count;
        }
    }

    // Return a copy of the list for safe iteration
    public List<T> GetAll()
    {
        lock (lockObject)
        {
            return new List<T>(list); // Return a copy to avoid exposing the internal list
        }
    }

    // Remove the first N elements from the list
    public bool RemoveFirstN(int n)
    {
        lock (lockObject)
        {
            if (n <= list.Count)
            {
                list.RemoveRange(0, n);
                return true;
            }
            return false;
        }
    }

    // Clear the list
    public void Clear()
    {
        lock (lockObject)
        {
            list.Clear();
        }
    }
}