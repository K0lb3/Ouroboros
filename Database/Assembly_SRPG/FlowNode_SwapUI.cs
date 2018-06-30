namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [NodeType("UI/Swap", 0x7fe5), Pin(1, "Swap In", 0, 0), Pin(2, "Swap Out", 0, 1), Pin(10, "Output", 1, 2)]
    public class FlowNode_SwapUI : FlowNode
    {
        [ShowInInfo]
        public GameObject Target;
        public bool Deactivate;
        private GameObject mDummy;
        private DestroyEventListener mTargetDestroyEvent;
        private DestroyEventListener mDummyDestroyEvent;

        public FlowNode_SwapUI()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <SwapOut>m__1CF(GameObject go)
        {
            this.mDummyDestroyEvent.Listeners = null;
            this.mTargetDestroyEvent.Listeners = null;
            Object.Destroy(this.Target);
            return;
        }

        [CompilerGenerated]
        private void <SwapOut>m__1D0(GameObject go)
        {
            this.mDummyDestroyEvent.Listeners = null;
            this.mTargetDestroyEvent.Listeners = null;
            Object.Destroy(this.mDummy);
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_0015;
            }
            if (num == 2)
            {
                goto Label_0029;
            }
            goto Label_003D;
        Label_0015:
            this.SwapIn();
            base.ActivateOutputLinks(10);
            goto Label_003D;
        Label_0029:
            this.SwapOut();
            base.ActivateOutputLinks(10);
        Label_003D:
            return;
        }

        private void SwapIn()
        {
            Transform transform;
            Transform transform2;
            if ((this.mDummy == null) != null)
            {
                goto Label_0022;
            }
            if ((this.Target == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            transform = this.Target.get_transform();
            transform2 = this.mDummy.get_transform();
            transform.SetParent(transform2.get_parent(), 0);
            transform.SetSiblingIndex(transform2.GetSiblingIndex());
            this.mDummy.GetComponent<DestroyEventListener>().Listeners = null;
            Object.Destroy(this.mDummy.get_gameObject());
            this.mDummy = null;
            if (this.Deactivate == null)
            {
                goto Label_0093;
            }
            this.Target.SetActive(1);
        Label_0093:
            return;
        }

        private void SwapOut()
        {
            Type[] typeArray1;
            Transform transform;
            Transform transform2;
            if ((this.mDummy != null) != null)
            {
                goto Label_0022;
            }
            if ((this.Target == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            transform = this.Target.get_transform();
            typeArray1 = new Type[] { typeof(DestroyEventListener) };
            this.mDummy = new GameObject(this.Target.get_name() + "(Dummy)", typeArray1);
            transform2 = this.mDummy.get_transform();
            transform2.SetParent(transform.get_parent(), 0);
            transform2.SetSiblingIndex(transform.GetSiblingIndex());
            this.mDummyDestroyEvent = this.mDummy.GetComponent<DestroyEventListener>();
            this.mTargetDestroyEvent = this.Target.get_gameObject().AddComponent<DestroyEventListener>();
            this.mDummyDestroyEvent.Listeners = new DestroyEventListener.DestroyEvent(this.<SwapOut>m__1CF);
            this.mTargetDestroyEvent.Listeners = new DestroyEventListener.DestroyEvent(this.<SwapOut>m__1D0);
            transform.SetParent(UIUtility.Pool, 0);
            if (this.Deactivate == null)
            {
                goto Label_00FF;
            }
            this.Target.SetActive(0);
        Label_00FF:
            return;
        }
    }
}

