// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class FriendPresentBadge : MonoBehaviour
  {
    public GameObject BadgeObject;
    [BitMask]
    public GameManager.BadgeTypes BadgeType;

    public FriendPresentBadge()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.BadgeObject, (Object) null))
        return;
      this.BadgeObject.SetActive(false);
    }

    private void Update()
    {
      if (Object.op_Equality((Object) this.BadgeObject, (Object) null))
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Inequality((Object) instanceDirect, (Object) null))
        return;
      bool flag = instanceDirect.CheckBadges(this.BadgeType);
      if (instanceDirect.Player != null)
        flag |= instanceDirect.Player.ValidFriendPresent;
      this.BadgeObject.SetActive(flag);
    }
  }
}
