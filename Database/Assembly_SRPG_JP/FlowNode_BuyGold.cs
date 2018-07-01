// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BuyGold
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/BuyGold", 32741)]
  [FlowNode.Pin(3, "購入回数制限", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "コインが足りない", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_BuyGold : FlowNode_Network
  {
    public static GameObject ConfirmBoxObj;
    public bool Confirm;
    public bool ShowResult;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      if (this.Confirm)
      {
        FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
        FlowNode_BuyGold.ConfirmBoxObj = UIUtility.ConfirmBox(LocalizedText.Get("sys.BUYGOLD", (object) fixParam.BuyGoldCost, (object) fixParam.BuyGoldAmount, (object) MonoSingleton<GameManager>.Instance.Player.PaidCoin, (object) (MonoSingleton<GameManager>.Instance.Player.FreeCoin + MonoSingleton<GameManager>.Instance.Player.ComCoin)), new UIUtility.DialogResultEvent(this.OnBuy), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      }
      else
        this.SendRequest();
    }

    private void OutOfCoin()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.OUTOFFREECOIN", (object) fixParam.BuyGoldCost, (object) fixParam.BuyGoldAmount), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
    }

    private void OutOfBuyCount()
    {
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.GOLD_BUY_LIMIT"), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
    }

    private void OnBuy(GameObject go)
    {
      this.SendRequest();
    }

    private void SendRequest()
    {
      if (MonoSingleton<GameManager>.Instance.Player.FreeCoin + MonoSingleton<GameManager>.Instance.Player.ComCoin < this.getRequiredCoin())
      {
        if (this.ShowResult)
          this.OutOfCoin();
        this.ActivateOutputLinks(2);
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (instance.MasterParam.GetVipBuyGoldLimit(instance.Player.VipRank) <= instance.Player.GoldBuyNum)
        {
          if (this.ShowResult)
            this.OutOfBuyCount();
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(3);
        }
        else
        {
          int requiredCoin = this.getRequiredCoin();
          ((Behaviour) this).set_enabled(true);
          this.ExecRequest((WebAPI) new ReqBuyGold(requiredCoin, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
      }
    }

    private void Success()
    {
      MonoSingleton<GameManager>.Instance.Player.OnBuyGold();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      instance.OnAbilityRankUpCountChange(instance.Player.AbilityRankUpCountNum);
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
        switch (Network.ErrCode)
        {
          case Network.EErrCode.GoldBuyCostShort:
            if (this.ShowResult)
              this.OutOfCoin();
            this.OnBack();
            break;
          case Network.EErrCode.GoldBuyLimit:
            if (this.ShowResult)
              this.OutOfBuyCount();
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
          PlayerData.EDeserializeFlags flag = (PlayerData.EDeserializeFlags) (0 | 2 | 1);
          if (!MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player, flag))
          {
            this.OnRetry();
          }
          else
          {
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("BuyGold", this.getRequiredCoin());
            if (this.ShowResult)
            {
              FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
              UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.GOLDBOUGHT", (object) fixParam.BuyGoldCost, (object) fixParam.BuyGoldAmount), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
              MonoSingleton<GameManager>.Instance.Player.OnGoldChange((int) fixParam.BuyGoldAmount);
            }
            this.Success();
          }
        }
      }
    }

    private int getRequiredCoin()
    {
      return (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.BuyGoldCost;
    }
  }
}
