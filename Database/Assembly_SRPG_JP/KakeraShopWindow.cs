// Decompiled with JetBrains decompiler
// Type: SRPG.KakeraShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "完了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(10, "魂の欠片に変換", FlowNode.PinTypes.Input, 10)]
  public class KakeraShopWindow : MonoBehaviour, IFlowInterface
  {
    public KakeraShopWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.OnConvert();
    }

    private void OnConvert()
    {
      if (GlobalVars.ConvertAwakePieceList == null)
        GlobalVars.ConvertAwakePieceList = new List<SellItem>();
      else
        GlobalVars.ConvertAwakePieceList.Clear();
      GlobalVars.ConvertAwakePieceList.AddRange((IEnumerable<SellItem>) GlobalVars.SellItemList);
      GlobalVars.SellItemList.Clear();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
