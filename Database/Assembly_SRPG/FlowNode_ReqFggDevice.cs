// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqFggDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "RequestAdd", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Network/fgg_device", 32741)]
  [FlowNode.Pin(2, "Success", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "RequestRelease", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_ReqFggDevice : FlowNode_Network
  {
    public string HikkoshiFgGMailAddress;
    public string HikkoshiFgGPassword;

    private FlowNode_ReqFggDevice.API m_Api { get; set; }

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
        GameObject gameObject1 = GameObjectID.FindGameObject(this.HikkoshiFgGMailAddress);
        GameObject gameObject2 = GameObjectID.FindGameObject(this.HikkoshiFgGPassword);
        InputField component1;
        if (Object.op_Equality((Object) gameObject1, (Object) null) || Object.op_Equality((Object) (component1 = (InputField) gameObject1.GetComponent<InputField>()), (Object) null))
        {
          DebugUtility.LogError("InputField doesn't exist.");
        }
        else
        {
          InputField component2;
          if (Object.op_Equality((Object) gameObject2, (Object) null) || Object.op_Equality((Object) (component2 = (InputField) gameObject2.GetComponent<InputField>()), (Object) null))
          {
            DebugUtility.LogError("InputField doesn't exist.");
          }
          else
          {
            this.ExecRequest((WebAPI) new ReqGAuthFgGDevice(SystemInfo.get_deviceUniqueIdentifier(), component1.get_text(), component2.get_text(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            ((Behaviour) this).set_enabled(true);
            this.m_Api = FlowNode_ReqFggDevice.API.Add;
          }
        }
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
    }

    private void Failure(int pinID)
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(pinID);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.AcheiveMigrateIllcode:
          case Network.EErrCode.AcheiveMigrateLock:
            this.OnBack();
            break;
          default:
            if (errCode != Network.EErrCode.MigrateSameDev)
            {
              this.OnRetry();
              break;
            }
            goto case Network.EErrCode.AcheiveMigrateIllcode;
        }
      }
      else
      {
        if (this.m_Api == FlowNode_ReqFggDevice.API.Add)
        {
          WebAPI.JSON_BodyResponse<ReqGAuthFgGDevice.Json_FggDevice> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGAuthFgGDevice.Json_FggDevice>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnRetry();
            return;
          }
          MonoSingleton<GameManager>.Instance.SaveAuthWithKey(jsonObject.body.device_id, jsonObject.body.secret_key);
          MonoSingleton<GameManager>.Instance.InitAuth();
          GameUtility.ClearPreferences();
        }
        Network.RemoveAPI();
        this.Success();
      }
    }

    private enum API
    {
      Add,
      Release,
    }
  }
}
