// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTobiraUnlock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "扉機能を解放する", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqTobiraUnlock")]
  [FlowNode.Pin(100, "扉機能を解放した", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_ReqTobiraUnlock : FlowNode_Network
  {
    public const int INPUT_REQUEST = 0;
    public const int OUTPUT_REQUEST = 100;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ((Behaviour) this).set_enabled(true);
      this.ExecRequest((WebAPI) new ReqTobiraUnlock((long) GlobalVars.SelectedUnitUniqueID, new Network.ResponseCallback(this.TobiraUnlockCallback)));
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
        ((Behaviour) this).set_enabled(false);
      }
    }

    private void TobiraUnlockCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(100);
    }
  }
}
