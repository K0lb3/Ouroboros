// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Quest", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("System/ReqRanking", 32741)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(3, "TowerMuch", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(2, "Arena", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqRanking : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      UsageRateRanking component = (UsageRateRanking) ((Component) this).get_gameObject().GetComponent<UsageRateRanking>();
      switch (pinID)
      {
        case 1:
          component.OnChangedToggle(UsageRateRanking.ViewInfoType.Quest);
          break;
        case 2:
          component.OnChangedToggle(UsageRateRanking.ViewInfoType.Arena);
          break;
        case 3:
          component.OnChangedToggle(UsageRateRanking.ViewInfoType.TowerMatch);
          break;
      }
      pinID = pinID <= 0 || pinID > 3 ? pinID : 0;
      if (pinID != 0)
        return;
      ((Behaviour) this).set_enabled(true);
      this.ExecRequest((WebAPI) new ReqRanking(UsageRateRanking.ViewInfo, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<RankingData[]> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<RankingData[]>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
      this.ActivateOutputLinks(100);
    }
  }
}
