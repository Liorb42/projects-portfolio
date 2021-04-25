using System;
using System.Collections.Generic;
using System.Text;

namespace BookLib
{
    public class Journal : AbstractItem
    {
        public enum PublicationFrequency { Annual, Monthly, Quarterly, Weekly }

        int _issue;
        PublicationFrequency _frequency;
        public int Issue
        {
            get { return _issue; }
            set
            {
                if (value >= 0)
                    _issue = value;
                else _issue = 0;
                PrintRepresantation = this.ToString(); //raises the INotifyPropertyChangeEvent
            }
        }
        public PublicationFrequency Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;
                PrintRepresantation = this.ToString(); //raises the INotifyPropertyChangeEvent
            }
        }
        public Journal(string name, string publisher, DateTime printingDate, float price,
            int issue, string issn, List<Genre> genres, PublicationFrequency frequency) :
            base(name, publisher, issn, printingDate, price, genres)
        {
            _issue = issue;
            _frequency = frequency;           
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (Book.Genre genre in base.Genres)
            {
                sb.Append(genre);
                count++;
                if (count == base.Genres.Count) break;
                sb.Append(", ");
            }
            return base.ToString() + $"   Frequency: {Frequency},   Code: {Code}\nPublisher: {Publisher},   Issue: {Issue}    Printing Date: {PrintingDate:y}   " +
                $"\nOriginal Price: {Price:F2},    Actual Price: {Price * (1 - Discount):F2}  ({Discount * 100}% discount)\n" +
                $"Genres: {sb.ToString()}\n" +
                $"Inventory: {InventoryAmount}";
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
