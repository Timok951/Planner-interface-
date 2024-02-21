using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

internal class Note
{
    public DateTime Date {get;set;}
    public string Bigdesc { get; set; }
    public string Desc {get; set;}


    public Note(DateTime date, string bigdesc, string desc)
    {
       Date = date;
       Bigdesc = bigdesc;
       Desc = desc;
    }
}




namespace Planner_interface_
{
 
    public partial class MainWindow : Window
    {
        public DateTime selectedDate = DateTime.Now;

        ObservableCollection<Note> NoteList = new ObservableCollection<Note>();
   
        public MainWindow()
        {
            
            InitializeComponent();
            ListBox.ItemsSource = NoteList;
            ListBox.DisplayMemberPath = "Date";
            ListBox.DisplayMemberPath = "Bigdesc";
            ListBox.DisplayMemberPath = "Desc";
            dtp.SelectedDate = selectedDate;

           

        }
        

        private void createbt_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = Convert.ToDateTime(dtp.SelectedDate);
            Note testnote = new Note(date,  Descriptiontext.Text, Nametext.Text); ;
            NoteList.Add(testnote);
            string json = JsonConvert.SerializeObject(NoteList);
            string relativePath = "result/file.json";
            string fullPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            File.WriteAllText(fullPath, json);
            selectedDate = Convert.ToDateTime(dtp.SelectedDate);
            ObservableCollection<Note> filteredNotes = new ObservableCollection<Note>(NoteList.Where(n => n.Date.Date == selectedDate.Date));
            ListBox.ItemsSource = filteredNotes;
        }


        private void dtp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            selectedDate = Convert.ToDateTime(dtp.SelectedDate);
            ObservableCollection<Note> filteredNotes = new ObservableCollection<Note>(NoteList.Where(n => n.Date.Date == selectedDate.Date));
            ListBox.ItemsSource = filteredNotes;

        }




        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox.SelectedItem != null)
            {
                Note selectedNote = (Note)ListBox.SelectedItem;
                if (!string.IsNullOrEmpty(selectedNote.Bigdesc))
                {
                    Descriptiontext.Text = selectedNote.Bigdesc.ToString();
                }
                if (!string.IsNullOrEmpty(selectedNote.Desc))
                {
                    Nametext.Text = selectedNote.Desc.ToString();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var note = ListBox.SelectedItem;

            NoteList.Remove((Note)note);
            string json = JsonConvert.SerializeObject(NoteList);
            string relativePath = "result/file.json";
            string fullPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            File.WriteAllText(fullPath, json);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var note = ListBox.SelectedItem;

            NoteList.Remove((Note)note);
            DateTime date = Convert.ToDateTime(dtp.SelectedDate);
            Note testnote = new Note(date, Descriptiontext.Text, Nametext.Text); ;
            NoteList.Add(testnote);
            string json = JsonConvert.SerializeObject(NoteList);
            string relativePath = "result/file.json";
            string fullPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            File.WriteAllText(fullPath, json);


        }
    }
}
