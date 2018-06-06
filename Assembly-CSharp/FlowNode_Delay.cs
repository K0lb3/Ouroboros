// Decompiled with JetBrains decompiler
// Type: FlowNode_Delay
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Finished", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(11, "Cancel", FlowNode.PinTypes.Input, 1)]
[FlowNode.NodeType("Delay", 32741)]
[FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(2, "Cancelled", FlowNode.PinTypes.Output, 3)]
public class FlowNode_Delay : FlowNode
{
  public float Timer = 1f;
  private float mTimer;
  public bool UnscaledTime;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        if ((double) this.Timer <= 0.0)
        {
          this.ActivateOutputLinks(1);
          break;
        }
        this.mTimer = 0.0f;
        ((Behaviour) this).set_enabled(true);
        break;
      case 11:
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(2);
        break;
    }
  }

  private void Update()
  {
    if (this.UnscaledTime)
      this.mTimer += Time.get_unscaledDeltaTime();
    else
      this.mTimer += Time.get_deltaTime();
    if ((double) this.mTimer < (double) this.Timer)
      return;
    ((Behaviour) this).set_enabled(false);
    this.ActivateOutputLinks(1);
  }
}
