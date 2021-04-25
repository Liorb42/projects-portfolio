using BoxesProject.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxesProject.Data
{
    class WidthX : IComparable<WidthX> 
    {
        double _x;
        public WidthX(double x)
        {
            X = x;
            YTree = new BST<HeightY>();
        }
        public WidthX(double x, HeightY HeightY) : this(x)
        {
            YTree.Add(HeightY);
        }
        public double X
        {
            get { return _x; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Value must be above 0");
                _x = value;
            }
        }
        public BST<HeightY> YTree { get; set; }
        public int CompareTo(WidthX other)
        {
            return X.CompareTo(other.X);
        }
    }
}
