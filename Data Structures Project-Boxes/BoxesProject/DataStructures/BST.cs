using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BoxesProject.DataStructures
{
    public class BST<T> where T: IComparable<T>
    {
        private Node root;

        public void Add(T newItem)
        {            
            Node n = new Node(newItem);

            if (root == null) 
            {
                root = n;
                return;
            }

            Node tmp = root;
            Node parent = root;

            while (tmp != null)
            {
                parent = tmp;
                if (newItem.CompareTo(tmp.data) < 0) tmp = tmp.left;
                else tmp = tmp.right;
            }
            
            if (newItem.CompareTo(parent.data) < 0) parent.left = n;
            else parent.right = n;
        }       
        public void ScanInOrder(Action<T> action)
        {            
            ScanInOrder(root, action);
        }
        private void ScanInOrder(Node tmpRoot, Action<T> action)
        {
            if (tmpRoot == null) return;

            ScanInOrder(tmpRoot.left, action);
            action(tmpRoot.data);
            ScanInOrder(tmpRoot.right, action);
        }
        public int GetDepth()
        {
            return GetDepth(root);
        }
        private int GetDepth(Node tmpRoot)
        {
            if (tmpRoot == null) return 0;

            return Math.Max(GetDepth(tmpRoot.left), GetDepth(tmpRoot.right)) + 1;
        }
        public bool Search (T searchData, out T foundData) 
        {
            foundData = default(T);

            if (root == null) 
            {
                return false;
            }

            Node tmp = root;

            while (tmp != null)
            {
                int compareRes = searchData.CompareTo(tmp.data);

                if (compareRes == 0)
                {
                    foundData = tmp.data;
                    return true;
                }
                if (compareRes < 0) tmp = tmp.left;
                else tmp = tmp.right;
            }
            return false;
        }
        public FoundOrAdded FindOrAdd(T searchData, out T foundData)
        {
            foundData = default(T);
            Node n = new Node(searchData);

            if (root == null)
            {
                root = n;
                foundData = n.data;
                return FoundOrAdded.ADDED;
            }

            Node tmp = root;
            Node parent = root;

            while (tmp != null)
            {
                parent = tmp;

                int compareRes = searchData.CompareTo(tmp.data);

                if (compareRes == 0)
                {
                    foundData =  tmp.data;
                    return FoundOrAdded.FOUND;

                }
                if (compareRes < 0) tmp = tmp.left;
                else tmp = tmp.right;
            }  

            // data wasn't found. add to the tree
            if (searchData.CompareTo(parent.data) < 0) parent.left = n;
            else parent.right = n;
            foundData = n.data;
            return FoundOrAdded.ADDED;

        }
        public bool Remove (T searchData, out T removedData)
        {
            removedData = default(T);

            if (root == null)
                return false;
                     
            Node parent = root; 
            Node tmp = root;

            while (tmp != null)
            {                
                int compareRes = searchData.CompareTo(tmp.data);

                if (compareRes == 0)
                {
                    if(tmp.left == null && tmp.right == null)
                    {
                        if (tmp == root)                      
                            root = null;    
                        else if (parent.data.CompareTo(tmp.data) > 0)
                            parent.left = null;
                        else parent.right = null;
                        removedData = tmp.data;
                        return true;
                    }         
                    else if (tmp.left != null && tmp.right == null)
                    {
                        if(tmp == root)
                            root = tmp.left;                        
                        else if (parent.data.CompareTo(tmp.data) > 0)
                            parent.left = tmp.left;
                        else parent.right = tmp.left;
                        removedData = tmp.data;
                        return true;

                    }
                    else if(tmp.left == null && tmp.right != null)
                    {
                        if (tmp == root)
                            root = tmp.right;                        
                        if (parent.data.CompareTo(tmp.data) > 0)
                            parent.left = tmp.right;
                        else parent.right = tmp.right;
                        removedData = tmp.data;
                        return true;

                    }
                    else if(tmp.left != null && tmp.right != null)
                    {
                        removedData = tmp.data;
                        Node replacment = tmp.right;

                        while (replacment.left != null)
                        {
                            replacment = replacment.left;
                        }
                        Remove(replacment.data, out tmp.data);
                        return true;
                    }                    
                }
                parent = tmp;
                if (compareRes < 0) 
                    tmp = tmp.left;
                else tmp = tmp.right;
            }
            return false;
        }
        public bool FindExactOrClosest(T searchData, T diviationMargin, out T closestData)
        {
            closestData = default(T);
            bool isDataFound = false;

            if (root == null)
            {
                return false;
            }

            Node tmp = root;

            while (tmp != null)
            {
                int compareExact = searchData.CompareTo(tmp.data);

                if (compareExact == 0)
                {
                    closestData = tmp.data;
                    return true;
                }
                
                if (compareExact < 0)
                {
                    if (diviationMargin.CompareTo(tmp.data) > 0)
                    {
                        closestData = tmp.data;
                        isDataFound = true;
                    }
                    tmp = tmp.left;
                }

                else tmp = tmp.right;                
            }
            return isDataFound;
        }
        class Node
        {
            public Node left;
            public Node right;
            public T data;

            public Node(T data)
            {
                this.data = data;
            }
           
        }      
    }    
}
