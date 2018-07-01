// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "一覧取得した", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("System/ReqConceptCard", 32741)]
  [FlowNode.Pin(0, "一覧取得", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqConceptCard : FlowNode_Network
  {
    private const int INPUT_CONCEPT_CARD_GET = 0;
    private const int OUTPUT_CONCEPT_CARD_GOT = 100;
    private bool mIsDataOverride;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (GlobalVars.IsDirtyConceptCardData.Get())
      {
        ((Behaviour) this).set_enabled(true);
        long last_card_iid = 0;
        this.mIsDataOverride = true;
        List<ConceptCardData> conceptCards = MonoSingleton<GameManager>.Instance.Player.ConceptCards;
        if (conceptCards != null)
        {
          for (int index = 0; index < conceptCards.Count; ++index)
          {
            if (last_card_iid < (long) conceptCards[index].UniqueID)
            {
              last_card_iid = (long) conceptCards[index].UniqueID;
              this.mIsDataOverride = false;
            }
          }
        }
        this.ExecRequest((WebAPI) new ReqGetConceptCard(last_card_iid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
        this.ActivateOutputLinks(100);
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCard.Json_ConceptCardList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCard.Json_ConceptCardList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.cards, this.mIsDataOverride);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.materials, this.mIsDataOverride);
          GlobalVars.IsDirtyConceptCardData.Set(false);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.StartCoroutine(this.DownloadAssetsAndOutputPin());
      }
    }

    [DebuggerHidden]
    private IEnumerator DownloadAssetsAndOutputPin()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ReqConceptCard.\u003CDownloadAssetsAndOutputPin\u003Ec__IteratorC3()
      {
        \u003C\u003Ef__this = this
      };
    }

    public class Json_ConceptCardList
    {
      public JSON_ConceptCard[] cards;
      public JSON_ConceptCardMaterial[] materials;
    }
  }
}
