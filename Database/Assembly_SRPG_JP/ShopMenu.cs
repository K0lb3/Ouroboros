// Decompiled with JetBrains decompiler
// Type: SRPG.ShopMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(3, "コイン交換所", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(1, "通常ショップ", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(4, "魂の交換所", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(2, "秘密の店", FlowNode.PinTypes.Input, 2)]
  public class ShopMenu : MonoBehaviour, IFlowInterface
  {
    public Button ShopButton;
    public Button GuerrillaShopButton;
    public Button LimitedShopButton;
    public Button CoinShopButton;
    public Button KakeraShopButton;
    [Space(10f)]
    public GameObject ActiveShop;
    public GameObject ActiveGuerrilla;
    public GameObject ActiveLimited;
    public GameObject ActiveCoin;
    public GameObject ActiveKakera;
    public GameObject GuerrillaBallon;

    public ShopMenu()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      switch (pinID)
      {
        case 1:
          flag1 = true;
          break;
        case 2:
          flag2 = true;
          break;
        case 3:
          flag3 = true;
          break;
        case 4:
          flag4 = true;
          break;
      }
      if (Object.op_Inequality((Object) this.ShopButton, (Object) null))
      {
        ((Component) this.ShopButton).get_gameObject().SetActive(true);
        ((Selectable) this.ShopButton).set_interactable(!flag1);
      }
      if (Object.op_Inequality((Object) this.ActiveShop, (Object) null))
        this.ActiveShop.SetActive(flag1);
      if (Object.op_Inequality((Object) this.GuerrillaBallon, (Object) null))
        this.GuerrillaBallon.SetActive(MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen());
      if (Object.op_Inequality((Object) this.LimitedShopButton, (Object) null))
        ((Selectable) this.LimitedShopButton).set_interactable(!flag2);
      if (Object.op_Inequality((Object) this.ActiveLimited, (Object) null))
        this.ActiveLimited.SetActive(flag2);
      if (Object.op_Inequality((Object) this.CoinShopButton, (Object) null))
        ((Selectable) this.CoinShopButton).set_interactable(!flag3);
      if (Object.op_Inequality((Object) this.ActiveCoin, (Object) null))
        this.ActiveCoin.SetActive(flag3);
      if (Object.op_Inequality((Object) this.KakeraShopButton, (Object) null))
        ((Selectable) this.KakeraShopButton).set_interactable(!flag4);
      if (!Object.op_Inequality((Object) this.ActiveKakera, (Object) null))
        return;
      this.ActiveKakera.SetActive(flag4);
    }
  }
}
