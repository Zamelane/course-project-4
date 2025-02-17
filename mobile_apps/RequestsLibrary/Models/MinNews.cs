﻿using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Models;
public class MinNews
{
    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("title")] public string? Title { get; set; }

    [JsonProperty("content")] public string? Content { get; set; }

    [JsonProperty("city")] public City? City { get; set; }
    [JsonProperty("cover")] public Image? Cover { get; set; }

    [JsonProperty("tags")] public ObservableCollection<Tag> Tags { get; set; }
        = new ObservableCollection<Tag>();
    [JsonProperty("category")] public Category? Category { get; set; }
    [JsonProperty("reactions")] public ObservableCollection<Reaction> Reactions { get; set; } 
        = new ObservableCollection<Reaction>();

    [JsonProperty("create_date")] public DateTime CreateDate { get; set; }

    [JsonProperty("update_date")] public DateTime? UpdateDate { get; set; }
    [JsonProperty("comments")] public int Comments { get; set; } = 0;
    [JsonProperty("views")] public int Views { get; set; } = 0;
}