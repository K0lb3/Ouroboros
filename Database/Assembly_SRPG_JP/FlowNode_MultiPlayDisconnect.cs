// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayDisconnect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "Disconnect", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Multi/MultiPlayDisconnect", 32741)]
  public class FlowNode_MultiPlayDisconnect : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.NOP)
        instance.Disconnect();
      this.ActivateOutputLinks(1);
    }
  }
}
