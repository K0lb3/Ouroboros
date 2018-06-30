namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class QuestResultData
    {
        public int StartExp;
        public int StartGold;
        public int StartBonusFlags;
        public Dictionary<long, UnitData.CharacterQuestParam> CharacterQuest;
        public Dictionary<long, string> SkillUnlocks;
        public SRPG.BattleCore.Record Record;
        public UnitGetParam GetUnits;
        public bool IsFirstWin;
        public Dictionary<long, string> CollaboSkillUnlocks;

        public QuestResultData(PlayerData player, int bonusFlags, SRPG.BattleCore.Record record, List<Unit> units, bool isFirstWin)
        {
            List<ItemParam> list;
            int num;
            UnitData.CharacterQuestParam param;
            List<UnitData> list2;
            int num2;
            string str;
            string str2;
            <QuestResultData>c__AnonStorey38C storeyc;
            <QuestResultData>c__AnonStorey38D storeyd;
            storeyc = new <QuestResultData>c__AnonStorey38C();
            storeyc.units = units;
            this.CharacterQuest = new Dictionary<long, UnitData.CharacterQuestParam>();
            this.SkillUnlocks = new Dictionary<long, string>();
            this.CollaboSkillUnlocks = new Dictionary<long, string>();
            base..ctor();
            this.StartExp = player.Exp;
            this.StartGold = player.Gold;
            this.Record = record;
            this.StartBonusFlags = bonusFlags;
            this.IsFirstWin = isFirstWin;
            if (this.Record.items == null)
            {
                goto Label_00FE;
            }
            list = new List<ItemParam>();
            if (this.Record.items.Count == null)
            {
                goto Label_00ED;
            }
            num = 0;
            goto Label_00D7;
        Label_0097:
            if (this.Record.items[num].itemParam != null)
            {
                goto Label_00B7;
            }
            goto Label_00D3;
        Label_00B7:
            list.Add(this.Record.items[num].itemParam);
        Label_00D3:
            num += 1;
        Label_00D7:
            if (num < this.Record.items.Count)
            {
                goto Label_0097;
            }
        Label_00ED:
            this.GetUnits = new UnitGetParam(list.ToArray());
        Label_00FE:
            if (storeyc.units.Count <= 1)
            {
                goto Label_0211;
            }
            storeyd = new <QuestResultData>c__AnonStorey38D();
            storeyd.<>f__ref$908 = storeyc;
            storeyd.i = 0;
            goto Label_01F9;
        Label_012D:
            if (storeyc.units[storeyd.i] == null)
            {
                goto Label_01E9;
            }
            if (storeyc.units[storeyd.i].Side != null)
            {
                goto Label_01E9;
            }
            if (storeyc.units[storeyd.i].UnitType != null)
            {
                goto Label_01E9;
            }
            if (player.Units.Find(new Predicate<UnitData>(storeyd.<>m__3DA)) == null)
            {
                goto Label_01E9;
            }
            param = storeyc.units[storeyd.i].UnitData.GetCurrentCharaEpisodeData();
            if (param == null)
            {
                goto Label_01E9;
            }
            this.CharacterQuest.Add(storeyc.units[storeyd.i].UnitData.UniqueID, param);
        Label_01E9:
            storeyd.i += 1;
        Label_01F9:
            if (storeyd.i < storeyc.units.Count)
            {
                goto Label_012D;
            }
        Label_0211:
            list2 = player.Units;
            num2 = 0;
            goto Label_0278;
        Label_0220:
            str = list2[num2].UnlockedSkillIds();
            this.SkillUnlocks.Add(list2[num2].UniqueID, str);
            str2 = list2[num2].UnlockedCollaboSkillIds();
            this.CollaboSkillUnlocks.Add(list2[num2].UniqueID, str2);
            num2 += 1;
        Label_0278:
            if (num2 < list2.Count)
            {
                goto Label_0220;
            }
            return;
        }

        [CompilerGenerated]
        private sealed class <QuestResultData>c__AnonStorey38C
        {
            internal List<Unit> units;

            public <QuestResultData>c__AnonStorey38C()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <QuestResultData>c__AnonStorey38D
        {
            internal int i;
            internal QuestResultData.<QuestResultData>c__AnonStorey38C <>f__ref$908;

            public <QuestResultData>c__AnonStorey38D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3DA(UnitData u)
            {
                return (u.UniqueID == this.<>f__ref$908.units[this.i].UnitData.UniqueID);
            }
        }
    }
}

