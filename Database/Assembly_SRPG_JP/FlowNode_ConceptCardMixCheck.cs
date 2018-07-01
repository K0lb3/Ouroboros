// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ConceptCardMixCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(11, "一括強化による合成可能かチェック", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(1001, "ゼニー不足", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.NodeType("UI/ConceptCardMixCheck", 32741)]
  [FlowNode.Pin(10, "合成可能かチェック", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1000, "合成可能", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ConceptCardMixCheck : FlowNode
  {
    private const int INPUT_MIX_CHECK = 10;
    private const int INPUT_BULK_MIX_CHECK = 11;
    private const int OUTPUT_MIX_OK = 1000;
    private const int OUTPUT_MIX_ZENY_NG = 1001;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10 && pinID != 11)
        return;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      int totalMixZeny = 0;
      if (pinID == 10)
        ConceptCardManager.GalcTotalMixZeny(instance.SelectedMaterials, out totalMixZeny);
      else
        ConceptCardManager.GalcTotalMixZenyMaterialData(out totalMixZeny);
      if (totalMixZeny > MonoSingleton<GameManager>.Instance.Player.Gold)
        this.ActivateOutputLinks(1001);
      else
        this.ActivateOutputLinks(1000);
    }
  }
}
