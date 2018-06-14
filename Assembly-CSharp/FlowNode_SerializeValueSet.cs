// Decompiled with JetBrains decompiler
// Type: FlowNode_SerializeValueSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;

[FlowNode.Pin(110, "次へ", FlowNode.PinTypes.Output, 110)]
[FlowNode.Pin(100, "設定", FlowNode.PinTypes.Input, 100)]
[FlowNode.NodeType("SerializeValue/Set", 32741)]
public class FlowNode_SerializeValueSet : FlowNode
{
  public List<FlowNode_SerializeValueSet.Value> m_Values = new List<FlowNode_SerializeValueSet.Value>();
  public const int INPUT_SET = 100;
  public const int OUTPUT_SETED = 110;
  public FlowNode_SerializeValueSet.ValueType m_Type;
  public SerializeValueBehaviour m_Object;

  protected override void Awake()
  {
    base.Awake();
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    if (this.m_Type == FlowNode_SerializeValueSet.ValueType.Global)
    {
      for (int index = 0; index < this.m_Values.Count; ++index)
      {
        SerializeValue serializeValue = new SerializeValue(SerializeValue.Type.Global, (string) null, this.m_Values[index].m_Key);
        if (serializeValue != null)
          serializeValue.Write(this.m_Values[index].m_PropertyType, this.m_Values[index].m_Value);
      }
    }
    else if (this.m_Type == FlowNode_SerializeValueSet.ValueType.RefObject)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Object, (UnityEngine.Object) null))
      {
        for (int index = 0; index < this.m_Values.Count; ++index)
        {
          SerializeValue field = this.m_Object.list.GetField(this.m_Values[index].m_Key);
          if (field != null)
            field.Write(this.m_Values[index].m_PropertyType, this.m_Values[index].m_Value);
        }
      }
    }
    else if (this.m_Type == FlowNode_SerializeValueSet.ValueType.ButtonEventArg)
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
      {
        for (int index = 0; index < this.m_Values.Count; ++index)
        {
          SerializeValue field = currentValue.GetField(this.m_Values[index].m_Key);
          if (field != null)
            field.Write(this.m_Values[index].m_PropertyType, this.m_Values[index].m_Value);
        }
      }
    }
    this.ActivateOutputLinks(110);
  }

  public enum ValueType
  {
    RefObject,
    Global,
    ButtonEventArg,
  }

  [Serializable]
  public class Value
  {
    public string m_Key = string.Empty;
    public SerializeValue m_Value = new SerializeValue();
    public SerializeValue.PropertyType m_PropertyType;
  }
}
