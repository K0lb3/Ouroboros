// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetAccessToken
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/GetAccessToken", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "Banned", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "Migrate", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_GetAccessToken : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || !MonoSingleton<GameManager>.Instance.IsDeviceId())
        return;
      this.ExecRequest((WebAPI) new ReqGetAccessToken(MonoSingleton<GameManager>.Instance.DeviceId, MonoSingleton<GameManager>.Instance.SecretKey, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(true);
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
      WebAPI.JSON_BodyResponse<FlowNode_GetAccessToken.JSON_AccessToken> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_GetAccessToken.JSON_AccessToken>>(www.text);
      if (jsonObject.body == null)
      {
        this.OnFailed();
      }
      else
      {
        if (jsonObject.body.access_token == null && jsonObject.body.status != null)
        {
          GlobalVars.BanStatus = int.Parse(jsonObject.body.status);
          GlobalVars.CustomerID = jsonObject.body.cuid;
          Network.RemoveAPI();
          this.ActivateOutputLinks(3);
        }
        else
        {
          if (jsonObject.body.device_id != null)
            MonoSingleton<GameManager>.Instance.SaveAuth(jsonObject.body.device_id);
          Network.SessionID = jsonObject.body.access_token;
          Network.RemoveAPI();
          if (jsonObject.body.old_device_id != null)
          {
            MonoSingleton<GameManager>.Instance.SaveAuth(jsonObject.body.old_device_id, jsonObject.body.secret_key);
            MonoSingleton<GameManager>.Instance.InitAuth();
            this.ActivateOutputLinks(2);
          }
          else
            this.ActivateOutputLinks(1);
        }
        ((Behaviour) this).set_enabled(false);
      }
    }

    private class JSON_AccessToken
    {
      public string access_token;
      public int expires_in;
      public string device_id;
      public string old_device_id;
      public string secret_key;
      public string status;
      public string cuid;
    }
  }
}
