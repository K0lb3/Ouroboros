// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BackgroundDLFinished
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(11, "Background DL Enabled", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(10, "Background DL Finished", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "BG Dl enabled?", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(0, "Begin Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/CheckBGDL", 32741)]
  public class FlowNode_BackgroundDLFinished : FlowNodePersistent
  {
    [FlowNode.ShowInInfo]
    [SerializeField]
    private bool check;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          DebugUtility.LogWarning("Starting check");
          if (!GameUtility.Config_UseAssetBundles.Value || !BackgroundDownloader.Instance.IsEnabled || BackgroundDownloader.Instance.IsCurrentStepComplete())
            this.ActivateOutputLinks(10);
          this.check = true;
          break;
        case 1:
          if (!BackgroundDownloader.Instance.IsEnabled)
            break;
          this.ActivateOutputLinks(11);
          break;
      }
    }

    private void Update()
    {
      if (!this.check || GameUtility.Config_UseAssetBundles.Value && BackgroundDownloader.Instance.IsEnabled && !BackgroundDownloader.Instance.IsCurrentStepComplete())
        return;
      this.ActivateOutputLinks(10);
    }
  }
}
