// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RegisterGreview
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Register Greview", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("System/RegisterGreview", 32741)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_RegisterGreview : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
        UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_GOOGLE_REVIEW"), new UIUtility.DialogResultEvent(this.OnSerialRegister), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else
        this.Success();
    }

    private void OnSerialRegister(GameObject go)
    {
      this.ExecRequest((WebAPI) new ReqGoogleReview(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(true);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.CountLimitForPlayer)
        {
          Network.RemoveAPI();
          Network.ResetError();
          this.Success();
        }
        else
          this.OnBack();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_GoogleReview> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GoogleReview>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.Failure();
          return;
        }
        this.Success();
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(11);
    }
  }
}
