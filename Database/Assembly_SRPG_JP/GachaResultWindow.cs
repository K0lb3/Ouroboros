// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Setup", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Refresh", FlowNode.PinTypes.Output, 10)]
  public class GachaResultWindow : MonoBehaviour, IFlowInterface
  {
    public GameObject ThumbnailListWindow;
    public Button BackButton;
    private bool Initalized;

    public GachaResultWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.SetUp();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) HomeWindow.Current, (Object) null))
        HomeWindow.Current.SetVisible(true);
      if (Object.op_Inequality((Object) this.BackButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BackButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__326)));
      }
      this.Initalized = true;
    }

    private void OnCloseWindow(Button button)
    {
      if (!this.Initalized)
        return;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CLOSED_RESULT");
    }

    private void SetUp()
    {
      if (GachaResultData.drops == null)
        return;
      FlowNode_Variable.Set("GachaResultCurrentDetail", string.Empty);
      FlowNode_Variable.Set("GachaResultSingle", "0");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }
  }
}
