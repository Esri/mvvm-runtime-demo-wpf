using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Tasks;

namespace DevSummitDemo
{
  /// <summary>
  /// Simple place finding service based on a Find service.
  /// </summary>
  public class PlaceFinderService
  {
    /// <summary>
    /// Searches asynchronously for places matching the specified text.
    /// </summary>
    public Task<List<Graphic>> FindAsync(string searchText)
    {
      FindParameters findParams = new FindParameters();
      findParams.LayerIds.AddRange(new int[] { 0 });  // cities layer
      findParams.SearchFields.AddRange(new string[] { "CITY_NAME" });
      findParams.SpatialReference = new SpatialReference(4326);
      findParams.SearchText = searchText;
      
      FindTask findTask = new FindTask("http://sampleserver1.arcgisonline.com/ArcGIS/rest/services/Specialty/ESRI_StatesCitiesRivers_USA/MapServer");

      var tcs = new TaskCompletionSource<List<Graphic>>();
      findTask.ExecuteCompleted += (sender, e) =>
      {
        List<Graphic> graphics = new List<Graphic>();
        foreach (var result in e.FindResults)
          graphics.Add(result.Feature);

        tcs.TrySetResult(graphics);
      };

      findTask.Failed += (sender, e) =>
      {
        tcs.TrySetResult(new List<Graphic>());
      };

      findTask.ExecuteAsync(findParams);
      return tcs.Task;
    }
  }
}
