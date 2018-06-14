// Decompiled with JetBrains decompiler
// Type: FlowNode_GUIGacha
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("")]
[FlowNode.NodeType("Gacha/GUI", 32741)]
public class FlowNode_GUIGacha : FlowNode_GUI
{
  protected override void OnInstanceCreate()
  {
    this.Instance.get_transform().SetParent(this.Instance.get_transform().get_root(), false);
    this.mListener = GameUtility.RequireComponent<DestroyEventListener>(this.mInstance);
    this.mListener.Listeners += new DestroyEventListener.DestroyEvent(((FlowNode_GUI) this).OnInstanceDestroyTrigger);
  }
}
