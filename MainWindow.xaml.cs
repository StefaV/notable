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

namespace Notes;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public class Info
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
    }

public partial class MainWindow : Window
{
    public void Save(object sender, RoutedEventArgs e)
    {
        var saveInfo = new Info
        {
            Name = "Naslov 2",
            Content = NoteContent.Text
        };

        string Note = JsonSerializer.Serialize(saveInfo);
        File.WriteAllText("saves.json", Note);

    }
    public void Open(object sender, RoutedEventArgs e)
    {   
        string FileName = "saves.json";
        string JsonString = File.ReadAllText(FileName);
        Info info = JsonSerializer.Deserialize<Info>(JsonString)!;
        
        NoteContent.Text =  info.Content;
    }
    public MainWindow()
    {
        InitializeComponent();
    }
}