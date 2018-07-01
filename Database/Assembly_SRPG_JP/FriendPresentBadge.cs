// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
