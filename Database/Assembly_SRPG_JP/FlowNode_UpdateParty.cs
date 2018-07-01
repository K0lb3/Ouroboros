// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1802, "NoMatchVersion", FlowNode.PinTypes.Output, 1802)]
  [FlowNode.Pin(1803, "MultiMaintenance", FlowNode.PinTypes.Output, 1803)]
  [FlowNode.NodeType("System/UpdateParty", 32741)]
  [FlowNode.Pin(1800, "NoUnit", FlowNode.PinTypes.Output, 1800)]
  [FlowNode.Pin(1801, "Illegal", FlowNode.PinTypes.Output, 1801)]
  public class FlowNode_UpdateParty : FlowNode_Network
  {
    public bool SetCurrent = true;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      player.AutoSetLeaderUnit();
      if (this.SetCurrent)
        player.SetPartyCurrentIndex((int) GlobalVars.SelectedPartyIndex);
      PartyData partyCurrent = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
      for (int index1 = 0; index1 < player.Partys.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < partyCurrent.MAX_UNIT; ++index2)
        {
          if (player.Partys[index1].GetUnitUniqueID(index2) != 0L)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          this.ActivateOutputLinks(1800);
          return;
        }
      }
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        bool needUpdateMultiRoom = false;
        bool needUpdateMultiRoomMT = false;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        GameUtility.EScene currentScene = GameUtility.GetCurrentScene();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        {
          if (currentScene == GameUtility.EScene.HOME_MULTI || instance.IsResume())
            needUpdateMultiRoom = instance.IsOldestPlayer();
          else if (currentScene == GameUtility.EScene.MULTI_TOWER || instance.IsResume())
            needUpdateMultiRoomMT = instance.IsOldestPlayer();
        }
        ((Behaviour) this).set_enabled(true);
        this.ExecRequest((WebAPI) new ReqParty(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), needUpdateMultiRoom, true, needUpdateMultiRoomMT));
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
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
            this.ActivateOutputLinks(1803);
            return;
          case Network.EErrCode.NoUnitParty:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(1800);
            return;
          case Network.EErrCode.IllegalParty:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(1801);
            return;
          case Network.EErrCode.MultiVersionMismatch:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(1802);
            return;
        }
      }
      WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      try
      {
        if (jsonObject.body == null)
          throw new InvalidJSONException();
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
        MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.parties);
      }
      catch (Exception ex)
      {
        this.OnRetry(ex);
        return;
      }
      Network.RemoveAPI();
      this.Success();
    }
  }
}
