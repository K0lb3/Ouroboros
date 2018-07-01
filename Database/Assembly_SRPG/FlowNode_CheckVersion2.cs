// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckVersion2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using Gsc.App;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/CheckVersion2", 32741)]
  [FlowNode.Pin(10, "Success Online", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Success Offline", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "Reset to Title", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(1000, "No Version", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(100, "Start Online", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_CheckVersion2 : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (BootLoader.BootStates == BootLoader.BootState.SUCCESS)
      {
        this.ExecRequest((WebAPI) new ReqCheckVersion2(Network.Version, new Network.ResponseCallback(this.CheckVersionResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
      {
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(10);
      }
    }

    public void CheckVersionResponseCallback(WWWResult www)
    {
      if (Network.IsNoVersion)
      {
        Network.RemoveAPI();
        Network.ResetError();
        this.ActivateOutputLinks(1000);
      }
      else
      {
        if (FlowNode_Network.HasCommonError(www))
          return;
        this.OnSuccess(www);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<FlowNode_CheckVersion2.Json_VersionInfo> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_CheckVersion2.Json_VersionInfo>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        if (jsonObject.body.environments == null || jsonObject.body.environments.alchemist == null)
          return;
        FlowNode_GsccInit.SettingAssets(jsonObject.body.environments.alchemist.assets, jsonObject.body.environments.alchemist.assets_ex);
        this.ActivateOutputLinks(10);
        ((Behaviour) this).set_enabled(false);
      }
    }

    private class Json_Alchemist
    {
      public string assets;
      public string assets_ex;
    }

    private class Json_Environment
    {
      public FlowNode_CheckVersion2.Json_Alchemist alchemist;
    }

    private class Json_VersionInfo
    {
      public FlowNode_CheckVersion2.Json_Environment environments;
    }
  }
}
