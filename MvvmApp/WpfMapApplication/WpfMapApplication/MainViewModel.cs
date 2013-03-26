using DevSummitDemo;
using ESRI.ArcGIS.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfMapApplication
{
  public class MainViewModel : ViewModel
  {
    PlaceFinderService _service = new PlaceFinderService();
    Graphic _currentResult;
    string _searchText;
    List<Graphic> _searchResults;
    string _summaryText;

    public MainViewModel()
    {
      SummaryText = "Ready";
    }

    public List<Graphic> SearchResults
    {
      get { return _searchResults; }
      set { SetProperty(ref _searchResults, value); }
    } 

    public string SearchText
    {
      get { return _searchText; }
      set { SetProperty(ref _searchText, value); }
    } 

    public string SummaryText
    {
      get { return _summaryText; }
      set { SetProperty(ref _summaryText, value); }
    } 

    public Graphic CurrentResult
    {
      get { return _currentResult; }
      set
      {
        if (_currentResult != null)
          _currentResult.UnSelect();

        SetProperty(ref _currentResult, value);

        if (_currentResult != null)
          _currentResult.Select();
      }
    }

    public async Task DoSearch()
    {
      List<Graphic> results = await _service.FindAsync(SearchText);

      SearchResults = results;
      SummaryText = results.Count.ToString() + " found";
    }

  }
}
