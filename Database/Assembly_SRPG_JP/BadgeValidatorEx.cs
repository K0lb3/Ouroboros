// Decompiled with JetBrains decompiler
// Type: SRPG.BadgeValidatorEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [DisallowMultipleComponent]
  public class BadgeValidatorEx : BadgeValidator
  {
    [BitMask]
    public GameManager.BadgeTypes PriorityBadgeType;

    private void Update()
    {
      this.UpdateBadge();
    }

    private void UpdateBadge()
    {
      if (this.BadgeType == ~GameManager.BadgeTypes.All)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (Object.op_Equality((Object) instanceDirect, (Object) null) || instanceDirect.CheckBusyBadges(this.BadgeType))
        return;
      int priorityBadgeType = (int) this.PriorityBadgeType;
      bool flag = instanceDirect.CheckBadges(this.BadgeType);
      if (priorityBadgeType != 0 && instanceDirect.CheckBadges(this.PriorityBadgeType))
        flag = false;
      ((Component) this).get_gameObject().SetActive(flag);
    }
  }
}
