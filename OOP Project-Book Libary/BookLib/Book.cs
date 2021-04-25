using System;
using System.Collections.Generic;
using System.Text;


namespace BookLib
{
    public class Book : AbstractItem
    {
        int _edition;        
        public string Author { get; set; }
        public string Summary { get; set; }
        public int Edition
        {
            get { return _edition; }
            set
            {
                if (value >= 0)
                    _edition = value;
                else _edition = 0;
                PrintRepresantation = this.ToString(); //raises the INotifyPropertyChangeEvent
            }
        }        
        public Book(string name, string publisher, DateTime printingDate, float price,
            string author, string isbn, int edition, string summary, List<Genre> genres) :
            base(name, publisher, isbn, printingDate, price, genres)
        {
            Author = author;
            _edition = edition;
            Summary = summary;            
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
            return base.ToString() + $"   Author: {Author},   Code: {Code}\nPublisher: {Publisher},   Edition: {Edition}    Printing Date: {PrintingDate:y}   " +
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

