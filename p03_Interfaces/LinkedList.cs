using System.Collections;
using System.Collections.Generic;

namespace p03_Interfaces
{
    public class Node<T> 
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        
        public Node(T data)
        {
            Data = data;
        }
    }
    
    public class LinkedList<T> : IEnumerable<T>
    {
        private int _count;
        private Node<T> tail;
        private Node<T> head;

        public int Count => _count;

        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);
            if (head == null)
            {
                head = node;
            }
            else
            {
                tail.Next = node;
            }

            tail = node;
            _count++;
        }

        public bool Remove(T data)
        {
            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    //if data in the center or in the end
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            tail = previous;
                    }
                    else //data in the head
                    {
                        head = head.Next;
                        if (head == null)
                            tail = null;
                    }
                    _count--;
                    return true;
                }
                
                previous = current;
                current = current.Next;
            }
            return false;
        }

        public bool Contains(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }

            return false;
        }

        public void Clear()
        {
            Node<T> current = head;
            Node<T> next = current.Next;
            while (next != null)
            {
                current = null;
                current = next;
                next = current.Next;
            }
            current = null;
            head = null;
            tail = null;
            _count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}