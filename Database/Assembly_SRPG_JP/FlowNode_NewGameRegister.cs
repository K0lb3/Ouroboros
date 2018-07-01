// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NewGameRegister
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Create New Account", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/NewGameRegister", 32741)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_NewGameRegister : FlowNode_Network
  {
    public static string gPassword = "DmmPassword";

    public static string gDeviceID { get; set; }

    public static string gEmail { get; set; }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MonoSingleton<GameManager>.Instance.InitAuth();
      this.ExecRequest((WebAPI) new ReqGetDeviceID(MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.UdId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(true);
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
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
        WebAPI.JSON_BodyResponse<FlowNode_NewGameRegister.JSON_DeviceID> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_NewGameRegister.JSON_DeviceID>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        FlowNode_NewGameRegister.gDeviceID = jsonObject.body.device_id;
        Network.RemoveAPI();
        this.Success();
      }
    }

    private class JSON_DeviceID
    {
      public string device_id;
    }
  }
}
