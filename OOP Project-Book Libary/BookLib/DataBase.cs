using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BookLib
{
    class DataBase : IEnumerable
    {
        public ObservableCollection<AbstractItem> Intentory { get; set; }
        public DataBase()
        {
            Intentory = new ObservableCollection<AbstractItem>();
        }        
        internal AbstractItem this[string code]
        {             
            get
            {
                for (int i = 0; i < Intentory.Count; i++)
                {
                    if (Intentory[i].Code.Equals(code))
                        return Intentory[i];                    
                }
                throw new ArgumentException($"item number {code} is not on the list");
            }
        }
        internal void AddItem(AbstractItem item)
        {
            if (item != null)
            {
                //check if the item is already on the list
                for (int i = 0; i < Intentory.Count; i++)
                {
                    if (Intentory[i].Equals(item)) throw new ArgumentException($"item number {item.Code} is already on the list");
                }
                Intentory.Add(item);
            }
            else throw new ArgumentNullException();
        }
        internal void RemoveItem (AbstractItem item)
        {
            if (item != null)
            {
                //check if item is on the list
                for (int i = 0; i < Intentory.Count; i++)
                {
                    if (Intentory[i].Equals(item))
                    {
                        Intentory.Remove(item);
                        return;
                    }
                }                
                throw new ArgumentException($"item number {item.Code} is not on the list");
            }
            else throw new ArgumentNullException();
        }     
        internal ObservableCollection<AbstractItem> BookSearchSublist(Book compareToBook, List<IComparer<AbstractItem>> compList)
        {
            //returns a sublist of books matching the filter serach
            //goes through the inventory list and compare each book with the compareToBook using each comparer on the list

            ObservableCollection<AbstractItem> searchSubArray = new ObservableCollection<AbstractItem>();
             
            for (int i = 0; i < Intentory.Count; i++)
            {
                bool isAddItemToList = false;
               
                if (Intentory[i].GetType() == typeof(Book))
                {
                    for (int j = 0; j < compList.Count; j++)
                    {
                        if (compList[j].Compare((Book)Intentory[i], compareToBook) == 0)
                            isAddItemToList = true;
                        else
                        {
                            isAddItemToList = false;
                            break;
                        }
                    }                        
                    if (isAddItemToList)
                    {
                        //add the first found book to the list
                        if (searchSubArray.Count == 0) searchSubArray.Add(Intentory[i]);

                        //if not first, check if the item was already added to the list
                        else 
                            foreach (Book book in searchSubArray)
                            {
                                if (Intentory[i].Code == book.Code)
                                    isAddItemToList = false;
                            }

                        if (isAddItemToList) searchSubArray.Add(Intentory[i]);
                    }                   
                }
            }
            return searchSubArray;
        }
        internal ObservableCollection<AbstractItem> JournalSearchSublist(Journal compareToJournal, List<IComparer<AbstractItem>> compList)
        {
            //returns a sublist of journals matching the filter serach
            //goes through the inventory list and compare each jouranl with the compareToJournal using each comparer on the list

            ObservableCollection<AbstractItem> searchSubArray = new ObservableCollection<AbstractItem>();

            for (int i = 0; i < Intentory.Count; i++)
            {
                bool isAddItemToList = false;

                if (Intentory[i].GetType() == typeof(Journal))
                {
                    for (int j = 0; j < compList.Count; j++)
                    {
                        if (compList[j].Compare((Journal)Intentory[i], compareToJournal) == 0)
                            isAddItemToList = true;
                        else
                        {
                            isAddItemToList = false;
                            break;
                        }
                    }
                    if (isAddItemToList)
                    {
                        //add the first found journal to the list
                        if (searchSubArray.Count == 0) searchSubArray.Add(Intentory[i]);

                        //if not first, check if the item was already added to the list
                        else
                            foreach (Book book in searchSubArray)
                            {
                                if (Intentory[i].Code == book.Code)
                                    isAddItemToList = false;
                            }
                        if (isAddItemToList) searchSubArray.Add(Intentory[i]);
                    }
                }
            }
            return searchSubArray;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (AbstractItem item in Intentory)
            {
                sb.Append($"{item}\n");
            }
            return sb.ToString();
        }
        public IEnumerator GetEnumerator()
        {
            return new AbstractItemEnumerator(Intentory);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        internal class AbstractItemEnumerator : IEnumerator
        {
            int position = -1;
            ObservableCollection<AbstractItem> _AbstractItemlist;

            public AbstractItemEnumerator(ObservableCollection<AbstractItem> list)
            {
                _AbstractItemlist = list;
            }
            public object Current
            {
                get
                {
                    try
                    {
                        return _AbstractItemlist[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
            object IEnumerator.Current { get { return Current; } }
            public bool MoveNext()
            {
                position++;
                return (position < _AbstractItemlist.Count);
            }
            public void Reset()
            {
                position = -1;
            }
        }
    }
}
