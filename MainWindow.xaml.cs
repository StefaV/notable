using System.Configuration;
using System.Drawing.Text;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;

namespace Notes;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public class NoteInfo
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
    }

public partial class MainWindow : Window
{
    public void Save(object sender, RoutedEventArgs e)
    {
        var saveInfo = new NoteInfo
        {
            Name = NoteTitle.Text,
            Content = NoteContent.Text
        };

        var NotePath = $@"Books\{NoteTitle.Text}.json";
        var Note = JsonSerializer.Serialize(saveInfo);
        File.WriteAllText(NotePath, Note);

        Button NoteButton = new Button();
        NoteButton.Tag = NotePath;
        NoteButton.Content = NoteTitle.Text;
        NoteButton.Height = 50;
        NoteButton.BorderThickness = new Thickness(0);
        NoteButton.Click += new RoutedEventHandler(Open);
        NoteList.Children.Add(NoteButton);

    }
    public void Open(object sender, RoutedEventArgs e)
    {   
        Button ClickedButton = (Button)sender;

        var FileName = $@"{ClickedButton.Tag}";
        var JsonString = File.ReadAllText(FileName);
        NoteInfo Info = JsonSerializer.Deserialize<NoteInfo>(JsonString)!;
        
        NoteTitle.Text = Info.Name;
        NoteContent.Text = Info.Content;
    }

    public void ListNotes()
    {
        IEnumerable<string> files = Directory.EnumerateFiles(@"C:\Users\Stefan\Desktop\Projekti\C#\Notes\Books","*.json");

        foreach (var file in files)
        {
            var JsonString = File.ReadAllText(file);
            NoteInfo Info = JsonSerializer.Deserialize<NoteInfo>(JsonString)!;

            Button NoteButton = new Button();
            NoteButton.Tag = file;
            NoteButton.Content = Info.Name;
            NoteButton.Height = 50;
            NoteButton.BorderThickness = new Thickness(0);
            NoteButton.Click += new RoutedEventHandler(Open);
            NoteList.Children.Add(NoteButton);
        }
    }
    public MainWindow()
    {
        InitializeComponent();
        ListNotes();
    }
}