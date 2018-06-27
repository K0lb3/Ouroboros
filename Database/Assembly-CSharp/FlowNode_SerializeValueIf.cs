// Decompiled with JetBrains decompiler
// Type: FlowNode_SerializeValueIf
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;

[FlowNode.Pin(110, "真", FlowNode.PinTypes.Output, 110)]
[FlowNode.NodeType("SerializeValue/If", 32741)]
[FlowNode.Pin(100, "条件分岐", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(120, "偽", FlowNode.PinTypes.Output, 120)]
public class FlowNode_SerializeValueIf : FlowNode
{
  public const int INPUT_OPERATOR = 100;
  public const int OUTPUT_SUCCESS = 110;
  public const int OUTPUT_FAILURE = 120;
  public FlowNode_SerializeValueIf.Value m_Value1;
  public FlowNode_SerializeValueIf.Value m_Value2;
  public SerializeValue.PropertyType m_PropertyType;
  public FlowNode_SerializeValueIf.Operator m_Operator;

  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    SerializeValue serializeValue = this.m_Value1.Get();
    SerializeValue src = this.m_Value2.Get();
    bool flag = false;
    if (serializeValue != null && src != null)
    {
      switch (this.m_Operator)
      {
        case FlowNode_SerializeValueIf.Operator.Equal:
          flag = serializeValue.Equal(this.m_PropertyType, src);
          break;
        case FlowNode_SerializeValueIf.Operator.NotEqual:
          flag = !serializeValue.Equal(this.m_PropertyType, src);
          break;
        case FlowNode_SerializeValueIf.Operator.Greater:
          flag = serializeValue.Greater(this.m_PropertyType, src);
          break;
        case FlowNode_SerializeValueIf.Operator.EqualGreater:
          flag = serializeValue.EqualGreater(this.m_PropertyType, src);
          break;
        case FlowNode_SerializeValueIf.Operator.Less:
          flag = serializeValue.Less(this.m_PropertyType, src);
          break;
        case FlowNode_SerializeValueIf.Operator.EqualLess:
          flag = serializeValue.EqualLess(this.m_PropertyType, src);
          break;
      }
    }
    this.ActivateOutputLinks(!flag ? 120 : 110);
  }

  public enum ValueType
  {
    Direct,
    RefObject,
    ButtonEventArg,
  }

  public enum Operator
  {
    Equal,
    NotEqual,
    Greater,
    EqualGreater,
    Less,
    EqualLess,
  }

  [Serializable]
  public class Value
  {
    public FlowNode_SerializeValueIf.ValueType m_Type;
    public SerializeValueBehaviour m_Object;
    public string m_Key;
    public SerializeValue m_Value;

    public void Clear()
    {
      this.m_Object = (SerializeValueBehaviour) null;
      this.m_Key = (string) null;
      this.m_Value.Clear();
    }

    public SerializeValue Get()
    {
      if (this.m_Type == FlowNode_SerializeValueIf.ValueType.RefObject)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Object, (UnityEngine.Object) null))
          return this.m_Object.list.GetField(this.m_Key);
        return (SerializeValue) null;
      }
      if (this.m_Type == FlowNode_SerializeValueIf.ValueType.Direct)
        return this.m_Value;
      if (this.m_Type == FlowNode_SerializeValueIf.ValueType.ButtonEventArg)
      {
        SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
        if (currentValue != null)
          return currentValue.GetField(this.m_Key);
      }
      return (SerializeValue) null;
    }
  }
}
