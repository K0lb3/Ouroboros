// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TowerRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(200, "Finish", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "Error", FlowNode.PinTypes.Output, 201)]
  [FlowNode.NodeType("System/TowerRank", 32741)]
  [FlowNode.Pin(100, "Request", FlowNode.PinTypes.Input, 100)]
  public class FlowNode_TowerRanking : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      ((Behaviour) this).set_enabled(true);
      this.ExecRequest((WebAPI) new ReqTowerRank(GlobalVars.SelectedTowerID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(200);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(201);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (TowerErrorHandle.Error((FlowNode_Network) this))
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      WebAPI.JSON_BodyResponse<ReqTowerRank.JSON_TowerRankResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqTowerRank.JSON_TowerRankResponse>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      towerResuponse.Deserialize(jsonObject.body);
      this.Success();
    }
  }
}
