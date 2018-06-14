// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ExecGacha2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(20, "Multi Gacha", FlowNode.PinTypes.Input, 20)]
  [FlowNode.NodeType("System/ExecGacha2", 32741)]
  [FlowNode.Pin(7, "幻晶石不足", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(6, "ゼニー不足", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(5, "Failed", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(4, "Success", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(40, "Comp Gacha", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(30, "Free Gacha", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(10, "Single Gacha", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(8, "有償幻晶石不足", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(9, "有償召喚リセット待ち", FlowNode.PinTypes.Output, 9)]
  public class FlowNode_ExecGacha2 : FlowNode_Network
  {
    private FlowNode_ExecGacha2.GachaCostType mCurrentCostType = FlowNode_ExecGacha2.GachaCostType.gold;
    public List<string> DownloadUnits = new List<string>();
    public List<string> DownloadArtifacts = new List<string>();
    private GachaTypes mCurrentGachaType;
    private int mCurrentNum;
    private List<AssetList.Item> mQueue;

    public void OnExecGacha(string iname, string input, int cost, string type, int is_free = 0, string ticket = "", int num = 0, bool isPaid = false)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.mCurrentCostType = FlowNode_ExecGacha2.GachaCostType.none;
      if (type == "coin")
        this.mCurrentCostType = FlowNode_ExecGacha2.GachaCostType.coin;
      else if (type == "gold")
        this.mCurrentCostType = FlowNode_ExecGacha2.GachaCostType.gold;
      else if (type == nameof (ticket))
        this.mCurrentCostType = FlowNode_ExecGacha2.GachaCostType.ticket;
      bool flag = false;
      if (type != "gold")
        this.mCurrentGachaType = GachaTypes.Rare;
      this.mCurrentNum = num;
      if (isPaid && player.PaidCoin < cost)
      {
        this.ActivateOutputLinks(8);
        ((Behaviour) this).set_enabled(false);
      }
      else if (input == "single")
      {
        if (is_free == 0)
        {
          if (this.mCurrentCostType == FlowNode_ExecGacha2.GachaCostType.gold)
          {
            if (player.Gold < cost)
            {
              this.ActivateOutputLinks(6);
              ((Behaviour) this).set_enabled(false);
            }
            else
              flag = true;
          }
          else if (this.mCurrentCostType == FlowNode_ExecGacha2.GachaCostType.coin || this.mCurrentCostType == FlowNode_ExecGacha2.GachaCostType.none)
          {
            if (player.Coin < cost)
            {
              this.ActivateOutputLinks(7);
              ((Behaviour) this).set_enabled(false);
            }
            else
              flag = true;
          }
        }
        else
          flag = true;
        if (!flag)
          return;
        this.ExecGacha(iname, is_free, 0);
        if (type == "coin")
        {
          if (isPaid)
            AnalyticsManager.TrackPaidPremiumCurrencyObtain((long) cost, "Summons");
          else
            AnalyticsManager.TrackFreePremiumCurrencyObtain((long) cost, "Summons");
        }
        else
        {
          if (!(type == "gold"))
            return;
          AnalyticsManager.TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType.Zeni, (long) cost, "Summons", (string) null);
        }
      }
      else if (input == "multiple")
      {
        if (is_free == 0)
        {
          if (this.mCurrentCostType == FlowNode_ExecGacha2.GachaCostType.gold)
          {
            if (player.Gold < cost)
            {
              this.ActivateOutputLinks(6);
              ((Behaviour) this).set_enabled(false);
            }
            else
              flag = true;
          }
          else if (this.mCurrentCostType == FlowNode_ExecGacha2.GachaCostType.coin || this.mCurrentCostType == FlowNode_ExecGacha2.GachaCostType.none)
          {
            if (player.Coin < cost)
            {
              this.ActivateOutputLinks(7);
              ((Behaviour) this).set_enabled(false);
            }
            else
              flag = true;
          }
        }
        else
          flag = true;
        if (!flag)
          return;
        this.ExecGacha(iname, is_free, 0);
        if (type == "coin")
        {
          if (isPaid)
            AnalyticsManager.TrackPaidPremiumCurrencyUse((long) cost, "Summons");
          else
            AnalyticsManager.TrackFreePremiumCurrencyObtain((long) cost, "Summons");
        }
        else
        {
          if (!(type == "gold"))
            return;
          AnalyticsManager.TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType.Zeni, (long) cost, "Summons", (string) null);
        }
      }
      else if (input == "charge")
      {
        if (player.PaidCoin < cost)
        {
          this.ActivateOutputLinks(8);
          ((Behaviour) this).set_enabled(false);
        }
        else
          flag = true;
        if (!flag)
          return;
        this.ExecGacha(iname, is_free, 0);
      }
      else if (input == nameof (ticket))
      {
        if (!true)
          return;
        AnalyticsManager.TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType.SummonTicket, (long) num, "Summon Gate", ticket);
        this.ExecGacha(iname, is_free, num);
      }
      else
        this.Failure();
    }

    private void OnClickYes(GameObject dialog)
    {
      this.Success();
    }

    private void ShowResultDialog(string result)
    {
      UIUtility.SystemMessage("獲得", result, new UIUtility.DialogResultEvent(this.OnClickYes), (GameObject) null, false, -1);
    }

    private void ExecGacha(string iname, int is_free = 0, int num = 0)
    {
      this.ExecRequest((WebAPI) new ReqGachaExec(iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), is_free, num));
      ((Behaviour) this).set_enabled(true);
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(4);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(5);
    }

    private void PaidGachaLimitOver()
    {
      ((Behaviour) this).set_enabled(false);
      Network.RemoveAPI();
      Network.ResetError();
      this.ActivateOutputLinks(9);
    }

    private string MakeResultString(Json_DropInfo[] drops)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      UnitParam[] allUnits = masterParam.GetAllUnits();
      string str = string.Empty;
      foreach (Json_DropInfo drop in drops)
      {
        ItemParam itemParam = masterParam.GetItemParam(drop.iname);
        if (itemParam != null)
        {
          str = str + itemParam.name + "\n";
        }
        else
        {
          foreach (UnitParam unitParam in allUnits)
          {
            if (unitParam.iname == drop.iname)
            {
              str = str + unitParam.name + "\n";
              break;
            }
          }
        }
      }
      return str;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoGacha:
            this.OnFailed();
            break;
          case Network.EErrCode.GachaCostShort:
            this.OnBack();
            break;
          case Network.EErrCode.GachaItemMax:
            this.OnBack();
            break;
          case Network.EErrCode.GachaPaidLimitOver:
            this.PaidGachaLimitOver();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_GachaResult> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaResult>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.Failure();
          return;
        }
        List<GachaDropData> gachaDropDataList = new List<GachaDropData>();
        foreach (Json_DropInfo json in jsonObject.body.add)
        {
          GachaDropData gachaDropData = new GachaDropData();
          if (gachaDropData.Deserialize(json))
            gachaDropDataList.Add(gachaDropData);
        }
        List<GachaDropData> a_dropMails = new List<GachaDropData>();
        if (jsonObject.body.addMail != null)
        {
          foreach (Json_DropInfo json in jsonObject.body.addMail)
          {
            GachaDropData gachaDropData = new GachaDropData();
            if (gachaDropData.Deserialize(json))
              a_dropMails.Add(gachaDropData);
          }
        }
        GachaReceiptData a_receipt = new GachaReceiptData();
        a_receipt.Deserialize(jsonObject.body.receipt);
        GachaResultData.Init(gachaDropDataList, a_dropMails, a_receipt, false);
        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(a_receipt.type);
        AnalyticsManager.TrackSummonComplete();
        MonoSingleton<GameManager>.Instance.Player.OnGacha(this.mCurrentGachaType, this.mCurrentNum);
        this.StartCoroutine(this.AsyncGachaResultData(gachaDropDataList));
      }
    }

    [DebuggerHidden]
    private IEnumerator AsyncGachaResultData(List<GachaDropData> drops)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ExecGacha2.\u003CAsyncGachaResultData\u003Ec__IteratorC4() { drops = drops, \u003C\u0024\u003Edrops = drops, \u003C\u003Ef__this = this };
    }

    public override void OnActivate(int pinID)
    {
    }

    private enum GachaCostType
    {
      none,
      gold,
      coin,
      ticket,
    }
  }
}
