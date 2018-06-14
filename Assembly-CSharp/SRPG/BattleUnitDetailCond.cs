// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailCond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleUnitDetailCond : MonoBehaviour
  {
    public ImageArray ImageCond;
    public Text TextValue;
    private string[] mStrShieldDesc;

    public BattleUnitDetailCond()
    {
      base.\u002Ector();
    }

    public void SetCond(EUnitCondition cond)
    {
      int index = new List<EUnitCondition>((IEnumerable<EUnitCondition>) Enum.GetValues(typeof (EUnitCondition))).IndexOf(cond);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ImageCond) && index >= 0 && index < this.ImageCond.Images.Length)
        this.ImageCond.ImageIndex = index;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextValue) || index < 0 || index >= Unit.StrNameUnitConds.Length)
        return;
      this.TextValue.set_text(Unit.StrNameUnitConds[index]);
    }

    public void SetCondShield(ShieldTypes s_type, int val)
    {
      int count = new List<EUnitCondition>((IEnumerable<EUnitCondition>) Enum.GetValues(typeof (EUnitCondition))).Count;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ImageCond) && count >= 0 && count < this.ImageCond.Images.Length)
        this.ImageCond.ImageIndex = count;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextValue))
        return;
      this.TextValue.set_text(string.Format(LocalizedText.Get("quest.BUD_COND_SHIELD_DETAIL"), (object) string.Format(LocalizedText.Get(this.mStrShieldDesc[(int) s_type]), (object) val)));
    }
  }
}
