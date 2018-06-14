// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetBoolPlayerPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(2, "Check", FlowNode.PinTypes.Input, 2)]
  [FlowNode.NodeType("System/GetBoolPlayerPrefs", 32741)]
  [FlowNode.Pin(1, "SetTrue", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "SetFalse", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_GetBoolPlayerPrefs : FlowNode
  {
    private const int CHECK = 2;
    private const int GET_FALSE = 0;
    private const int GET_TRUE = 1;
    public string Name;

    public override void OnActivate(int pinID)
    {
      if (pinID == 2)
        this.ActivateOutputLinks(PlayerPrefsUtility.GetInt(this.Name, 0) != 1 ? 0 : 1);
      else
        this.ActivateOutputLinks(0);
    }
  }
}
