// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayJingle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("Sound/PlayJingle", 32741)]
[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
public class FlowNode_PlayJingle : FlowNode
{
  public string cueID;

  public override void OnActivate(int pinID)
  {
    MonoSingleton<MySound>.Instance.PlayJingle(this.cueID, 0.0f, (string) null);
    this.ActivateOutputLinks(1);
  }
}
