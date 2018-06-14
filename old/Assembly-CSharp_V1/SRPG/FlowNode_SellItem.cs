// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SellItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SellItem", 32741)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SellItem : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).get_enabled())
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
          AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.Zeni, AnalyticsManager.CurrencySubType.FREE, (long) (sellItem.item.Sell * sellItem.num), "Sell Item", (Dictionary<string, object>) null);
          AnalyticsManager.TrackCurrencyUse(AnalyticsManager.CurrencyType.Item, AnalyticsManager.CurrencySubType.FREE, (long) sellItem.num, "Sell Item", new Dictionary<string, object>()
          {
            {
              "item_id",
              (object) sellItem.item.ItemID
            }
          });
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
        this.ExecRequest((WebAPI) new ReqItemSell(sells, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        if (Network.ErrCode == Network.EErrCode.NoItemSell)
          this.OnBack();
        else
          this.OnRetry();
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
        Network.RemoveAPI();
        GlobalVars.SellItemList.Clear();
        this.Success();
      }
    }
  }
}
