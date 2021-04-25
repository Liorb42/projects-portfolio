using System;
using System.Collections.Generic;
using System.Text;

namespace BoxesProject.DataStructures
{
    class DoubleLinkedList<T> 
    {
        private Node start;
        private Node end;
        private int cnt = 0;

        public Node End { get => end; }

        public void AddFirst(T val)
        {
            Node n = new Node(val);
            n.next = start;           
            start = n;
            if (end == null) end = n;
            if (n.next != null) n.next.previous = n;
            cnt++;
        }
        public bool RemoveFirst(out T saveFirstValue)
        {
            if (start != null)
            {
                if (start.next == null) end = null;
                saveFirstValue = start.data;
                start = start.next;
                start.previous = null;
                cnt--;
                return true;
            }
            else
            {
                saveFirstValue = default(T);
                return false;
            }
        }
        public void AddLast(T val)
        {
            if (start == null) AddFirst(val);
            else
            {
                Node n = new Node(val);
                n.previous = end;
                end.next = n;
                end = n;
                cnt++;
            }
        }
        public bool RemoveLast(out T saveLastValue)
        {
            if (end != null)
            {
                if (end.previous == null) start = null;
                saveLastValue = end.data;
                end = end.previous;
                end.next = null;
                cnt--;
                return true;
            }
            else
            {
                saveLastValue = default(T);
                return false;
            }
        }
        public bool MoveNodeToLast (Node node)
        {         
            if (node != null)
            {
                //emplty list 
                if (start == null)
                    return false;

                //node is the only node
                if (start == end && start != null && node == start)
                    return true;

                //node is last but not the only one
                if (node == end)
                {
                    return true;
                }
                //node is first but not the only one
                if (node == start)
                {
                    node.next.previous = null;
                    start = node.next;
                    node.previous = end;
                    node.next = null;
                    end = node;
                    return true;
                }
                //node is in the middle
                if (node.next != null)
                {
                    node.previous.next = node.next;
                    node.next.previous = node.previous;
                    node.previous = end;
                    node.next = null;
                    end = node;
                    return true;
                }               
            }            
            return false;

        }
        public bool GetAt(int position, out T value)
        {
            if (position >= 0 && position < cnt)
            {
                Node temp = start;
                for (int i = 0; i < position; i++)
                {
                    temp = temp.next;
                }
                value = temp.data;
                return true;
            }
            else
            {
                value = default(T);
                return false;
            }
        }
        public bool AddAt(int position, T value)
        {
            if (position == 0)
            {
                AddFirst(value);
                return true;
            }
            if (position == cnt)
            {
                AddLast(value);
                return true;
            }
            if (position > 0 && position < cnt)
            {
                Node n = new Node(value);
                Node temp = start;
                for (int i = 0; i < position; i++)
                {
                    temp = temp.next;
                }
                temp.previous.next = n;
                temp.previous = n;
                n.next = temp;
                cnt++;
                return true;
            }
            else
                return false;
        }
        public class Node
        {
            public T data;
            public Node next;
            public Node previous;

            public Node(T data)
            {
                this.data = data;
                next = null;
                previous = null;
            }
        }
    }
}
