namespace SRPG
{
    using System;

    [Pin(1, "Clear Slot", 0, 1), Pin(100, "Out", 1, 100), NodeType("SRPG/ClearActiveUnitSlot", 0x7fe5)]
    public class FlowNode_ClearActiveUnitSlot : FlowNode
    {
        public FlowNode_ClearActiveUnitSlot()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            PartyData data;
            if (pinID != 1)
            {
                goto Label_0049;
            }
            if ((PartyUnitSlot.Active != null) == null)
            {
                goto Label_0040;
            }
            data = DataSource.FindDataOfClass<PartyData>(PartyUnitSlot.Active.get_gameObject(), null);
            if (data == null)
            {
                goto Label_0040;
            }
            data.SetUnitUniqueID(PartyUnitSlot.Active.Index, 0L);
        Label_0040:
            base.ActivateOutputLinks(100);
        Label_0049:
            return;
        }
    }
}

