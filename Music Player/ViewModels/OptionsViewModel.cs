﻿using Music_Player.Interfaces;
using Music_Player.Services;

namespace Music_Player.ViewModels {
  public class OptionsViewModel {

    private readonly MainLogic _logic = MainLogic.Instance;
    private readonly ITrack _track;

    public OptionsViewModel(ITrack track) {
      this._track = track;
    }

    public void AddNext() {
      var queue = this._logic.TrackQueue;
      queue.Tracks.Insert(queue.Index + 1, this._track);
    }

    public void AddToQueue() => this._logic.TrackQueue.Tracks.Add(this._track);

  }
}
