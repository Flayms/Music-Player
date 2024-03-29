﻿using Music_Player.Models;
using System.Collections.Generic;

namespace Music_Player.Interfaces {
  public interface IDisplayGroup {
    string Name { get; }
    int TrackAmount { get; }
    string TrackAmountUi { get; }
    List<Track> Tracks { get; }
  }
}
