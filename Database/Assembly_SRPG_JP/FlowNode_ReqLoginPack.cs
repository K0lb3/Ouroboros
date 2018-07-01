// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqLoginPack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using Gsc.App;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/ReqLoginPack", 32741)]
  public class FlowNode_ReqLoginPack : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqLoginPack(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), MonoSingleton<GameManager>.Instance.IsRelogin));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    private void reflectTrophyProgs(JSON_TrophyProgress[] trophy_progs)
    {
      if (trophy_progs == null)
        return;
      Dictionary<int, List<JSON_TrophyProgress>> progs = new Dictionary<int, List<JSON_TrophyProgress>>();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < trophy_progs.Length; ++index)
      {
        JSON_TrophyProgress trophyProg = trophy_progs[index];
        if (trophyProg != null)
        {
          TrophyParam trophy = instance.MasterParam.GetTrophy(trophyProg.iname);
          if (trophy == null)
          {
            DebugUtility.LogWarning("存在しないミッション:" + trophyProg.iname);
          }
          else
          {
            if (trophy.Objectives[0].type.IsExtraClear())
            {
              int type = (int) trophy.Objectives[0].type;
              if (!progs.ContainsKey(type))
                progs[type] = new List<JSON_TrophyProgress>();
              progs[type].Add(trophy_progs[index]);
            }
            instance.Player.RegistTrophyStateDictByProg(instance.MasterParam.GetTrophy(trophyProg.iname), trophyProg);
          }
        }
      }
      instance.Player.CreateInheritingExtraTrophy(progs);
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
          TrophyState trophyCounter = player.GetTrophyCounter(trophiesOfType[index].Param, false);
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
          instance.Player.ResetQuestChallenges();
          instance.ResetJigenQuests();
          if (!instance.Deserialize(jsonObject.body.quests))
          {
            this.OnFailed();
          }
          else
          {
            if (instance.IsRelogin)
            {
              instance.Player.DeleteTrophies(jsonObject.body.trophyprogs);
              instance.Player.DeleteTrophies(jsonObject.body.bingoprogs);
            }
            LoginNewsInfo.SetPubInfo(jsonObject.body.pubinfo);
            this.reflectTrophyProgs(jsonObject.body.trophyprogs);
            this.reflectTrophyProgs(jsonObject.body.bingoprogs);
            this.reflectLoginTrophyProgs();
            if (jsonObject.body.channel != 0)
              GlobalVars.CurrentChatChannel.Set(jsonObject.body.channel);
            if (jsonObject.body.support != 0L)
              GlobalVars.SelectedSupportUnitUniqueID.Set(jsonObject.body.support);
            if (!string.IsNullOrEmpty(jsonObject.body.device_id))
              BootLoader.GetAccountManager().SetDeviceId((string) null, jsonObject.body.device_id);
            if (jsonObject.body.is_pending == 1)
              FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "1");
            else
              FlowNode_Variable.Set("REDRAW_GACHA_PENDING", "0");
            FlowNode_Variable.Set("SHOW_CHAPTER", "0");
            Network.RemoveAPI();
            instance.Player.OnLogin();
            instance.IsRelogin = false;
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
      public string device_id;
      public int is_pending;
      public LoginNewsInfo.JSON_PubInfo pubinfo;
    }
  }
}
