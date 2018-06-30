namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using UnityEngine;

    public class ToggleTeleport : AnimEvent
    {
        private bool mIsValid;
        private Vector3 mPosStart;
        private Vector3 mPosEnd;

        public ToggleTeleport()
        {
            base..ctor();
            return;
        }

        public override void OnEnd(GameObject go)
        {
            TacticsUnitController controller;
            SceneBattle battle;
            if (this.mIsValid != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            controller = go.GetComponentInParent<TacticsUnitController>();
            if (controller != null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            controller.CollideGround = 1;
            controller.get_transform().set_position(this.mPosEnd);
            controller.SetStartPos(controller.get_transform().get_position());
            controller.LookAtTarget();
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_0065;
            }
            battle.OnGimmickUpdate();
        Label_0065:
            this.mIsValid = 0;
            return;
        }

        public override void OnStart(GameObject go)
        {
            TacticsUnitController controller;
            SceneBattle battle;
            controller = go.GetComponentInParent<TacticsUnitController>();
            if (controller != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_0025;
            }
            return;
        Label_0025:
            this.mPosStart = controller.CenterPosition;
            this.mPosEnd = controller.GetTargetPos();
            battle.OnGimmickUpdate();
            controller.CollideGround = 0;
            this.mIsValid = 1;
            return;
        }

        public override void OnTick(GameObject go, float ratio)
        {
            TacticsUnitController controller;
            if (this.mIsValid != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            controller = go.GetComponentInParent<TacticsUnitController>();
            if (controller != null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            controller.get_transform().set_position(Vector3.Lerp(this.mPosStart, this.mPosEnd, ratio));
            return;
        }
    }
}

