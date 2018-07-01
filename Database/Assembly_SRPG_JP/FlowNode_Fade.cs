// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Fade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "Fade In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(100, "Fade Out", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("UI/Fade", 32741)]
  public class FlowNode_Fade : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          if (!FadeController.InstanceExists)
            FadeController.Instance.FadeTo(Color.get_clear(), 0.0f, 0);
          FadeController.Instance.FadeTo(Color.get_black(), 1f, 0);
          ((Behaviour) this).set_enabled(true);
          break;
        case 101:
          if (!FadeController.InstanceExists)
            FadeController.Instance.FadeTo(Color.get_black(), 0.0f, 0);
          FadeController.Instance.FadeTo(Color.get_clear(), 1f, 0);
          ((Behaviour) this).set_enabled(true);
          break;
      }
    }

    private void Update()
    {
      if (FadeController.Instance.IsFading(0))
        return;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }
  }
}
