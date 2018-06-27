// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayerCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/PlayerCheck", 32741)]
  [FlowNode.Pin(12, "Migrate", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "Banned", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(11, "Success", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_PlayerCheck : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MonoSingleton<GameManager>.Instance.IsDeviceId())
      {
        this.ExecRequest((WebAPI) new ReqPlayerCheck(MonoSingleton<GameManager>.Instance.DeviceId, MonoSingleton<GameManager>.Instance.SecretKey, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
      {
        Debug.LogError((object) "No Device ID!");
        this.ActivateOutputLinks(11);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoDevice:
          case Network.EErrCode.Authorize:
            this.OnFailed();
            return;
        }
      }
      WebAPI.JSON_BodyResponse<FlowNode_PlayerCheck.JSON_PlayerCheck> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_PlayerCheck.JSON_PlayerCheck>>(www.text);
      if (jsonObject.body == null)
      {
        this.OnFailed();
      }
      else
      {
        if (jsonObject.body.result != 1 && jsonObject.body.status != null)
        {
          GlobalVars.BanStatus = int.Parse(jsonObject.body.status);
          GlobalVars.CustomerID = jsonObject.body.cuid;
          Network.RemoveAPI();
          this.ActivateOutputLinks(13);
        }
        else
        {
          Network.RemoveAPI();
          if (jsonObject.body.old_device_id != null)
          {
            MonoSingleton<GameManager>.Instance.SaveAuth(jsonObject.body.old_device_id, jsonObject.body.secret_key);
            MonoSingleton<GameManager>.Instance.InitAuth();
            this.ActivateOutputLinks(12);
          }
          else
            this.ActivateOutputLinks(11);
        }
        ((Behaviour) this).set_enabled(false);
      }
    }

    private class JSON_PlayerCheck
    {
      public int result;
      public string old_device_id;
      public string secret_key;
      public string status;
      public string cuid;
    }
  }
}
