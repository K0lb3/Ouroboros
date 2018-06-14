// Decompiled with JetBrains decompiler
// Type: FlowNodeExtensions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public static class FlowNodeExtensions
{
  public static string GetPinNameForPinID(this FlowNode inFlowNode, int inPinID)
  {
    for (int index = 0; index < inFlowNode.Pins.Length; ++index)
    {
      if (inFlowNode.Pins[index].PinID == inPinID)
        return inFlowNode.Pins[index].Name;
    }
    return string.Empty;
  }

  public static bool IsPinMineAndPartOfMyOutputLinks(this FlowNode inFlowNode, int inPinID)
  {
    if (inFlowNode.FindPin(inPinID) == -1)
      return false;
    for (int index = 0; index < inFlowNode.OutputLinks.Length; ++index)
    {
      if (inFlowNode.OutputLinks[index].SrcPinID == inPinID)
        return true;
    }
    return false;
  }

  public static bool GetOutputLinkThatIsPartOfID(this FlowNode inFlowNode, out FlowNode.Link foundLink, int inPinID)
  {
    if (inFlowNode.FindPin(inPinID) == -1)
    {
      foundLink = new FlowNode.Link();
      return false;
    }
    for (int index = 0; index < inFlowNode.OutputLinks.Length; ++index)
    {
      if (inFlowNode.OutputLinks[index].SrcPinID == inPinID)
      {
        foundLink = inFlowNode.OutputLinks[index];
        return true;
      }
    }
    foundLink = new FlowNode.Link();
    return false;
  }

  public static string ShortName(this FlowNode inFlowNode)
  {
    return ((Object) inFlowNode).ToString().ToLower().Replace("flownode_", string.Empty).Replace(((Object) inFlowNode).get_name().ToLower(), string.Empty);
  }
}
