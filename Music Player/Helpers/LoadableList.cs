﻿using System.Collections;
using System.Collections.Generic;

namespace Music_Player.Helpers {
  public class LoadableList<T> : ALoadable, IReadOnlyList<T> {

    protected IReadOnlyList<T> items = new List<T>(); 

    public void Init(List<T> items) {
      this.items = items;
      this.IsLoading = false;
    }

    public T this[int index] => this.items[index];
    public int Count => this.items.Count;

    public IEnumerator<T> GetEnumerator() => this.items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.items.GetEnumerator();
  }
}
