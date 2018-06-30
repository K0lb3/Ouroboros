namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class PartySlotData
    {
        public PartySlotType Type;
        public PartySlotIndex Index;
        public string UnitName;
        public bool IsSettable;

        public PartySlotData(PartySlotType type, string unitName, PartySlotIndex index, bool isSettable)
        {
            base..ctor();
            this.Type = type;
            this.Index = index;
            this.UnitName = unitName;
            this.IsSettable = isSettable;
            return;
        }

        public override string ToString()
        {
            string str;
            PartySlotType type;
            str = "PartySlotData";
            str = (str + "\n") + "    枠 : ";
            switch (this.Type)
            {
                case 0:
                    goto Label_006A;

                case 1:
                    goto Label_007B;

                case 2:
                    goto Label_0048;

                case 3:
                    goto Label_0059;

                case 4:
                    goto Label_008C;

                case 5:
                    goto Label_009D;
            }
            goto Label_00AE;
        Label_0048:
            str = str + "強制出撃";
            goto Label_00AE;
        Label_0059:
            str = str + "強制出撃(主人公)";
            goto Label_00AE;
        Label_006A:
            str = str + "自由";
            goto Label_00AE;
        Label_007B:
            str = str + "出撃不可";
            goto Label_00AE;
        Label_008C:
            str = str + "NPC";
            goto Label_00AE;
        Label_009D:
            str = str + "NPC(主人公)";
        Label_00AE:
            return ((((((str + "\n") + "    要素 : ") + Enum.GetName(typeof(PartySlotIndex), (PartySlotIndex) this.Index)) + "\n") + "    ユニット名 : ") + this.UnitName);
        }
    }
}

