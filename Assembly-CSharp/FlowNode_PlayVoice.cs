// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayVoice
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

[FlowNode.Pin(100, "OneShot", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Sound/PlayVoice", 32741)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
public class FlowNode_PlayVoice : FlowNode
{
  public string charaName;
  public string cueID;

  public override void OnActivate(int pinID)
  {
    MySound.Voice voice = new MySound.Voice(this.charaName);
    if (voice != null)
      voice.Play(this.cueID, 0.0f);
    this.ActivateOutputLinks(1);
  }
}
