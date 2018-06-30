namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PartyData
    {
        private int mMAX_UNIT;
        private int mMAX_MAINMEMBER;
        private int mMAX_SUBMEMBER;
        private int mMAINMEMBER_START;
        private int mMAINMEMBER_END;
        private int mSUBMEMBER_START;
        private int mSUBMEMBER_END;
        private int mVSWAITMEMBER_START;
        private int mVSWAITMEMBER_END;
        private string mName;
        private long[] mUniqueIDs;
        private int mLeaderIndex;
        [CompilerGenerated]
        private PlayerPartyTypes <PartyType>k__BackingField;
        [CompilerGenerated]
        private bool <Selected>k__BackingField;
        [CompilerGenerated]
        private bool <IsDefense>k__BackingField;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$mapB;

        public PartyData(PlayerPartyTypes type)
        {
            PlayerPartyTypes types;
            base..ctor();
            types = type;
            switch ((types - 6))
            {
                case 0:
                    goto Label_0087;

                case 1:
                    goto Label_00E5;

                case 2:
                    goto Label_0156;

                case 3:
                    goto Label_01B4;

                case 4:
                    goto Label_0212;
            }
        Label_0029:
            this.mMAX_UNIT = 6;
            this.mMAX_MAINMEMBER = 4;
            this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
            this.mMAINMEMBER_START = 0;
            this.mMAINMEMBER_END = (0 + this.MAX_MAINMEMBER) - 1;
            this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
            this.mSUBMEMBER_END = (this.SUBMEMBER_START + this.MAX_SUBMEMBER) - 1;
            goto Label_0283;
        Label_0087:
            this.mMAX_UNIT = 7;
            this.mMAX_MAINMEMBER = 5;
            this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
            this.mMAINMEMBER_START = 0;
            this.mMAINMEMBER_END = (0 + this.MAX_MAINMEMBER) - 1;
            this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
            this.mSUBMEMBER_END = (this.SUBMEMBER_START + this.MAX_SUBMEMBER) - 1;
            goto Label_0283;
        Label_00E5:
            this.mMAX_UNIT = 5;
            this.mMAX_MAINMEMBER = 5;
            this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
            this.mMAINMEMBER_START = 0;
            this.mMAINMEMBER_END = (0 + this.MAX_MAINMEMBER) - 1;
            this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
            this.mSUBMEMBER_END = (this.SUBMEMBER_START + this.MAX_SUBMEMBER) - 1;
            this.mVSWAITMEMBER_START = 3;
            this.mVSWAITMEMBER_END = this.mMAX_UNIT;
            goto Label_0283;
        Label_0156:
            this.mMAX_UNIT = 3;
            this.mMAX_MAINMEMBER = 2;
            this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
            this.mMAINMEMBER_START = 0;
            this.mMAINMEMBER_END = (0 + this.MAX_MAINMEMBER) - 1;
            this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
            this.mSUBMEMBER_END = (this.SUBMEMBER_START + this.MAX_SUBMEMBER) - 1;
            goto Label_0283;
        Label_01B4:
            this.mMAX_UNIT = 4;
            this.mMAX_MAINMEMBER = 4;
            this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
            this.mMAINMEMBER_START = 0;
            this.mMAINMEMBER_END = (0 + this.MAX_MAINMEMBER) - 1;
            this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
            this.mSUBMEMBER_END = (this.SUBMEMBER_START + this.MAX_SUBMEMBER) - 1;
            goto Label_0283;
        Label_0212:
            this.mMAX_UNIT = 5;
            this.mMAX_MAINMEMBER = 5;
            this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
            this.mMAINMEMBER_START = 0;
            this.mMAINMEMBER_END = (0 + this.MAX_MAINMEMBER) - 1;
            this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
            this.mSUBMEMBER_END = (this.SUBMEMBER_START + this.MAX_SUBMEMBER) - 1;
            this.mVSWAITMEMBER_START = 3;
            this.mVSWAITMEMBER_END = this.mMAX_UNIT;
        Label_0283:
            this.mUniqueIDs = new long[this.MAX_UNIT];
            return;
        }

        public void Deserialize(Json_Party json)
        {
            int num;
            this.Reset();
            if (json != null)
            {
                goto Label_0012;
            }
            throw new InvalidCastException();
        Label_0012:
            this.mLeaderIndex = 0;
            num = 0;
            goto Label_0047;
        Label_0020:
            if (((int) this.mUniqueIDs.Length) > num)
            {
                goto Label_0033;
            }
            goto Label_0055;
        Label_0033:
            this.mUniqueIDs[num] = json.units[num];
            num += 1;
        Label_0047:
            if (num < ((int) json.units.Length))
            {
                goto Label_0020;
            }
        Label_0055:
            this.Selected = (json.flg_sel == 0) == 0;
            this.IsDefense = (json.flg_seldef == 0) == 0;
            return;
        }

        public int FindPartyUnit(long uniqueid)
        {
            int num;
            num = 0;
            goto Label_001B;
        Label_0007:
            if (this.mUniqueIDs[num] != uniqueid)
            {
                goto Label_0017;
            }
            return num;
        Label_0017:
            num += 1;
        Label_001B:
            if (num < this.MAX_UNIT)
            {
                goto Label_0007;
            }
            return -1;
        }

        public static unsafe PlayerPartyTypes GetPartyTypeFromString(string ptype)
        {
            string str;
            Dictionary<string, int> dictionary;
            int num;
            str = ptype;
            if (str == null)
            {
                goto Label_0107;
            }
            if (<>f__switch$mapB != null)
            {
                goto Label_00A6;
            }
            dictionary = new Dictionary<string, int>(11);
            dictionary.Add("norm", 0);
            dictionary.Add("ev", 1);
            dictionary.Add("mul", 2);
            dictionary.Add("col", 3);
            dictionary.Add("coldef", 4);
            dictionary.Add("chara", 5);
            dictionary.Add("tower", 6);
            dictionary.Add("vs", 7);
            dictionary.Add("multi_tw", 8);
            dictionary.Add("ordeal", 9);
            dictionary.Add("rm", 10);
            <>f__switch$mapB = dictionary;
        Label_00A6:
            if (<>f__switch$mapB.TryGetValue(str, &num) == null)
            {
                goto Label_0107;
            }
            switch (num)
            {
                case 0:
                    goto Label_00EF;

                case 1:
                    goto Label_00F1;

                case 2:
                    goto Label_00F3;

                case 3:
                    goto Label_00F5;

                case 4:
                    goto Label_00F7;

                case 5:
                    goto Label_00F9;

                case 6:
                    goto Label_00FB;

                case 7:
                    goto Label_00FD;

                case 8:
                    goto Label_00FF;

                case 9:
                    goto Label_0101;

                case 10:
                    goto Label_0104;
            }
            goto Label_0107;
        Label_00EF:
            return 0;
        Label_00F1:
            return 1;
        Label_00F3:
            return 2;
        Label_00F5:
            return 3;
        Label_00F7:
            return 4;
        Label_00F9:
            return 5;
        Label_00FB:
            return 6;
        Label_00FD:
            return 7;
        Label_00FF:
            return 8;
        Label_0101:
            return 9;
        Label_0104:
            return 10;
        Label_0107:
            return 0;
        }

        public static string GetStringFromPartyType(PlayerPartyTypes type)
        {
            PlayerPartyTypes types;
            types = type;
            switch (types)
            {
                case 0:
                    goto Label_0039;

                case 1:
                    goto Label_003F;

                case 2:
                    goto Label_0045;

                case 3:
                    goto Label_004B;

                case 4:
                    goto Label_0051;

                case 5:
                    goto Label_0057;

                case 6:
                    goto Label_005D;

                case 7:
                    goto Label_0063;

                case 8:
                    goto Label_0069;

                case 9:
                    goto Label_006F;

                case 10:
                    goto Label_0075;
            }
            goto Label_007B;
        Label_0039:
            return "norm";
        Label_003F:
            return "ev";
        Label_0045:
            return "mul";
        Label_004B:
            return "col";
        Label_0051:
            return "coldef";
        Label_0057:
            return "chara";
        Label_005D:
            return "tower";
        Label_0063:
            return "vs";
        Label_0069:
            return "multi_tw";
        Label_006F:
            return "ordeal";
        Label_0075:
            return "rm";
        Label_007B:
            return "norm";
        }

        public long GetUnitUniqueID(int index)
        {
            if (index < 0)
            {
                goto Label_0013;
            }
            if (this.MAX_UNIT > index)
            {
                goto Label_0016;
            }
        Label_0013:
            return 0L;
        Label_0016:
            return this.mUniqueIDs[index];
        }

        public bool IsPartyUnit(long uniqueid)
        {
            return ((this.FindPartyUnit(uniqueid) == -1) == 0);
        }

        public bool IsSub(UnitData unit)
        {
            return this.IsSub(unit.UniqueID);
        }

        public bool IsSub(long uniqueid)
        {
            int num;
            if (this.FindPartyUnit(uniqueid) >= this.MAX_MAINMEMBER)
            {
                goto Label_0016;
            }
            return 0;
        Label_0016:
            return 1;
        }

        public void Reset()
        {
            Array.Clear(this.mUniqueIDs, 0, (int) this.mUniqueIDs.Length);
            return;
        }

        public void SetParty(PartyData org)
        {
            int num;
            if (org != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.Reset();
            num = 0;
            goto Label_0027;
        Label_0014:
            this.mUniqueIDs[num] = org.GetUnitUniqueID(num);
            num += 1;
        Label_0027:
            if (num < this.MAX_UNIT)
            {
                goto Label_0014;
            }
            return;
        }

        public void SetUnitUniqueID(int index, long uniqueid)
        {
            if (index < 0)
            {
                goto Label_0013;
            }
            if (this.MAX_UNIT > index)
            {
                goto Label_0014;
            }
        Label_0013:
            return;
        Label_0014:
            this.mUniqueIDs[index] = uniqueid;
            return;
        }

        public int MAX_UNIT
        {
            get
            {
                return this.mMAX_UNIT;
            }
        }

        public int MAX_MAINMEMBER
        {
            get
            {
                return this.mMAX_MAINMEMBER;
            }
        }

        public int MAX_SUBMEMBER
        {
            get
            {
                return this.mMAX_SUBMEMBER;
            }
        }

        public int MAINMEMBER_START
        {
            get
            {
                return this.mMAINMEMBER_START;
            }
        }

        public int MAINMEMBER_END
        {
            get
            {
                return this.mMAINMEMBER_END;
            }
        }

        public int SUBMEMBER_START
        {
            get
            {
                return this.mSUBMEMBER_START;
            }
        }

        public int SUBMEMBER_END
        {
            get
            {
                return this.mSUBMEMBER_END;
            }
        }

        public int VSWAITMEMBER_START
        {
            get
            {
                return this.mVSWAITMEMBER_START;
            }
        }

        public int VSWAITMEMBER_END
        {
            get
            {
                return this.mVSWAITMEMBER_END;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
            set
            {
                this.mName = value;
                return;
            }
        }

        public int Num
        {
            get
            {
                int num;
                int num2;
                num = 0;
                num2 = 0;
                goto Label_001E;
            Label_0009:
                if (this.mUniqueIDs[num2] == null)
                {
                    goto Label_001A;
                }
                num += 1;
            Label_001A:
                num2 += 1;
            Label_001E:
                if (num2 < ((int) this.mUniqueIDs.Length))
                {
                    goto Label_0009;
                }
                return num;
            }
        }

        public int LeaderIndex
        {
            get
            {
                return this.mLeaderIndex;
            }
        }

        public PlayerPartyTypes PartyType
        {
            [CompilerGenerated]
            get
            {
                return this.<PartyType>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<PartyType>k__BackingField = value;
                return;
            }
        }

        public bool Selected
        {
            [CompilerGenerated]
            get
            {
                return this.<Selected>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<Selected>k__BackingField = value;
                return;
            }
        }

        public bool IsDefense
        {
            [CompilerGenerated]
            get
            {
                return this.<IsDefense>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsDefense>k__BackingField = value;
                return;
            }
        }
    }
}

