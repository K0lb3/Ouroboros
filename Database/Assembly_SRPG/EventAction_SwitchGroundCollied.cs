namespace SRPG
{
    using System;

    [EventActionInfo("New/アクター/地面判定切り替え", "指定のアクターの地面判定を切り替えます。", 0x555555, 0x444488)]
    public class EventAction_SwitchGroundCollied : EventAction
    {
        [StringIsActorList]
        public string ActorID;
        public bool GroundSnap;

        public EventAction_SwitchGroundCollied()
        {
            this.GroundSnap = 1;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            TacticsUnitController controller;
            controller = TacticsUnitController.FindByUniqueName(this.ActorID);
            if ((controller != null) == null)
            {
                goto Label_0024;
            }
            controller.CollideGround = this.GroundSnap;
        Label_0024:
            base.ActivateNext();
            return;
        }
    }
}

