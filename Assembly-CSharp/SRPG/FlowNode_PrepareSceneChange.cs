// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PrepareSceneChange
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/PrepareSceneChange")]
  [FlowNode.Pin(0, "Done", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "Cancel", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_PrepareSceneChange : FlowNodePersistent
  {
    private bool mStart;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (!MonoSingleton<GameManager>.Instance.PrepareSceneChange())
        this.Cancel();
      else
        this.mStart = true;
    }

    private void Reset()
    {
      this.mStart = false;
    }

    private void Done()
    {
      this.Reset();
      this.ActivateOutputLinks(0);
    }

    private void Cancel()
    {
      this.Reset();
      this.ActivateOutputLinks(1);
    }

    private void Update()
    {
      if (!this.mStart || MonoSingleton<GameManager>.Instance.IsImportantJobRunning)
        return;
      this.Done();
    }
  }
}
