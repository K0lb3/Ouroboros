// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SummonCoinConvertWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/SummonCoinConvertWindow")]
  public class FlowNode_SummonCoinConvertWindow : FlowNode_GUI
  {
    [SerializeField]
    private GachaCoinChangeWindow.CoinType coinType;

    protected override void OnCreatePinActive()
    {
      base.OnCreatePinActive();
      if (!Object.op_Inequality((Object) this.Instance, (Object) null))
        return;
      GachaCoinChangeWindow componentInChildren = (GachaCoinChangeWindow) this.Instance.GetComponentInChildren<GachaCoinChangeWindow>(true);
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.Refresh(this.coinType);
    }
  }
}
