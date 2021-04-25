using BookLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BookLibaryEx
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddBook : Page
    {
        #region Item to add params
        string _name;
        string _publisher;
        DateTime _printingDate;
        float _price;
        float _discount;
        int _inventoryAmount;
        string _author;
        string _code;
        int _edition;
        string _summary;
        List<AbstractItem.Genre> _genres;
        int _issue;
        Journal.PublicationFrequency _frequency;
        #endregion
        List<CheckBox> _allGenres;
        List<RadioButton> _allFrequencies;       

        public AddBook()
        {            
            this.InitializeComponent();            
            InitInstenceParams();
        }
        private void InitInstenceParams()
        {
            _name = null;
            _publisher = null;
            _printingDate = default;
            _price = -1;
            _discount = -1;
            _inventoryAmount = -1;
            _author = null;
            _code = null;
            _edition = -1;
            _summary = null;
            _genres = null;            
            _issue = -1;
            _frequency = default;
            GenerateGenreCheckBoxes();
            GenerateFrequencyRadionButtons();
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
                radioButton.IsEnabled = false;
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
                if (!isValid || _price < 0)
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
        private void Inventory_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp != null)
            {
                bool isValid = int.TryParse(temp.Text, out _inventoryAmount);
                if (!isValid  || _inventoryAmount < 0)
                {
                    temp.Text = "";
                }
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
        private void AddToInventory_btn_Click(object sender, RoutedEventArgs e)
        {
            bool isValidItem = false, isValidBook = false, isValidJournal = false;
            StringBuilder errorMsg = new StringBuilder();

            Error_txt.Text = "";

            //check validity for all the fields and print error message if necessary
            isValidItem = CheckInputValidity_AbstractItemParams(out string errorMsgItem);
            errorMsg.Append(errorMsgItem);
            if (AddBook_RadioButton.IsChecked == true)
            {
                isValidBook = CheckInputValidity_BookParams(out string errorMsgBook);
                errorMsg.Append(errorMsgBook);
            }
            if (AddJournal_RadioButton.IsChecked == true)
            {
                isValidJournal = CheckInputValidity_JournalParams(out string errorMsgJournal);
                errorMsg.Append(errorMsgJournal);
            }              
            Error_txt.Text = errorMsg.ToString();

            //add book
            if (AddBook_RadioButton.IsChecked == true && isValidItem && isValidBook)
            {
                try
                {
                    MainPage.Manger.AddBook(_name, _publisher, _printingDate, _price, _discount, _inventoryAmount, _author, _code, _edition, _summary, _genres);
                    Error_txt.Text = "Book was added successfully";
                }
                catch (Exception ex)
                {
                    Error_txt.Text = ex.Message;
                    MainPage.Manger.WriteToLog($"[{ex.GetType()}]\t[{ex.Message}]");
                }
            }
            //add journal
            if (AddJournal_RadioButton.IsChecked == true && isValidItem && isValidJournal)
            {
                try
                {
                    MainPage.Manger.AddJournal(_name, _publisher, _printingDate, _price, _discount, _inventoryAmount, _issue, _code, _genres, _frequency);
                    Error_txt.Text = "Journal was added successfully";
                }
                catch (Exception ex)
                {
                    Error_txt.Text = ex.Message;
                    MainPage.Manger.WriteToLog($"[{ex.GetType()}]\t[{ex.Message}]");
                }
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
            if (MainPage.Manger.isCodeOnList(_code))
            {
                sb.Append("ISBN/ISSN code is already on the list | ");
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
            if (_inventoryAmount < 0)
            {
                sb.Append("Please enter inventory amount |");
                isValid = false;
            }
            if (_printingDate == default)
            {
                sb.Append("Please choose a printing date |");
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
                if(checkbox.IsChecked == true)
                {
                    //convert genre name into the enum genre 
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
                    //convert frequency name into the enum frequency
                    _frequency = (Enum.Parse<Journal.PublicationFrequency>(radioButton.Content.ToString()));
                    isFrequencyChecked = true;
                }            
            }
            return isFrequencyChecked;
        }       
        private void AddJournal_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            foreach (RadioButton radioButton in _allFrequencies)
            {
                radioButton.IsEnabled = true;
            }
            Issue_txt.IsEnabled = true;
            Authour_txt.IsEnabled = false;
            Summary_txt.IsEnabled = false;
            Edition_txt.IsEnabled = false;
            foreach (CheckBox checkBox in _allGenres)
            {
                checkBox.IsChecked = false;
            }
        }
        private void AddBook_RadioButton_Click(object sender, RoutedEventArgs e)
        {
            Authour_txt.IsEnabled = true;
            Summary_txt.IsEnabled = true;
            Edition_txt.IsEnabled = true;
            foreach (RadioButton radioButton in _allFrequencies)
            {
                radioButton.IsEnabled = false;
            }
            Issue_txt.IsEnabled = false;
            foreach (CheckBox checkBox in _allGenres)
            {
                checkBox.IsChecked = false;
            }
        }
        private void BackToStart_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StartMenu));
        }
        private void BackToSearch_btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Search));
        }
    }
}
