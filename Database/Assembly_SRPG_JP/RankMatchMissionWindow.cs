// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMissionWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Receive", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  public class RankMatchMissionWindow : SRPG_ListBase, IFlowInterface
  {
    [SerializeField]
    private RankMatchMissionItem ListItem;
    private RankMatchMissionWindow.APIType mAPIType;

    protected override void Start()
    {
      base.Start();
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    public void ReceiveReward(VersusRankMissionParam mission)
    {
      GameManager gm = MonoSingleton<GameManager>.Instance;
      gm.Player.RewardedRankMatchMission(mission.IName);
      RewardData param = new RewardData();
      gm.GetVersusRankClassRewardList(mission.RewardId).ForEach((Action<VersusRankReward>) (reward =>
      {
        switch (reward.Type)
        {
          case RewardType.Item:
            ItemParam itemParam = gm.GetItemParam(reward.IName);
            if (itemParam == null)
              break;
            ItemData itemData = new ItemData();
            if (!itemData.Setup(0L, itemParam.iname, reward.Num))
              break;
            param.Items.Add(itemData);
            break;
          case RewardType.Gold:
            param.Gold = reward.Num;
            break;
          case RewardType.Coin:
            param.Coin = reward.Num;
            break;
          case RewardType.Artifact:
            ArtifactParam artifactParam = gm.MasterParam.GetArtifactParam(reward.IName);
            if (artifactParam == null)
              break;
            param.Artifacts.Add(new ArtifactRewardData()
            {
              ArtifactParam = artifactParam,
              Num = 1
            });
            break;
          case RewardType.Unit:
            if (gm.GetUnitParam(reward.IName) == null)
              break;
            param.GiftRecieveItemDataDic.Add(reward.IName, new GiftRecieveItemData()
            {
              iname = reward.IName,
              num = 1,
              type = GiftTypes.Unit
            });
            break;
        }
      }));
      GlobalVars.LastReward.Set(param);
      this.mAPIType = RankMatchMissionWindow.APIType.MISSION_EXEC;
      Network.RequestAPI((WebAPI) new ReqRankMatchMissionExec(mission.IName, new Network.ResponseCallback(this.ResponseCallback)), false);
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        return;
      this.ClearItems();
      ((Component) this.ListItem).get_gameObject().SetActive(false);
      this.mAPIType = RankMatchMissionWindow.APIType.MISSION;
      Network.RequestAPI((WebAPI) new ReqRankMatchMission(new Network.ResponseCallback(this.ResponseCallback)), false);
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.MultiMaintenance:
          case Network.EErrCode.VsMaintenance:
          case Network.EErrCode.MultiVersionMaintenance:
          case Network.EErrCode.MultiTowerMaintenance:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            break;
          default:
            if (errCode != Network.EErrCode.OutOfDateQuest)
            {
              if (errCode == Network.EErrCode.MultiVersionMismatch || errCode == Network.EErrCode.VS_Version)
              {
                Network.RemoveAPI();
                Network.ResetError();
                ((Behaviour) this).set_enabled(false);
                break;
              }
              FlowNode_Network.Retry();
              break;
            }
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            break;
        }
      }
      else
      {
        if (this.mAPIType == RankMatchMissionWindow.APIType.MISSION)
        {
          Dictionary<string, ReqRankMatchMission.MissionProgress> mission_progs = new Dictionary<string, ReqRankMatchMission.MissionProgress>();
          WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response> jsonBodyResponse = (WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchMission.Response>>(www.text);
          DebugUtility.Assert(jsonBodyResponse != null, "res == null");
          if (jsonBodyResponse.body != null && jsonBodyResponse.body.missionprogs != null)
          {
            PlayerData player = MonoSingleton<GameManager>.Instance.Player;
            player.RankMatchMissionState.Clear();
            foreach (ReqRankMatchMission.MissionProgress missionprog in jsonBodyResponse.body.missionprogs)
            {
              RankMatchMissionState matchMissionState = new RankMatchMissionState();
              matchMissionState.Deserialize(missionprog.iname, missionprog.prog, missionprog.rewarded_at);
              player.RankMatchMissionState.Add(matchMissionState);
              mission_progs.Add(missionprog.iname, missionprog);
            }
          }
          GameManager instance = MonoSingleton<GameManager>.Instance;
          List<VersusRankMissionParam> versusRankMissionList = instance.GetVersusRankMissionList(instance.RankMatchScheduleId);
          versusRankMissionList.Sort((Comparison<VersusRankMissionParam>) ((m1, m2) =>
          {
            int num1 = 0;
            int num2 = 0;
            if (mission_progs.ContainsKey(m1.IName))
            {
              if (string.IsNullOrEmpty(mission_progs[m1.IName].rewarded_at))
              {
                if (mission_progs[m1.IName].prog >= m1.IVal)
                  num1 = 1;
              }
              else
                num1 = -1;
            }
            if (mission_progs.ContainsKey(m2.IName))
            {
              if (string.IsNullOrEmpty(mission_progs[m2.IName].rewarded_at))
              {
                if (mission_progs[m2.IName].prog >= m2.IVal)
                  num2 = 1;
              }
              else
                num2 = -1;
            }
            if (num1 != num2)
              return num2 - num1;
            return m1.IName.CompareTo(m2.IName);
          }));
          for (int index = 0; index < versusRankMissionList.Count; ++index)
          {
            VersusRankMissionParam data = versusRankMissionList[index];
            if (!mission_progs.ContainsKey(data.IName) || string.IsNullOrEmpty(mission_progs[data.IName].rewarded_at))
            {
              RankMatchMissionItem matchMissionItem = (RankMatchMissionItem) UnityEngine.Object.Instantiate<RankMatchMissionItem>((M0) this.ListItem);
              DataSource.Bind<VersusRankMissionParam>(((Component) matchMissionItem).get_gameObject(), data);
              if (mission_progs.ContainsKey(data.IName))
                DataSource.Bind<ReqRankMatchMission.MissionProgress>(((Component) matchMissionItem).get_gameObject(), mission_progs[data.IName]);
              this.AddItem((ListItemEvents) matchMissionItem);
              ((Component) matchMissionItem).get_transform().SetParent(((Component) this).get_transform(), false);
              ((Component) matchMissionItem).get_gameObject().SetActive(true);
              matchMissionItem.Initialize(this);
            }
          }
        }
        else if (this.mAPIType == RankMatchMissionWindow.APIType.MISSION_EXEC)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
        Network.RemoveAPI();
      }
    }

    private enum APIType
    {
      MISSION,
      MISSION_EXEC,
    }
  }
}
