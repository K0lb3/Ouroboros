// Decompiled with JetBrains decompiler
// Type: SRPG.ShopWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "退店", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "換金", FlowNode.PinTypes.Output, 10)]
  public class ShopWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string ImgPathPrefix = "MenuChar/MenuChar_Shop_";
    public RawImage ImgBackGround;
    public RawImage ImgNPC;
    public EShopType[] NpcRandArray;
    public ShopWindow.ChangeButton[] ChangeButtons;
    [Space(16f)]
    public ImageArray NamePlateImages;
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
    }

    private void Start()
    {
      if (MonoSingleton<GameManager>.Instance.Player.CheckEnableConvertGold())
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      if (Object.op_Inequality((Object) this.ImgNPC, (Object) null))
      {
        EShopType npcRand = this.NpcRandArray[Random.Range(0, this.NpcRandArray.Length)];
        this.ImgNPC.set_texture((Texture) AssetManager.Load<Texture2D>(ShopWindow.ImgPathPrefix + (object) npcRand));
      }
      MonoSingleton<GameManager>.Instance.OnSceneChange += new GameManager.SceneChangeEvent(this.OnGoOutShop);
    }

    public void Activated(int pinID)
    {
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        return;
      MonoSingleton<GameManager>.GetInstanceDirect().OnSceneChange -= new GameManager.SceneChangeEvent(this.OnGoOutShop);
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
