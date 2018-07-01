// Decompiled with JetBrains decompiler
// Type: SRPG.WindowControllerEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class WindowControllerEvent : StateMachineBehaviour
  {
    public WindowControllerEvent.EventTypes Type;

    public WindowControllerEvent()
    {
      base.\u002Ector();
    }

    public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      WindowController component = (WindowController) ((Component) animator).GetComponent<WindowController>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      if (this.Type == WindowControllerEvent.EventTypes.Opened)
        component.OnOpen();
      else
        component.OnClose();
    }

    public enum EventTypes
    {
      Opened,
      Closed,
    }
  }
}
