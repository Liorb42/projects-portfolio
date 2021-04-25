using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace BookLib
{
    public class Manager
    {
        DataBase _dataBase;
        string _logFileName;

        public Manager()
        {
            _dataBase = new DataBase();
            InitializeLog();
        }
        public void AddBook (string name, string publisher, DateTime printingDate, float price, float discount, int inventoryAmount,
            string author, string isbn, int edition, string summary, List<AbstractItem.Genre> genres)
        {
            AbstractItem book = new Book(name, publisher, printingDate, price, author, isbn, edition, summary, genres);
            book.Discount = discount;
            book.InventoryAmount = inventoryAmount;
            try
            {
                _dataBase.AddItem(book);
            }
            catch (ArgumentException e) { WriteToLog($"[{e.GetType()}\t[{e.Message}]"); }
        }
        public void AddJournal(string name, string publisher, DateTime printingDate, float price, float discount, int inventoryAmount,
            int issue, string issn, List<AbstractItem.Genre> genres, Journal.PublicationFrequency frequency)
        {
            AbstractItem journal = new Journal(name, publisher, printingDate, price, issue, issn, genres, frequency);
            journal.Discount = discount;
            journal.InventoryAmount = inventoryAmount;           
            try
            {
                _dataBase.AddItem(journal);
            }
            catch (ArgumentException e) { WriteToLog($"[{e.GetType()}\t[{e.Message}]"); }
        }
        public void RemoveItem (string code)
        {
            AbstractItem item = _dataBase[code];           
            try
            {
                _dataBase.RemoveItem(item);
            }
            catch (ArgumentException e) { WriteToLog($"[{e.GetType()}\t[{e.Message}]"); }
        }       
        public AbstractItem GetItem(string code) 
        {
            try
            {
                return _dataBase[code];
            }
            catch (ArgumentException e) 
            {
                WriteToLog($"[{e.GetType()}\t[{e.Message}]");
                return null;
            }                       
        }               
        public void SetDiscount(string code, float discount)
        {
            try
            {
                _dataBase[code].Discount = discount;
            }
            catch (ArgumentException e) { WriteToLog($"[{e.GetType()}\t[{e.Message}]"); }            
        }               
        public void BuyItem (string code, int amount) 
        {            
            if (amount < 0) throw new ArgumentException("amount cannot be less then 0");
            if (_dataBase[code].InventoryAmount < amount)
                throw new ArgumentOutOfRangeException($"Item number {_dataBase[code].Code} has {_dataBase[code].InventoryAmount} copies left");
            _dataBase[code].InventoryAmount -= amount;
        }      
        public Book CreateCompareToBook (string name, string publisher, DateTime printingDate,
            string author,float discount, List<AbstractItem.Genre> genres)
        {
            Book compToBook = new Book(name: name, publisher: publisher, printingDate: printingDate, 
                price: 0, author: author, isbn: "", edition: 0, summary: "", genres: genres);
            compToBook.Discount = discount;
            return compToBook;
        }
        public Journal CreateCompareToJournal(string name, string publisher, DateTime printingDate,
            float discount, List<AbstractItem.Genre> genres, Journal.PublicationFrequency frequency)
        {
            Journal compToJournal = new Journal(name: name, publisher: publisher, printingDate: printingDate,
                price: 0, issue:0, issn: "", genres: genres, frequency);
            compToJournal.Discount = discount;
            return compToJournal;
        }
        public ObservableCollection<AbstractItem> GetBookSearchSublist(Book CompareToBook, List<ItemEnumCompareBy> comparersEnumList) 
        {
            //creates a comparer list using the comparersEnumList and sends if to the database for search
            //convert compare enums into comparers
            List<IComparer<AbstractItem>> comparerList = new List<IComparer<AbstractItem>>();
            for (int i = 0; i < comparersEnumList.Count; i++)
            {
                IComparer<AbstractItem> comparer = CreateComparer(comparersEnumList[i]);
                comparerList.Add(comparer);
            }
            //get the sublist from the database 
            return _dataBase.BookSearchSublist(CompareToBook, comparerList);
        }
        public ObservableCollection<AbstractItem> GetJournalSearchSublist(Journal CompareToJournal, List<ItemEnumCompareBy> comparersEnumList)
        {
            //creates a comparer list using the comparersEnumList and sends if to the database for search
            //convert compare enums into comparers
            List<IComparer<AbstractItem>> comparerList = new List<IComparer<AbstractItem>>();
            for (int i = 0; i < comparersEnumList.Count; i++)
            {
                IComparer<AbstractItem> comparer = CreateComparer(comparersEnumList[i]);
                comparerList.Add(comparer);
            }
            //get the sublist from the database 
            return _dataBase.JournalSearchSublist(CompareToJournal, comparerList);
        }
        public ObservableCollection<AbstractItem> GetCollection ()
        {
            return _dataBase.Intentory;
        }
        private IComparer<AbstractItem> CreateComparer(ItemEnumCompareBy comparer)
        {
            switch (comparer)
            {
                case ItemEnumCompareBy.Name:
                    return new ItemComparer.CompareByName();                    
                case ItemEnumCompareBy.Author:
                    return new ItemComparer.CompareByAuthor();
                case ItemEnumCompareBy.PrintingDate:
                    return new ItemComparer.CompareByPrintingDate();
                case ItemEnumCompareBy.DiscountRate:
                    return new ItemComparer.CompareByDiscount();
                case ItemEnumCompareBy.Genre:
                    return new ItemComparer.CompareByGenre();
                case ItemEnumCompareBy.Publisher:
                    return new ItemComparer.CompareByPublisher();
                case ItemEnumCompareBy.Frequency:
                    return new ItemComparer.CompareByFrequency();
                default:
                    return null;
            }
        }
        public bool isCodeOnList (string code)
        {            
            foreach (AbstractItem item in _dataBase)
            {
                if (item.Code == code) return true;
            }
            return false;
        }        
        public string WriteToLog (string logMessage)
        {
            try
            {                
               File.AppendAllText(_logFileName, $"[{DateTime.Now}]\t[{logMessage}]\n");                   
            }
            catch (Exception e)
            {
                string errorMassase;

                errorMassase = e.Message;
                return errorMassase;
            }
            return "Message written to log";
        }
        private string InitializeLog()
        {
            string errorMassase;

            try
            {
                _logFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "myLog.txt");
            }
            catch (Exception e)
            {
                errorMassase = e.Message;
                return errorMassase;
            }
            return "Log initialized";

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (AbstractItem item in _dataBase)
            {
                sb.Append($"{item}\n");
            }            
            return sb.ToString();            
        }
        

    }
}