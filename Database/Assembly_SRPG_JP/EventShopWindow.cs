// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "退店", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "換金", FlowNode.PinTypes.Output, 10)]
  public class EventShopWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string ImgPathPrefix = "MenuChar/MenuChar_Shop_Monozuki";
    public RawImage ImgBackGround;
    public RawImage ImgNPC;
    public Text TxtHaveCoin;
    [Space(16f)]
    public ImageArray NamePlateImages;

    public EventShopWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.TxtHaveCoin, (Object) null))
        return;
      this.TxtHaveCoin.set_text(LocalizedText.Get("sys.CMD_COIN_LIST"));
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ImgNPC, (Object) null))
        this.ImgNPC.set_texture((Texture) AssetManager.Load<Texture2D>(EventShopWindow.ImgPathPrefix));
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
  }
}
