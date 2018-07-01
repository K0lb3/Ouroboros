// Decompiled with JetBrains decompiler
// Type: SRPG.SortBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SortBadge : MonoBehaviour
  {
    [FourCC]
    public int ID;
    public Image Icon;
    public Text Value;

    public SortBadge()
    {
      base.\u002Ector();
    }

    public void SetValue(string value)
    {
      if (!Object.op_Inequality((Object) this.Value, (Object) null))
        return;
      this.Value.set_text(value);
    }

    public void SetValue(int value)
    {
      if (!Object.op_Inequality((Object) this.Value, (Object) null))
        return;
      this.Value.set_text(value.ToString());
    }
  }
}
