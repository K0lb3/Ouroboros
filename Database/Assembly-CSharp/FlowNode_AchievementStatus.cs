// Decompiled with JetBrains decompiler
// Type: FlowNode_AchievementStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;

[FlowNode.NodeType("Achievement/Status", 58751)]
[FlowNode.Pin(2, "Turn Auth False", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(1, "Turn Auth True", FlowNode.PinTypes.Output, 0)]
public class FlowNode_AchievementStatus : FlowNodePersistent
{
  private bool mIsAuth;

  public override void OnActivate(int pinID)
  {
  }

  private void Update()
  {
    bool flag = GameCenterManager.IsAuth();
    if (this.mIsAuth != flag)
    {
      if (flag)
        this.ActivateOutputLinks(1);
      else
        this.ActivateOutputLinks(2);
    }
    this.mIsAuth = flag;
  }
}
