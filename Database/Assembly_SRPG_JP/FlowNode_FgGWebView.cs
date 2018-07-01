// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FgGWebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(3, "Finished", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Enable", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("FgGID/FgGWebView", 32741)]
  [FlowNode.Pin(2, "Disable", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_FgGWebView : FlowNode
  {
    private const int PIN_ID_ENABLE = 1;
    private const int PIN_ID_DISABLE = 2;
    private const int PIN_ID_FINISHED = 3;
    [FlowNode.DropTarget(typeof (GameObject), true)]
    [FlowNode.ShowInInfo]
    public GameObject Target;
    public string URL;
    public RawImage mClientArea;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (!Object.op_Inequality((Object) this.Target, (Object) null))
            break;
          ((Behaviour) this.mClientArea).set_enabled(true);
          this.OpenURL();
          break;
        case 2:
          if (!Object.op_Inequality((Object) this.Target, (Object) null))
            break;
          ((Behaviour) this.mClientArea).set_enabled(false);
          break;
      }
    }

    private void OpenURL()
    {
    }
  }
}
