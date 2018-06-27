// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SellItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "RequestConvert", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("System/SellItem", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SellItem : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1 && pinID != 2 || ((Behaviour) this).get_enabled())
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        for (int index = 0; index < GlobalVars.SellItemList.Count; ++index)
        {
          SellItem sellItem = GlobalVars.SellItemList[index];
          player.GainGold(sellItem.item.Sell * sellItem.num);
          player.GainItem(sellItem.item.Param.iname, -sellItem.num);
          sellItem.num = 0;
          sellItem.index = -1;
          AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.Zeni, (long) (sellItem.item.Sell * sellItem.num), "Sell Item", (string) null);
          AnalyticsManager.TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType.Item, (long) sellItem.num, "Sell Item", sellItem.item.ItemID);
        }
        GlobalVars.SelectSellItem = (SellItem) null;
        GlobalVars.SellItemList.Clear();
        GlobalVars.SellItemList = (List<SellItem>) null;
        this.Success();
      }
      else
      {
        Dictionary<long, int> sells = new Dictionary<long, int>();
        List<SellItem> sellItemList = GlobalVars.SellItemList;
        for (int index = 0; index < sellItemList.Count; ++index)
        {
          long uniqueId = sellItemList[index].item.UniqueID;
          int num = sellItemList[index].num;
          sells[uniqueId] = num;
        }
        this.ExecRequest((WebAPI) new ReqItemSell(sells, pinID == 2, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoItemSell:
            this.OnBack();
            break;
          case Network.EErrCode.ConvertAnotherItem:
            if (GlobalVars.SellItemList != null)
              GlobalVars.SellItemList.Clear();
            this.OnFailed();
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
        try
        {
          if (jsonObject.body == null)
            throw new InvalidJSONException();
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
        }
        catch (Exception ex)
        {
          this.OnRetry(ex);
          return;
        }
        int delta = 0;
        if (GlobalVars.SellItemList != null)
        {
          for (int index = 0; index < GlobalVars.SellItemList.Count; ++index)
            delta += GlobalVars.SellItemList[index].item.Sell;
        }
        Network.RemoveAPI();
        GlobalVars.SellItemList.Clear();
        MonoSingleton<GameManager>.Instance.Player.OnGoldChange(delta);
        this.Success();
      }
    }
  }
}
