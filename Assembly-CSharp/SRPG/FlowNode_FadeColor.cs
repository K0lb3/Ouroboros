// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FadeColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/Fade (Color)", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 10)]
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
