// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqArtifactSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqArtifactSet", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqArtifactSet : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      long selectedJobUniqueId = (long) GlobalVars.SelectedJobUniqueID;
      long artifactUniqueId = (long) GlobalVars.SelectedArtifactUniqueID;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        if (selectedJobUniqueId < 1L || artifactUniqueId < 1L)
        {
          ((Behaviour) this).set_enabled(false);
          this.Success();
        }
        else
        {
          ((Behaviour) this).set_enabled(true);
          this.ExecRequest((WebAPI) new ReqArtifactSet(selectedJobUniqueId, artifactUniqueId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, false);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
