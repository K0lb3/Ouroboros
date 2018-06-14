// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AbilityPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "石が足りない", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/アビリティポイント回復", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_AbilityPoint : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.Coin < this.getRequiredCoin())
        this.ActivateOutputLinks(10);
      else if (Network.Mode == Network.EConnectMode.Offline)
      {
        ((Behaviour) this).set_enabled(false);
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        player.DEBUG_CONSUME_COIN(this.getRequiredCoin());
        player.RestoreAbilityRankUpCount();
        this.Success();
      }
      else
      {
        ((Behaviour) this).set_enabled(true);
        this.ExecRequest((WebAPI) new ReqItemAbilPointPaid(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        if (Network.ErrCode == Network.EErrCode.AbilityCoinShort)
          this.OnBack();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          AnalyticsManager.TrackOriginalCurrencyUse(ESaleType.Coin, this.getRequiredCoin(), "AbilityPoint");
          this.Success();
        }
      }
    }

    private int getRequiredCoin()
    {
      return (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.AbilityRankUpCountCoin;
    }
  }
}
