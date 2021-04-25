using System;
using System.Collections.Generic;
using System.Text;

namespace BoxesProject.DataStructures
{
    public class BoxDetails
    {
        public BoxDetails(double displayBoxWidth, double displayBoxHeight, int displayBoxAmount, DateTime lastAccessDate)
        {
            DisplayBoxWidth = displayBoxWidth;
            DisplayBoxHeight = displayBoxHeight;
            DisplayBoxAmount = displayBoxAmount;
            LastAccessDate = lastAccessDate;
        }
       public double DisplayBoxWidth { get; set; }
       public double DisplayBoxHeight { get; set; }
       public int DisplayBoxAmount { get; set; }
       public DateTime LastAccessDate { get; set; }
    }
}
