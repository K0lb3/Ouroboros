// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Sound/PlayVoice", 32741)]
public class FlowNode_PlayVoice : FlowNode
{
  public string charaName;
  public string cueID;

  public override void OnActivate(int pinID)
  {
    MySound.Voice voice = new MySound.Voice(this.charaName);
    if (voice != null)
      voice.Play(this.cueID, 0.0f, false);
    this.ActivateOutputLinks(1);
  }
}
