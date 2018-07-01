// Decompiled with JetBrains decompiler
// Type: SRPG.ShopHomeIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class ShopHomeIcon : MonoBehaviour
  {
    public GameObject ShopIcon;
    public GameObject GuerrillaIcon;

    public ShopHomeIcon()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      bool flag = MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen();
      if (Object.op_Inequality((Object) this.ShopIcon, (Object) null))
        this.ShopIcon.SetActive(!flag);
      if (!Object.op_Inequality((Object) this.GuerrillaIcon, (Object) null))
        return;
      this.GuerrillaIcon.SetActive(flag);
    }
  }
}
