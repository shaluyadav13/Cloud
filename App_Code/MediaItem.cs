using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class represents a general media item and is used for searching all media item types at once
/// </summary>
public class MediaItem
{
    public double SearchWeight {get; set;}
    public string MediaType { get; set; }
    public string ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public System.DateTime DatePosted { get; set; }
    public string Username { get; set; }
    public long Size { get; set; }
    public int NumOfHits { get; set; }
    public System.Nullable<System.DateTime> LastHit { get; set; }
    public System.Nullable<System.DateTime> AutoDeleteDate { get; set; }
    public string Author { get; set; }
    public bool ShareStatus { get; set; }
    public System.Nullable<int> GroupID { get; set; }
}