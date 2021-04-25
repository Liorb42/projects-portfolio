using BookLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BookLibaryEx
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Search : Page
    {
        ObservableCollection<AbstractItem> ItemsToShow { get; set; }
        AbstractItem _itemToEdit;
        float _discountAll;
        string _codeSearch;
        #region Compare item params
        string _compareName;
        string _comparePublisher;
        DateTime _comparePrintingDate;
        string _compareAuthor;
        float _compareDiscount;
        List<AbstractItem.Genre> _compareGenres;
        Journal.PublicationFrequency _compareFrequency;
        List<ItemEnumCompareBy> _comparersEnumList; 
        #endregion

        public Search()
        {
            this.InitializeComponent();
            ItemsToShow = MainPage.Manger.GetCollection();
            ItemList_listView.DataContext = ItemsToShow;
            InitCompareParams();
            CreateCompareGenreComboBox();
            CreateCompareFrequencyComboBox();
        }
        private void CreateCompareGenreComboBox()
        {
            string[] genresName = Enum.GetNames(typeof(AbstractItem.Genre));
            for (int i = 0; i < genresName.Length; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = genresName[i];
                GenreSearch_comboBox.Items.Add(textBlock);
            }
        }
        private void CreateCompareFrequencyComboBox()
        {
            string[] frequencyName = Enum.GetNames(typeof(Journal.PublicationFrequency));
            for (int i = 0; i < frequencyName.Length; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = frequencyName[i];
                FrequencySearch_comboBox.Items.Add(textBlock);
            }
        }
        private void InitCompareParams()
        {
            _compareName = "";
            _comparePublisher = "";
            _comparePrintingDate = default;
            _compareAuthor = "";
            _compareDiscount = -1;
            _compareGenres = new List<AbstractItem.Genre>();
            _comparersEnumList = new List<ItemEnumCompareBy>();
            _compareFrequency = default;
        }        
        private void ItemList_listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView view = sender as ListView;
            if (view != null)
            {
                object selectedItem = view.SelectedItem;
                AbstractItem item = selectedItem as AbstractItem;
                if (item != null)
                {
                    Edit_btn.IsEnabled = true;
                    Remove_btn.IsEnabled = true;
                    Buy_btn.IsEnabled = true;
                    _itemToEdit = item;
                }
            }
        }       
        private void SetAllDiscount_Click(object sender, RoutedEventArgs e)
        {
            foreach (AbstractItem item in ItemsToShow)
            {
                MainPage.Manger.SetDiscount(item.Code, _discountAll);                
                ItemsToShow = MainPage.Manger.GetCollection();
                ItemList_listView.DataContext = ItemsToShow;
            }
        }
        private void DiscountValue_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                bool isValid = float.TryParse(temp.Text, out _discountAll);                
                if (!isValid || _discountAll > 100 || _discountAll < 0)
                {
                    temp.Text = "";
                }
                else  _discountAll /= 100;
            }
        }        
        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateCompareEnumList();
            if (SearchBook_RadioButton.IsChecked == true)
            {
                Book compareToBook = MainPage.Manger.CreateCompareToBook(_compareName, _comparePublisher, _comparePrintingDate, _compareAuthor, _compareDiscount, _compareGenres);
                ItemsToShow = new ObservableCollection<AbstractItem>();
                ItemsToShow = MainPage.Manger.GetBookSearchSublist(compareToBook, _comparersEnumList);
                ItemList_listView.DataContext = ItemsToShow;                
            }
            else if (SearchJournal_RadioButton.IsChecked == true)
            {
                Journal compareToJournal = MainPage.Manger.CreateCompareToJournal(_compareName, _comparePublisher, _comparePrintingDate, _compareDiscount, _compareGenres, _compareFrequency);
                ItemsToShow = new ObservableCollection<AbstractItem>();
                ItemsToShow = MainPage.Manger.GetJournalSearchSublist(compareToJournal, _comparersEnumList);
                ItemList_listView.DataContext = ItemsToShow;
            }
            else // serching for both Books and Journals
            {
                Book compareToBook = MainPage.Manger.CreateCompareToBook(_compareName, _comparePublisher, _comparePrintingDate, _compareAuthor, _compareDiscount, _compareGenres);
                Journal compareToJournal = MainPage.Manger.CreateCompareToJournal(_compareName, _comparePublisher, _comparePrintingDate, _compareDiscount, _compareGenres, _compareFrequency);
                
                //create temp list to hold the journals matching the search
                ObservableCollection<AbstractItem> tempJournalCollection = MainPage.Manger.GetJournalSearchSublist(compareToJournal, _comparersEnumList);

                ItemsToShow = new ObservableCollection<AbstractItem>();
                ItemsToShow = MainPage.Manger.GetBookSearchSublist(compareToBook, _comparersEnumList);

                //add the journals found to the main list
                foreach (Journal journal in tempJournalCollection)
                {
                    ItemsToShow.Add(journal);
                }
                ItemList_listView.DataContext = ItemsToShow;
            }
            ResetComboBoxes();
            ResetTextBoxes();
        }       
        private void CreateCompareEnumList()
        {
            _comparersEnumList.Clear();
            if (NameSearch_checkBox.IsChecked == true)
                _comparersEnumList.Add(ItemEnumCompareBy.Name);
            if (AuthorSearch_checkBox.IsChecked == true)
                _comparersEnumList.Add(ItemEnumCompareBy.Author);
            if (DiscountSearch_checkBox.IsChecked == true)
                _comparersEnumList.Add(ItemEnumCompareBy.DiscountRate);
            if (GenreSearch_checkBox.IsChecked == true)
                _comparersEnumList.Add(ItemEnumCompareBy.Genre);
            if (PublisherSearch_checkBox.IsChecked == true)
                _comparersEnumList.Add(ItemEnumCompareBy.Publisher);
            if (PrintingDateSearch_checkBox.IsChecked == true)
                _comparersEnumList.Add(ItemEnumCompareBy.PrintingDate);
            if (FrequencySearch_checkBox.IsChecked == true)
                _comparersEnumList.Add(ItemEnumCompareBy.Frequency);            
        }
        private void NameSearch_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                _compareName = temp.Text;
            }
        }
        private void AuthorSearch_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                _compareAuthor = temp.Text;
            }
        }
        private void PublisherSearch_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                _comparePublisher = temp.Text;
            }
        }
        private void DiscountSearch_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                bool isValid = float.TryParse(temp.Text, out _compareDiscount);                
                if (!isValid || _compareDiscount < 0 || _compareDiscount > 100)
                {
                    temp.Text = "";
                }
                else _compareDiscount /= 100;
            }
        }
        private void PrintingDateSearch_picker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            _comparePrintingDate = sender.Date.DateTime;
        }
        private void GenreSearch_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            TextBlock genretxtBlock = GenreSearch_comboBox.SelectedItem as TextBlock;
            if (genretxtBlock != null)
            {
                Enum.TryParse(genretxtBlock.Text, out AbstractItem.Genre genre);

                _compareGenres.Add(genre);
            }
        }
        private void ResetSearch_btn_Click(object sender, RoutedEventArgs e)
        {            
            InitCompareParams();
            ResetComboBoxes();
            ResetTextBoxes();
            ItemsToShow = MainPage.Manger.GetCollection();
            ItemList_listView.DataContext = ItemsToShow;
        }
        private void FrequencySearch_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBlock frequencyTxtBlock = FrequencySearch_comboBox.SelectedItem as TextBlock;
            if (frequencyTxtBlock != null)
            {
                Enum.TryParse(frequencyTxtBlock.Text, out Journal.PublicationFrequency _compareFrequency);
            }
        }
        private void SearchBook_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AuthorSearch_checkBox.IsEnabled = true;
            AuthorSearch_txt.IsEnabled = true;
            FrequencySearch_comboBox.IsEnabled = false;
            FrequencySearch_checkBox.IsEnabled = false;
        }
        private void SearchJournal_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AuthorSearch_checkBox.IsEnabled = false;
            AuthorSearch_txt.IsEnabled = false;
            FrequencySearch_comboBox.IsEnabled = true;
            FrequencySearch_checkBox.IsEnabled = true;
        }
        private void SearchBoth_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AuthorSearch_checkBox.IsEnabled = false;
            AuthorSearch_txt.IsEnabled = false;
            FrequencySearch_comboBox.IsEnabled = false;
            FrequencySearch_checkBox.IsEnabled = false;
        }
        private void CodeSearch_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                _codeSearch = temp.Text;
            }
        }
        private void CodeSearch_btn_Click(object sender, RoutedEventArgs e)
        {
            if (_codeSearch != null)
            {
                AbstractItem itemToGet = MainPage.Manger.GetItem(_codeSearch);
                if (itemToGet != null)
                {
                    Frame.Navigate(typeof(Edit), itemToGet);
                }
                else Error_txt.Text = "Invalid code";
            }
            else Error_txt.Text = "Please enter code to get";
        }
        private void Buy_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainPage.Manger.BuyItem(_itemToEdit.Code, 1);
                Error_txt.Text = "Item inventory amount has been updated";
                ItemList_listView.DataContext = ItemsToShow;
            }
            catch (Exception ex) 
            { 
                Error_txt.Text = ex.Message;
                MainPage.Manger.WriteToLog($"[{ex.GetType()}]\t[{ex.Message}]");
            }            
        }
        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Edit), _itemToEdit);
        }
        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StartMenu));
        }        
        private void Remove_btn_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Manger.RemoveItem(_itemToEdit.Code);
        }
        private void AddBook_btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddBook));
        }
        private void ResetComboBoxes()
        {            
            _compareGenres = new List<AbstractItem.Genre>();
            GenreSearch_comboBox.SelectedIndex = -1;
            _compareFrequency = new Journal.PublicationFrequency();
            FrequencySearch_comboBox.SelectedIndex = -1;
        }
        private void ResetTextBoxes()
        {
            NameSearch_checkBox.IsChecked = false;
            NameSearch_txt.Text = "";            
            AuthorSearch_txt.Text = "";
            DiscountSearch_checkBox.IsChecked = false;
            DiscountSearch_txt.Text = "";            
            PrintingDateSearch_checkBox.IsChecked = false;
            PrintingDateSearch_picker.SelectedDate = null;
            PublisherSearch_checkBox.IsChecked = false;
            PublisherSearch_txt.Text = "";
            GenreSearch_checkBox.IsChecked = false;

            if (SearchBook_RadioButton.IsChecked == true || SearchBoth_RadioButton.IsChecked == true)
            {
                FrequencySearch_checkBox.IsChecked = false;
                FrequencySearch_checkBox.IsEnabled = false;
            }
            if (SearchJournal_RadioButton.IsChecked == true || SearchBoth_RadioButton.IsChecked == true)
            {
                AuthorSearch_checkBox.IsChecked = false;
                AuthorSearch_checkBox.IsEnabled = false;
            }
        }
    }
}
