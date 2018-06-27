// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTrophyAchievedQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Trophy/ReqAchievedQuest", 32741)]
  [FlowNode.Pin(2, "NoTrophy", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqTrophyAchievedQuest : FlowNode_Network
  {
    public Text Quests;
    public Toggle toggle;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).get_gameObject(), (TrophyParam) null);
      if (dataOfClass == null || !dataOfClass.Objectives[0].type.IsExtraClear())
      {
        this.toggle.set_isOn(true);
        this.ActivateOutputLinks(2);
      }
      else
        this.ExecRequest((WebAPI) new ReqTrophyAchievedQuest(dataOfClass.iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
        return;
      WebAPI.JSON_BodyResponse<TrophyQuests> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<TrophyQuests>>(www.text);
      if (Object.op_Inequality((Object) this.Quests, (Object) null))
      {
        string[] histories = jsonObject.body.histories;
        if (histories == null || histories.Length <= 0)
        {
          this.Quests.set_text(LocalizedText.Get("sys.TROPHY_NOT_ACHIEVEDQUEST"));
          Network.RemoveAPI();
          this.ActivateOutputLinks(1);
          return;
        }
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index = 0; index < histories.Length; ++index)
        {
          QuestParam quest = instance.FindQuest(histories[index]);
          if (quest != null)
          {
            Text quests = this.Quests;
            quests.set_text(quests.get_text() + quest.name + "\n");
          }
        }
      }
      Network.RemoveAPI();
      this.ActivateOutputLinks(1);
    }
  }
}
