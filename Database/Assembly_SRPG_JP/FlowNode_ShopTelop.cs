// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ShopTelop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "SetText", FlowNode.PinTypes.Input, 10)]
  [FlowNode.NodeType("SRPG/ShopTelop", 32741)]
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
