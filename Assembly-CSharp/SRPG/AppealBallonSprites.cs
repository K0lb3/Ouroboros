// Decompiled with JetBrains decompiler
// Type: SRPG.AppealBallonSprites
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class AppealBallonSprites : ScriptableObject
  {
    public AppealBallonSprites.Item[] Items;
    private bool Dirty;

    public AppealBallonSprites()
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
          return this.Items[index].CharSprite;
      }
      return (Sprite) null;
    }

    public Sprite GetSpriteTextL(string id)
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
          return this.Items[index].TextLSprite;
      }
      return (Sprite) null;
    }

    public Sprite GetSpriteTextR(string id)
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
          return this.Items[index].TextRSprite;
      }
      return (Sprite) null;
    }

    public Sprite[] GetSprites(string id)
    {
      int hashCode = id.GetHashCode();
      if (this.Dirty)
      {
        this.RecalcHashCodes();
        this.Dirty = false;
      }
      Sprite[] spriteArray = new Sprite[3];
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (hashCode == this.Items[index].hash && id == this.Items[index].ID)
        {
          spriteArray[0] = this.Items[index].CharSprite;
          spriteArray[1] = this.Items[index].TextLSprite;
          spriteArray[1] = this.Items[index].TextRSprite;
          break;
        }
      }
      return spriteArray;
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
      public Sprite CharSprite;
      public Sprite TextLSprite;
      public Sprite TextRSprite;
      [NonSerialized]
      public int hash;
    }
  }
}
