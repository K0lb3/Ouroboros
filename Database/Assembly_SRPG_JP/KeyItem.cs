// Decompiled with JetBrains decompiler
// Type: SRPG.KeyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [Serializable]
  public class KeyItem
  {
    public string iname;
    public int num;

    public bool IsHasItem()
    {
      return MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.iname) >= this.num;
    }
  }
}
