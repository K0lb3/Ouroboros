namespace SRPG
{
    using System;

    [EventActionInfo("New/アクター/揺れもの切替", "アクターの揺れもの状態を切り替えます。", 0x664444, 0xaa4444)]
    public class EventAction_Yuremono : EventAction
    {
        [StringIsActorList]
        public string ActorID;
        public bool EnableYuremono;

        public EventAction_Yuremono()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            TacticsUnitController controller;
            YuremonoInstance[] instanceArray;
            int num;
            controller = TacticsUnitController.FindByUniqueName(this.ActorID);
            if ((controller != null) == null)
            {
                goto Label_0046;
            }
            instanceArray = controller.get_gameObject().GetComponentsInChildren<YuremonoInstance>();
            num = 0;
            goto Label_003D;
        Label_002B:
            instanceArray[num].set_enabled(this.EnableYuremono);
            num += 1;
        Label_003D:
            if (num < ((int) instanceArray.Length))
            {
                goto Label_002B;
            }
        Label_0046:
            base.ActivateNext();
            return;
        }
    }
}

