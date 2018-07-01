// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqFirstChargeBonus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failue", FlowNode.PinTypes.Output, 11)]
  [FlowNode.NodeType("Network/ReqFirstChargeBonus", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqFirstChargeBonus : FlowNode_Network
  {
    public const int PIN_IN_REQUEST = 0;
    public const int PIN_OT_SUCCESS = 10;
    public const int PIN_OT_FAILURE = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      this.ExecRequest((WebAPI) new ReqFirstChargeBonus(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(true);
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.FirstChargeInvalid:
            this.OnBack();
            break;
          case Network.EErrCode.FirstChargeNoLog:
            this.OnBack();
            break;
          case Network.EErrCode.FirstChargeReceipt:
            this.OnBack();
            break;
          case Network.EErrCode.FirstChargePast:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqFirstChargeBonus.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqFirstChargeBonus.Json>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
          return;
        ChargeInfoResultWindow component = (ChargeInfoResultWindow) ((Component) this).get_gameObject().GetComponent<ChargeInfoResultWindow>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          List<FirstChargeReward> firstChargeRewardList = new List<FirstChargeReward>();
          for (int index = 0; index < jsonObject.body.rewards.Length; ++index)
          {
            FirstChargeReward firstChargeReward = new FirstChargeReward(jsonObject.body.rewards[index]);
            if (firstChargeReward != null)
              firstChargeRewardList.Add(firstChargeReward);
          }
          if (firstChargeRewardList != null && firstChargeRewardList.Count > 0)
            component.SetUp(firstChargeRewardList.ToArray());
        }
        this.Success();
      }
    }

    [Serializable]
    public class Json
    {
      public FlowNode_ReqFirstChargeBonus.Reward[] rewards;
    }

    public class Reward
    {
      public string iname = string.Empty;
      public string type = string.Empty;
      public int num;

      public GiftTypes GetGiftType()
      {
        if (string.IsNullOrEmpty(this.type))
          return (GiftTypes) 0;
        long num = 0;
        if (this.type == "item")
          num |= 1L;
        else if (this.type == "unit")
          num |= 128L;
        else if (this.type == "artifact")
          num |= 64L;
        else if (this.type == "concept_card")
          num |= 4096L;
        else if (this.type == "coin")
          num |= 4L;
        else if (this.type == "gold")
          num |= 2L;
        return (GiftTypes) num;
      }
    }
  }
}
