// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTobiraEnhance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "扉を強化する", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "扉を強化した", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("System/ReqTobiraEnhance", 32741)]
  public class FlowNode_ReqTobiraEnhance : FlowNode_Network
  {
    private const int INPUT_REQUEST = 0;
    private const int OUTPUT_REQUEST = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ((Behaviour) this).set_enabled(true);
      this.ExecRequest((WebAPI) new ReqTobiraEnhance((long) GlobalVars.SelectedUnitUniqueID, (TobiraParam.Category) GlobalVars.PreBattleUnitTobiraCategory, new Network.ResponseCallback(this.EnhanceTobiraCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.GLOBAL_PLAYER_GOLD);
        ((Behaviour) this).set_enabled(false);
      }
    }

    private void EnhanceTobiraCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(100);
    }
  }
}
