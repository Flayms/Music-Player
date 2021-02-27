﻿using MediaManager;
using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Music_Player.ViewModels {
  public class TrackViewModel : INotifyPropertyChanged {

    public ITrack Track {
      get => this._track;
      set {
        this._track = value;
        this._isPlaying = true;
        this.OnPropertyChanged(nameof(Title));
        this.OnPropertyChanged(nameof(Producer));
        this.OnPropertyChanged(nameof(CoverSource));
        this.OnPropertyChanged(nameof(PlayPauseImageSource));
      }
    }

    public string Title => this.Track.Title;
    public string Producer => this.Track.Producer;
    public ImageSource CoverSource => this.Track.CoverSource;
    public string PlayPauseImageSource => this._isPlaying ? "pause.png" : "play.png";
    public string ShuffleImageSource => this._queue.IsShuffle ? "shuffle_selected.png" : "shuffle.png";

    //colors used for gradient of trackview
    public Color Color { get; }
    public Color ColorDark { get; }

    private bool _isPlaying;

    private readonly TrackQueue _queue; 
    private ITrack _track;

    public TrackViewModel() {
      var logic = MainLogic.Instance;
      var queue = logic.TrackQueue;

      this._queue = queue;
      this._track = queue.CurrentTrack; //todo: shouldnt access backing property
      
      this.PlayTapCommand = new Command(PlayTapped);
      this.NextTapCommand = new Command(NextTapped);
      this.PreviousTapCommand = new Command(PreviousTapped);
      this.ShuffleTapCommand = new Command(ShuffleTapped);
      logic.TrackQueue.NewSongSelected += _OnNewSongSelected;

      var color =  this._track.GetImageColor();

      if (color.R == 0 && color.G == 0 && color.B == 0) {
        this.Color = Color.DimGray;
        this.ColorDark = color;
      } else {
        this.Color = color;
        this.ColorDark = new Color(color.R / 2, color.G / 2, color.B / 2, color.A);
      }
    }

    public ICommand PlayTapCommand { get; }
    public ICommand NextTapCommand { get; }
    public ICommand PreviousTapCommand { get; }
    public ICommand ShuffleTapCommand { get; }

    public void PlayTapped() {
      this._isPlaying = !this._isPlaying;
      this.OnPropertyChanged(nameof(PlayPauseImageSource));

      if (this._isPlaying)
        this._queue.Play();
      else
        this._queue.Pause();
    }

    public void NextTapped() => this._queue.Next();
    public void PreviousTapped() => this._queue.Previous();
    public void ShuffleTapped() {
      this._queue.Shuffle();
      this.OnPropertyChanged(nameof(ShuffleImageSource));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
      => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void _OnNewSongSelected(object sender, TrackEventArgs args) {
      this.Track = args.Track;
    }
  }
}
