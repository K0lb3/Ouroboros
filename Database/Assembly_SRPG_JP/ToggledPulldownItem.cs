// Decompiled with JetBrains decompiler
// Type: SRPG.ToggledPulldownItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
