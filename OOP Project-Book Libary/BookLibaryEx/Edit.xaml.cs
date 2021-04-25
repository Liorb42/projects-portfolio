using BookLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BookLibaryEx
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Edit : Page
    {
        AbstractItem _itemToEdit;
        List<CheckBox> _allGenres;
        List<RadioButton> _allFrequencies;
        #region Item to edit params
        string _name;
        string _publisher;
        DateTime _printingDate;
        float _price;
        float _discount;
        string _author;
        string _code;
        int _edition;
        string _summary;
        List<AbstractItem.Genre> _genres;
        int _inventory;
        int _issue;
        Journal.PublicationFrequency _frequency; 
        #endregion

        public Edit()
        {
            this.InitializeComponent();
        }
        private void DisplayItemDetails()
        {
            Name_txt.Text = _itemToEdit.Name;
            _name = _itemToEdit.Name;
            Publisher_txt.Text = _itemToEdit.Publisher;
            _publisher = _itemToEdit.Publisher;
            PrintingDate_picker.Date = _itemToEdit.PrintingDate;
            _printingDate = _itemToEdit.PrintingDate;
            Price_txt.Text = _itemToEdit.Price.ToString();
            _price = _itemToEdit.Price;
            Discount_txt.Text = (_itemToEdit.Discount * 100).ToString();
            _discount = _itemToEdit.Discount;
            ISBN_ISSN_txt.Text = _itemToEdit.Code;
            _code = _itemToEdit.Code;
            _inventory = _itemToEdit.InventoryAmount;
            Inventory_txt.Text = _itemToEdit.InventoryAmount.ToString();
            CheckGenreBoxes(_itemToEdit);

            Book bookToEdit = _itemToEdit as Book;
            if (bookToEdit != null)
            {
                Author_txt.Text = bookToEdit.Author;
                _author = bookToEdit.Author;
                Edition_txt.Text = bookToEdit.Edition.ToString();
                _edition = bookToEdit.Edition;
                Summary_txt.Text = bookToEdit.Summary;
                _summary = bookToEdit.Summary;
                foreach (RadioButton frequency in _allFrequencies)
                {
                    frequency.IsEnabled = false;
                }
                Issue_txt.IsEnabled = false;
            }
            Journal journalToEdit = _itemToEdit as Journal;
            if (journalToEdit != null)
            {
                Issue_txt.Text = journalToEdit.Issue.ToString();
                _issue = journalToEdit.Issue;
                CheckFrequencyBox(journalToEdit);
                Author_txt.IsEnabled = false;
                Summary_txt.IsEnabled = false;
                Edition_txt.IsEnabled = false;
            }
        }
        private void GenerateGenreCheckBoxes()
        {
            string[] genresName = Enum.GetNames(typeof(AbstractItem.Genre));
            _allGenres = new List<CheckBox>();
            double left = 0;
            double top = 0;
            for (int i = 0; i < genresName.Length; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = genresName[i];
                checkBox.FontSize = 10;
                checkBox.FontFamily = new FontFamily("Eras ITC");
                Canvas.SetLeft(checkBox, left);
                Canvas.SetTop(checkBox, top);
                left += 110;
                if (left > 300)
                {
                    top += 35;
                    left = 0;
                }
                _allGenres.Add(checkBox);
                Genre_canvas.Children.Add(checkBox);
            }
        }
        private void GenerateFrequencyRadionButtons()
        {
            string[] frequencyName = Enum.GetNames(typeof(Journal.PublicationFrequency));
            _allFrequencies = new List<RadioButton>();
            double left = 0;
            double top = 0;
            for (int i = 0; i < frequencyName.Length; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Content = frequencyName[i];
                radioButton.FontSize = 10;
                radioButton.FontFamily = new FontFamily("Eras ITC");
                radioButton.GroupName = "Frequency";
                Canvas.SetLeft(radioButton, left);
                Canvas.SetTop(radioButton, top);
                left += 110;
                if (left > 300)
                {
                    top += 35;
                    left = 0;
                }
                _allFrequencies.Add(radioButton);
                Frequency_canvas.Children.Add(radioButton);
            }
        }
        private void CheckGenreBoxes(AbstractItem item)
        {
            for (int i = 0; i < item.Genres.Count; i++)
            {
                string itemGenreName = Enum.GetName(typeof(AbstractItem.Genre), item.Genres[i]);

                for (int j = 0; j < _allGenres.Count; j++)
                {
                    if (itemGenreName == _allGenres[j].Content.ToString())
                    {
                        _allGenres[j].IsChecked = true;
                    }
                }
            }
        }
        private void CheckFrequencyBox(Journal journal)
        {
            string journalFrequency = Enum.GetName(typeof(Journal.PublicationFrequency), journal.Frequency);

            for (int i = 0; i < _allFrequencies.Count; i++)
            {
                if (journalFrequency == _allFrequencies[i].Content.ToString())
                    _allFrequencies[i].IsChecked = true;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AbstractItem temp = e.Parameter as AbstractItem;
            if (temp != null)
            {
                _itemToEdit = temp;
            }
            GenerateGenreCheckBoxes();
            GenerateFrequencyRadionButtons();
            DisplayItemDetails();
        }
        private void Name_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                _name = temp.Text;
            }
        }
        private void Author_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                _author = temp.Text;
            }
        }
        private void Publisher_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                _publisher = temp.Text;
            }
        }
        private void ISBN_ISSN_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                _code = temp.Text;
            }
        }
        private void Edition_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                bool isValid = int.TryParse(temp.Text, out _edition);
                if (!isValid || _edition < 0)
                {
                    temp.Text = "";
                }
            }
        }
        private void Price_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                bool isValid = float.TryParse(temp.Text, out _price);
                if (!isValid || _price <= 0)
                {
                    temp.Text = "";                    
                }
            }
        }
        private void Discount_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                bool isValid = float.TryParse(temp.Text, out _discount);
                if (!isValid || _discount > 100 || _discount < 0)
                {
                    temp.Text = "";                   
                }
                else _discount /= 100;
            }
        }
        private void Summary_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                _summary = temp.Text;
            }
        }
        private void PrintingDate_picker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            _printingDate = sender.Date.DateTime;
        }
        private void Inventory_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                bool isValid = int.TryParse(temp.Text, out _inventory);
                if (!isValid || _inventory < 0)
                {
                    temp.Text = "";
                }
            }
        }
        private void Issue_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                bool isValid = int.TryParse(temp.Text, out _issue);
                if (!isValid || _issue < 0)
                {
                    temp.Text = "";
                }
            }
        }
        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            bool isValidItem = false, isValidBook = false, isValidJournal = false;
            StringBuilder errorMsg = new StringBuilder();
            Error_txt.Text = "";

            //check if the ISBN/ISSN code has changed
            if ((_itemToEdit.Code != _code))
            {
                //check that the new code isn't already on the list
                if (MainPage.Manger.isCodeOnList(_code))
                {
                    Error_txt.Text = "ISBN/ISSN Code is already on the list";
                    return;
                }
                else _itemToEdit.Code = _code;
            }
            //check validity for all the fields and print error message if necessary
            isValidItem = CheckInputValidity_AbstractItemParams(out string errorMsgItem);
            errorMsg.Append(errorMsgItem);
            if (_itemToEdit.GetType() == typeof(Book))
            {
                isValidBook = CheckInputValidity_BookParams(out string errorMsgBook);
                errorMsg.Append(errorMsgBook);
            }
            if (_itemToEdit.GetType() == typeof(Journal))
            {
                isValidJournal = CheckInputValidity_JournalParams(out string errorMsgJournal);
                errorMsg.Append(errorMsgJournal);
            }
            Error_txt.Text = errorMsg.ToString();

            //edit book
            if (_itemToEdit.GetType() == typeof(Book) && isValidItem && isValidBook)
            {
                _itemToEdit.Name = _name;
                _itemToEdit.Publisher = _publisher;
                _itemToEdit.PrintingDate = _printingDate;
                _itemToEdit.Price = _price;
                _itemToEdit.Discount = _discount;
                _itemToEdit.Genres = _genres;
                _itemToEdit.InventoryAmount = _inventory;
                Book bookToEdit = _itemToEdit as Book;
                if (bookToEdit != null)
                {
                    bookToEdit.Author = _author;
                    bookToEdit.Edition = _edition;
                    bookToEdit.Summary = _summary;
                }
                Error_txt.Text = "Changes Saved";
            }
            //edit journal
            if (_itemToEdit.GetType() == typeof(Journal) && isValidItem && isValidJournal)
            {
                _itemToEdit.Name = _name;
                _itemToEdit.Publisher = _publisher;
                _itemToEdit.PrintingDate = _printingDate;
                _itemToEdit.Price = _price;
                _itemToEdit.Discount = _discount;
                _itemToEdit.Genres = _genres;
                _itemToEdit.InventoryAmount = _inventory;
                Journal journalToEdit = _itemToEdit as Journal;
                if (journalToEdit != null)
                {
                    journalToEdit.Issue = _issue;
                    journalToEdit.Frequency = _frequency;
                }
                Error_txt.Text = "Changes Saved";
            }
        }        
        private bool CheckInputValidity_AbstractItemParams(out string errorMsg)
        {
            StringBuilder sb = new StringBuilder();
            bool isValid = true;
            if (_name == null || _name.Length == 0)
            {
                sb.Append("Please enter a name |");
                isValid = false;
            }
            if (_publisher == null || _publisher.Length == 0)
            {
                sb.Append("Please enter a publisher |");
                isValid = false;
            }
            if (_code == null || _code.Length == 0)
            {
                sb.Append("Please enter an ISBN/ISSN |");
                isValid = false;
            }
            if (_price <= 0)
            {
                sb.Append("Please enter a price |");
                isValid = false;
            }
            if (_discount < 0 || _discount > 100)
            {
                sb.Append("Please enter discount rate |");
                isValid = false;
            }
            if (_inventory < 0)
            {
                sb.Append("Please enter inventory amount |");
                isValid = false;
            }
            if (_printingDate == default)
            {
                sb.Append("Please chose a printing date |");
                isValid = false;
            }
            if (!GetSelectedGenres())
            {
                sb.Append("Please choose genres |");
                isValid = false;
            }
            errorMsg = sb.ToString();
            return isValid;
        }
        private bool CheckInputValidity_BookParams(out string errorMsg)
        {
            StringBuilder sb = new StringBuilder();
            bool isValid = true;
            if (_author == null || _author.Length == 0)
            {
                sb.Append("Please enter an author |");
                isValid = false;
            }
            if (_edition <= 0)
            {
                sb.Append("Please enter edition number |");
                isValid = false;
            }
            if (_summary == null)
            {
                sb.Append("Please enter a summary |");
                isValid = false;
            }
            errorMsg = sb.ToString();
            return isValid;
        }
        private bool CheckInputValidity_JournalParams(out string errorMsg)
        {
            StringBuilder sb = new StringBuilder();
            bool isValid = true;
            if (!GetSelectedFrequency())
            {
                sb.Append("Please choose a frequency |");
                isValid = false;
            }
            if (_issue <= 0)
            {
                sb.Append("Please enter issue number |");
                isValid = false;
            }
            errorMsg = sb.ToString();
            return isValid;
        }
        private bool GetSelectedGenres()
        {
            bool isGenreChecked = false;
            _genres = new List<AbstractItem.Genre>();
            foreach (CheckBox checkbox in _allGenres)
            {
                if (checkbox.IsChecked == true)
                {
                    _genres.Add(Enum.Parse<AbstractItem.Genre>(checkbox.Content.ToString()));
                    isGenreChecked = true;
                }
            }
            return isGenreChecked;
        }
        private bool GetSelectedFrequency()
        {
            bool isFrequencyChecked = false;
            foreach (RadioButton radioButton in _allFrequencies)
            {
                if (radioButton.IsChecked == true)
                {
                    _frequency = (Enum.Parse<Journal.PublicationFrequency>(radioButton.Content.ToString()));
                    isFrequencyChecked = true;
                }
            }
            return isFrequencyChecked;
        }
        private void BackToStart_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StartMenu));
        }
        private void BackToSearch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search));
        }
    }
}
       