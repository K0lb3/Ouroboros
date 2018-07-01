// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "装備", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "外す", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "装備した", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "外した", FlowNode.PinTypes.Output, 110)]
  [FlowNode.NodeType("System/ReqConceptCardSet", 32741)]
  public class FlowNode_ReqConceptCardSet : FlowNode_Network
  {
    private const int INPUT_CONCEPT_CARD_SET = 0;
    private const int INPUT_CONCEPT_CARD_UNSET = 10;
    private const int OUTPUT_CONCEPT_CARD_SET = 100;
    private const int OUTPUT_CONCEPT_CARD_UNSET = 110;
    private long mEquipCardId;
    private long mTargetUnitId;

    public void SetEquipParam(long equip_card_iid, long unit_iid)
    {
      this.ResetParam();
      this.mEquipCardId = equip_card_iid;
      this.mTargetUnitId = unit_iid;
    }

    public void SetReleaseParam(long equip_card_iid)
    {
      this.ResetParam();
      this.mEquipCardId = equip_card_iid;
    }

    private void ResetParam()
    {
      this.mEquipCardId = 0L;
      this.mTargetUnitId = 0L;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        ((Behaviour) this).set_enabled(true);
        this.ExecRequest((WebAPI) ReqSetConceptCard.CreateSet(this.mEquipCardId, this.mTargetUnitId, new Network.ResponseCallback(this.ConceptCardSetResponseCallback)));
      }
      else
      {
        if (pinID != 10)
          return;
        long card_iid = 0;
        ((Behaviour) this).set_enabled(true);
        this.ExecRequest((WebAPI) ReqSetConceptCard.CreateUnset(card_iid, new Network.ResponseCallback(this.ConceptCardUnsetResponseCallback)));
      }
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
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) UnitEnhanceV3.Instance, (UnityEngine.Object) null))
            UnitEnhanceV3.Instance.OnEquipConceptCardSelect();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        ((Behaviour) this).set_enabled(false);
      }
    }

    private void ConceptCardSetResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(100);
    }

    private void ConceptCardUnsetResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      this.ActivateOutputLinks(110);
    }
  }
}
