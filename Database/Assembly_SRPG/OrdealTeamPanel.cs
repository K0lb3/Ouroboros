namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class OrdealTeamPanel : MonoBehaviour
    {
        public GameObject UnitSlotContainer;
        public OrdealUnitSlot[] UnitSlots;
        public OrdealUnitSlot SupportSlot;
        public Text TotalAtack;
        public Text TeamName;
        public UnityEngine.UI.Button Button;
        private int mUnitCount;

        public OrdealTeamPanel()
        {
            this.UnitSlots = new OrdealUnitSlot[1];
            base..ctor();
            return;
        }

        public void Add(UnitData unitData)
        {
            OrdealUnitSlot slot;
            if (this.mUnitCount >= ((int) this.UnitSlots.Length))
            {
                goto Label_004E;
            }
            slot = this.UnitSlots[this.mUnitCount];
            slot.Unit.SetActive(1);
            DataSource.Bind<UnitData>(slot.Unit.get_gameObject(), unitData);
            GameParameter.UpdateAll(slot.Unit.get_gameObject());
        Label_004E:
            this.mUnitCount += 1;
            return;
        }

        private void Awake()
        {
        }

        private void Reset()
        {
            OrdealUnitSlot slot;
            OrdealUnitSlot[] slotArray;
            int num;
            if (this.UnitSlots == null)
            {
                goto Label_0036;
            }
            slotArray = this.UnitSlots;
            num = 0;
            goto Label_002D;
        Label_0019:
            slot = slotArray[num];
            slot.Unit.SetActive(0);
            num += 1;
        Label_002D:
            if (num < ((int) slotArray.Length))
            {
                goto Label_0019;
            }
        Label_0036:
            if ((this.SupportSlot != null) == null)
            {
                goto Label_0058;
            }
            this.SupportSlot.Unit.SetActive(0);
        Label_0058:
            this.mUnitCount = 0;
            return;
        }

        public void SetSupport(SupportData supportData)
        {
            DataSource.Bind<SupportData>(this.SupportSlot.Unit.get_gameObject(), supportData);
            this.SupportSlot.Unit.SetActive(1);
            return;
        }
    }
}

