// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqColo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Network/btl_colo", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqColo : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        this.ExecRequest((WebAPI) new ReqBtlColo(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
    }

    private void Success()
    {
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
        WebAPI.JSON_BodyResponse<Json_ArenaPlayers> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaPlayers>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          ((Behaviour) this).set_enabled(false);
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (!instance.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            Network.RemoveAPI();
            instance.Player.UpdateArenaRankTrophyStates(-1, -1);
            this.Success();
          }
        }
      }
    }
  }
}
