using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BookLib
{
    public abstract class AbstractItem : INotifyPropertyChanged
    {        
        int _idNumber;
        float _price;
        float _discount;
        int _inventoryAmount;
        List<Genre> _genres;
        static int _counter;
        string _printRepresantation;        

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Code { get; set; }
        public DateTime PrintingDate { get; set; }
        public float Price { get { return _price; } 
            set 
            {
                if (value >= 0)
                    _price = value;
                else _price = 0;
                PrintRepresantation = this.ToString(); //raises the INotifyPropertyChangeEvent
            }
        }
        public int InventoryAmount
        {
            get { return _inventoryAmount; }
            set
            {
                if (value >= 0)
                    _inventoryAmount = value;
                else _inventoryAmount = 0;
                PrintRepresantation = this.ToString(); //raises the INotifyPropertyChangeEvent
            }
        }
        public float Discount { get { return _discount; } 
            set 
            {
                if (value >= 0)
                    _discount = value;
                else _discount = 0;
                PrintRepresantation = this.ToString(); //raises the INotifyPropertyChangeEvent
            }
        }
        public List<Genre> Genres 
        { 
            get 
            { 
                return _genres;
            } 
            set
            { 
                _genres = value;
                PrintRepresantation = this.ToString(); //raises the INotifyPropertyChangeEvent
            }
        }
        public string PrintRepresantation
        {
            get
            {
                return _printRepresantation;
            }
            set
            {
                _printRepresantation = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PrintRepresantation"));
                }
            }
        }        
        public AbstractItem(string name, string publisher, string code, DateTime printingDate, float price, List<Genre> genres)
        {
            Name = name;
            Publisher = publisher;
            Code = code;
            PrintingDate = printingDate;
            _price = price;
            _discount = 0;
            _inventoryAmount = 0; 
            _genres = new List<Genre>();
            if (genres != null) 
            {
                for (int i = 0; i < genres.Count; i++)
                {
                    AddGenre(genres[i]);
                }
            }            
            _idNumber = ++_counter;
            PrintRepresantation = this.ToString();
        }
        internal void AddGenre(Genre genre)
        {
            for (int i = 0; i < _genres.Count; i++)
            {
                //check if the genre was already added
                if (_genres[i] == genre) return;     
            }
            _genres.Add(genre);
        }
        public enum Genre { Mystery, Thriller, Crime, Children, Fantasy, SciFi, Romance, Biography, Cookbook, History, Comicbook, SelfHelp, Misc }
        public override string ToString()
        {
            return $"#{_idNumber}  {Name}";
        }
        public override bool Equals(object obj)
        {
            AbstractItem temp = obj as AbstractItem;
            return Code.Equals(temp.Code);
        }

    }
}
