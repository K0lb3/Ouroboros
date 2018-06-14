// Decompiled with JetBrains decompiler
// Type: SpriteSheet
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class SpriteSheet : ScriptableObject
{
  public SpriteSheet.Item[] Items;
  [NonSerialized]
  public bool Dirty;

  public SpriteSheet()
  {
    base.\u002Ector();
  }

  public Sprite GetSprite(string name)
  {
    int hashCode = name.GetHashCode();
    if (this.Dirty)
    {
      this.RecalcHashCodes();
      this.Dirty = false;
    }
    for (int index = 0; index < this.Items.Length; ++index)
    {
      if (hashCode == this.Items[index].h && name == this.Items[index].n)
        return this.Items[index].s;
    }
    return (Sprite) null;
  }

  private void RecalcHashCodes()
  {
    for (int index = 0; index < this.Items.Length; ++index)
      this.Items[index].h = this.Items[index].n.GetHashCode();
  }

  [Serializable]
  public struct Item
  {
    public string n;
    public Sprite s;
    [NonSerialized]
    public int h;
  }
}
