// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetJob
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/SetJob", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SetJob : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        long selectedUnitUniqueId = (long) GlobalVars.SelectedUnitUniqueID;
        JobData currentJob = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(selectedUnitUniqueId).CurrentJob;
        if (currentJob == null || !currentJob.IsActivated)
        {
          this.Success();
        }
        else
        {
          this.ExecRequest((WebAPI) new ReqUnitJob(selectedUnitUniqueId, currentJob.UniqueID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).set_enabled(true);
        }
      }
      else
        this.Success();
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
          case Network.EErrCode.NoJobSetJob:
          case Network.EErrCode.CantSelectJob:
          case Network.EErrCode.NoUnitSetJob:
            this.OnFailed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        try
        {
          if (jsonObject.body == null)
            throw new InvalidJSONException();
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
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
}
