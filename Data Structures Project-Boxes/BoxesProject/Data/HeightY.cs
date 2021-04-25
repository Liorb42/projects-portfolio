using BoxesProject.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxesProject.Data
{
    class HeightY : IComparable<HeightY> 
    {
        double _y;
        int _amount;
        public HeightY(double y)
        {
            Y = y;
        }
        public HeightY(double y, int amount) : this(y)
        {
            Amount = amount;
        }
        public double Y
        {
            get { return _y; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Value must be above 0");
                else
                    _y = value;
            }
        }
        public int Amount
        {
            get { return _amount; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Value must be above 0");
                else
                    _amount = value;
            }
        }
        public DoubleLinkedList<TimeData>.Node TimeCollectionRef { get; set; } 
        public int CompareTo(HeightY other)
        {
            return Y.CompareTo(other.Y);
        }

    }
}
