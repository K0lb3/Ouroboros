// Decompiled with JetBrains decompiler
// Type: SRPG.ReviewAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Action", FlowNode.PinTypes.Input, 1)]
  public class ReviewAction : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    public string url;

    public ReviewAction()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.OnAction();
    }

    private void Start()
    {
    }

    public void OnAction()
    {
      if (string.IsNullOrEmpty(this.url))
        return;
      this.Success();
    }

    private void Success()
    {
      Application.OpenURL(this.url);
    }
  }
}
