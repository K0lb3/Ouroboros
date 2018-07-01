// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FadeColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("UI/Fade (Color)", 32741)]
  public class FlowNode_FadeColor : FlowNode
  {
    public Color Color = Color.get_clear();
    public float Time = 1f;
    public bool ForceReset;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (!FadeController.InstanceExists || this.ForceReset)
        FadeController.Instance.FadeTo(new Color((float) this.Color.r, (float) this.Color.g, (float) this.Color.b, (float) (1.0 - this.Color.a)), 0.0f, 0);
      FadeController.Instance.FadeTo(this.Color, this.Time, 0);
      ((Behaviour) this).set_enabled(true);
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
