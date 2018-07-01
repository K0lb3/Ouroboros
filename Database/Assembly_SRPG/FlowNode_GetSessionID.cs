// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetSessionID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/GetSessionID", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_GetSessionID : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MonoSingleton<GameManager>.Instance.InitAuth();
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
        if (Network.ErrCode == Network.EErrCode.SessionFailure)
          this.OnFailed();
        else
          this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_GetSessionID.JSON_DeviceID> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_GetSessionID.JSON_DeviceID>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        string deviceId = jsonObject.body.device_id;
        AnalyticsManager.TrackUserID(deviceId);
        MonoSingleton<GameManager>.Instance.SaveAuth(deviceId);
        Network.RemoveAPI();
        this.ActivateOutputLinks(1);
        ((Behaviour) this).set_enabled(false);
      }
    }

    private class JSON_DeviceID
    {
      public string device_id;
    }

    private class JSON_SessionID
    {
      public string sid;
    }
  }
}
