// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleUnitDetailStatus : MonoBehaviour
  {
    public Text StatusValue;
    public Text StatusValueUp;
    public Text StatusValueDown;
    public GameObject GoUpBG;
    public GameObject GoDownBG;
    public Text UpDownValue;

    public BattleUnitDetailStatus()
    {
      base.\u002Ector();
    }

    public void SetStatus(BattleUnitDetailStatus.eBudStat stat, int val, int add)
    {
      int num = (int) stat;
      if (num >= 0)
      {
        ImageArray component = (ImageArray) ((Component) this).GetComponent<ImageArray>();
        if (Object.op_Inequality((Object) component, (Object) null) && num < component.Images.Length)
          component.ImageIndex = num;
      }
      if (Object.op_Implicit((Object) this.StatusValue))
        ((Component) this.StatusValue).get_gameObject().SetActive(false);
      if (Object.op_Implicit((Object) this.StatusValueUp))
        ((Component) this.StatusValueUp).get_gameObject().SetActive(false);
      if (Object.op_Implicit((Object) this.StatusValueDown))
        ((Component) this.StatusValueDown).get_gameObject().SetActive(false);
      if (Object.op_Implicit((Object) this.GoUpBG))
        this.GoUpBG.SetActive(false);
      if (Object.op_Implicit((Object) this.GoDownBG))
        this.GoDownBG.SetActive(false);
      if (Object.op_Implicit((Object) this.UpDownValue))
        ((Component) this.UpDownValue).get_gameObject().SetActive(false);
      if (add > 0)
      {
        if (Object.op_Implicit((Object) this.StatusValueUp))
        {
          this.StatusValueUp.set_text(val.ToString());
          ((Component) this.StatusValueUp).get_gameObject().SetActive(true);
        }
        if (Object.op_Implicit((Object) this.UpDownValue))
        {
          this.UpDownValue.set_text("+" + add.ToString());
          ((Component) this.UpDownValue).get_gameObject().SetActive(true);
        }
        if (!Object.op_Implicit((Object) this.GoUpBG))
          return;
        this.GoUpBG.SetActive(true);
      }
      else if (add < 0)
      {
        if (Object.op_Implicit((Object) this.StatusValueDown))
        {
          this.StatusValueDown.set_text(val.ToString());
          ((Component) this.StatusValueDown).get_gameObject().SetActive(true);
        }
        if (Object.op_Implicit((Object) this.UpDownValue))
        {
          this.UpDownValue.set_text(add.ToString());
          ((Component) this.UpDownValue).get_gameObject().SetActive(true);
        }
        if (!Object.op_Implicit((Object) this.GoDownBG))
          return;
        this.GoDownBG.SetActive(true);
      }
      else
      {
        if (!Object.op_Implicit((Object) this.StatusValue))
          return;
        this.StatusValue.set_text(val.ToString());
        ((Component) this.StatusValue).get_gameObject().SetActive(true);
      }
    }

    public enum eBudStat
    {
      MHP,
      ATK,
      DEF,
      MAG,
      MND,
      DEX,
      SPD,
      CRI,
      LUK,
      CMB,
      MOV,
      JMP,
      MAX,
    }
  }
}
