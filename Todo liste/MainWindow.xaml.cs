using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace Todo_liste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataAccess db = new DataAccess();
        private int editingId = -1;
        public MainWindow()
        {
            InitializeComponent();
            LoadNotes();
        }
        private void LoadNotes()
        {
            NotesList.ItemsSource = db.GetNotes();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            db.AddNote(NoteInput.Text);
            LoadNotes();
        }
        private void Delete_Click(object sender, RoutedEventArgs e) 
        {
            if (sender is Button btn && btn.DataContext is Note note)
            {
                db.DeleteNote(note.Id);
                LoadNotes() ;
            }
        }
        private void CheckBox_Changed(object sender, RoutedEventArgs e) 
        { 
         if (sender is CheckBox cb && cb.DataContext is Note note)
            {
                db.UpdateDone(note.Id, cb.IsChecked == true);
            }
        }

        private void btngem_Click(object sender, RoutedEventArgs e) 
        {
            if (editingId == -1)
            {
                db.AddNote(NoteInput.Text);
                
            }
            else
            {
                db.UpdateNote(editingId, NoteInput.Text);
                editingId = -1;
            }
            LoadNotes();
            NoteInput.Text = "Indtast Opgave";
        }   

        private void NoteInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NoteInput.Text == "Indtast Opgave")
            {
                NoteInput.Text = "";
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender is Button btn && btn.DataContext is Note note)
            {
                NoteInput.Text = note.Opgaver;
                editingId = note.Id;
            }
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}