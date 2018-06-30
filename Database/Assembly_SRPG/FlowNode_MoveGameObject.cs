namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(1, "Out", 1, 1), NodeType("MoveGameObject", 0x7fe5), Pin(0, "In", 0, 0)]
    public class FlowNode_MoveGameObject : FlowNode
    {
        public GameObject Target;
        public GameObject Destination;
        public float Time;
        public ObjectAnimator.CurveType InterpolationMode;

        public FlowNode_MoveGameObject()
        {
            this.Time = 1f;
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            Transform transform;
            if (pinID != null)
            {
                goto Label_0064;
            }
            if ((this.Target != null) == null)
            {
                goto Label_005C;
            }
            if ((this.Destination != null) == null)
            {
                goto Label_005C;
            }
            transform = this.Destination.get_transform();
            ObjectAnimator.Get(this.Target).AnimateTo(transform.get_position(), transform.get_rotation(), this.Time, this.InterpolationMode);
        Label_005C:
            base.ActivateOutputLinks(1);
        Label_0064:
            return;
        }
    }
}

