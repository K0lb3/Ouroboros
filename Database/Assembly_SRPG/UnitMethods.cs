namespace SRPG
{
    using System;
    using UnityEngine;

    public class UnitMethods : MonoBehaviour
    {
        public UnitMethods()
        {
            base..ctor();
            return;
        }

        public void SetUnitToActivePartySlot()
        {
            PartyUnitSlot slot;
            UnitData data;
            PartyData data2;
            int num;
            slot = PartyUnitSlot.Active;
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if ((slot != null) == null)
            {
                goto Label_008E;
            }
            data2 = DataSource.FindDataOfClass<PartyData>(slot.get_gameObject(), null);
            if (data == null)
            {
                goto Label_007B;
            }
            num = 0;
            goto Label_0058;
        Label_0039:
            if (data2.GetUnitUniqueID(num) != data.UniqueID)
            {
                goto Label_0054;
            }
            data2.SetUnitUniqueID(num, 0L);
        Label_0054:
            num += 1;
        Label_0058:
            if (num < data2.MAX_UNIT)
            {
                goto Label_0039;
            }
            data2.SetUnitUniqueID(slot.Index, data.UniqueID);
            goto Label_0089;
        Label_007B:
            data2.SetUnitUniqueID(slot.Index, 0L);
        Label_0089:
            FlowNodeEvent<FlowNode_OnPartyChange>.Invoke();
        Label_008E:
            return;
        }
    }
}

