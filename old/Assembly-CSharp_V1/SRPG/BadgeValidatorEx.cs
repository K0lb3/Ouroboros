// Decompiled with JetBrains decompiler
// Type: SRPG.BadgeValidatorEx
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
