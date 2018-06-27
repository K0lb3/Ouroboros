// Decompiled with JetBrains decompiler
// Type: FlowNode_GUIEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[FlowNode.NodeType("GUIEX", 32741)]
[FlowNode.Pin(210, "Closed", FlowNode.PinTypes.Output, 210)]
[FlowNode.Pin(200, "Opened", FlowNode.PinTypes.Output, 200)]
[AddComponentMenu("")]
public class FlowNode_GUIEx : FlowNode_GUI
{
  public SerializeValueList m_ValueList = new SerializeValueList();
  public bool m_ButtonEventArg;
  public FlowNode_GUIEx.ValueType m_Type;
  public SerializeValueBehaviour m_Object;

  protected override void OnInstanceCreate()
  {
    base.OnInstanceCreate();
    if (!Object.op_Inequality((Object) this.mInstance, (Object) null))
      return;
    SerializeValueBehaviour serializeValueBehaviour = (SerializeValueBehaviour) this.mInstance.GetComponent<SerializeValueBehaviour>();
    if (Object.op_Equality((Object) serializeValueBehaviour, (Object) null))
    {
      for (int index = 0; index < this.mInstance.get_transform().get_childCount(); ++index)
      {
        Transform child = this.mInstance.get_transform().GetChild(index);
        if (Object.op_Inequality((Object) child, (Object) null))
        {
          serializeValueBehaviour = (SerializeValueBehaviour) ((Component) child).GetComponent<SerializeValueBehaviour>();
          if (Object.op_Inequality((Object) serializeValueBehaviour, (Object) null))
            break;
        }
      }
      if (Object.op_Equality((Object) serializeValueBehaviour, (Object) null))
        serializeValueBehaviour = (SerializeValueBehaviour) this.mInstance.GetComponentInChildren<SerializeValueBehaviour>();
    }
    if (!Object.op_Inequality((Object) serializeValueBehaviour, (Object) null))
      return;
    SerializeValueList src = (SerializeValueList) null;
    if (this.m_Type == FlowNode_GUIEx.ValueType.Direct)
      src = this.m_ValueList;
    else if (this.m_Type == FlowNode_GUIEx.ValueType.RefObject && Object.op_Inequality((Object) this.m_Object, (Object) null))
      src = this.m_Object.list;
    if (src == null)
      return;
    if (this.m_ButtonEventArg)
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
        src.Write(currentValue);
    }
    serializeValueBehaviour.list.Write(src);
  }

  public enum ValueType
  {
    Direct,
    RefObject,
  }
}
