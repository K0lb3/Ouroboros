// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CreateItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "所持限界に達している", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/CreateItem", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "費用が足りない", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "素材が足りない", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(100, "ワンタップ合成", FlowNode.PinTypes.Input, 100)]
  public class FlowNode_CreateItem : FlowNode_Network
  {
    private ItemParam mItemParam;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 100)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.mItemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.SelectedCreateItemID);
      if (!player.CheckItemCapacity(this.mItemParam, 1))
      {
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(2);
      }
      else if (pinID == 0)
      {
        if (MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParam.recipe).cost > player.Gold)
        {
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(3);
        }
        else if (!player.CheckCreateItem(this.mItemParam))
        {
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(4);
        }
        else if (Network.Mode == Network.EConnectMode.Online)
        {
          this.ExecRequest((WebAPI) new ReqItemComposit(this.mItemParam.iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).set_enabled(true);
        }
        else
          player.CreateItem(this.mItemParam);
      }
      else
      {
        int cost = 0;
        Dictionary<string, int> consumes = (Dictionary<string, int>) null;
        bool is_ikkatsu = false;
        bool flag = player.CheckEnableCreateItem(this.mItemParam, ref is_ikkatsu, ref cost, ref consumes);
        if (cost > player.Gold)
        {
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(3);
        }
        else if (!flag)
        {
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(4);
        }
        else if (Network.Mode == Network.EConnectMode.Online)
        {
          this.ExecRequest((WebAPI) new ReqItemCompositAll(this.mItemParam.iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).set_enabled(true);
        }
        else
          player.CreateItemAll(this.mItemParam);
      }
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
          case Network.EErrCode.GouseiMaterialShort:
            this.OnBack();
            break;
          case Network.EErrCode.GouseiCostShort:
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
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          if (this.mItemParam != null)
            UIUtility.SystemMessage((string) null, string.Format(LocalizedText.Get("sys.UNIT_EQUIPMENT_CREATE_MESSAGE"), (object) this.mItemParam.name), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
