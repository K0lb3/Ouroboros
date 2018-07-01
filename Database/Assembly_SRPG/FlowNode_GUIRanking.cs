// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GUIRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(202, "CreateTowerMatch", FlowNode.PinTypes.Input, 202)]
  [FlowNode.NodeType("UI/GUIRanking", 32741)]
  [FlowNode.Pin(200, "CreateQuest", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "CreateArena", FlowNode.PinTypes.Input, 201)]
  public class FlowNode_GUIRanking : FlowNode_GUI
  {
    private UsageRateRanking.ViewInfoType type;

    public override void OnActivate(int pinID)
    {
      if (pinID == 200)
        this.type = UsageRateRanking.ViewInfoType.Quest;
      if (pinID == 201)
        this.type = UsageRateRanking.ViewInfoType.Arena;
      if (pinID == 202)
        this.type = UsageRateRanking.ViewInfoType.TowerMatch;
      if (pinID == 200 || pinID == 201 || pinID == 202)
        pinID = 100;
      base.OnActivate(pinID);
    }

    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      UsageRateRanking componentInChildren = (UsageRateRanking) this.Instance.GetComponentInChildren<UsageRateRanking>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.OnChangedToggle(this.type);
    }
  }
}
