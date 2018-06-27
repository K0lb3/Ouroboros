// Decompiled with JetBrains decompiler
// Type: FlowNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[AddComponentMenu("")]
public abstract class FlowNode : MonoBehaviour
{
  public const int DefaultNodeColor = 32741;
  public const int ListenerNodeColor = 58751;
  private string[] mLastKnownSimpleLines;
  private string[] mLastKnownDetailedLines;
  [HideInInspector]
  public FlowNode.Link[] OutputLinks;

  protected FlowNode()
  {
    base.\u002Ector();
  }

  public abstract void OnActivate(int pinID);

  public FlowNode.Pin[] Pins
  {
    get
    {
      FlowNode.Pin[] customAttributes = (FlowNode.Pin[]) ((object) this).GetType().GetCustomAttributes(typeof (FlowNode.Pin), true);
      FlowNode.Pin[] dynamicPins = this.GetDynamicPins();
      if (dynamicPins.Length > 0)
      {
        int length = customAttributes.Length;
        Array.Resize<FlowNode.Pin>(ref customAttributes, customAttributes.Length + dynamicPins.Length);
        for (int index = 0; index < dynamicPins.Length; ++index)
          customAttributes[length + index] = dynamicPins[index];
      }
      for (int index1 = 0; index1 < customAttributes.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < customAttributes.Length; ++index2)
        {
          if (customAttributes[index1].Priority > customAttributes[index2].Priority)
          {
            FlowNode.Pin pin = customAttributes[index1];
            customAttributes[index1] = customAttributes[index2];
            customAttributes[index2] = pin;
          }
        }
      }
      return customAttributes;
    }
  }

  public virtual string[] GetInfoLines()
  {
    return this.mLastKnownSimpleLines;
  }

  public string[] GetInfoLinesInDetail()
  {
    return this.mLastKnownDetailedLines;
  }

  private List<string> GetDetailedInfoLines()
  {
    List<FieldInfo> fieldsExcludingLinks = FlowNode.GetFlowNodeEditableFieldsExcludingLinks((object) this);
    List<string> stringList = new List<string>();
    for (int index = 0; index < fieldsExcludingLinks.Count; ++index)
    {
      object obj = fieldsExcludingLinks[index].GetValue((object) this);
      string str = string.Empty;
      if (obj != null)
        str = !obj.GetType().IsSubclassOf(typeof (Object)) || !Object.op_Inequality((Object) obj, (Object) null) ? obj.ToString() : ((Object) obj).get_name();
      stringList.Add(fieldsExcludingLinks[index].Name + ": [" + str + "]");
    }
    return stringList;
  }

  private List<string> GetSimpleInfoLines()
  {
    FieldInfo[] fields = ((object) this).GetType().GetFields();
    List<string> stringList = new List<string>();
    for (int index = 0; index < fields.Length; ++index)
    {
      FlowNode.ShowInInfo[] customAttributes = (FlowNode.ShowInInfo[]) fields[index].GetCustomAttributes(typeof (FlowNode.ShowInInfo), false);
      if (customAttributes.Length > 0)
      {
        object obj = fields[index].GetValue((object) this);
        if (obj != null)
        {
          string str = !obj.GetType().IsSubclassOf(typeof (Object)) || !Object.op_Inequality((Object) obj, (Object) null) ? obj.ToString() : ((Object) obj).get_name();
          if (customAttributes[0].ShowType && obj != null)
            str = str + "(" + obj.GetType().Name + ")";
          if (str != "null")
            stringList.Add(fields[index].Name + " is [" + str + "]");
        }
      }
    }
    return stringList;
  }

  protected virtual void Awake()
  {
    ((Behaviour) this).set_enabled(false);
  }

  protected virtual void OnDestroy()
  {
  }

  public virtual FlowNode.Pin[] GetDynamicPins()
  {
    return new FlowNode.Pin[0];
  }

  public void Activate(int pinID)
  {
    this.OnActivate(pinID);
  }

  public void ActivateOutputLinks(int pinID)
  {
    for (int index = 0; index < this.OutputLinks.Length; ++index)
    {
      if (this.OutputLinks[index].SrcPinID == pinID && !Object.op_Equality((Object) this.OutputLinks[index].Dest, (Object) null))
        this.OutputLinks[index].Dest.Activate(this.OutputLinks[index].DestPinID);
    }
  }

  protected bool IsParentOf(GameObject go)
  {
    if (Object.op_Equality((Object) go, (Object) null))
      return false;
    return go.get_transform().IsChildOf(((Component) this).get_transform());
  }

  public void RemoveOutputLink(int index)
  {
    List<FlowNode.Link> linkList = new List<FlowNode.Link>((IEnumerable<FlowNode.Link>) this.OutputLinks);
    linkList.RemoveAt(index);
    this.OutputLinks = linkList.ToArray();
  }

  public int FindPin(int pinID)
  {
    FlowNode.Pin[] pins = this.Pins;
    for (int index = 0; index < pins.Length; ++index)
    {
      if (pins[index].PinID == pinID)
        return index;
    }
    return -1;
  }

  public void LinkPin(FlowNode.Pin srcPin, FlowNode dest, FlowNode.Pin destPin)
  {
    if (srcPin.PinType == destPin.PinType)
      return;
    FlowNode flowNode1;
    FlowNode flowNode2;
    if (srcPin.PinType == FlowNode.PinTypes.Output)
    {
      flowNode1 = this;
      flowNode2 = dest;
    }
    else
    {
      flowNode1 = dest;
      flowNode2 = this;
      FlowNode.Pin pin = srcPin;
      srcPin = destPin;
      destPin = pin;
    }
    for (int index = 0; index < flowNode1.OutputLinks.Length; ++index)
    {
      if (flowNode1.OutputLinks[index].SrcPinID == srcPin.PinID && Object.op_Equality((Object) flowNode1.OutputLinks[index].Dest, (Object) flowNode2) && flowNode1.OutputLinks[index].DestPinID == destPin.PinID)
        return;
    }
    Array.Resize<FlowNode.Link>(ref flowNode1.OutputLinks, flowNode1.OutputLinks.Length + 1);
    flowNode1.OutputLinks[flowNode1.OutputLinks.Length - 1] = new FlowNode.Link()
    {
      SrcPinID = srcPin.PinID,
      Dest = flowNode2,
      DestPinID = destPin.PinID
    };
  }

  public Color GetNodeColor()
  {
    FlowNode.NodeType[] customAttributes = (FlowNode.NodeType[]) ((object) this).GetType().GetCustomAttributes(typeof (FlowNode.NodeType), true);
    if (customAttributes.Length > 0)
      return customAttributes[0].Color;
    return Color.get_gray();
  }

  public virtual string GetCaption()
  {
    string str = ((object) this).GetType().Name;
    if (str.StartsWith("FlowNode_"))
      str = str.Substring("FlowNode_".Length);
    return str;
  }

  public virtual bool OnDragUpdate(object[] objectReferences)
  {
    return false;
  }

  public virtual bool OnDragPerform(object[] objectReferences)
  {
    return false;
  }

  private static List<FieldInfo> GetFlowNodeEditableFieldsExcludingLinks(object obj)
  {
    List<FieldInfo> fieldInfoList1 = new List<FieldInfo>((IEnumerable<FieldInfo>) obj.GetType().GetFields());
    List<FieldInfo> fieldInfoList2 = new List<FieldInfo>((IEnumerable<FieldInfo>) obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic));
    fieldInfoList1.RemoveAll((Predicate<FieldInfo>) (fInfo =>
    {
      if (!fInfo.IsNotSerialized && !fInfo.Name.Equals("Position"))
        return fInfo.Name.Equals("OutputLinks");
      return true;
    }));
    fieldInfoList2.RemoveAll((Predicate<FieldInfo>) (fInfo =>
    {
      if (fInfo.GetCustomAttributes(typeof (FlowNode.ShowInInfo), true).Length == 0)
        return fInfo.GetCustomAttributes(typeof (SerializeField), true).Length == 0;
      return false;
    }));
    List<FieldInfo> fieldInfoList3 = new List<FieldInfo>();
    fieldInfoList3.AddRange((IEnumerable<FieldInfo>) fieldInfoList1);
    fieldInfoList3.AddRange((IEnumerable<FieldInfo>) fieldInfoList2);
    return fieldInfoList3;
  }

  public enum PinTypes
  {
    Input,
    Output,
  }

  public class NodeType : Attribute
  {
    public string Name;
    public Color Color;

    public NodeType(string name)
    {
      this.Name = name;
      this.Color = Color.get_cyan();
    }

    public NodeType(string name, int color)
    {
      this.Name = name;
      this.Color = Color32.op_Implicit(new Color32((byte) (color >> 16 & (int) byte.MaxValue), (byte) (color >> 8 & (int) byte.MaxValue), (byte) (color & (int) byte.MaxValue), byte.MaxValue));
    }
  }

  public class ShowInInspector : Attribute
  {
  }

  public class ShowInInfo : PropertyAttribute
  {
    public bool ShowType;

    public ShowInInfo()
    {
      base.\u002Ector();
    }

    public ShowInInfo(bool withType)
    {
      base.\u002Ector();
      this.ShowType = withType;
    }
  }

  public class FlowDefinitionSearchKeyAttribute : Attribute
  {
    public System.Type ObjType;

    public FlowDefinitionSearchKeyAttribute(System.Type type)
    {
      this.ObjType = type;
    }
  }

  public class DropTarget : PropertyAttribute
  {
    public System.Type AcceptType;
    public bool OnlyChildren;

    public DropTarget(System.Type type, bool onlyChildren)
    {
      this.\u002Ector();
      this.AcceptType = type;
      this.OnlyChildren = onlyChildren;
    }
  }

  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
  public class Pin : Attribute
  {
    public Color Color = Color.get_black();
    public int PinID;
    public string Name;
    public FlowNode.PinTypes PinType;
    public int Priority;

    public Pin(int id, string name, FlowNode.PinTypes type, int priority = 0)
    {
      this.Name = name;
      this.PinID = id;
      this.PinType = type;
      this.Priority = priority;
    }
  }

  [Serializable]
  public struct Link
  {
    public FlowNode Dest;
    public int SrcPinID;
    public int DestPinID;

    public override string ToString()
    {
      return string.Format("[Link] Dest: {0}, SrcPinID: {1}, DestPinID: {2}", (object) this.Dest, (object) this.SrcPinID, (object) this.DestPinID);
    }

    public override bool Equals(object inObj)
    {
      if (inObj == null || !(inObj is FlowNode.Link))
        return false;
      FlowNode.Link link = (FlowNode.Link) inObj;
      if (((object) this.Dest).GetType().Equals(((object) link.Dest).GetType()) && this.SrcPinID.Equals(link.SrcPinID))
        return this.DestPinID.Equals(link.DestPinID);
      return false;
    }
  }
}
