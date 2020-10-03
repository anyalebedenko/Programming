using System.Collections;
using System;
using System.Collections.Generic;
namespace Collection
{
    public class Node<T>
    {
        public Node(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        
    }
     public class CircularLinkedList<T> : IEnumerable<T>
    {
        Node<T> head;
        Node<T> tail;
        int count;
        public event EventHandler<AddToCollection<T>> Added;
        public event EventHandler<RemFromCollection<T>> Removed;
        public void Add(T data)
        {
           
            if (data == null)
            { 
                throw new System.ArgumentException("Parameter cannot be null", "data");
            }
            else
            {
                Node<T> node = new Node<T>(data);
                if (head == null)
                {
                    head = node;
                    tail = node;
                    tail.Next = head;
                }
                else
                {
                    node.Next = head;
                    tail.Next = node;
                    tail = node;
                }
                count++;
                Added?.Invoke(sender:this, e:new AddToCollection<T>(data, message:$"{data} was added"));
            }
           
        }

        public bool Remove(T data)
        {
            if (data == null)
            {
                throw new System.ArgumentException("Parameter cannot be null", "data");
            }
            else
            {
                Node<T> current = head;
                Node<T> previous = null;
                
                if (IsEmpty) return false;

                do
                {
                    
                    if (current.Data.Equals(data))
                    {
                        if (previous != null)
                        {
                            previous.Next = current.Next;
                            if (current == tail)
                                tail = previous;
                       
                        }
                        else
                        {
                            if (count == 1)
                            {
                                head = tail = null;
                            }
                            else
                            {
                                head = current.Next;
                                tail.Next = current.Next;
                            }
                        }
                        
                        count--;
                        Removed?.Invoke(sender:this, e:new RemFromCollection<T>(data, message:$"{data} was removed"));
                        return true;
                    }

                    previous = current;
                    current = current.Next;
                  
                } while (current != head);

                return false;
            }
        }

        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
 
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
 
        public bool Contains(T data)
        {
            if (data == null)
            { 
                throw new System.ArgumentException("Parameter cannot be null", "data");
            }
            else
            {
                Node<T> current = head;
                if (current == null) return false;
                do
                {
                    if (current.Data.Equals(data))
                        return true;
                    current = current.Next;
                } while (current != head);

                return false;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
 
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            do
            {
                if (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
            while (current != head);
        }
     
    }

     public class AddToCollection<T> : EventArgs
     {
         public T AddItem { get; }
         public string Message { get; }

         public AddToCollection(T addItem, string message)
         {
             Message = message;
             AddItem = addItem;
         }
     }
     public class RemFromCollection<T> : EventArgs
     {
         public T RemItem { get; }
         public string Message { get; }

         public RemFromCollection(T remItem, string message)
         {
             Message = message;
             RemItem = remItem;
         }
     }
}