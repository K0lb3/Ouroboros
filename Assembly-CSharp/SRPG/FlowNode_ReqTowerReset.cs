// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTowerReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/ReqTowerReset", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqTowerReset : FlowNode_Network
  {
    private long rtime;
    private byte round;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqTowerReset(GlobalVars.SelectedTowerID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
      {
        GlobalVars.SelectedTowerID = "QE_TW_BABEL";
        this.Success();
      }
    }

    private void Success()
    {
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      ((Behaviour) this).set_enabled(false);
      JSON_ReqTowerResuponse json = new JSON_ReqTowerResuponse();
      json.is_reset = (byte) 0;
      json.round = this.round;
      json.rank = new JSON_ReqTowerResuponse.Json_RankStatus();
      if (json.rank != null)
      {
        json.rank.spd_rank = towerResuponse.speedRank;
        json.rank.tec_rank = towerResuponse.techRank;
        json.rank.spd_score = towerResuponse.spd_score;
        json.rank.tec_score = towerResuponse.tec_score;
        json.rank.ret_score = towerResuponse.ret_score;
        json.rank.rcv_score = towerResuponse.rcv_score;
      }
      MonoSingleton<GameManager>.Instance.Deserialize(json);
      MonoSingleton<GameManager>.Instance.TowerResuponse.rtime = this.rtime;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (TowerErrorHandle.Error((FlowNode_Network) this))
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      WebAPI.JSON_BodyResponse<FlowNode_ReqTowerReset.Json_ReqTowerReset> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqTowerReset.Json_ReqTowerReset>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      this.rtime = jsonObject.body.rtime;
      Json_PlayerData json = new Json_PlayerData();
      json.coin = new Json_Coin();
      json.coin.free = jsonObject.body.coin.free;
      json.coin.paid = jsonObject.body.coin.paid;
      json.coin.com = jsonObject.body.coin.com;
      this.round = jsonObject.body.round;
      if (jsonObject.body.rank != null)
      {
        towerResuponse.speedRank = jsonObject.body.rank.spd_rank;
        towerResuponse.techRank = jsonObject.body.rank.tec_rank;
        towerResuponse.spd_score = jsonObject.body.rank.spd_score;
        towerResuponse.tec_score = jsonObject.body.rank.tec_score;
        towerResuponse.ret_score = jsonObject.body.rank.ret_score;
        towerResuponse.rcv_score = jsonObject.body.rank.rcv_score;
      }
      MonoSingleton<GameManager>.Instance.Player.Deserialize(json, PlayerData.EDeserializeFlags.Coin);
      GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.GLOBAL_PLAYER_COIN);
      GlobalVars.SelectedQuestID = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(MonoSingleton<GameManager>.Instance.TowerResuponse.TowerID).iname;
      this.Success();
    }

    public class Json_ReqTowerReset
    {
      public long rtime;
      public byte round;
      public JSON_ReqTowerResuponse.Json_RankStatus rank;
      public JSON_ReqTowerResuponse.Json_UserCoin coin;
    }
  }
}
