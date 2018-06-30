namespace SRPG
{
    using System;
    using System.Collections.Generic;

    [Pin(0xca, "Unit Place NG", 1, 12), Pin(0xc9, "Unit Slot NG", 1, 11), Pin(200, "Unit Slot OK", 1, 10), Pin(120, "Check Unit Slot", 0, 3), NodeType("Multi/CheckRankMatchUnitSlot", 0x7fe5)]
    public class FlowNode_CheckRankMatchUnitSlot : FlowNode
    {
        public const int PINID_CHECK_UNIT_SLOT = 120;
        public const int PINID_UNIT_SLOT_OK = 200;
        public const int PINID_UNIT_SLOT_NG = 0xc9;
        public const int PINID_UNIT_PLACE_NG = 0xca;

        public FlowNode_CheckRankMatchUnitSlot()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            int num;
            List<PartyEditData> list;
            PartyEditData data;
            int num2;
            List<int> list2;
            int num3;
            int num4;
            int num5;
            num5 = pinID;
            if (num5 == 120)
            {
                goto Label_0011;
            }
            goto Label_00EC;
        Label_0011:
            data = PartyUtility.LoadTeamPresets(10, &num, 0)[num];
            num2 = 0;
            goto Label_0059;
        Label_002B:
            if ((num2 + 1) > ((int) data.Units.Length))
            {
                goto Label_0048;
            }
            if (data.Units[num2] != null)
            {
                goto Label_0055;
            }
        Label_0048:
            base.ActivateOutputLinks(0xc9);
            return;
        Label_0055:
            num2 += 1;
        Label_0059:
            if (num2 < data.PartyData.VSWAITMEMBER_START)
            {
                goto Label_002B;
            }
            list2 = new List<int>();
            num3 = 0;
            goto Label_00C9;
        Label_0079:
            num4 = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.RANKMATCH_ID_KEY + ((int) num3), -1);
            if (num4 >= 0)
            {
                goto Label_009F;
            }
            goto Label_00C3;
        Label_009F:
            if (list2.Contains(num4) == null)
            {
                goto Label_00BA;
            }
            base.ActivateOutputLinks(0xca);
            return;
        Label_00BA:
            list2.Add(num4);
        Label_00C3:
            num3 += 1;
        Label_00C9:
            if (num3 < data.PartyData.VSWAITMEMBER_START)
            {
                goto Label_0079;
            }
            base.ActivateOutputLinks(200);
        Label_00EC:
            return;
        }
    }
}

