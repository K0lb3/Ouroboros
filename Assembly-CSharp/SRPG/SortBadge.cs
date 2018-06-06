// Decompiled with JetBrains decompiler
// Type: SRPG.SortBadge
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
