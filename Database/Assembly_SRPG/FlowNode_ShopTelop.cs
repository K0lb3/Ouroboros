// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ShopTelop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/ShopTelop", 32741)]
  [FlowNode.Pin(10, "SetText", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ShopTelop : FlowNode
  {
    public string ShopTelopGameObjectID = "ShopTelop";
    public string Text;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      GameObject gameObject = GameObjectID.FindGameObject(this.ShopTelopGameObjectID);
      ShopTelop shopTelop = !Object.op_Equality((Object) gameObject, (Object) null) ? (ShopTelop) gameObject.GetComponent<ShopTelop>() : (ShopTelop) null;
      if (Object.op_Inequality((Object) shopTelop, (Object) null))
        shopTelop.SetText(LocalizedText.Get(this.Text));
      this.ActivateOutputLinks(1);
    }
  }
}
