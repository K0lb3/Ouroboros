// Decompiled with JetBrains decompiler
// Type: SRPG.SortBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
