﻿using System.Collections;
using System.Collections.Generic;

namespace Music_Player.Helpers {
  public class AReadOnlyList<T> : IReadOnlyList<T> {

    protected IReadOnlyList<T> items;

    public T this[int index] => this.items[index];
    public int Count => this.items.Count;

    public IEnumerator<T> GetEnumerator() => this.items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.items.GetEnumerator();
  }
}
