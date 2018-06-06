// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RegisterFacebook
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Network/RegisterFacebook", 32741)]
  [FlowNode.Pin(3, "Account Not Bound", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(2, "Account Bound", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_RegisterFacebook : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      string oauth2AccessToken = GlobalVars.OAuth2AccessToken;
      if (oauth2AccessToken == string.Empty)
      {
        this.ActivateOutputLinks(3);
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqRegisterDeviceToFacebook(MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.DeviceId, oauth2AccessToken, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ReqRegisterDeviceToFacebookResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ReqRegisterDeviceToFacebookResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          PlayerPrefs.SetInt("AccountLinked", 1);
          this.ActivateOutputLinks(2);
          ((Behaviour) this).set_enabled(false);
        }
      }
    }
  }
}
