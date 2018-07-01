// Decompiled with JetBrains decompiler
// Type: SRPG.InnWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "退店", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class InnWindow : MonoBehaviour, IFlowInterface
  {
    [Description("フレンド申請通知バッジ")]
    public GameObject NotificationBadge;
    [Description("フレンドプレゼントボタン")]
    public GameObject FriendPresentButton;

    public InnWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      MonoSingleton<GameManager>.Instance.OnSceneChange += new GameManager.SceneChangeEvent(this.OnGoOutInn);
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    public void Refresh()
    {
      if (Object.op_Inequality((Object) this.NotificationBadge, (Object) null))
      {
        this.NotificationBadge.SetActive(MonoSingleton<GameManager>.Instance.Player.FollowerNum > 0);
        GameParameter.UpdateAll(this.NotificationBadge);
      }
      if (!Object.op_Inequality((Object) this.FriendPresentButton, (Object) null))
        return;
      if (MonoSingleton<GameManager>.Instance.MasterParam.IsFriendPresentItemParamValid())
        this.FriendPresentButton.SetActive(true);
      else
        this.FriendPresentButton.SetActive(false);
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnGoOutInn);
    }

    private bool OnGoOutInn()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      return true;
    }
  }
}
