// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EndQuest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/クエスト終了", 32741)]
  [FlowNode.Pin(0, "End", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "ForceEnd", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ForceEnded", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_EndQuest : FlowNode
  {
    public bool Restart;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0 && Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
      {
        if (Network.Mode == Network.EConnectMode.Offline)
        {
          QuestParam quest = SceneBattle.Instance.Battle.GetQuest();
          BattleCore.Record questRecord = SceneBattle.Instance.Battle.GetQuestRecord();
          if (quest != null && questRecord != null)
          {
            QuestParam questParam = quest;
            questParam.clear_missions = (OInt) ((int) questParam.clear_missions | questRecord.bonusFlags);
          }
        }
        SceneBattle.Instance.ExitRequest = !this.Restart ? SceneBattle.ExitRequests.End : SceneBattle.ExitRequests.Restart;
      }
      else
      {
        if (pinID != 1)
          return;
        if (Object.op_Equality((Object) SceneBattle.Instance, (Object) null))
        {
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(101);
        }
        else
        {
          ((Behaviour) this).set_enabled(true);
          SceneBattle.Instance.ForceEndQuest();
        }
      }
    }

    private void Update()
    {
      if (!Object.op_Equality((Object) SceneBattle.Instance, (Object) null))
        return;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(101);
    }
  }
}
