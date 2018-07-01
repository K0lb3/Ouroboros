// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailTag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleUnitDetailTag : MonoBehaviour
  {
    public Text TextValue;

    public BattleUnitDetailTag()
    {
      base.\u002Ector();
    }

    public void SetTag(string tag)
    {
      if (tag == null)
        tag = string.Empty;
      if (!Object.op_Implicit((Object) this.TextValue))
        return;
      this.TextValue.set_text(tag);
    }
  }
}
