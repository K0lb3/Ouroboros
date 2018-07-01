// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardRarityCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1000, "指定レア値以上", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "指定レア値より少ない", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.NodeType("UI/ConceptCardRarityCheck", 32741)]
  [FlowNode.Pin(10, "入力", FlowNode.PinTypes.Input, 10)]
  public class FlowNode_ConceptCardRarityCheck : FlowNode
  {
    private const int INPUT_CHECK = 10;
    private const int OUTPUT_HIGH = 1000;
    private const int OUTPUT_LOW = 1001;
    public int Rarity;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      instance.CostConceptCardRare = this.Rarity;
      using (List<ConceptCardData>.Enumerator enumerator = instance.SelectedMaterials.GetList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if ((int) enumerator.Current.Rarity + 1 >= this.Rarity)
          {
            this.ActivateOutputLinks(1000);
            return;
          }
        }
      }
      this.ActivateOutputLinks(1001);
    }
  }
}
