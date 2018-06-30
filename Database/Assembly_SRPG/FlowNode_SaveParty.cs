namespace SRPG
{
    using GR;
    using System;

    [Pin(0xc9, "OutResetWithID", 1, 0xc9), Pin(200, "OutSaveWithID", 1, 200), Pin(100, "Out", 1, 100), Pin(0x33, "ResetWithID", 0, 0x33), Pin(50, "SaveWithID", 0, 50), Pin(2, "Reset", 0, 2), Pin(1, "Save", 0, 1), NodeType("UI/SaveParty", 0x7fe5)]
    public class FlowNode_SaveParty : FlowNode
    {
        private long[,] mSavedUnits;
        private int mSavedPartyID;
        private ItemData[] mSavedInventory;

        public FlowNode_SaveParty()
        {
            this.mSavedInventory = new ItemData[5];
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_001B;
            }
            this.Save();
            base.ActivateOutputLinks(100);
            goto Label_00A0;
        Label_001B:
            if (pinID != 2)
            {
                goto Label_003B;
            }
            this.Reset();
            base.ActivateOutputLinks(100);
            FlowNodeEvent<FlowNode_OnPartyChange>.Invoke();
            goto Label_00A0;
        Label_003B:
            if (pinID != 50)
            {
                goto Label_0070;
            }
            this.Save();
            this.mSavedPartyID = GlobalVars.SelectedPartyIndex;
            this.SaveInventory();
            base.ActivateOutputLinks(200);
            goto Label_00A0;
        Label_0070:
            if (pinID != 0x33)
            {
                goto Label_00A0;
            }
            this.Reset();
            GlobalVars.SelectedPartyIndex.Set(this.mSavedPartyID);
            this.ResetInventory();
            base.ActivateOutputLinks(0xc9);
        Label_00A0:
            return;
        }

        private void Reset()
        {
            PlayerData data;
            PartyData data2;
            int num;
            int num2;
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
            num = 0;
            goto Label_005C;
        Label_0022:
            num2 = 0;
            goto Label_004C;
        Label_0029:
            data.Partys[num].SetUnitUniqueID(num2, this.mSavedUnits[num, num2]);
            num2 += 1;
        Label_004C:
            if (num2 < data2.MAX_UNIT)
            {
                goto Label_0029;
            }
            num += 1;
        Label_005C:
            if (num < 11)
            {
                goto Label_0022;
            }
            return;
        }

        private void ResetInventory()
        {
            PlayerData data;
            int num;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_0025;
        Label_0012:
            data.SetInventory(num, this.mSavedInventory[num]);
            num += 1;
        Label_0025:
            if (num < 5)
            {
                goto Label_0012;
            }
            return;
        }

        private void Save()
        {
            PlayerData data;
            PartyData data2;
            int num;
            int num2;
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
            num = 0;
            goto Label_005C;
        Label_0022:
            num2 = 0;
            goto Label_004C;
        Label_0029:
            this.mSavedUnits[num, num2] = data.Partys[num].GetUnitUniqueID(num2);
            num2 += 1;
        Label_004C:
            if (num2 < data2.MAX_UNIT)
            {
                goto Label_0029;
            }
            num += 1;
        Label_005C:
            if (num < 11)
            {
                goto Label_0022;
            }
            return;
        }

        private void SaveInventory()
        {
            PlayerData data;
            int num;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_0026;
        Label_0012:
            this.mSavedInventory[num] = data.Inventory[num];
            num += 1;
        Label_0026:
            if (num < 5)
            {
                goto Label_0012;
            }
            return;
        }
    }
}

