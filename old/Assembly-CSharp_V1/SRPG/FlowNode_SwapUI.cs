// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SwapUI
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("UI/Swap", 32741)]
  [FlowNode.Pin(1, "Swap In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Swap Out", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_SwapUI : FlowNode
  {
    [FlowNode.ShowInInfo]
    public GameObject Target;
    public bool Deactivate;
    private GameObject mDummy;
    private DestroyEventListener mTargetDestroyEvent;
    private DestroyEventListener mDummyDestroyEvent;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.SwapIn();
          this.ActivateOutputLinks(10);
          break;
        case 2:
          this.SwapOut();
          this.ActivateOutputLinks(10);
          break;
      }
    }

    private void SwapIn()
    {
      if (Object.op_Equality((Object) this.mDummy, (Object) null) || Object.op_Equality((Object) this.Target, (Object) null))
        return;
      Transform transform1 = this.Target.get_transform();
      Transform transform2 = this.mDummy.get_transform();
      transform1.SetParent(transform2.get_parent(), false);
      transform1.SetSiblingIndex(transform2.GetSiblingIndex());
      ((DestroyEventListener) this.mDummy.GetComponent<DestroyEventListener>()).Listeners = (DestroyEventListener.DestroyEvent) null;
      Object.Destroy((Object) this.mDummy.get_gameObject());
      this.mDummy = (GameObject) null;
      if (!this.Deactivate)
        return;
      this.Target.SetActive(true);
    }

    private void SwapOut()
    {
      if (Object.op_Inequality((Object) this.mDummy, (Object) null) || Object.op_Equality((Object) this.Target, (Object) null))
        return;
      Transform transform1 = this.Target.get_transform();
      this.mDummy = new GameObject(((Object) this.Target).get_name() + "(Dummy)", new System.Type[1]
      {
        typeof (DestroyEventListener)
      });
      Transform transform2 = this.mDummy.get_transform();
      transform2.SetParent(transform1.get_parent(), false);
      transform2.SetSiblingIndex(transform1.GetSiblingIndex());
      this.mDummyDestroyEvent = (DestroyEventListener) this.mDummy.GetComponent<DestroyEventListener>();
      this.mTargetDestroyEvent = (DestroyEventListener) this.Target.get_gameObject().AddComponent<DestroyEventListener>();
      this.mDummyDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) (go =>
      {
        this.mDummyDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) null;
        this.mTargetDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) null;
        Object.Destroy((Object) this.Target);
      });
      this.mTargetDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) (go =>
      {
        this.mDummyDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) null;
        this.mTargetDestroyEvent.Listeners = (DestroyEventListener.DestroyEvent) null;
        Object.Destroy((Object) this.mDummy);
      });
      transform1.SetParent((Transform) UIUtility.Pool, false);
      if (!this.Deactivate)
        return;
      this.Target.SetActive(false);
    }
  }
}
