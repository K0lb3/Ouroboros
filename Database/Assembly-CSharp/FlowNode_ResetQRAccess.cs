// Decompiled with JetBrains decompiler
// Type: FlowNode_ResetQRAccess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

[FlowNode.NodeType("QRCode/ResetQRCodeAccess", 32741)]
[FlowNode.Pin(100, "Finished", FlowNode.PinTypes.Output, 100)]
[FlowNode.Pin(0, "Reset", FlowNode.PinTypes.Input, 0)]
public class FlowNode_ResetQRAccess : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    DebugUtility.Log("Cancel QRCodeAccess");
    MonoSingleton<UrlScheme>.Instance.ParamString = (string) null;
    FlowNode_OnUrlSchemeLaunch.QRCampaignID = -1;
    FlowNode_OnUrlSchemeLaunch.QRSerialID = string.Empty;
    FlowNode_OnUrlSchemeLaunch.IsQRAccess = false;
    this.ActivateOutputLinks(100);
  }
}
