// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BuyCoin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Buy", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/BuyCoin", 32741)]
  public class FlowNode_BuyCoin : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        ((Behaviour) this).set_enabled(true);
        this.ExecRequest((WebAPI) new ReqProductBuy("p_10_coin", "test_receipt", "test_transactionid", new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
      {
        MonoSingleton<GameManager>.Instance.Player.DEBUG_ADD_COIN(0, 10, 0);
        PunMonoSingleton<MyPhoton>.Instance.EnableKeepAlive(true);
        ((Behaviour) this).set_enabled(false);
        this.Success();
      }
    }

    private void Success()
    {
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      PunMonoSingleton<MyPhoton>.Instance.EnableKeepAlive(true);
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (jsonObject.body == null)
      {
        this.OnFailed();
      }
      else
      {
        PlayerData.EDeserializeFlags flag = PlayerData.EDeserializeFlags.Coin;
        if (!MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player, flag))
        {
          this.OnFailed();
        }
        else
        {
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
