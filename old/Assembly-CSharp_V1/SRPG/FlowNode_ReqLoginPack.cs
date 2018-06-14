// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqLoginPack
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqLoginPack", 32741)]
  public class FlowNode_ReqLoginPack : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqLoginPack(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    private void Success()
    {
      PlayerPrefs.SetString("LastLoginTime", DateTime.Now.ToString("O"));
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    private void reflectTrophyProgs(JSON_TrophyProgress[] trophy_progs)
    {
      if (trophy_progs == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index1 = 0; index1 < trophy_progs.Length; ++index1)
      {
        JSON_TrophyProgress trophyProg = trophy_progs[index1];
        if (trophyProg != null)
        {
          if (instance.MasterParam.GetTrophy(trophyProg.iname) == null)
          {
            DebugUtility.LogWarning("存在しないミッション:" + trophyProg.iname);
          }
          else
          {
            TrophyState trophyCounter = instance.Player.GetTrophyCounter(instance.MasterParam.GetTrophy(trophyProg.iname));
            for (int index2 = 0; index2 < trophyProg.pts.Length && index2 < trophyCounter.Count.Length; ++index2)
              trophyCounter.Count[index2] = trophyProg.pts[index2];
            trophyCounter.StartYMD = trophyProg.ymd;
            trophyCounter.IsEnded = trophyProg.rewarded_at != 0;
            if (trophyProg.rewarded_at != 0)
            {
              try
              {
                trophyCounter.RewardedAt = trophyProg.rewarded_at.FromYMD();
              }
              catch
              {
                trophyCounter.RewardedAt = DateTime.MinValue;
              }
            }
            else
              trophyCounter.RewardedAt = DateTime.MinValue;
          }
        }
      }
    }

    private void reflectLoginTrophyProgs()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      int loginCount = player.LoginCount;
      TrophyObjective[] trophiesOfType = instance.MasterParam.GetTrophiesOfType(TrophyConditionTypes.logincount);
      if (trophiesOfType == null)
        return;
      for (int index = 0; index < trophiesOfType.Length; ++index)
      {
        if (trophiesOfType[index] != null)
        {
          TrophyState trophyCounter = player.GetTrophyCounter(trophiesOfType[index].Param);
          if (trophyCounter != null && !(trophyCounter.Count == null | trophyCounter.Count.Length < 1))
          {
            trophyCounter.Count[0] = loginCount;
            trophyCounter.IsDirty = true;
          }
        }
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqLoginPack.JSON_ReqLoginPackResponse>>(www.text);
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          instance.ResetJigenQuests();
          if (!instance.Deserialize(jsonObject.body.quests))
          {
            this.OnFailed();
          }
          else
          {
            this.reflectTrophyProgs(jsonObject.body.trophyprogs);
            this.reflectTrophyProgs(jsonObject.body.bingoprogs);
            this.reflectLoginTrophyProgs();
            GlobalVars.CurrentChatChannel.Set(jsonObject.body.channel);
            GlobalVars.SelectedSupportUnitUniqueID.Set(jsonObject.body.support);
            Network.RemoveAPI();
            this.Success();
          }
        }
      }
    }

    public class JSON_ReqLoginPackResponse
    {
      public JSON_QuestProgress[] quests;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
      public Json_ChatChannelMasterParam[] channels;
      public int channel;
      public long support;
    }
  }
}
