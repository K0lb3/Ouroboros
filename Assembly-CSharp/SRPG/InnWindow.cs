// Decompiled with JetBrains decompiler
// Type: SRPG.InnWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "退店", FlowNode.PinTypes.Output, 10)]
  public class InnWindow : MonoBehaviour, IFlowInterface
  {
    [Description("フレンド申請通知バッジ")]
    public GameObject NotificationBadge;

    public InnWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      this.Refresh();
      MonoSingleton<GameManager>.Instance.OnSceneChange += new GameManager.SceneChangeEvent(this.OnGoOutInn);
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Refresh()
    {
      if (!Object.op_Inequality((Object) this.NotificationBadge, (Object) null))
        return;
      this.NotificationBadge.SetActive(MonoSingleton<GameManager>.Instance.Player.FollowerNum > 0);
      GameParameter.UpdateAll(this.NotificationBadge);
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
