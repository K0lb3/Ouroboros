namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class GachaResultData
    {
        private static List<GachaDropData> drops_;
        private static List<GachaDropData> dropMails_;
        private static List<int> summonCoins_;
        private static int[] excites_;
        private static bool use_one_more_;
        private static int m_is_pending;
        private static int m_redraw_rest;
        [CompilerGenerated]
        private static GachaReceiptData <receipt>k__BackingField;

        static GachaResultData()
        {
            drops_ = new List<GachaDropData>();
            dropMails_ = new List<GachaDropData>();
            summonCoins_ = new List<int>();
            excites_ = new int[5];
            use_one_more_ = 0;
            m_is_pending = 0;
            m_redraw_rest = 0;
            return;
        }

        public GachaResultData()
        {
            base..ctor();
            return;
        }

        public static unsafe int[] CalcExcites(List<GachaDropData> a_drops)
        {
            int num;
            GachaDropData data;
            List<GachaDropData>.Enumerator enumerator;
            string str;
            Json_GachaExcite[] exciteArray;
            num = 1;
            enumerator = a_drops.GetEnumerator();
        Label_0009:
            try
            {
                goto Label_002E;
            Label_000E:
                data = &enumerator.Current;
                if (data != null)
                {
                    goto Label_0021;
                }
                goto Label_002E;
            Label_0021:
                num = Math.Max(num, data.Rare);
            Label_002E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_000E;
                }
                goto Label_004B;
            }
            finally
            {
            Label_003F:
                ((List<GachaDropData>.Enumerator) enumerator).Dispose();
            }
        Label_004B:
            return GachaExciteMaster.Select(JSONParser.parseJSONArray<Json_GachaExcite>(AssetManager.LoadTextData("Data/gacha/animation_pattern")), num);
        }

        public static int[] CalcExcitesForDrop(GachaDropData a_drop)
        {
            int num;
            string str;
            Json_GachaExcite[] exciteArray;
            num = 0;
            if (a_drop == null)
            {
                goto Label_000F;
            }
            num = a_drop.Rare;
        Label_000F:
            return GachaExciteMaster.SelectStone(JSONParser.parseJSONArray<Json_GachaExcite>(AssetManager.LoadTextData("Data/gacha/stone_animation_pattern")), num);
        }

        public static unsafe void Init(List<GachaDropData> a_drops, List<GachaDropData> a_dropMails, List<int> a_summonCoins, GachaReceiptData a_receipt, bool a_use_onemore, int a_is_pending, int a_redraw_rest)
        {
            GachaDropData data;
            List<GachaDropData>.Enumerator enumerator;
            Reset();
            if (a_drops == null)
            {
                goto Label_0011;
            }
            drops_ = a_drops;
        Label_0011:
            if (a_dropMails == null)
            {
                goto Label_001D;
            }
            dropMails_ = a_dropMails;
        Label_001D:
            if (a_summonCoins == null)
            {
                goto Label_0029;
            }
            summonCoins_ = a_summonCoins;
        Label_0029:
            excites_ = CalcExcites(a_drops);
            if (a_receipt == null)
            {
                goto Label_0040;
            }
            receipt = a_receipt;
        Label_0040:
            enumerator = drops_.GetEnumerator();
        Label_004B:
            try
            {
                goto Label_0064;
            Label_0050:
                data = &enumerator.Current;
                data.excites = CalcExcitesForDrop(data);
            Label_0064:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0050;
                }
                goto Label_0081;
            }
            finally
            {
            Label_0075:
                ((List<GachaDropData>.Enumerator) enumerator).Dispose();
            }
        Label_0081:
            use_one_more_ = a_use_onemore;
            m_is_pending = a_is_pending;
            m_redraw_rest = a_redraw_rest;
            return;
        }

        public static void Reset()
        {
            int num;
            drops_.Clear();
            dropMails_.Clear();
            summonCoins_.Clear();
            num = 0;
            goto Label_0031;
        Label_0025:
            excites_[num] = 1;
            num += 1;
        Label_0031:
            if (num < ((int) excites_.Length))
            {
                goto Label_0025;
            }
            receipt = null;
            use_one_more_ = 0;
            m_is_pending = -1;
            m_redraw_rest = -1;
            return;
        }

        public static GachaDropData[] drops
        {
            get
            {
                return drops_.ToArray();
            }
        }

        public static GachaDropData[] dropMails
        {
            get
            {
                return dropMails_.ToArray();
            }
        }

        public static List<int> summonCoins
        {
            get
            {
                return summonCoins_;
            }
        }

        public static int[] excites
        {
            get
            {
                return excites_;
            }
        }

        public static GachaReceiptData receipt
        {
            [CompilerGenerated]
            get
            {
                return <receipt>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                <receipt>k__BackingField = value;
                return;
            }
        }

        public static bool IsCoin
        {
            get
            {
                return (((receipt == null) || ((receipt.type == "gold") == null)) ? 1 : 0);
            }
        }

        public static bool UseOneMore
        {
            get
            {
                return use_one_more_;
            }
        }

        public static bool IsPending
        {
            get
            {
                return (m_is_pending == 1);
            }
        }

        public static int RedrawRest
        {
            get
            {
                return m_redraw_rest;
            }
        }

        public static bool IsRedrawGacha
        {
            get
            {
                return ((m_is_pending == -1) == 0);
            }
        }
    }
}

