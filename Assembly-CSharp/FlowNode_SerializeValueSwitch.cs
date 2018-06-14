// Decompiled with JetBrains decompiler
// Type: FlowNode_SerializeValueSwitch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;

[FlowNode.NodeType("SerializeValue/Switch", 32741)]
[FlowNode.Pin(9999, "DEFAULT", FlowNode.PinTypes.Output, 9999)]
[FlowNode.Pin(100, "スイッチ分岐", FlowNode.PinTypes.Input, 100)]
public class FlowNode_SerializeValueSwitch : FlowNode
{
  public FlowNode_SerializeValueSwitch.Value m_Value = new FlowNode_SerializeValueSwitch.Value();
  public List<FlowNode_SerializeValueSwitch.Case> m_Case = new List<FlowNode_SerializeValueSwitch.Case>();
  public const int INPUT_OPERATOR = 100;
  public const int OUTPUT_TOP = 200;
  public const int OUTPUT_DEFAULT = 9999;
  public SerializeValue.PropertyType m_PropertyType;

  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    SerializeValue serializeValue = this.m_Value.Get();
    int pinID1 = -1;
    for (int index = 0; index < this.m_Case.Count; ++index)
    {
      FlowNode_SerializeValueSwitch.Case @case = this.m_Case[index];
      if (@case != null && @case.isValid)
      {
        SerializeValue src = @case.Get();
        if (serializeValue != null && src != null && serializeValue.Equal(this.m_PropertyType, src))
        {
          pinID1 = @case.GetPinId();
          break;
        }
      }
    }
    if (pinID1 == -1)
      this.ActivateOutputLinks(9999);
    else
      this.ActivateOutputLinks(pinID1);
  }

  public override FlowNode.Pin[] GetDynamicPins()
  {
    List<FlowNode.Pin> pinList = new List<FlowNode.Pin>();
    for (int index = 0; index < this.m_Case.Count; ++index)
    {
      if (this.m_Case[index].isValid)
      {
        FlowNode.Pin pin = this.m_Case[index].ToPin(this.m_PropertyType);
        if (pin != null)
          pinList.Add(pin);
      }
    }
    return pinList.ToArray();
  }

  public enum ValueType
  {
    Direct,
    RefObject,
    ButtonEventArg,
  }

  [Serializable]
  public class Value
  {
    public FlowNode_SerializeValueSwitch.ValueType m_Type;
    public SerializeValueBehaviour m_Object;
    public string m_Key;
    public SerializeValue m_Value;

    public Value()
    {
      this.m_Value = new SerializeValue();
    }

    public void Clear()
    {
      this.m_Object = (SerializeValueBehaviour) null;
      this.m_Key = (string) null;
      this.m_Value.Clear();
    }

    public SerializeValue Get()
    {
      if (this.m_Type == FlowNode_SerializeValueSwitch.ValueType.RefObject)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Object, (UnityEngine.Object) null))
          return this.m_Object.list.GetField(this.m_Key);
        return (SerializeValue) null;
      }
      if (this.m_Type == FlowNode_SerializeValueSwitch.ValueType.Direct)
        return this.m_Value;
      if (this.m_Type == FlowNode_SerializeValueSwitch.ValueType.ButtonEventArg)
      {
        SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
        if (currentValue != null)
          return currentValue.GetField(this.m_Key);
      }
      return (SerializeValue) null;
    }
  }

  [Serializable]
  public class Case
  {
    public int m_Pin;
    public FlowNode_SerializeValueSwitch.ValueType m_Type;
    public SerializeValueBehaviour m_Object;
    public string m_Key;
    public SerializeValue m_Value;

    public Case()
    {
      this.m_Pin = -1;
      this.m_Value = new SerializeValue();
    }

    public bool isValid
    {
      get
      {
        return this.m_Pin != -1;
      }
    }

    public void Clear()
    {
      this.m_Object = (SerializeValueBehaviour) null;
      this.m_Key = (string) null;
      this.m_Value.Clear();
    }

    public SerializeValue Get()
    {
      if (this.m_Type == FlowNode_SerializeValueSwitch.ValueType.RefObject)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Object, (UnityEngine.Object) null))
          return this.m_Object.list.GetField(this.m_Key);
        return (SerializeValue) null;
      }
      if (this.m_Type == FlowNode_SerializeValueSwitch.ValueType.Direct)
        return this.m_Value;
      if (this.m_Type == FlowNode_SerializeValueSwitch.ValueType.ButtonEventArg)
      {
        SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
        if (currentValue != null)
          return currentValue.GetField(this.m_Key);
      }
      return (SerializeValue) null;
    }

    public int GetPinId()
    {
      return 200 + this.m_Pin;
    }

    public FlowNode.Pin ToPin(SerializeValue.PropertyType propertyType)
    {
      SerializeValue serializeValue = this.Get();
      if (serializeValue != null)
        return new FlowNode.Pin(this.GetPinId(), serializeValue.GetPropertyName(propertyType), FlowNode.PinTypes.Output, this.GetPinId());
      return (FlowNode.Pin) null;
    }
  }
}
