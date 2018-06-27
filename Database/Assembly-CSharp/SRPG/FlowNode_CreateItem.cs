// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CreateItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(3, "費用が足りない", FlowNode.PinTypes.Output, 3)]
  [FlowNode.NodeType("System/CreateItem", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "所持限界に達している", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(4, "素材が足りない", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(100, "ワンタップ合成", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_CreateItem : FlowNode_Network
  {
    private ItemParam mItemParam;

    public override void OnActivate(int pinID)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FlowNode_CreateItem.\u003COnActivate\u003Ec__AnonStorey2C8 activateCAnonStorey2C8 = new FlowNode_CreateItem.\u003COnActivate\u003Ec__AnonStorey2C8();
      // ISSUE: reference to a compiler-generated field
      activateCAnonStorey2C8.\u003C\u003Ef__this = this;
      if (pinID != 0 && pinID != 100)
        return;
      // ISSUE: reference to a compiler-generated field
      activateCAnonStorey2C8.player = MonoSingleton<GameManager>.Instance.Player;
      this.mItemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.SelectedCreateItemID);
      // ISSUE: reference to a compiler-generated field
      if (!activateCAnonStorey2C8.player.CheckItemCapacity(this.mItemParam, 1))
      {
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(2);
      }
      else if (pinID == 0)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        FlowNode_CreateItem.\u003COnActivate\u003Ec__AnonStorey2C9 activateCAnonStorey2C9 = new FlowNode_CreateItem.\u003COnActivate\u003Ec__AnonStorey2C9();
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey2C9.\u003C\u003Ef__ref\u0024712 = activateCAnonStorey2C8;
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey2C9.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        if (MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParam.recipe).cost > activateCAnonStorey2C8.player.Gold)
        {
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(3);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey2C9.result_type = activateCAnonStorey2C8.player.CheckCreateItem(this.mItemParam);
          // ISSUE: reference to a compiler-generated field
          if (activateCAnonStorey2C9.result_type == CreateItemResult.NotEnough)
          {
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (activateCAnonStorey2C9.result_type == CreateItemResult.CanCreateCommon)
            {
              int cost = 0;
              Dictionary<string, int> consumes = (Dictionary<string, int>) null;
              bool is_ikkatsu = false;
              NeedEquipItemList item_list = new NeedEquipItemList();
              MonoSingleton<GameManager>.GetInstanceDirect().Player.CheckEnableCreateItem(this.mItemParam, ref is_ikkatsu, ref cost, ref consumes, item_list);
              // ISSUE: reference to a compiler-generated method
              UIUtility.ConfirmBox(LocalizedText.Get("sys.COMMON_EQUIP_CHECK_MADE", new object[1]
              {
                (object) item_list.GetCommonItemListString()
              }), new UIUtility.DialogResultEvent(activateCAnonStorey2C9.\u003C\u003Em__2A1), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              this.CallApiNormal(activateCAnonStorey2C8.player, activateCAnonStorey2C9.result_type);
            }
          }
        }
      }
      else
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        FlowNode_CreateItem.\u003COnActivate\u003Ec__AnonStorey2CA activateCAnonStorey2Ca = new FlowNode_CreateItem.\u003COnActivate\u003Ec__AnonStorey2CA();
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey2Ca.\u003C\u003Ef__ref\u0024712 = activateCAnonStorey2C8;
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey2Ca.\u003C\u003Ef__this = this;
        int cost = 0;
        Dictionary<string, int> consumes = (Dictionary<string, int>) null;
        bool is_ikkatsu = false;
        // ISSUE: reference to a compiler-generated field
        activateCAnonStorey2Ca.need_euip_item = new NeedEquipItemList();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        bool flag = activateCAnonStorey2C8.player.CheckEnableCreateItem(this.mItemParam, ref is_ikkatsu, ref cost, ref consumes, activateCAnonStorey2Ca.need_euip_item);
        // ISSUE: reference to a compiler-generated field
        if (cost > activateCAnonStorey2C8.player.Gold)
        {
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(3);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (!flag && !activateCAnonStorey2Ca.need_euip_item.IsEnoughCommon())
          {
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (activateCAnonStorey2Ca.need_euip_item.IsEnoughCommon())
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated method
              UIUtility.ConfirmBox(LocalizedText.Get("sys.COMMON_EQUIP_CHECK_ONETAP", new object[1]
              {
                (object) activateCAnonStorey2Ca.need_euip_item.GetCommonItemListString()
              }), new UIUtility.DialogResultEvent(activateCAnonStorey2Ca.\u003C\u003Em__2A2), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              this.CallApi(activateCAnonStorey2Ca.need_euip_item, activateCAnonStorey2C8.player);
            }
          }
        }
      }
    }

    public void CallApi(NeedEquipItemList need_euip_item, PlayerData player)
    {
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqItemCompositAll(this.mItemParam.iname, need_euip_item.IsEnoughCommon(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        player.CreateItemAll(this.mItemParam);
    }

    public void CallApiNormal(PlayerData player, CreateItemResult result_type)
    {
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqItemComposit(this.mItemParam.iname, result_type == CreateItemResult.CanCreateCommon, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        player.CreateItem(this.mItemParam);
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
