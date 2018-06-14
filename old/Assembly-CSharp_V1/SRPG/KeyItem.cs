// Decompiled with JetBrains decompiler
// Type: SRPG.KeyItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
