// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetLimitedShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "IsLimitedShop", FlowNode.PinTypes.Input, 2)]
  [FlowNode.NodeType("System/SetLimitedShopList", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SetLimitedShopList : FlowNode_Network
  {
    private int inputPin = 1;
    public LimitedShopList limited_shop_list;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 && pinID != 2 || ((Behaviour) this).get_enabled())
        return;
      this.inputPin = pinID;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqLimitedShopList(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
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
        WebAPI.JSON_BodyResponse<JSON_ShopListArray> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ShopListArray>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body.shops == null || jsonObject.body.shops.Length <= 0)
        {
          MonoSingleton<GameManager>.Instance.LimitedShopEndAt = 0L;
          MonoSingleton<GameManager>.Instance.LimitedShopList = (JSON_ShopListArray.Shops[]) null;
        }
        else
        {
          for (int index = 0; index < jsonObject.body.shops.Length; ++index)
          {
            if (jsonObject.body.shops[index] == null)
            {
              this.OnRetry();
              return;
            }
          }
          MonoSingleton<GameManager>.Instance.LimitedShopEndAt = this.GetLimitedShopPeriodEndAt(jsonObject.body);
          if (this.inputPin != 2)
            this.limited_shop_list.SetLimitedShopList(jsonObject.body.shops);
          MonoSingleton<GameManager>.Instance.LimitedShopList = jsonObject.body.shops;
        }
        this.EraseTimeOutPopupHistory(jsonObject.body.shops);
        this.Success();
      }
    }

    public void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    public long GetLimitedShopPeriodEndAt(JSON_ShopListArray list)
    {
      long num = 0;
      if (list != null && list.shops.Length > 0)
      {
        for (int index = 0; index < list.shops.Length; ++index)
        {
          long end = list.shops[index].end;
          num = num >= end ? num : end;
        }
      }
      return num;
    }

    private void EraseTimeOutPopupHistory(JSON_ShopListArray.Shops[] shops)
    {
      if (shops == null || shops.Length <= 0)
      {
        string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray());
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, json, false);
      }
      else
      {
        if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT))
          return;
        ShopTimeOutItemInfoArray outItemInfoArray = (ShopTimeOutItemInfoArray) JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, string.Empty));
        List<ShopTimeOutItemInfo> shopTimeOutItemInfoList = new List<ShopTimeOutItemInfo>();
        if (outItemInfoArray.Infos != null)
        {
          foreach (ShopTimeOutItemInfo info1 in outItemInfoArray.Infos)
          {
            ShopTimeOutItemInfo info = info1;
            if (((IEnumerable<JSON_ShopListArray.Shops>) shops).Any<JSON_ShopListArray.Shops>((Func<JSON_ShopListArray.Shops, bool>) (sh => sh.gname == info.ShopId)))
              shopTimeOutItemInfoList.Add(info);
          }
        }
        string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray(shopTimeOutItemInfoList.ToArray()));
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, json, false);
      }
    }
  }
}
