// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqRankMatchReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/ReqRankMatchReward", 32741)]
  [FlowNode.Pin(5000, "NoMatchVersion", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(6000, "MultiMaintenance", FlowNode.PinTypes.Output, 6000)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Req", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqRankMatchReward : FlowNode_Network
  {
    private bool isChecked;
    private FlowNode_ReqRankMatchReward.ReqType mReqType;
    private int mToDay;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.isChecked)
        return;
      this.isChecked = true;
      this.mToDay = TimeManager.ServerTime.Year * 10000 + TimeManager.ServerTime.Month * 100 + TimeManager.ServerTime.Day;
      if (PlayerPrefsUtility.GetInt(PlayerPrefsUtility.RANKMATCH_SEASON_REWARD_RECEIVE_DATE, 0) >= this.mToDay)
      {
        this.ActivateOutputLinks(2);
      }
      else
      {
        this.mReqType = FlowNode_ReqRankMatchReward.ReqType.Reward;
        this.ExecRequest((WebAPI) new ReqRankMatchReward(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      DebugUtility.Log(nameof (OnSuccess));
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.MultiMaintenance:
          case Network.EErrCode.VsMaintenance:
          case Network.EErrCode.MultiVersionMaintenance:
          case Network.EErrCode.MultiTowerMaintenance:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(6000);
            break;
          case Network.EErrCode.MultiVersionMismatch:
          case Network.EErrCode.VS_Version:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(5000);
            break;
          default:
            this.OnFailed();
            break;
        }
      }
      else if (this.mReqType == FlowNode_ReqRankMatchReward.ReqType.Status)
      {
        WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnFailed();
        }
        else
        {
          Network.RemoveAPI();
          ((Behaviour) this).set_enabled(false);
          if (jsonObject.body.RankingStatus == ReqRankMatchStatus.RankingStatus.Rewarding)
          {
            this.mReqType = FlowNode_ReqRankMatchReward.ReqType.Reward;
            this.ExecRequest((WebAPI) new ReqRankMatchReward(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          }
          else
            this.ActivateOutputLinks(2);
        }
      }
      else
      {
        if (this.mReqType != FlowNode_ReqRankMatchReward.ReqType.Reward)
          return;
        WebAPI.JSON_BodyResponse<ReqRankMatchReward.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchReward.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnFailed();
        }
        else
        {
          Network.RemoveAPI();
          ((Behaviour) this).set_enabled(false);
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.RANKMATCH_SEASON_REWARD_RECEIVE_DATE, this.mToDay, false);
          if (jsonObject.body.rank == 0)
          {
            this.ActivateOutputLinks(2);
          }
          else
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            instance.Player.mRankMatchSeasonResult.Deserialize(jsonObject.body);
            GlobalVars.RankMatchSeasonReward.Clear();
            GlobalVars.RankMatchSeasonReward.Add(instance.GetVersusRankClassRewardList(jsonObject.body.reward.ranking));
            GlobalVars.RankMatchSeasonReward.Add(instance.GetVersusRankClassRewardList(jsonObject.body.reward.type));
            this.ActivateOutputLinks(1);
          }
        }
      }
    }

    private enum ReqType
    {
      Status,
      Reward,
    }
  }
}
