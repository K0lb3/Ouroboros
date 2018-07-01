// Decompiled with JetBrains decompiler
// Type: SRPG.ToggledPulldownItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ToggledPulldownItem : PulldownItem
  {
    public GameObject imageOn;
    public GameObject imageOff;

    public override void OnStatusChanged(bool enabled)
    {
      if (Object.op_Inequality((Object) this.imageOn, (Object) null))
        this.imageOn.SetActive(enabled);
      if (!Object.op_Inequality((Object) this.imageOff, (Object) null))
        return;
      this.imageOff.SetActive(!enabled);
    }
  }
}
