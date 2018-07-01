// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqArtifactAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqArtifactAdd", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqArtifactAdd : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string selectedArtifactIname = (string) GlobalVars.SelectedArtifactIname;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        if (string.IsNullOrEmpty(selectedArtifactIname))
        {
          ((Behaviour) this).set_enabled(false);
          this.Success();
        }
        else
        {
          ((Behaviour) this).set_enabled(true);
          this.ExecRequest((WebAPI) new ReqArtifactAdd(selectedArtifactIname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), (string) null, (string) null));
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
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ArtifactBoxLimit:
            this.OnBack();
            break;
          case Network.EErrCode.ArtifactPieceShort:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
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
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
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
