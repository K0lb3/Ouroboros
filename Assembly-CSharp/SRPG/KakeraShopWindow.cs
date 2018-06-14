// Decompiled with JetBrains decompiler
// Type: SRPG.KakeraShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "魂の欠片に変換", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "完了", FlowNode.PinTypes.Output, 100)]
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
