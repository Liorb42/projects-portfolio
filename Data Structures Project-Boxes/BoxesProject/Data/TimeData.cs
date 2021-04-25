using System;
using System.Collections.Generic;
using System.Text;

namespace BoxesProject.Data
{
    class TimeData 
    {
        double _x;
        double _y;
        public TimeData(double x, double y)
        {
            X = x;
            Y = y;
            LastAccessedDate = DateTime.Now;
        }
        public double X
        {
            get { return _x; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Value must be above 0");
                else
                    _x = value;
            }
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
        public DateTime LastAccessedDate { get; set; }
    }
}
