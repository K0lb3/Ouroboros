// Decompiled with JetBrains decompiler
// Type: FlowNode_Variable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[FlowNode.Pin(9, "Assigned", FlowNode.PinTypes.Output, 9)]
[FlowNode.Pin(10, "== Variable", FlowNode.PinTypes.Output, 10)]
[FlowNode.Pin(11, "!= Variable", FlowNode.PinTypes.Output, 11)]
[FlowNode.Pin(12, "< Variable", FlowNode.PinTypes.Output, 12)]
[FlowNode.Pin(13, "<= Variable", FlowNode.PinTypes.Output, 13)]
[FlowNode.Pin(14, "> Variable", FlowNode.PinTypes.Output, 14)]
[FlowNode.Pin(15, ">= Variable", FlowNode.PinTypes.Output, 15)]
[AddComponentMenu("")]
[FlowNode.NodeType("Variable", 32741)]
[FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(2, "Compare", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(3, "SetIfNull", FlowNode.PinTypes.Input, 3)]
public class FlowNode_Variable : FlowNode
{
  private static Dictionary<string, string> mKeyValues = new Dictionary<string, string>();
  public string Name;
  public string Value;

  public static void Set(string name, string value)
  {
    FlowNode_Variable.mKeyValues[name] = value;
  }

  public static string Get(string name)
  {
    if (!FlowNode_Variable.mKeyValues.ContainsKey(name))
      return (string) null;
    return FlowNode_Variable.mKeyValues[name];
  }

  public override void OnActivate(int pinID)
  {
    if (string.IsNullOrEmpty(this.Name))
      return;
    switch (pinID)
    {
      case 1:
        FlowNode_Variable.Set(this.Name, this.Value);
        this.ActivateOutputLinks(9);
        break;
      case 2:
        string s = (string) null;
        try
        {
          s = FlowNode_Variable.mKeyValues[this.Name];
        }
        catch (Exception ex)
        {
          Debug.LogWarning((object) (ex.Message + ":" + this.Name));
        }
        if (s == this.Value)
        {
          this.ActivateOutputLinks(10);
          break;
        }
        if (s != this.Value)
        {
          this.ActivateOutputLinks(11);
          break;
        }
        try
        {
          int num1 = int.Parse(s);
          int num2 = int.Parse(this.Value);
          if (num1 < num2)
          {
            this.ActivateOutputLinks(12);
            break;
          }
          if (num1 <= num2)
          {
            this.ActivateOutputLinks(13);
            break;
          }
          if (num1 > num2)
          {
            this.ActivateOutputLinks(14);
            break;
          }
          if (num1 < num2)
            break;
          this.ActivateOutputLinks(15);
          break;
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
          break;
        }
      case 3:
        if (!FlowNode_Variable.mKeyValues.ContainsKey(this.Name))
          FlowNode_Variable.Set(this.Name, this.Value);
        this.ActivateOutputLinks(9);
        break;
    }
  }
}
