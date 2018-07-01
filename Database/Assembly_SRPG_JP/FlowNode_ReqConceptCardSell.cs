// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardSell
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "売却した", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("System/ReqConceptCardSell", 32741)]
  [FlowNode.Pin(0, "売却する", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqConceptCardSell : FlowNode_Network
  {
    private const int INPUT_CONCEPT_CARD_SELL = 0;
    private const int OUTPUT_CONCEPT_CARD_SOLD = 100;
    private long[] sellCardIDs;
    private int totalSellZeny;

    public void SetSellParam()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      ConceptCardManager.GalcTotalSellZeny(instance.SelectedMaterials, out this.totalSellZeny);
      this.sellCardIDs = instance.SelectedMaterials.GetUniqueIDs().ToArray();
      instance.SelectedMaterials.Clear();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.SetSellParam();
      ((Behaviour) this).set_enabled(true);
      this.ExecRequest((WebAPI) new ReqSellConceptCard(this.sellCardIDs, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardSell.Json_ConceptCardSell> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardSell.Json_ConceptCardSell>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Player.RemoveConceptCardData(jsonObject.body.sell_ids);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        MonoSingleton<GameManager>.Instance.Player.OnGoldChange(this.totalSellZeny);
        this.ActivateOutputLinks(100);
        ((Behaviour) this).set_enabled(false);
      }
    }

    public class Json_ConceptCardSell
    {
      public Json_PlayerData player;
      public long[] sell_ids;
    }
  }
}
