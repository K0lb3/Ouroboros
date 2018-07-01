// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTowerFloorRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "ランキングを取得完了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("Request/Tower/Floor/Ranking", 32741)]
  [FlowNode.Pin(1, "この階層のランキングを取得", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_ReqTowerFloorRanking : FlowNode_Network
  {
    private const int INPUT_REQUEST_RANKING = 1;
    private const int OUTPUT_REQUEST_RANKING = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      ((Behaviour) this).set_enabled(true);
      this.ExecRequest((WebAPI) new ReqTowerFloorRanking(GlobalVars.SelectedTowerID, GlobalVars.SelectedQuestID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (TowerErrorHandle.Error((FlowNode_Network) this))
        return;
      WebAPI.JSON_BodyResponse<ReqTowerFloorRanking.Json_Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqTowerFloorRanking.Json_Response>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      try
      {
        MonoSingleton<GameManager>.Instance.TowerResuponse.OnFloorRanking(jsonObject.body);
        this.ActivateOutputLinks(100);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return;
      }
      ((Behaviour) this).set_enabled(false);
    }
  }
}
