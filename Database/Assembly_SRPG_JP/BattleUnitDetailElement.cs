// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class BattleUnitDetailElement : MonoBehaviour
  {
    public ImageArray ImageElement;
    public ImageArray ImageFluct;

    public BattleUnitDetailElement()
    {
      base.\u002Ector();
    }

    public void SetElement(EElement elem, BattleUnitDetail.eBudFluct fluct)
    {
      if (Object.op_Implicit((Object) this.ImageElement))
      {
        int num = (int) elem;
        if (num >= 0 && num < this.ImageElement.Images.Length)
          this.ImageElement.ImageIndex = num;
      }
      if (!Object.op_Implicit((Object) this.ImageFluct))
        return;
      ((Component) this.ImageFluct).get_gameObject().SetActive(fluct != BattleUnitDetail.eBudFluct.NONE);
      if (fluct == BattleUnitDetail.eBudFluct.NONE)
        return;
      int num1 = (int) fluct;
      if (num1 >= this.ImageFluct.Images.Length)
        return;
      this.ImageFluct.ImageIndex = num1;
    }
  }
}
