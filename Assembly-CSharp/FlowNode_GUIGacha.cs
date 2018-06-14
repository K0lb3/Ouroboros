// Decompiled with JetBrains decompiler
// Type: FlowNode_GUIGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Gacha/GUI", 32741)]
[AddComponentMenu("")]
public class FlowNode_GUIGacha : FlowNode_GUI
{
  protected override void OnInstanceCreate()
  {
    this.Instance.get_transform().SetParent(this.Instance.get_transform().get_root(), false);
    this.mListener = GameUtility.RequireComponent<DestroyEventListener>(this.mInstance);
    this.mListener.Listeners += new DestroyEventListener.DestroyEvent(((FlowNode_GUI) this).OnInstanceDestroyTrigger);
  }
}
