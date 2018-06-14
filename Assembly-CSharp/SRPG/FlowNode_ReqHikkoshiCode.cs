// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqHikkoshiCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("Network/gauth_passcode", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqHikkoshiCode : FlowNode_Network
  {
    public Text HikkoshiCodeText;
    public Text ExpireTimeText;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqGAuthPasscode(MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.DeviceId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
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
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_PassCode> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_PassCode>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HikkoshiCodeText, (UnityEngine.Object) null))
            this.HikkoshiCodeText.set_text(jsonObject.body.passcode);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpireTimeText, (UnityEngine.Object) null))
          {
            DateTime dateTime = DateTime.Now.AddSeconds((double) jsonObject.body.expires_in);
            this.ExpireTimeText.set_text(string.Format(LocalizedText.Get("sys.HIKKOSHICODE_EXPIRETIME"), (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute, (object) dateTime.Second));
          }
          this.Success();
        }
      }
    }
  }
}
