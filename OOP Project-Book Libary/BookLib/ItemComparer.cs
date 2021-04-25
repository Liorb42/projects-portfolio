using System;
using System.Collections.Generic;

namespace BookLib
{
    public enum ItemEnumCompareBy { Name, Author, PrintingDate, Publisher, DiscountRate, Genre, Frequency}

    public class ItemComparer
    {      
        public class CompareByName : IComparer<AbstractItem>
        {
            public int Compare(AbstractItem x, AbstractItem y)
            {
                return x.Name.CompareTo(y.Name);
            }            
        }
        public class CompareByAuthor : IComparer<AbstractItem>
        {
            public int Compare(AbstractItem x, AbstractItem y)
            {
                Book a = x as Book;
                Book b = y as Book;
                if (a != null && b != null)
                {
                    return a.Author.CompareTo(b.Author);
                }
                else throw new ArgumentException("Comparing Authour proprety can only be done between Books");
            }            
        }
        public class CompareByFrequency : IComparer<AbstractItem>
        {
            public int Compare(AbstractItem x, AbstractItem y)
            {
                Journal a = x as Journal;
                Journal b = y as Journal;
                if (a != null && b != null)
                {
                    return a.Frequency.CompareTo(b.Frequency);
                }
                else throw new ArgumentException("Comparing Frequency proprety can only be done between Journals");
            }
        }
        public class CompareByPublisher : IComparer<AbstractItem>
        {
            public int Compare(AbstractItem x, AbstractItem y)
            {
                return x.Publisher.CompareTo(y.Publisher);
            }
        }
        public class CompareByDiscount : IComparer<AbstractItem>
        {
            public int Compare(AbstractItem x, AbstractItem y)
            {
                return x.Discount.CompareTo(y.Discount);
            }
        }
        public class CompareByGenre : IComparer<AbstractItem>
        {
            public int Compare(AbstractItem x, AbstractItem y)
            {
                for (int i = 0; i < x.Genres.Count; i++)
                {
                    for (int j = 0; j < y.Genres.Count; j++)
                    {
                        if (x.Genres[i] == y.Genres[j]) return 0;
                    }
                }
                return -1;
            }
        }
        public class CompareByPrintingDate : IComparer<AbstractItem>
        {
            public int Compare(AbstractItem x, AbstractItem y)
            {
                if (x.PrintingDate.Month == y.PrintingDate.Month && x.PrintingDate.Year == y.PrintingDate.Year)
                    return 0;
                else return -1;
            }
        }
    }
}
