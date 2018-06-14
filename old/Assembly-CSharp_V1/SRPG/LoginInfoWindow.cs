// Decompiled with JetBrains decompiler
// Type: SRPG.LoginInfoWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Gacha", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "LimitedShop", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "EventQuest", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "TowerQuest", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "BuyCoin", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(0, "None", FlowNode.PinTypes.Output, 0)]
  public class LoginInfoWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Button Move;
    [SerializeField]
    private Text MoveBtnText;
    [SerializeField]
    private Image InfoImage;
    [SerializeField]
    private Toggle CheckToggle;
    [SerializeField]
    private Button CloseBtn;
    private LoginInfoParam.SelectScene mSelectScene;
    private bool mLoaded;
    private bool mRefresh;

    public LoginInfoWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.Move, (Object) null))
        ((Selectable) this.Move).set_interactable(false);
      if (!Object.op_Inequality((Object) this.InfoImage, (Object) null))
        return;
      ((Component) this.InfoImage).get_gameObject().SetActive(false);
    }

    private void Start()
    {
      LoginInfoParam[] activeLoginInfos = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetActiveLoginInfos();
      if (activeLoginInfos == null || activeLoginInfos.Length <= 0)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
      }
      else
      {
        int index = Random.Range(0, activeLoginInfos.Length);
        this.mSelectScene = activeLoginInfos[index].scene;
        string[] strArray = activeLoginInfos[index].path.Split('/');
        if (strArray == null || strArray.Length < 2)
          return;
        this.StartCoroutine(this.LoadImages(strArray[0], strArray[1]));
      }
    }

    private void Update()
    {
      if (!this.mLoaded || this.mRefresh)
        return;
      this.mRefresh = true;
      this.Refresh();
    }

    private void Refresh()
    {
      if (Object.op_Inequality((Object) this.Move, (Object) null) && Object.op_Inequality((Object) this.MoveBtnText, (Object) null))
      {
        if (this.mSelectScene == LoginInfoParam.SelectScene.Gacha)
          this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_GACHA"));
        else if (this.mSelectScene == LoginInfoParam.SelectScene.LimitedShop)
          this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_LIMITEDSHOP"));
        else if (this.mSelectScene == LoginInfoParam.SelectScene.EventQuest)
          this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_EVENTQUEST"));
        else if (this.mSelectScene == LoginInfoParam.SelectScene.TowerQuest)
          this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_TOWERQUEST"));
        else if (this.mSelectScene == LoginInfoParam.SelectScene.BuyShop)
          this.MoveBtnText.set_text(LocalizedText.Get("sys.TEXT_LOGININFO_BUYCOIN"));
        else
          this.MoveBtnText.set_text(LocalizedText.Get("sys.OK"));
        // ISSUE: method pointer
        ((UnityEvent) this.Move.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnMoveScene)));
        ((Selectable) this.Move).set_interactable(true);
      }
      if (!Object.op_Inequality((Object) this.InfoImage, (Object) null))
        return;
      ((Component) this.InfoImage).get_gameObject().SetActive(Object.op_Inequality((Object) this.InfoImage.get_sprite(), (Object) null));
    }

    private void OnMoveScene()
    {
      if (Object.op_Inequality((Object) this.CheckToggle, (Object) null) && this.CheckToggle.get_isOn())
        GameUtility.setLoginInfoRead(TimeManager.FromUnixTime(TimeManager.Now()).ToString("yyyy/MM/dd"));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, (int) this.mSelectScene);
    }

    [DebuggerHidden]
    private IEnumerator LoadImages(string path, string img)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginInfoWindow.\u003CLoadImages\u003Ec__IteratorBF() { path = path, img = img, \u003C\u0024\u003Epath = path, \u003C\u0024\u003Eimg = img, \u003C\u003Ef__this = this };
    }

    public enum SelectScene : byte
    {
      None,
      Gacha,
      LimitedShop,
      EventQuest,
      TowerQuest,
      BuyShop,
    }
  }
}
