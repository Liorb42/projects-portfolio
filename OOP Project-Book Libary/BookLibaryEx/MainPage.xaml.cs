using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using BookLib;

namespace BookLibaryEx
{
    public sealed partial class MainPage : Page
    {
        static Manager _manager;      
        public static Manager Manger { get { return _manager; } }

        public MainPage()
        {
            this.InitializeComponent();
            _manager = new Manager();
        }
        private void AddTestInventory()
        {
            List<AbstractItem.Genre> _testGenreList = new List<AbstractItem.Genre>();
            List<AbstractItem.Genre> _testGenreList2 = new List<AbstractItem.Genre>();
            _testGenreList.Add(AbstractItem.Genre.Biography);
            _testGenreList.Add(AbstractItem.Genre.History);
            _testGenreList2.Add(AbstractItem.Genre.Comicbook);            
            try
            {
                _manager.AddBook(name: "name1", publisher: "publisher1", printingDate: DateTime.Parse(@"07/2016"), price: 55, discount: 0.3F, inventoryAmount: 20, author: "testAuthour1", 
                    isbn: "1234", edition: 2, summary: "jsdhfkjsdhkds", genres: _testGenreList);
                _testGenreList.Add(AbstractItem.Genre.Children);
                _manager.AddBook(name: "name2", publisher: "publisher2", printingDate: DateTime.Parse(@"02/2019"), price: 66, discount: 0.4F, inventoryAmount: 10, author: "testAuthour2", 
                    isbn: "12343", edition: 2, summary: "jsdhfkjsdhkds", genres: _testGenreList);
                _manager.AddBook(name: "name3", publisher: "publisher1", printingDate: DateTime.Parse(@"02/2014"), price: 55, discount: 0, inventoryAmount: 2, author: "testAuthour3", 
                    isbn: "12345", edition: 2, summary: "jsdhfkjsdhkds", genres: _testGenreList);
                _manager.AddBook(name: "name4", publisher: "publisher1", printingDate: DateTime.Today, price: 55, discount: 0.15F, inventoryAmount: 13, author: "testAuthour4",
                    isbn: "55555", edition: 2, summary: "jsdhfkjsdhkds", genres: _testGenreList);
                _manager.AddBook(name: "name5", publisher: "publisher3", printingDate: DateTime.Today, price: 55, discount: 0, inventoryAmount: 0, author: "testAuthour2",
                    isbn: "798ew79", edition: 2, summary: "jsdhfkjsdhkds", genres: _testGenreList);
                _manager.AddBook(name: "name6", publisher: "publisher3", printingDate: DateTime.Today, price: 55, discount: 0, inventoryAmount: 77, author: "testAuthour5",
                    isbn: "5rr5555", edition: 2, summary: "jsdhfkjsdhkds", genres: _testGenreList);
                _manager.AddBook(name: "name7", publisher: "publisher2", printingDate: DateTime.Parse(@"08/1999"), price: 55, discount: 0.15F, inventoryAmount: 0, author: "testAuthour1",
                    isbn: "55hftrr555", edition: 2, summary: "jsdhfkjsdhkds", genres: _testGenreList);
                _manager.AddJournal(name: "journal1", publisher: "publisher4", printingDate: DateTime.Today, price: 23.3F, discount: 0.15F, inventoryAmount: 15, 
                    issue: 81, issn: "6628229", genres: _testGenreList2, frequency: Journal.PublicationFrequency.Monthly);
                _testGenreList2.Add(AbstractItem.Genre.Children);
                _manager.AddJournal(name: "journal2", publisher: "publisher5", printingDate: DateTime.Today, price: 23.3F, discount: 0, inventoryAmount: 15, 
                    issue: 31, issn: "383983", genres: _testGenreList2, frequency: Journal.PublicationFrequency.Weekly);
            }
            catch (ArgumentException e) { _manager.WriteToLog($"[{e.GetType()}]\t[{e.Message}]"); }     
        }
        private void Start_btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StartMenu));
        }  
        private void Stock_btn_Click(object sender, RoutedEventArgs e)
        {            
            AddTestInventory();            
            Stock_btn.IsEnabled = false;
            Start_btn.IsEnabled = true;        
        }
        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
