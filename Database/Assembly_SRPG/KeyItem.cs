// Decompiled with JetBrains decompiler
// Type: SRPG.KeyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
