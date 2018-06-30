namespace SRPG
{
    using System;

    public class QuestPartyParam
    {
        public string iname;
        public PartySlotType type_1;
        public PartySlotType type_2;
        public PartySlotType type_3;
        public PartySlotType type_4;
        public PartySlotType support_type;
        public PartySlotType subtype_1;
        public PartySlotType subtype_2;
        public string unit_1;
        public string unit_2;
        public string unit_3;
        public string unit_4;
        public string subunit_1;
        public string subunit_2;
        public int l_npc_rare;

        public QuestPartyParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_QuestPartyParam json)
        {
            this.iname = json.iname;
            this.type_1 = json.type_1;
            this.type_2 = json.type_2;
            this.type_3 = json.type_3;
            this.type_4 = json.type_4;
            this.support_type = json.support_type;
            this.subtype_1 = json.subtype_1;
            this.subtype_2 = json.subtype_2;
            this.unit_1 = json.unit_1;
            this.unit_2 = json.unit_2;
            this.unit_3 = json.unit_3;
            this.unit_4 = json.unit_4;
            this.subunit_1 = json.subunit_1;
            this.subunit_2 = json.subunit_2;
            this.l_npc_rare = json.l_npc_rare;
            return 1;
        }

        public PartySlotTypeUnitPair[] GetMainSlots()
        {
            PartySlotTypeUnitPair[] pairArray1;
            PartySlotTypeUnitPair pair;
            pairArray1 = new PartySlotTypeUnitPair[4];
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.type_1;
            pair.Unit = this.unit_1;
            pairArray1[0] = pair;
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.type_2;
            pair.Unit = this.unit_2;
            pairArray1[1] = pair;
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.type_3;
            pair.Unit = this.unit_3;
            pairArray1[2] = pair;
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.type_4;
            pair.Unit = this.unit_4;
            pairArray1[3] = pair;
            return pairArray1;
        }

        public PartySlotTypeUnitPair[] GetMainSubSlots()
        {
            PartySlotTypeUnitPair[] pairArray1;
            PartySlotTypeUnitPair pair;
            pairArray1 = new PartySlotTypeUnitPair[6];
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.type_1;
            pair.Unit = this.unit_1;
            pairArray1[0] = pair;
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.type_2;
            pair.Unit = this.unit_2;
            pairArray1[1] = pair;
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.type_3;
            pair.Unit = this.unit_3;
            pairArray1[2] = pair;
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.type_4;
            pair.Unit = this.unit_4;
            pairArray1[3] = pair;
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.subtype_1;
            pair.Unit = this.subunit_1;
            pairArray1[4] = pair;
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.subtype_2;
            pair.Unit = this.subunit_2;
            pairArray1[5] = pair;
            return pairArray1;
        }

        public string GetNpcLeaderUnitIname()
        {
            PartySlotType type;
            type = this.type_1;
            if (type == 4)
            {
                goto Label_001A;
            }
            if (type == 5)
            {
                goto Label_001A;
            }
            goto Label_0021;
        Label_001A:
            return this.unit_1;
        Label_0021:
            return null;
        }

        public PartySlotTypeUnitPair[] GetSubSlots()
        {
            PartySlotTypeUnitPair[] pairArray1;
            PartySlotTypeUnitPair pair;
            pairArray1 = new PartySlotTypeUnitPair[2];
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.subtype_1;
            pair.Unit = this.subunit_1;
            pairArray1[0] = pair;
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.subtype_2;
            pair.Unit = this.subunit_2;
            pairArray1[1] = pair;
            return pairArray1;
        }

        public PartySlotTypeUnitPair[] GetSupportSlots()
        {
            PartySlotTypeUnitPair[] pairArray1;
            PartySlotTypeUnitPair pair;
            pairArray1 = new PartySlotTypeUnitPair[1];
            pair = new PartySlotTypeUnitPair();
            pair.Type = this.support_type;
            pair.Unit = null;
            pairArray1[0] = pair;
            return pairArray1;
        }
    }
}

