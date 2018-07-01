// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DestroyAsset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("System/DestroyAsset", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_DestroyAsset : FlowNode
  {
    public AssetBundleFlags flags;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
        AssetDownloader.DestroyAssetStart(this.flags);
      this.ActivateOutputLinks(1);
    }
  }
}
