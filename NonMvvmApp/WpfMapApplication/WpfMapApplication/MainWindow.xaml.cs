using DevSummitDemo;
using ESRI.ArcGIS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WpfMapApplication
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    PlaceFinderService _service = new PlaceFinderService();

    public MainWindow()
    {
      InitializeComponent();
    }

    private async void Button1_Click_1(object sender, RoutedEventArgs e)
    {
      List<Graphic> results = await _service.FindAsync(TextBox1.Text);

      ListBox1.ItemsSource = results;
      TextBlock1.Text = results.Count + " found";
      GraphicsLayer graphicsLayer = Map1.Layers["MyGraphicsLayer"] as GraphicsLayer;
      graphicsLayer.GraphicsSource = results;
    }

    private void ListBox1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
      if (e.RemovedItems != null)
        foreach (Graphic g in e.RemovedItems)
          g.UnSelect();

      if (e.AddedItems != null)
        foreach (Graphic g in e.AddedItems)
          g.Select();
    }

    private void Button1_Click_2(object sender, RoutedEventArgs e)
    {

    }
  }
}
