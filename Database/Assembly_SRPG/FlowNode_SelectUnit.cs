namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/Select Unit", 0x7fe5), Pin(2, "Unit Not Found", 1, 2), Pin(1, "Selected", 1, 1), Pin(0, "Select", 0, 0)]
    public class FlowNode_SelectUnit : FlowNode
    {
        public string UnitID;
        public long UniqueID;
        public bool KeepSelection;
        public bool SelectJob;
        public string JobID;
        public bool SelectEquipSlot;
        public int EquipSlot;

        public FlowNode_SelectUnit()
        {
            this.EquipSlot = -1;
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            UnitData data;
            int num;
            PlayerData data2;
            int num2;
            int num3;
            data = null;
            if (pinID == null)
            {
                goto Label_0011;
            }
            goto Label_018E;
        Label_0011:
            num = 2;
            data2 = MonoSingleton<GameManager>.Instance.Player;
            if (string.IsNullOrEmpty(this.UnitID) != null)
            {
                goto Label_0058;
            }
            if ((data = data2.FindUnitDataByUnitID(this.UnitID)) == null)
            {
                goto Label_0088;
            }
            GlobalVars.SelectedUnitUniqueID.Set(data.UniqueID);
            num = 1;
            goto Label_0088;
        Label_0058:
            if (this.UniqueID == null)
            {
                goto Label_0088;
            }
            if ((data = data2.FindUnitDataByUniqueID(this.UniqueID)) == null)
            {
                goto Label_0088;
            }
            GlobalVars.SelectedUnitUniqueID.Set(data.UniqueID);
            num = 1;
        Label_0088:
            if (num != 1)
            {
                goto Label_0136;
            }
            if (this.SelectJob == null)
            {
                goto Label_0116;
            }
            num2 = 0;
            goto Label_00C7;
        Label_00A1:
            if ((data.Jobs[num2].JobID == this.JobID) == null)
            {
                goto Label_00C3;
            }
            goto Label_00D5;
        Label_00C3:
            num2 += 1;
        Label_00C7:
            if (num2 < ((int) data.Jobs.Length))
            {
                goto Label_00A1;
            }
        Label_00D5:
            if (num2 >= ((int) data.Jobs.Length))
            {
                goto Label_00FF;
            }
            GlobalVars.SelectedJobUniqueID.Set(data.Jobs[num2].UniqueID);
            goto Label_0116;
        Label_00FF:
            if (this.KeepSelection != null)
            {
                goto Label_0116;
            }
            GlobalVars.SelectedJobUniqueID.Set(0L);
        Label_0116:
            if (this.SelectEquipSlot == null)
            {
                goto Label_0181;
            }
            GlobalVars.SelectedEquipmentSlot.Set(this.EquipSlot);
            goto Label_0181;
        Label_0136:
            if (num != 2)
            {
                goto Label_0181;
            }
            if (this.KeepSelection != null)
            {
                goto Label_0181;
            }
            GlobalVars.SelectedUnitUniqueID.Set(0L);
            if (this.SelectJob == null)
            {
                goto Label_016B;
            }
            GlobalVars.SelectedJobUniqueID.Set(0L);
        Label_016B:
            if (this.SelectEquipSlot == null)
            {
                goto Label_0181;
            }
            GlobalVars.SelectedEquipmentSlot.Set(-1);
        Label_0181:
            base.ActivateOutputLinks(num);
            goto Label_0193;
        Label_018E:;
        Label_0193:
            return;
        }
    }
}

