﻿using Music_Player.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.Models {
  public class ArtistList : AReadOnlyList<Artist> {

    public static ArtistList Instance = new ArtistList();
    private ArtistList() { }

    public void Init() {
      var tracks = TrackList.Instance;
      var allArtistNames = new List<string>();

      foreach (var track in tracks)
        foreach (var artist in track.ArtistNames) {
          if (!allArtistNames.Any(a => a.Equals(artist, StringComparison.OrdinalIgnoreCase)))
            allArtistNames.Add(artist);
        }

      allArtistNames = allArtistNames.ToList();
      allArtistNames.Sort();
      var artists = allArtistNames.Select(g => new Artist(g, tracks.Where(t => t.ArtistNames.Contains(g)).ToList())).ToList();

      this.items = artists;
      this.IsLoading = false;
    }
  }
}
