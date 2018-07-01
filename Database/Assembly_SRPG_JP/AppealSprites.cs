// Decompiled with JetBrains decompiler
// Type: SRPG.AppealSprites
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class AppealSprites : ScriptableObject
  {
    public AppealSprites.Item[] Items;
    private bool Dirty;

    public AppealSprites()
    {
      base.\u002Ector();
    }

    public Sprite GetSprite(string id)
    {
      int hashCode = id.GetHashCode();
      if (this.Dirty)
      {
        this.RecalcHashCodes();
        this.Dirty = false;
      }
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (hashCode == this.Items[index].hash && id == this.Items[index].ID)
          return this.Items[index].Sprite;
      }
      return (Sprite) null;
    }

    private void RecalcHashCodes()
    {
      for (int index = 0; index < this.Items.Length; ++index)
        this.Items[index].hash = this.Items[index].ID.GetHashCode();
    }

    [Serializable]
    public struct Item
    {
      public string ID;
      public Sprite Sprite;
      [NonSerialized]
      public int hash;
    }
  }
}
