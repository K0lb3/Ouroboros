// Decompiled with JetBrains decompiler
// Type: SRPG.ShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "換金", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "換金(1度だけ表示)", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(12, "ゲリラショップへ遷移", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "退店", FlowNode.PinTypes.Output, 11)]
  public class ShopWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string ImgPathPrefix = "MenuChar/MenuChar_Shop_";
    public RawImage ImgBackGround;
    public RawImage ImgNPC;
    public EShopType[] NpcRandArray;
    public ShopWindow.ChangeButton[] ChangeButtons;
    [Space(16f)]
    public ImageArray NamePlateImages;
    public GameObject GuerrillaShopBanner;
    private bool alreadyShowExchange;
    public LevelLock[] ShopLevelLock;

    public ShopWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      List<EShopType> eshopTypeList = new List<EShopType>();
      for (int index = 0; index < this.ShopLevelLock.Length; ++index)
      {
        if (MonoSingleton<GameManager>.Instance.Player.CheckUnlock(this.ShopLevelLock[index].Condition))
        {
          switch (this.ShopLevelLock[index].Condition)
          {
            case UnlockTargets.Shop:
              eshopTypeList.Add(EShopType.Normal);
              continue;
            case UnlockTargets.ShopTabi:
              eshopTypeList.Add(EShopType.Tabi);
              continue;
            case UnlockTargets.ShopKimagure:
              eshopTypeList.Add(EShopType.Kimagure);
              continue;
            default:
              continue;
          }
        }
      }
      this.NpcRandArray = eshopTypeList.ToArray();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ImgNPC, (UnityEngine.Object) null))
      {
        EShopType npcRand = this.NpcRandArray[Random.Range(0, this.NpcRandArray.Length)];
        this.ImgNPC.set_texture((Texture) AssetManager.Load<Texture2D>(ShopWindow.ImgPathPrefix + (object) npcRand));
      }
      MonoSingleton<GameManager>.Instance.OnSceneChange += new GameManager.SceneChangeEvent(this.OnGoOutShop);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          bool flag = MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GuerrillaShopBanner, (UnityEngine.Object) null))
            this.GuerrillaShopBanner.SetActive(flag);
          if (flag)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
            break;
          }
          this.ShowExchangeDialog();
          break;
        case 2:
          this.ShowExchangeDialog();
          break;
      }
    }

    private void OnDestroy()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.GetInstanceDirect().OnSceneChange -= new GameManager.SceneChangeEvent(this.OnGoOutShop);
    }

    private void ShowExchangeDialog()
    {
      if (this.alreadyShowExchange || !MonoSingleton<GameManager>.Instance.Player.CheckEnableConvertGold())
        return;
      this.alreadyShowExchange = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private bool OnGoOutShop()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      return true;
    }

    [Serializable]
    public class ChangeButton
    {
      public EShopType shopType;
      public Button button;
    }
  }
}
