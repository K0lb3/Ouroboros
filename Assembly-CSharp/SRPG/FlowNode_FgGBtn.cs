// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FgGBtn
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("FgGID/FgGBtn", 32741)]
  [FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_FgGBtn : FlowNode
  {
    [FlowNode.ShowInInfo]
    public string ParameterName = "authStatus";
    [FlowNode.DropTarget(typeof (GameObject), true)]
    [FlowNode.ShowInInfo]
    public GameObject Target;
    public bool UpdateAnimator;

    public override string GetCaption()
    {
      return base.GetCaption() + ":" + this.ParameterName;
    }

    public override void OnActivate(int pinID)
    {
      Animator component = (Animator) (!Object.op_Inequality((Object) this.Target, (Object) null) ? ((Component) this).get_gameObject() : this.Target).GetComponent<Animator>();
      switch (MonoSingleton<GameManager>.Instance.AuthStatus)
      {
        case ReqFgGAuth.eAuthStatus.Disable:
          this.Target.SetActive(false);
          break;
        case ReqFgGAuth.eAuthStatus.NotSynchronized:
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.SetInteger(this.ParameterName, 2);
            if (this.UpdateAnimator)
            {
              component.Update(0.0f);
              break;
            }
            break;
          }
          break;
        case ReqFgGAuth.eAuthStatus.Synchronized:
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.SetInteger(this.ParameterName, 3);
            if (this.UpdateAnimator)
            {
              component.Update(0.0f);
              break;
            }
            break;
          }
          break;
        default:
          this.Target.SetActive(false);
          break;
      }
      this.ActivateOutputLinks(1);
    }
  }
}
