// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardNoSaleCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1001, "非売品は無い", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1000, "非売品が存在する", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(10, "入力", FlowNode.PinTypes.Input, 10)]
  [FlowNode.NodeType("UI/ConceptCardNoSaleCheck", 32741)]
  public class FlowNode_ConceptCardNoSaleCheck : FlowNode
  {
    private const int INPUT_CHECK = 10;
    private const int OUTPUT_EXIST = 1000;
    private const int OUTPUT_NO_EXIST = 1001;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      using (List<ConceptCardData>.Enumerator enumerator = instance.SelectedMaterials.GetList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Param.not_sale)
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
