// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayGetRankMatchMissionProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "Failure", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(0, "Get", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("Multi/GetRankMatchMissionProgress", 32741)]
  [FlowNode.Pin(5000, "NoMatchVersion", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(6000, "MultiMaintenance", FlowNode.PinTypes.Output, 6000)]
  public class FlowNode_MultiPlayGetRankMatchMissionProgress : FlowNode_Network
  {
    private const int PIN_IN_RANKMATCH_START = 72;
    private const int PIN_IN_RANKMATCH_STATUS = 70;
    private const int PIN_IN_RANKMATCH_CREATE_ROOM = 71;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqRankMatchMission(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      DebugUtility.Log("MultiPlayAPI success");
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("MultiPlayAPI failure");
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(101);
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
          case Network.EErrCode.OutOfDateQuest:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4802);
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
      else
      {
        WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response> jsonBodyResponse = (WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response>>(www.text);
        DebugUtility.Assert(jsonBodyResponse != null, "res == null");
        if (jsonBodyResponse.body == null)
        {
          this.OnFailed();
        }
        else
        {
          if (jsonBodyResponse.body.missionprogs != null)
          {
            PlayerData player = MonoSingleton<GameManager>.Instance.Player;
            player.RankMatchMissionState.Clear();
            foreach (ReqRankMatchMission.MissionProgress missionprog in jsonBodyResponse.body.missionprogs)
            {
              RankMatchMissionState matchMissionState = new RankMatchMissionState();
              matchMissionState.Deserialize(missionprog.iname, missionprog.prog, missionprog.rewarded_at);
              player.RankMatchMissionState.Add(matchMissionState);
            }
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
