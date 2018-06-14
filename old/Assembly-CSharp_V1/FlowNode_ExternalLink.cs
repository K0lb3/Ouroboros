// Decompiled with JetBrains decompiler
// Type: FlowNode_ExternalLink
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

[FlowNode.NodeType("ExternalLink", 32741)]
public class FlowNode_ExternalLink : FlowNode
{
  [SerializeField]
  [HideInInspector]
  private FlowNode_ExternalLink.PinData[] mPins = new FlowNode_ExternalLink.PinData[0];
  public GameObject Target;
  protected GameObject mInstance;
  public string InstanceName;
  public bool NoAutoDestruct;

  public GameObject Instance
  {
    get
    {
      return this.mInstance;
    }
  }

  protected virtual bool ShouldCreateInstanceOnStart
  {
    get
    {
      return true;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    if (!this.ShouldCreateInstanceOnStart)
      return;
    ((Behaviour) this).set_enabled(true);
  }

  protected void CreateInstance()
  {
    ((Behaviour) this).set_enabled(false);
    this.DestroyInstance();
    if (Object.op_Equality((Object) this.Target, (Object) null))
      return;
    this.mInstance = (GameObject) Object.Instantiate<GameObject>((M0) this.Target);
    this.mInstance.get_transform().SetParent(((Component) this).get_transform(), false);
    RectTransform component1 = (RectTransform) this.Target.GetComponent<RectTransform>();
    if (Object.op_Inequality((Object) component1, (Object) null) && Object.op_Inequality((Object) this.Target.GetComponent<Canvas>(), (Object) null))
    {
      RectTransform component2 = (RectTransform) this.mInstance.GetComponent<RectTransform>();
      component2.set_anchorMax(component1.get_anchorMax());
      component2.set_anchorMin(component1.get_anchorMin());
      component2.set_anchoredPosition(component1.get_anchoredPosition());
      component2.set_sizeDelta(component1.get_sizeDelta());
    }
    if (!string.IsNullOrEmpty(this.InstanceName))
      ((Object) this.mInstance).set_name(this.InstanceName);
    this.BindPins();
    this.OnInstanceCreate();
  }

  protected void BindPins()
  {
    FlowNode[] components = (FlowNode[]) this.mInstance.GetComponents<FlowNode>();
    for (int index = 0; index < components.Length; ++index)
    {
      if (components[index] is FlowNode_Output)
      {
        FlowNode_Output flowNodeOutput = components[index] as FlowNode_Output;
        int embeddedPin = this.FindEmbeddedPin(flowNodeOutput.PinName);
        if (embeddedPin >= 0 && this.mPins[embeddedPin].PinType == FlowNode.PinTypes.Output)
        {
          this.mPins[embeddedPin].NodeBinding = components[index];
          flowNodeOutput.TargetNode = this;
          flowNodeOutput.TargetPinID = this.mPins[embeddedPin].PinID;
        }
      }
      else if (components[index] is FlowNode_Input)
      {
        int embeddedPin = this.FindEmbeddedPin((components[index] as FlowNode_Input).PinName);
        if (embeddedPin >= 0 && this.mPins[embeddedPin].PinType == FlowNode.PinTypes.Input)
          this.mPins[embeddedPin].NodeBinding = components[index];
      }
    }
  }

  protected void DestroyInstance()
  {
    if (Object.op_Equality((Object) this.mInstance, (Object) null))
      return;
    for (int index = 0; index < this.mPins.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.mPins[index].NodeBinding, (Object) null))
      {
        if (this.mPins[index].PinType == FlowNode.PinTypes.Output)
        {
          FlowNode_Output nodeBinding = this.mPins[index].NodeBinding as FlowNode_Output;
          nodeBinding.TargetNode = (FlowNode_ExternalLink) null;
          nodeBinding.TargetPinID = -1;
        }
        this.mPins[index].NodeBinding = (FlowNode) null;
      }
    }
    Object.Destroy((Object) this.mInstance);
    this.mInstance = (GameObject) null;
    this.OnInstanceDestroy();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (this.NoAutoDestruct)
      return;
    this.DestroyInstance();
  }

  public override FlowNode.Pin[] GetDynamicPins()
  {
    FlowNode.Pin[] pinArray = new FlowNode.Pin[this.mPins.Length];
    for (int index = 0; index < this.mPins.Length; ++index)
      pinArray[index] = new FlowNode.Pin(this.mPins[index].PinID, this.mPins[index].PinName, this.mPins[index].PinType, 1000 + index);
    return pinArray;
  }

  protected virtual void Start()
  {
    this.CreateInstance();
  }

  protected virtual void OnInstanceCreate()
  {
  }

  protected virtual void OnInstanceDestroy()
  {
  }

  public override void OnActivate(int pinID)
  {
    for (int index = 0; index < this.mPins.Length; ++index)
    {
      if (this.mPins[index].PinID == pinID && this.mPins[index].PinType == FlowNode.PinTypes.Input && Object.op_Inequality((Object) this.mPins[index].NodeBinding, (Object) null))
        this.mPins[index].NodeBinding.ActivateOutputLinks(1);
    }
  }

  public void RefreshPins()
  {
    if (Object.op_Equality((Object) this.Target, (Object) null))
      return;
    FlowNode[] components = (FlowNode[]) this.Target.GetComponents<FlowNode>();
    for (int index = 0; index < components.Length; ++index)
    {
      if (components[index] is FlowNode_Output)
      {
        FlowNode_Output flowNodeOutput = components[index] as FlowNode_Output;
        if (this.FindEmbeddedPin(flowNodeOutput.PinName) < 0)
        {
          Array.Resize<FlowNode_ExternalLink.PinData>(ref this.mPins, this.mPins.Length + 1);
          this.mPins[this.mPins.Length - 1].PinID = this.GenerateUniquePinID(this.Pins);
          this.mPins[this.mPins.Length - 1].PinName = flowNodeOutput.PinName;
          this.mPins[this.mPins.Length - 1].PinType = FlowNode.PinTypes.Output;
        }
      }
      else if (components[index] is FlowNode_Input)
      {
        FlowNode_Input flowNodeInput = components[index] as FlowNode_Input;
        if (this.FindEmbeddedPin(flowNodeInput.PinName) < 0)
        {
          Array.Resize<FlowNode_ExternalLink.PinData>(ref this.mPins, this.mPins.Length + 1);
          this.mPins[this.mPins.Length - 1].PinID = this.GenerateUniquePinID(this.Pins);
          this.mPins[this.mPins.Length - 1].PinName = flowNodeInput.PinName;
          this.mPins[this.mPins.Length - 1].PinType = FlowNode.PinTypes.Input;
        }
      }
    }
  }

  private int GenerateUniquePinID(FlowNode.Pin[] originalPins)
  {
    FlowNode.Pin[] pinArray = originalPins;
    int num;
    do
    {
      num = Random.Range(1, (int) ushort.MaxValue);
      for (int index = 0; index < pinArray.Length; ++index)
      {
        if (pinArray[index].PinID == num)
        {
          num = -1;
          break;
        }
      }
    }
    while (num < 0);
    return num;
  }

  private int FindEmbeddedPin(string pinName)
  {
    for (int index = 0; index < this.mPins.Length; ++index)
    {
      if (this.mPins[index].PinName == pinName)
        return index;
    }
    return -1;
  }

  [Serializable]
  public struct PinData
  {
    public int PinID;
    public string PinName;
    public FlowNode.PinTypes PinType;
    [NonSerialized]
    public FlowNode NodeBinding;
  }
}
