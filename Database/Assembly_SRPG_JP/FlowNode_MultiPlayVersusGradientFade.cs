// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayVersusGradientFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Finish", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Fade Out", FlowNode.PinTypes.Input, 2)]
  [FlowNode.NodeType("Multi/GradientFade", 32741)]
  [FlowNode.Pin(1, "Fade In", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_MultiPlayVersusGradientFade : FlowNode
  {
    private const int PIN_IN_FADE_IN = 1;
    private const int PIN_IN_FADE_OUT = 2;
    private const int PIN_OUT_FINISH = 10;
    private bool mFading;

    public override void OnActivate(int pinID)
    {
      MultiPlayVersusGradientFade instance = MultiPlayVersusGradientFade.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
      {
        DebugUtility.Log("MultiPlayVersus専用です");
      }
      else
      {
        if (pinID == 1)
          instance.FadeIn();
        else
          instance.FadeOut();
        ((Behaviour) this).set_enabled(true);
        this.mFading = true;
      }
    }

    private void Update()
    {
      if (!this.mFading)
        return;
      MultiPlayVersusGradientFade instance = MultiPlayVersusGradientFade.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
      {
        this.mFading = false;
        this.ActivateOutputLinks(10);
        ((Behaviour) this).set_enabled(false);
      }
      else
      {
        if (instance.Fading)
          return;
        this.mFading = false;
        this.ActivateOutputLinks(10);
        ((Behaviour) this).set_enabled(false);
      }
    }
  }
}
