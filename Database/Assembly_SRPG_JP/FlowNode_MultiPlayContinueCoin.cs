// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayContinueCoin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayContinueCoin", 32741)]
  [FlowNode.Pin(0, "Revive", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "コインが足りない", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "Continue", FlowNode.PinTypes.Input, 3)]
  public class FlowNode_MultiPlayContinueCoin : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 3)
        return;
      SceneBattle instance = SceneBattle.Instance;
      int coin = (int) (pinID != 0 ? MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost : MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMulti);
      if (Object.op_Inequality((Object) instance, (Object) null) && instance.Battle != null && instance.Battle.IsMultiTower)
        coin = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMultiTower;
      if (MonoSingleton<GameManager>.Instance.Player.Coin < coin)
      {
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(2);
      }
      else if (Network.Mode == Network.EConnectMode.Offline)
      {
        if (!MonoSingleton<GameManager>.Instance.Player.DEBUG_CONSUME_COIN(coin))
        {
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(2);
        }
        else
          this.Success();
      }
      else
      {
        BattleCore.Record questRecord = SceneBattle.Instance.Battle.GetQuestRecord();
        this.ExecRequest((WebAPI) new ReqBtlComCont(SceneBattle.Instance.Battle.BtlID, questRecord, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true, SceneBattle.Instance.Battle.IsMultiTower));
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
      this.ActivateOutputLinks(3);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<BattleCore.Json_BattleCont> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_BattleCont>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnFailed();
        }
        else
        {
          GlobalVars.MultiPlayBattleCont = jsonObject.body;
          PlayerData.EDeserializeFlags flag = (PlayerData.EDeserializeFlags) (0 | 2);
          if (!MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player, flag))
          {
            this.OnFailed();
          }
          else
          {
            Network.RemoveAPI();
            SceneBattle instance = SceneBattle.Instance;
            int num = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMulti;
            if (Object.op_Inequality((Object) instance, (Object) null) && instance.Battle != null && instance.Battle.IsMultiTower)
              num = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCostMultiTower;
            MyMetaps.TrackSpendCoin("ContinueMultiQuest", num);
            this.Success();
          }
        }
      }
    }
  }
}
