// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Network/gacha", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "ToCheckPending(引き直し召喚チェック)", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_ReqGachaList : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OT_REQUEST_SUCCESS = 1;
    private const int PIN_OT_REQUEST_FAILURE = 2;
    private const int PIN_OT_TO_GACHA_PENDING = 3;

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
        this.ExecRequest((WebAPI) new ReqGacha(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
    }

    private void ToCheckPending()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(3);
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
        WebAPI.JSON_BodyResponse<Json_GachaList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
        {
          this.Failure();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          try
          {
            if (!instance.Deserialize(jsonObject.body))
            {
              this.Failure();
              return;
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.Failure();
            return;
          }
          GachaResultData.Reset();
          if (FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1")
            this.ToCheckPending();
          else
            this.Success();
        }
      }
    }
  }
}
