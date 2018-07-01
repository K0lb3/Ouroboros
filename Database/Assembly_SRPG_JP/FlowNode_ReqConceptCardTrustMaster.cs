// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardTrustMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1000, "報酬受取完了", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.NodeType("System/ReqConceptCardTrustMaster", 32741)]
  [FlowNode.Pin(100, "報酬未受取のトラストマスター達成", FlowNode.PinTypes.Input, 100)]
  public class FlowNode_ReqConceptCardTrustMaster : FlowNode_Network
  {
    private const int INPUT_TRUSTMASTER_ON = 100;
    private const int OUTPUT_TRUSTMASTER_ON = 1000;
    private int mOutPutPinId;

    public override void OnActivate(int pinID)
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      if (pinID == 100)
      {
        this.ExecRequest((WebAPI) new ReqTrustMasterConceptCard((long) instance.SelectedConceptCardData.UniqueID, true, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.mOutPutPinId = 1000;
      }
      ((Behaviour) this).set_enabled(true);
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardTrustMaster.Json_ConceptCardTrustMaster> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardTrustMaster.Json_ConceptCardTrustMaster>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.concept_card);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(this.mOutPutPinId);
        ((Behaviour) this).set_enabled(false);
      }
    }

    public class Json_ConceptCardTrustMaster
    {
      public JSON_ConceptCard concept_card;
    }
  }
}
